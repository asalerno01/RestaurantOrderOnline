import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';
import { BsCheckSquareFill, BsCalendarWeek } from 'react-icons/bs';
import { TiArrowSortedUp, TiArrowSortedDown } from 'react-icons/ti';
import { IoMdClose } from 'react-icons/io';
import { IconContext } from 'react-icons/lib';
import MiniCalendar from '../../../components/MiniCalendar';
import { getLastSevenDays, monthStrings } from '../../../components/functions/DateInfo';
import './orders.css';
import { isEmptyObject } from '../functions/OrderFunctions';
import StickyBox from 'react-sticky-box';
import OrdersStyles from './css/Orders.module.css';
import LoadingSpinner from '../../../components/LoadingSpinner';
import ItemModifiers from './ItemModifiers';

const Orders = () => {
    const [orders, setOrders] = useState([]);
    const [openOrder, setOpenOrder] = useState({});
    const [miniCalendarOpen, setMiniCalendarOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [statusDropdownOpen, setStatusDropdownOpen] = useState(false);
    const [filterDate, setFilterDate] = useState(getLastSevenDays());
    const [orderOpen, setOrderOpen] = useState(false);
    const openOrderRef = useRef();
    const timeButtonWrapperRef = useRef();
    const [accept, setAccept] = useState(false);
    const [selectedPickupTime, setSelectedPickupTime] = useState(null);

    const getOrders = async () => {
        setIsLoading(true);
        await axios({
            method: "POST",
            url: "https://localhost:7074/api/orders/date",
            data: {
                startDate: new Date(filterDate[0].Year, filterDate[0].Month - 1, filterDate[0].Day).toISOString(),
                endDate: new Date(filterDate[1].Year, filterDate[1].Month - 1, filterDate[1].Day).toISOString()
            }
        })
        .then(res => {
            console.log(res.data)
            setOrders(res.data);
            wait(500);
        })
        .catch(err => {
            console.log(err);
        });
    }
    useEffect(() => { getOrders(); }, [filterDate]);
    const handleUpdateOrderStatus = event => {
        event.preventDefault();
    }

    const CalendarButton = () => {
        return (
            <>
                <button className={OrdersStyles.calendar_button} onClick={() => setMiniCalendarOpen(prev => !prev)}>
                    <BsCalendarWeek size={"22px"} style={{ verticalAlign: 'middle', marginRight: "5px" }}/>
                    <span>{`${monthStrings[filterDate[0].Month - 1]} ${filterDate[0].Day}, ${filterDate[0].Year} - ${monthStrings[filterDate[1].Month - 1]} ${filterDate[1].Day}, ${filterDate[1].Year}`}</span>
                </button>
                <MiniCalendar miniCalendarOpen={miniCalendarOpen} setMiniCalendarOpen={setMiniCalendarOpen} setFilterDate={setFilterDate} filterDate={filterDate}/>
            </>
        )
    }
    function timeout(delay) {
        return new Promise( res => setTimeout(res, delay) );
    }
    async function wait(delay) {
        console.log("waiting")
        await timeout(delay); //for 1 sec delay
        setIsLoading(false);
        console.log("done")
    }
    
    function getOrderItemPrice(orderItem) {
        let orderItemPrice = orderItem.price;
        orderItem.addons.forEach(addon => { orderItemPrice += Number(addon.addon.price); });
        orderItem.noOptions.forEach(noOption => { orderItemPrice -= Number(noOption.noOption.price); });
        orderItem.groups.forEach(group => { orderItemPrice += Number(group.group.price); });
        return Number(orderItemPrice * orderItem.count);
    }
    function formatTime(dateString) {
        var date = new Date(dateString);
        var hours = date.getHours();
        var minutes = date.getMinutes();
        // Check whether AM or PM
        var newformat = hours >= 12 ? 'PM' : 'AM';
          
        // Find current hour in AM-PM Format
        hours = hours % 12;
          
        // To display "0" as "12"
        hours = hours ? hours : 12;
        // minutes = minutes < 10 ? '0' + minutes : minutes;
        
        return `${hours}:${(minutes < 10) ? "0" + minutes : minutes} ${newformat}`;
    }
    function orderIsOpen(type) {
        if (type) {
            setOrderOpen(true);
            openOrderRef.current.style.width = "1100px";
        } else {
            openOrderRef.current.style.width = "0";
            setOrderOpen(false);
        }
    }

    const Checkbox = () => {
        return (
            <span className="Orders_Status_Checkbox_Icon">
                <BsCheckSquareFill size={"1em"} />
            </span>
        )
    }
    const handleOrderItemClick = async (orderId) => {
        orderIsOpen(true);
        setOpenOrder({})
        await axios({
            method: "GET",
            url: `https://localhost:7074/api/orders/${orderId}`
        })
        .then(res => setOpenOrder(res.data))
        .catch(err => console.log(err));
    }
    const handleAcceptClick = event => {
        event.preventDefault();
        console.log("clicked")
        timeButtonWrapperRef.current.style.width = "204px";
        setAccept(true);
    }
    const handleDeclineClick = event => {
        event.preventDefault();
        timeButtonWrapperRef.current.style.width = "0px";
        setAccept(false);
    }
    const OpenOrder = () => {
        let pickupDate = new Date(openOrder.orderDate);
        pickupDate.setMinutes()
        console.log(openOrder)
        if (!orderOpen) return <></>
        else if (isEmptyObject(openOrder)) {
            return (
                <div className={OrdersStyles.modal}>
                    <div className={OrdersStyles.modal_header_wrapper}>
                        <button className={OrdersStyles.close_button} onClick={() => orderIsOpen(false)}>
                            <IoMdClose size={"1.5em"}/>
                        </button>
                        <h2 className={OrdersStyles.modal_header}></h2>
                    </div>
                    <LoadingSpinner />
                </div>
            )
        }
        else return (
                    <div className={OrdersStyles.modal}>
                        <div className={OrdersStyles.modal_header_wrapper}>
                            <button className={OrdersStyles.close_button} onClick={() => orderIsOpen(false)}>
                                <IoMdClose size={"1.5em"}/>
                            </button>
                            <h2 className={OrdersStyles.modal_header}>Order #{openOrder.orderId}</h2>
                        </div>
                        <div className={OrdersStyles.gap}></div>
                        <div className={OrdersStyles.accept_decline_button_wrapper}>
                            <button type="button" className={(accept === true) ? OrdersStyles.accept__selected : OrdersStyles.accept} onClick={handleAcceptClick}>Accept</button>
                            <div className={OrdersStyles.time_wrapper} ref={timeButtonWrapperRef}>
                                <button type="button" className={OrdersStyles.time_button} onClick={() => setSelectedPickupTime(new Date(Date.now() + (30 * 60 * 1000)))}>
                                    {formatTime(new Date(Date.now() + (30 * 60 * 1000)))}
                                </button>
                                <button type="button" className={OrdersStyles.time_button} onClick={() => setSelectedPickupTime(new Date(Date.now() + (30 * 60 * 1000)))}>
                                    {formatTime(new Date(Date.now() + (60 * 60 * 1000)))}
                                </button>
                            </div>
                            <button type="button" className={OrdersStyles.decline} onClick={handleDeclineClick}>Decline</button>
                        </div>
                        <div className={OrdersStyles.gap}></div>
                        <div className={OrdersStyles.pickup}>
                            <h2 className={OrdersStyles.pickup_header}>Pick-Up Details</h2>
                            <div>
                                <div className={OrdersStyles.pickup_label}>Customer</div>
                                <div className={OrdersStyles.pickup_value}>{`${openOrder.account.firstName} ${openOrder.account.lastName}`}<span style={{marginLeft: "10px"}}>{(openOrder.account.isVerified) ? "" : "(New Customer)"}</span></div>
                            </div>
                            <div className={OrdersStyles.border}></div>
                            <div>
                                <div className={OrdersStyles.pickup_label}>Picked Up</div>
                                <div className={OrdersStyles.pickup_value}>
                                    {/* {(openOrder.pickUpDate === null) ? "In Progress" : openOrder.pickUpDate}
                                    <span style={{marginLeft: "10px"}}>
                                        {(openOrder.QuotedTime === null) ? "No Quote" : `Quoted (${openOrder.quotedTime})`}
                                    </span> */}
                                    <span>{openOrder.orderDate}</span><span style={{marginLeft: "10px"}}>{openOrder.orderTime}</span>
                                </div>
                                {/* <div className={OrdersStyles.pickup_value}>{new Date(openOrder["orderDate"]).toLocaleDateString()}</div> */}
                            </div>
                        </div>
                        <div className={OrdersStyles.gap}></div>
                        <div className={OrdersStyles.details}>
                            <h2 className={OrdersStyles.details_header}>Order Details</h2>
                            <div>
                            {
                                openOrder.orderItems.map(orderItem => (
                                    <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}`} className={OrdersStyles.item}>
                                        <span className={OrdersStyles.count}>{orderItem.count}x</span>
                                        <div style={{ flexGrow: "1" }}>
                                            <span className={OrdersStyles.item_details}>
                                                <span className={OrdersStyles.name}>{orderItem["name"]}</span>
                                                <span className={OrdersStyles.price}>${getOrderItemPrice(orderItem).toFixed(2)}</span>
                                            </span>
                                            <ItemModifiers orderItem={orderItem}/>
                                        </div>
                                    </div>
                                ))
                            }
                            <div>
                                <div className={OrdersStyles.total_item}>
                                    <span className={OrdersStyles.total_label}>Subtotal</span>
                                    <span className={OrdersStyles.total_value}>{`$${openOrder["subtotal"].toFixed(2)}`}</span>
                                </div>
                                <div className={OrdersStyles.total_item}>
                                    <span className={OrdersStyles.item_label}>Subtotal Tax</span>
                                    <span className={OrdersStyles.total_value}>{`$${openOrder["subtotalTax"].toFixed(2)}`}</span>
                                </div>
                                <div className={OrdersStyles.total_item}>
                                    <span className={OrdersStyles.total_label} style={{fontSize: "16px", fontWeight: "700"}}>Total</span>
                                    <span className={OrdersStyles.total_value} style={{fontSize: "16px", fontWeight: "700", margin: "6px 0 0 0"}}>{`$${openOrder["total"].toFixed(2)}`}</span>
                                </div>
                            </div>
                            </div>
                        </div>
                    </div>
        )
    }
    const OrdersList = () => {
        if (isLoading) return <LoadingSpinner />
        return (
            <table className={OrdersStyles.table}>
                <tbody>
                {
                    orders.map(order => (
                        <tr key={"orderid-" + order["orderId"]} onClick={() => handleOrderItemClick(order.orderId)}>
                            <td>{order["orderId"]}</td>
                            <td>{order.status}</td>
                            <td>{order.orderDate}</td>
                            <td>{order.orderTime}</td>
                            <td style={{ textTransform: "capitalize" }}>{`${(order["firstName"])} ${order["lastName"]}`}</td>
                        </tr>
                    ))
                }
                </tbody>
            </table>
        )
    }

    return (
        <div className={OrdersStyles.orders}>
                <div className={OrdersStyles.backdrop} style={(orderOpen) ? {width: "100%"} : {width: "0"}} onClick={() => orderIsOpen(false)}></div>
                <div className={OrdersStyles.slider} ref={openOrderRef} onClick={e => e.stopPropagation()}>
                    <div className={OrdersStyles.open_order}>  
                        <OpenOrder />
                    </div>
                </div>
                <h1 className={OrdersStyles.header}>Orders</h1>
                <div className={OrdersStyles.tab_buttons}>
                    <button type="button" className={OrdersStyles.tab_button__current}>Active</button>
                    <button type="button" className={OrdersStyles.tab_button}>History</button>
                </div>
                <div className={OrdersStyles.controls}>
                    <CalendarButton />
                    <div className={OrdersStyles.dropdown_container}>
                        <button 
                            className={`Orders_Status_Dropdown_Button ${statusDropdownOpen ? "DropdownOpen" : ""}`}
                            onClick={() => (setStatusDropdownOpen(prev => !prev))}>
                            <span>Status</span>
                            <IconContext.Provider value={{ style: { verticalAlign: 'middle', height: '100%', float: 'right', color: 'rgb(139, 139, 139)' }, size: '1.25em' }}>
                                {statusDropdownOpen ? <TiArrowSortedUp className='Modifiers_Groups_Copy_Import_Arrow' /> : <TiArrowSortedDown className='Modifiers_Groups_Copy_Import_Arrow' /> }
                            </IconContext.Provider>
                        </button>
                        <div className={OrdersStyles.dropdown} style={statusDropdownOpen ? {display: "block"} : {display: "none"}} onClick={e => e.stopPropagation()}>
                            <ul className={OrdersStyles.dropdown_list}>
                                <li className={OrdersStyles.dropdown_item}><Checkbox />Pending</li>
                                <li className={OrdersStyles.dropdown_item}><Checkbox />Complete</li>
                                <li className={OrdersStyles.dropdown_item}><Checkbox />Cancelled</li>
                            </ul>
                        </div>
                    </div>
                    <div className={OrdersStyles.search_wrapper}>
                        <input type="text" className={OrdersStyles.search} placeholder="Search Orders..." />
                    </div>
                </div>
                <div>
                    <StickyBox style={{zIndex: "1"}} offsetTop={-41}>
                        <table className={OrdersStyles.sticky_table}>
                            <thead>
                                <tr>
                                    <th>Order ID</th>
                                    <th>Status</th>
                                    <th>Date</th>
                                    <th>Time</th>
                                    <th>Customer</th>
                                </tr>
                            </thead>
                        </table>
                    </StickyBox>
                    <OrdersList />
                </div>
        </div>
    )
}

export default Orders;

// {/* <div className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_Addons">
//     {/* <ul className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_Addons_List">
//         {
//             (orderItem["addons"].length === 0) ? <></> :
//             orderItem["addons"].map(addon => (
//                 <li className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_List_Item">
//                     {/* <div className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_List_Item_Label">Addon:</div> */}
//                     <div className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_List_Item_Modifier">{addon["addon"]["name"]}</div>
//                 </li>
//             ))
//         }
//     </ul> */}
// </div>
// <div className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_NoOptions">
//     <ul className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_NoOptions_List">
//         {
//             (orderItem["noOptions"].length === 0) ? <></> :
//             orderItem["noOptions"].map(noOption => (
//                 <li className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_List_Item">
//                     {/* <div className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_List_Item_Label">No Option:</div> */}
//                     <div className="Orders_OrderItem_Modal_Details_Content_Item_Modifiers_List_Item_Modifier">{noOption["noOption"]["name"]}</div>
//                 </li>
//             ))
//         }
//     </ul>
// </div> */}