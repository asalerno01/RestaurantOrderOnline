import React, { useEffect, useRef, useState } from 'react';
import { BsRecordCircle, BsCircle, BsCreditCard } from 'react-icons/bs';
import { FaCcPaypal } from 'react-icons/fa';
import { RxCaretRight, RxCaretLeft } from 'react-icons/rx';
import { IoLogoVenmo, IoStorefrontOutline, IoLogoPaypal } from 'react-icons/io5';
import CheckoutStyles from './css/Checkout.module.css';
import OrderItemSummary from './OrderItemSummary';
import { removeOrderItem } from './functions/OrderFunctions';
import axios from 'axios';
import EditItem from './EditItem';
import { useNavigate } from 'react-router-dom';
import CreditDebitModal from './components/CreditDebitModal';

const Checkout = () => {
    const [order, setOrder] = useState({
        "orderId": 0,
        "subtotal": 0,
        "subtotalTax": 0,
        "total": 0,
        "orderItems": []
    });
    const [items, setItems] = useState([]);
    const [editItemIndex, setEditItemIndex] = useState(null);
    const [editItem, setEditItem] = useState({})
    const [pickupType, setPickupType] = useState("");
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");

    const [creditDebit, setCreditDebit] = useState("");
    const [creditDebitModalIsOpen, setCreditDebitModalIsOpen] = useState(false);

    const firstNameRef = useRef();
    const lastNameRef = useRef();
    const phoneNumberRef = useRef();
    const emailRef = useRef();
    const navigate = useNavigate();
    
    useEffect(() => {
        const order = JSON.parse(localStorage.getItem("order"));
        setOrder(order);
    }, []);
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
    useEffect(() => {
        getItems();
    }, []);

    const handlePickupClick = (type) => {
        if (pickupType !== type) setPickupType(type);
    }

    const canSubmitOrder = () => {
        return (firstName.length > 0
            && lastName.length > 0
                && phoneNumber.length > 0
                    && email.length > 0)
    }
    const handleInputChange = event => {
        event.preventDefault();
        const id = event.target.id;
        const value = event.target.value;
        switch (id) {
            case "firstName":
                setFirstName(value);
                break;
            case "lastName":
                setLastName(value);
                break;
            case "phoneNumber":
                if (!isNaN(value))
                    setPhoneNumber(value);
                break;
            case "email":
                setEmail(value);
                break;
            default:
                break;
        }
    }
    const handleRemoveItemClick = (index) => {
        const newOrder = removeOrderItem(order, index);
        console.log(newOrder)
        setOrder(newOrder);
    }
    const handleItemClick = (itemId, index) => {
        setEditItemIndex(index);
        let x = items.find(i => i.itemId === itemId);
        setEditItem(x);
    }
    const handleNavigateToMenu = event => {
        event.preventDefault();
        localStorage.setItem("order", JSON.stringify(order));
        navigate("/salerno/order");
    }

    const handleSubmitOrderClick = event => {
        event.preventDefault();
        if (canSubmitOrder()) {
            localStorage.removeItem("order");
            navigate("/salerno/order");
        } else {
            if (firstName.length === 0) {
                firstNameRef.current.style.borderColor = "rgb(255, 0, 0)";
                firstNameRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (lastName.length === 0) {
                lastNameRef.current.style.borderColor = "rgb(255, 0, 0)";
                lastNameRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (phoneNumber.length === 0) {
                phoneNumberRef.current.style.borderColor = "rgb(255, 0, 0)";
                phoneNumberRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }
            if (email.length === 0) {
                emailRef.current.style.borderColor = "rgb(255, 0, 0)";
                emailRef.current.style.boxShadow = "0 0 8px 0 rgb(255 0 0 / 60%)"
            }

        }
    }

    useEffect(() => {
        if (firstNameRef.current.style.borderColor = "rgb(255, 0, 0)") {
            firstNameRef.current.style.borderColor = "rgb(221, 223, 225)";
            firstNameRef.current.style.boxShadow = "none"
        }
    }, [firstName]);
    useEffect(() => {
        if (lastNameRef.current.style.borderColor = "rgb(255, 0, 0)") {
            lastNameRef.current.style.borderColor = "rgb(221, 223, 225)";
            lastNameRef.current.style.boxShadow = "none"
        }
    }, [lastName]);
    useEffect(() => {
        if (phoneNumberRef.current.style.borderColor = "rgb(255, 0, 0)") {
            phoneNumberRef.current.style.borderColor = "rgb(221, 223, 225)";
            phoneNumberRef.current.style.boxShadow = "none"
        }
    }, [phoneNumber]);
    useEffect(() => {
        if (emailRef.current.style.borderColor = "rgb(255, 0, 0)") {
            emailRef.current.style.borderColor = "rgb(221, 223, 225)";
            emailRef.current.style.boxShadow = "none"
        }
    }, [email]);

    return (
        <div className={CheckoutStyles.checkout}>
            <EditItem itemI={editItem} setOrder={setOrder} setEditItem={setEditItem} order={order} editItemIndex={editItemIndex} setEditItemIndex={setEditItemIndex} />
            <CreditDebitModal setCreditDebit={setCreditDebit} creditDebitModalIsOpen={creditDebitModalIsOpen} setCreditDebitModalIsOpen={setCreditDebitModalIsOpen} />
            <div className={CheckoutStyles.container}>
                <div className={CheckoutStyles.wrapper}>
                    <button type="button" className={CheckoutStyles.back_button} onClick={handleNavigateToMenu}>
                        <RxCaretLeft size={"30px"} style={{color: "red"}} />
                        <span style={{height: "100%", lineHeight: "31px", fontFamily: "Lato"}}>Back to Menu</span>
                    </button>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Pickup Time</h3>
                        <div className={CheckoutStyles.right}>
                            <button className={CheckoutStyles.select_button} onClick={() => handlePickupClick("Standard")}>
                                <span className={CheckoutStyles.radio_icon}>
                                {
                                    (pickupType === "Standard") ? <BsRecordCircle size="1.4em" style={{color: "red"}} /> : <BsCircle size="1.4em" />
                                }
                                </span>
                                <span style={{fontSize: "15px", fontWeight: "700", userSelect: "none"}}>Standard (15-20 minutes)</span>
                            </button>
                            <button className={CheckoutStyles.select_button} onClick={() => handlePickupClick("Scheduled")}>
                                <span className={CheckoutStyles.radio_icon}>
                                {
                                    (pickupType === "Scheduled") ? <BsRecordCircle size="1.4em" style={{color: "red"}} /> : <BsCircle size="1.4em" />
                                }
                                </span>
                                <span style={{fontSize: "15px", fontWeight: "700", userSelect: "none"}}>Schedule for later</span>
                            </button>
                        </div>
                    </div>
                    <div className={CheckoutStyles.border}></div>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Contact and Payment</h3>
                        <div className={CheckoutStyles.right}>
                            <div className={CheckoutStyles.input_container}>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="firstName" className={CheckoutStyles.label}>First Name</label>
                                    <input type="text" value={firstName} ref={firstNameRef} className={CheckoutStyles.input_text} id="firstName" onChange={handleInputChange} />
                                </div>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="lastName" className={CheckoutStyles.label}>Last Name</label>
                                    <input type="text" value={lastName} ref={lastNameRef} className={CheckoutStyles.input_text} id="lastName" onChange={handleInputChange} />
                                </div>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="phoneNumber" className={CheckoutStyles.label}>Phone Number</label>
                                    <input type="tel" value={phoneNumber} ref={phoneNumberRef} className={CheckoutStyles.input_text} id="phoneNumber" pattern="[0-9]{3}-[0-9]{2}-[0-9]{3}" onChange={handleInputChange} />
                                </div>
                                <div className={CheckoutStyles.input_item}>
                                    <label htmlFor="email" className={CheckoutStyles.label}>Email</label>
                                    <input type="email" value={email} ref={emailRef} className={CheckoutStyles.input_text} id="email" onChange={handleInputChange} />
                                </div>
                            </div>
                            <div className={CheckoutStyles.payment_container}>
                                <span className={CheckoutStyles.label}>Payment Methods</span>
                                <span className={CheckoutStyles.payment_description}>Add New Payment Method</span>
                                <button className={CheckoutStyles.payment_type_button} onClick={() => setCreditDebitModalIsOpen(true)}>
                                    <BsCreditCard size={"35px"} className={CheckoutStyles.payment_icon} />
                                    <span className={CheckoutStyles.payment_label}>Credit/Debit Card</span>
                                    <RxCaretRight size={"35px"} style={{color: "rgb(71, 71, 71)"}} />
                                </button>
                                <div className={CheckoutStyles.border}></div>
                                <button className={CheckoutStyles.payment_type_button}>
                                    <span className={CheckoutStyles.paypal_logo}><IoLogoPaypal size={"20px"} style={{color: "rgb(255, 255, 255)"}} /></span>
                                    <span className={CheckoutStyles.payment_label}>Paypal</span>
                                    <RxCaretRight size={"35px"} style={{color: "rgb(71, 71, 71)"}} />
                                </button>
                                <div className={CheckoutStyles.border}></div>
                                <button className={CheckoutStyles.payment_type_button}>
                                    <IoLogoVenmo size={"35px"} style={{color: "rgb(0, 140, 255)", marginRight: "9px"}} />
                                    <span className={CheckoutStyles.payment_label}>Venmo</span>
                                    <RxCaretRight size={"35px"} style={{color: "rgb(71, 71, 71)"}} />
                                </button>
                                <div className={CheckoutStyles.border}></div>
                                <button className={CheckoutStyles.payment_type_button}>
                                    <IoStorefrontOutline size="30px" style={{margin: "0 12px 0 3px"}} />
                                    <span className={CheckoutStyles.payment_label}>Pay in Store</span>
                                    <RxCaretRight size={"35px"} style={{color: "rgb(71, 71, 71)"}} />
                                </button>
                            </div>
                        </div>
                    </div>
                    <div className={CheckoutStyles.border}></div>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Rewards & Promo</h3>
                        <div>Add Promo Code</div>
                    </div>
                    <div className={CheckoutStyles.border}></div>
                    <div className={CheckoutStyles.detail_item}>
                        <h3 className={CheckoutStyles.header}>Summary</h3>
                        <div className={CheckoutStyles.items}>
                            <OrderItemSummary order={order} handleItemClick={handleItemClick} handleRemoveItemClick={handleRemoveItemClick} />
                        </div>
                    </div>
                </div>
            </div>
            <div className={CheckoutStyles.order_summary_container}>
                <div className={CheckoutStyles.summary_grid}>
                    <span className={CheckoutStyles.total_label}>Subtotal:</span>
                    <span className={CheckoutStyles.total}>$1.00</span>
                    <span className={CheckoutStyles.total_label}>Tax:</span>
                    <span className={CheckoutStyles.total}>$0.08</span>
                    <span className={CheckoutStyles.total_label}>Total:</span>
                    <span className={CheckoutStyles.total}>$1.08</span>
                </div>
                <button type="button" className={canSubmitOrder() ? CheckoutStyles.submit_button : CheckoutStyles.submit_button__disabled} onClick={handleSubmitOrderClick}>Submit Order</button>
            </div>
        </div>
    )
}

export default Checkout;