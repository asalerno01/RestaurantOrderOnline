import React, { useEffect, useRef, useState } from 'react';
import { BsRecordCircle, BsCircle, BsCreditCard, BsFillCheckCircleFill } from 'react-icons/bs';
import { FaCcPaypal } from 'react-icons/fa';
import { RxCaretRight, RxCaretLeft } from 'react-icons/rx';
import { IoLogoVenmo, IoStorefrontOutline, IoLogoPaypal } from 'react-icons/io5';
import { ImCheckboxChecked, ImCheckboxUnchecked } from 'react-icons/im';
import CheckoutStyles from './css/Checkout.module.css';
import OrderItemSummary from './OrderItemSummary';
import { createEmptyOrder, getOrderSubtotal, isEmptyObject, removeOrderItem } from './functions/OrderFunctions';
import axios from 'axios';
import OrderItem from './OrderItem';
import { useNavigate } from 'react-router-dom';
import CreditDebitModal from './components/CreditDebitModal';
import { canSubmitOrder } from './functions/OrderFunctions';
import useAuth from '../../hooks/useAuth';

const Checkout = () => {
    const { auth } = useAuth();
    const [order, setOrder] = useState(createEmptyOrder());
    const [items, setItems] = useState([]);
    const [editItemIndex, setEditItemIndex] = useState(null);
    const [selectedItemData, setSelectedItemData] = useState({ item: null, index: null });
    const [inputData, setInputData] = useState({ firstName: "", lastName: "", phoneNumber: "", email: "", pickupType: "", paymentType: "", saveOrder: false, saveOrderName: "" });
    const [creditDebitModalIsOpen, setCreditDebitModalIsOpen] = useState(false);

    const [selectedBaseItem, setSelectedBaseItem] = useState(null);

    const firstNameRef = useRef();
    const lastNameRef = useRef();
    const phoneNumberRef = useRef();
    const emailRef = useRef();
    const saveOrderNameRef = useRef();
    const navigate = useNavigate();

    useEffect(() => { setOrder(JSON.parse(localStorage.getItem("order"))); }, []);

    const getItems = async () => {
        await axios.get("https://localhost:7074/api/items")
        .then(res => {
            console.log(res.data)
            setItems(res.data);
        })
        .catch(err => {
            console.log(err);
        })
    }
    useEffect(() => { getItems(); }, []);
    useEffect(() => {
        if (!isEmptyObject(auth)) {
            setInputData({
                firstName: auth.firstName,
                lastName: auth.lastName,
                phoneNumber: auth.phoneNumber,
                email: auth.email,
                pickupType: "",
                paymentType: "",
                saveOrderName: ""
            });
        }
    }, [])

    const handleInputChange = event => {
        const id = event.target.id;
        const value = event.target.value;
        const tempInput = Object.assign({}, inputData);
        switch (id) {
            case "firstName":
                tempInput.firstName = value;
                break;
            case "lastName":
                tempInput.lastName = value;
                break;
            case "phoneNumber":
                if (!isNaN(value))
                    tempInput.phoneNumber = value;
                break;
            case "email":
                tempInput.email = value;
                break;
            case "pickupType":
                if (value !== inputData.pickupType) tempInput.pickupType = value;
                break;
            case "paymentType":
                tempInput.paymentType = value;
                break;
            case "saveOrder":
                tempInput.saveOrder = !tempInput.saveOrder;
                if (tempInput.saveOrder) {
                    saveOrderNameRef.current.style.height = "39px";
                    saveOrderNameRef.current.style.width = "100%";
                } else {
                    saveOrderNameRef.current.style.height = "0";
                }
                break;
            case "saveOrderName":
                tempInput.saveOrderName = value;
                break;
            default:
                break;
        }
        setInputData(tempInput);
    }
    // const handleRemoveItemClick = (index) => setOrder(removeOrderItem(order, index));

    const handleEditItemClick = (itemId, index) => setSelectedItemData({ item: items.find(item => item.itemId === itemId), index: index });
    const handleRemoveItemClick = (index) => {
        const temp = Object.assign({}, order);
        let subtotal = 0;
        temp["orderItems"].splice(index, 1);
        if (isNaN(subtotal)) subtotal = 0;
        temp["subtotal"] = subtotal;
        setOrder(temp);
    }

    const handleNavigateToMenu = event => {
        localStorage.setItem("order", JSON.stringify(order));
        navigate("/salerno/order");
    }

    const handleSubmitOrderClick = async event => {
        event.preventDefault();
        console.log(inputData.saveOrder);
        if (canSubmitOrder(inputData)) {
            order.accountId = auth.accountId;
            order.savedOrderName = inputData.saveOrderName;
            order.saveOrder = inputData.saveOrder;
            order.tax = 0;
            order.total = 0;
            order.tax = 0;
            await axios({
                method: "POST",
                url: "https://localhost:7074/api/orders",
                data: order,
                withCredentials: true
            })
            .then(res => {
                console.log(res);
                localStorage.removeItem("order");
                navigate("/salerno/order");
            })
            .catch(err => {
                console.log(err);
            });
        } else {
            if (inputData.firstName.length === 0) {
                firstNameRef.current.style.borderColor = "rgb(255, 0, 0)";
                firstNameRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (inputData.lastName.length === 0) {
                lastNameRef.current.style.borderColor = "rgb(255, 0, 0)";
                lastNameRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (inputData.phoneNumber.length === 0) {
                phoneNumberRef.current.style.borderColor = "rgb(255, 0, 0)";
                phoneNumberRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (inputData.email.length === 0) {
                emailRef.current.style.borderColor = "rgb(255, 0, 0)";
                emailRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (inputData.saveOrder && inputData.saveOrderName.length === 0) {
                saveOrderNameRef.current.lastChild.style.borderColor = "rgb(255, 0, 0)";
                saveOrderNameRef.current.lastChild.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
        }
    }
    const Checkbox = () => {
        if (inputData.saveOrder === true) {
            return <ImCheckboxChecked className={CheckoutStyles.checkbox} size="14px" style={{color: "rgb(255, 27, 26)"}} />
        } else {
            return <ImCheckboxUnchecked className={CheckoutStyles.checkbox} size="14px" />
        }
    }
    useEffect(() => {
        if (firstNameRef.current.style.borderColor = "rgb(255, 0, 0)") {
            firstNameRef.current.style.borderColor = "rgb(221, 223, 225)";
            firstNameRef.current.style.boxShadow = "none"
        }
    }, [inputData.firstName]);
    useEffect(() => {
        if (lastNameRef.current.style.borderColor = "rgb(255, 0, 0)") {
            lastNameRef.current.style.borderColor = "rgb(221, 223, 225)";
            lastNameRef.current.style.boxShadow = "none"
        }
    }, [inputData.lastName]);
    useEffect(() => {
        if (phoneNumberRef.current.style.borderColor = "rgb(255, 0, 0)") {
            phoneNumberRef.current.style.borderColor = "rgb(221, 223, 225)";
            phoneNumberRef.current.style.boxShadow = "none"
        }
    }, [inputData.phoneNumber]);
    useEffect(() => {
        if (emailRef.current.style.borderColor = "rgb(255, 0, 0)") {
            emailRef.current.style.borderColor = "rgb(221, 223, 225)";
            emailRef.current.style.boxShadow = "none"
        }
    }, [inputData.email]);
    useEffect(() => {
        if (saveOrderNameRef.current.lastChild.style.borderColor = "rgb(255, 0, 0)") {
            saveOrderNameRef.current.lastChild.style.borderColor = "rgb(221, 223, 225)";
            saveOrderNameRef.current.lastChild.style.boxShadow = "none"
        }
    }, [inputData.saveOrderName, inputData.saveOrder]);

    const PaymentSetIcon = ({ paymentType }) => {
        if (inputData.paymentType === paymentType)
            return <BsFillCheckCircleFill size="22px" style={{color: "green"}}/>
        return <RxCaretRight size="35px" style={{color: "rgb(71, 71, 71)"}} />
    }
    return (
        <div className={CheckoutStyles.checkout}>
            <OrderItem
                selectedItemData={selectedItemData}
                setSelectedItemData={setSelectedItemData}
                order={order}
                setOrder={setOrder}
            />
            <CreditDebitModal
                inputData={inputData}
                setInputData={setInputData}
                creditDebitModalIsOpen={creditDebitModalIsOpen}
                setCreditDebitModalIsOpen={setCreditDebitModalIsOpen}
            />
            <div className={CheckoutStyles.container}>
                <div className={CheckoutStyles.wrapper}>
                    <button type="button" className={CheckoutStyles.back_button} onClick={handleNavigateToMenu}>
                        <RxCaretLeft size={"30px"} style={{color: "red"}} />
                        <span style={{height: "100%", lineHeight: "31px", fontFamily: "Lato"}}>Back to Menu</span>
                    </button>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Pickup Time</h3>
                        <div className={CheckoutStyles.right}>
                            <button className={CheckoutStyles.select_button} id="pickupType" value="Standard" onClick={handleInputChange}>
                                <span className={CheckoutStyles.radio_icon}>
                                {
                                    (inputData.pickupType === "Standard") ? <BsRecordCircle size="1.4em" style={{color: "red"}}/> : <BsCircle size="1.4em"/>
                                }
                                </span>
                                <span style={{fontSize: "15px", fontWeight: "700", userSelect: "none", pointerEvents: "none"}}>Standard (15-20 minutes)</span>
                            </button>
                            <button className={CheckoutStyles.select_button} id="pickupType" value="Scheduled" onClick={handleInputChange}>
                                <span className={CheckoutStyles.radio_icon}>
                                {
                                    (inputData.pickupType === "Scheduled") ? <BsRecordCircle size="1.4em" style={{color: "red"}} /> : <BsCircle size="1.4em" />
                                }
                                </span>
                                <span style={{fontSize: "15px", fontWeight: "700", userSelect: "none", pointerEvents: "none"}}>Schedule for later</span>
                            </button>
                        </div>
                    </div>
                    <div className={CheckoutStyles.border}></div>
                    <div className={CheckoutStyles.detail_item}>
                        <div className={CheckoutStyles.save_container}>
                            <button type="button" className={CheckoutStyles.save_button} id="saveOrder" onClick={handleInputChange}>
                                <Checkbox />
                                <span>Save this order for faster ordering again</span>
                            </button>
                            <div className={CheckoutStyles.save_name_input_wrapper} ref={saveOrderNameRef} >
                                <input type="text" value={inputData.saveOrderName} placeholder="Enter a name for the order" className={CheckoutStyles.input_text__save} id="saveOrderName" onChange={handleInputChange} />
                            </div>
                        </div>
                    </div>
                    <div className={CheckoutStyles.border}></div>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Contact and Payment</h3>
                        <div className={CheckoutStyles.right}>
                            <div className={CheckoutStyles.input_container}>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="firstName" className={CheckoutStyles.label}>First Name</label>
                                    <input type="text" value={inputData.firstName} disabled={!isEmptyObject(auth)} ref={firstNameRef} className={CheckoutStyles.input_text} id="firstName" onChange={handleInputChange} />
                                </div>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="lastName" className={CheckoutStyles.label}>Last Name</label>
                                    <input type="text" value={inputData.lastName} disabled={!isEmptyObject(auth)} ref={lastNameRef} className={CheckoutStyles.input_text} id="lastName" onChange={handleInputChange} />
                                </div>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="phoneNumber" className={CheckoutStyles.label}>Phone Number</label>
                                    <input type="tel" value={inputData.phoneNumber} disabled={!isEmptyObject(auth)} ref={phoneNumberRef} className={CheckoutStyles.input_text} id="phoneNumber" pattern="[0-9]{3}-[0-9]{2}-[0-9]{3}" onChange={handleInputChange} />
                                </div>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="email" className={CheckoutStyles.label}>Email</label>
                                    <input type="email" value={inputData.email} disabled={!isEmptyObject(auth)} ref={emailRef} className={CheckoutStyles.input_text} id="email" onChange={handleInputChange} />
                                </div>
                            </div>
                            <div className={CheckoutStyles.payment_container}>
                                <span className={CheckoutStyles.label}>Payment Methods</span>
                                <span className={CheckoutStyles.payment_description}>Add New Payment Method</span>
                                <button className={CheckoutStyles.payment_type_button} onClick={() => setCreditDebitModalIsOpen(true)}>
                                    <BsCreditCard size={"35px"} className={CheckoutStyles.payment_icon}/>
                                    <span className={CheckoutStyles.payment_label}>Credit/Debit Card</span>
                                    <span style={{width: "35px"}}>
                                        <PaymentSetIcon paymentType="CreditDebit"/>
                                    </span>
                                </button>
                                <div className={CheckoutStyles.border}></div>
                                <button className={CheckoutStyles.payment_type_button} value="Paypal" id="paymentType" onClick={handleInputChange}>
                                    <span className={CheckoutStyles.paypal_logo}><IoLogoPaypal size={"20px"} style={{color: "rgb(255, 255, 255)"}}/></span>
                                    <span className={CheckoutStyles.payment_label}>Paypal</span>
                                    <span style={{width: "35px"}}>
                                        <PaymentSetIcon paymentType="Paypal"/>
                                    </span>
                                </button>
                                <div className={CheckoutStyles.border}></div>
                                <button className={CheckoutStyles.payment_type_button} value="Venmo" id="paymentType" onClick={handleInputChange}>
                                    <IoLogoVenmo size={"35px"} style={{color: "rgb(0, 140, 255)", marginRight: "9px"}}/>
                                    <span className={CheckoutStyles.payment_label}>Venmo</span>
                                    <span style={{width: "35px"}}>
                                        <PaymentSetIcon paymentType="Venmo"/>
                                    </span>
                                </button>
                                <div className={CheckoutStyles.border}></div>
                                <button className={CheckoutStyles.payment_type_button} value="PayInStore" id="paymentType" onClick={handleInputChange}>
                                    <IoStorefrontOutline size="30px" style={{margin: "0 12px 0 3px"}}/>
                                    <span className={CheckoutStyles.payment_label}>Pay in Store</span>
                                    <span style={{width: "35px"}}>
                                        <PaymentSetIcon paymentType="PayInStore"/>
                                    </span>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div className={CheckoutStyles.border}></div>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Summary</h3>
                        <div className={CheckoutStyles.items}>
                            <OrderItemSummary
                                order={order}
                                setOrder={setOrder}
                                handleEditItemClick={handleEditItemClick}
                                handleRemoveItemClick={handleRemoveItemClick}
                            />
                        </div>
                    </div>
                </div>
            </div>
            <div className={CheckoutStyles.order_summary_container}>
                <div className={CheckoutStyles.summary_grid}>
                    <span className={CheckoutStyles.total_label}>Subtotal:</span>
                    <span className={CheckoutStyles.total}>{`$${getOrderSubtotal(order).toFixed(2)}`}</span>
                    <span className={CheckoutStyles.total_label}>Tax:</span>
                    <span className={CheckoutStyles.total}>{`$${(getOrderSubtotal(order) * 0.0285).toFixed(2)}`}</span>
                    <span className={CheckoutStyles.total_label}>Total:</span>
                    <span className={CheckoutStyles.total}>{`$${(getOrderSubtotal(order) * 1.0285).toFixed(2)}`}</span>
                </div>
                <button type="button" className={canSubmitOrder(inputData) ? CheckoutStyles.submit_button : CheckoutStyles.submit_button__disabled} onClick={handleSubmitOrderClick}>Submit Order</button>
            </div>
        </div>
    )
}

export default Checkout;