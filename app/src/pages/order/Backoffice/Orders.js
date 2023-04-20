import React, { useState, useEffect } from 'react';
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

const Orders = () => {
    const [orders, setOrders] = useState([]);
    const [openOrder, setOpenOrder] = useState({});
    const [miniCalendarOpen, setMiniCalendarOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [statusDropdownOpen, setStatusDropdownOpen] = useState(false);
    const [filterDate, setFilterDate] = useState(getLastSevenDays());

    const getOrders = async () => {
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
            setIsLoading(false);
        })
        .catch(err => {
            console.log(err);
        });
    }
    useEffect(() => { getOrders(); }, [filterDate]);


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

    function getModifierLabel(modifier, type) {
        if (!isNaN(modifier[type]["price"]) && modifier[type]["price"] > 0) return `${modifier[type]["name"]} ($${modifier[type]["price"].toFixed(2)})`;
        return modifier[type]["name"];
    }
    function getOrderItemPrice(orderItem) {
        let orderItemPrice = orderItem.basePrice;
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

    const Checkbox = () => {
        return (
            <span className="Orders_Status_Checkbox_Icon">
                <BsCheckSquareFill size={"1em"} />
            </span>
        )
    }
    const handleOrderItemClick = async (orderId) => {
        await axios({
            method: "GET",
            url: `https://localhost:7074/api/orders/${orderId}`
        })
        .then(res => setOpenOrder(res.data))
        .catch(err => console.log(err));
    }

    const OpenOrder = () => {
        if (isEmptyObject(openOrder)) return <></>
        else return (
            <div className={OrdersStyles.stretch} onClick={() => setOpenOrder({})}>
                <div onClick={e => e.stopPropagation()}>
                    <div className={OrdersStyles.modal}>
                        <div className={OrdersStyles.modal_header_wrapper}>
                            <h2 className={OrdersStyles.modal_header}>Order #{openOrder.orderId}</h2>
                            <div className={OrdersStyles.close_button} onClick={() => setOpenOrder({})}><IoMdClose size={"1.5em"}/></div>
                        </div>
                        <div className={OrdersStyles.gap}></div>
                        <div className={OrdersStyles.pickup}>
                            <h2 className={OrdersStyles.pickup_header}>Pick-Up Details</h2>
                            <div>
                                <div className={OrdersStyles.pickup_label}>Customer</div>
                                <div className={OrdersStyles.pickup_value}>{(openOrder.account === null) ? "New Customer" : `${openOrder.account.firstName} ${openOrder.account.lastName}`}</div>
                            </div>
                            <div className={OrdersStyles.border}></div>
                            <div>
                                <div className={OrdersStyles.pickup_label}>Picked Up</div>
                                <div className={OrdersStyles.pickup_value}>
                                    {(openOrder.pickUpDate === null) ? "Pending" : formatTime(openOrder.pickupDate)}
                                    <span style={{marginLeft: "10px"}}>
                                        {`(Quoted ${formatTime(new Date(openOrder.orderDate).setMinutes(new Date(openOrder.orderDate).getMinutes() + 20))})`}
                                    </span>
                                </div>
                                <div className={OrdersStyles.pickup_value}>{new Date(openOrder["orderDate"]).toLocaleDateString()}</div>
                            </div>
                        </div>
                        <div className={OrdersStyles.gap}></div>
                        <div className={OrdersStyles.details}>
                            <h2 className={OrdersStyles.details_header}>Order Details</h2>
                            <div>
                            {
                                openOrder.orderItems.map(orderItem => (
                                    <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}`} className={OrdersStyles.item}>
                                        <div className={OrdersStyles.count}>{orderItem.count}x</div>
                                        <div className={OrdersStyles.item_details}>
                                            <div className={OrdersStyles.name}>{orderItem["itemName"]}</div>
                                            <div className={OrdersStyles.price}>${getOrderItemPrice(orderItem).toFixed(2)}</div>
                                        </div>
                                        <div className={OrdersStyles.addons}>
                                        {
                                            (orderItem["addons"].length === 0) ? <></> : orderItem["addons"].map(addon => (
                                                <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}-addon-${addon["addon"]["addonId"]}`}>
                                                    <span className={OrdersStyles.modifier_label}>Addon:</span>
                                                    <span className={OrdersStyles.modifier_text}>{getModifierLabel(addon, "addon")}</span>
                                                </div>
                                            ))
                                        }
                                        </div>
                                        <div className={OrdersStyles.noOptions}>
                                        {
                                            (orderItem["noOptions"].length === 0) ? <></> : orderItem["noOptions"].map(noOption => (
                                                <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}-noOption-${noOption["noOption"]["noOptionId"]}`}>
                                                    <span className={OrdersStyles.modifier_label}>Remove:</span>
                                                    <span className={OrdersStyles.modifier_text}>{getModifierLabel(noOption, "noOption")}</span>
                                                </div>
                                            ))
                                        }
                                        </div>
                                        <div className={OrdersStyles.groups}>
                                        {
                                            (orderItem["groups"].length === 0) ? <></> : orderItem["groups"].map(group => (
                                                <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}-group-${group["group"]["groupId"]}-groupOption-${group["groupOption"]["groupOptionId"]}`}>
                                                    <span className={OrdersStyles.modifier_label}>{group["group"]["name"]}</span>
                                                    <span className={OrdersStyles.modifier_text}>{getModifierLabel(group, "groupOption")}</span>
                                                </div>
                                            ))
                                        }
                                        </div>
                                    </div>
                                ))
                            }
                            <div>
                                <div className={OrdersStyles.total_item}>
                                    <span className={OrdersStyles.total_label}>Subtotal</span>
                                    <span className={OrdersStyles.total_label}>{`$${openOrder["subtotal"].toFixed(2)}`}</span>
                                </div>
                                <div className={OrdersStyles.total_item}>
                                    <span cclassName={OrdersStyles.item_label}>Subtotal Tax</span>
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
                </div>
            </div>
        )
    }

    if (isLoading) return <div>Loading...</div>
    return (
        <div className={OrdersStyles.orders}>
                <OpenOrder />
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
                    <table className={OrdersStyles.table}>
                        <tbody>
                        {
                            (isLoading) ? <></> : orders.map(order => (
                                    <tr key={"orderid-" + order["orderId"]} onClick={() => handleOrderItemClick(order.orderId)}>
                                        <td>{order["orderId"]}</td>
                                        <td>{order.status}</td>
                                        <td>{new Date(order.orderDate).toLocaleDateString()}</td>
                                        <td>{formatTime(order["orderDate"])}</td>
                                        <td className="Orders_Content_Table_Name_Col">{`${(order["firstName"])} ${order["lastName"]}`}</td>
                                    </tr>
                                )
                            )
                        }
                        </tbody>
                    </table>
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