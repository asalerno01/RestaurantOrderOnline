import React, { useEffect } from 'react';
import ActiveStyles from './css/Active.module.css';

const Active = () => {
    const [orders, setOrders] = useState([]);
    const [openOrder, setOpenItem] = useState({});
    const [miniCalendarOpen, setMiniCalendarOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(true);
    const [statusDropdownOpen, setStatusDropdownOpen] = useState(false);
    const [filterDate, setFilterDate] = useState(getLastSevenDays());

    const getOrders = async () => {
        await axios({
            method: "GET",
            url: "https://localhost:7074/api/orders/simple/active"
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
    useEffect(() => getOrders(), []);

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
    const handleOrderItemClick = async (orderId) => {
        await axios({
            method: "GET",
            url: `https://localhost:7074/api/orders/${orderId}`
        })
        .then(res => setOpenItem(res.data))
        .catch(err => console.log(err));
    }

    const OpenItem = () => {
        if (isEmptyObject(openOrder)) return <></>
        else return (
            <div className={ActiveStyles.stretch} onClick={() => setOpenItem({})}>
                <div onClick={e => e.stopPropagation()}>
                    <div className={ActiveStyles.modal}>
                        <div className={ActiveStyles.modal_header_wrapper}>
                            <h2 className={ActiveStyles.modal_header}>Order #{openOrder.orderId}</h2>
                            <div className={ActiveStyles.close_button} onClick={() => setOpenItem({})}><IoMdClose size={"1.5em"}/></div>
                        </div>
                        <div className={ActiveStyles.gap}></div>
                        <div className={ActiveStyles.pickup}>
                            <h2 className={ActiveStyles.pickup_header}>Pick-Up Details</h2>
                            <div>
                                <div className={ActiveStyles.pickup_label}>Customer</div>
                                <div className={ActiveStyles.pickup_value}>{(openOrder.account === null) ? "New Customer" : `${openOrder.account.firstName} ${openOrder.account.lastName}`}</div>
                            </div>
                            <div className={ActiveStyles.border}></div>
                            <div>
                                <div className={ActiveStyles.pickup_label}>Picked Up</div>
                                <div className={ActiveStyles.pickup_value}>
                                    {(openOrder.pickUpDate === null) ? "Pending" : formatTime(openOrder.pickupDate)}
                                    <span style={{marginLeft: "10px"}}>
                                        {`(Quoted ${formatTime(new Date(openOrder.orderDate).setMinutes(new Date(openOrder.orderDate).getMinutes() + 20))})`}
                                    </span>
                                </div>
                                <div className="Orders_OrderItem_Modal_Customer_Details_Value">{new Date(openOrder["orderDate"]).toLocaleDateString()}</div>
                            </div>
                        </div>
                        <div className={ActiveStyles.gap}></div>
                        <div className={ActiveStyles.details}>
                            <h2 className={ActiveStyles.details_header}>Order Details</h2>
                            <div>
                            {
                                openOrder.map(orderItem => (
                                    <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}`} className={ActiveStyles.item}>
                                        <div className={ActiveStyles.count}>{orderItem.count}x</div>
                                        <div className={ActiveStyles.item_details}>
                                            <div className={ActiveStyles.name}>{orderItem["name"]}</div>
                                            <div className={ActiveStyles.price}>${getOrderItemPrice(orderItem).toFixed(2)}</div>
                                        </div>
                                        <div className={ActiveStyles.addons}>
                                        {
                                            (orderItem["addons"].length === 0) ? <></> : orderItem["addons"].map(addon => (
                                                <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}-addon-${addon["addon"]["addonId"]}`}>
                                                    <span className={ActiveStyles.modifier_label}>Addon:</span>
                                                    <span className={ActiveStyles.modifier_text}>{getModifierLabel(addon, "addon")}</span>
                                                </div>
                                            ))
                                        }
                                        </div>
                                        <div className={ActiveStyles.noOptions}>
                                        {
                                            (orderItem["noOptions"].length === 0) ? <></> : orderItem["noOptions"].map(noOption => (
                                                <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}-noOption-${noOption["noOption"]["noOptionId"]}`}>
                                                    <span className={ActiveStyles.modifier_label}>Remove:</span>
                                                    <span className={ActiveStyles.modifier_text}>{getModifierLabel(noOption, "noOption")}</span>
                                                </div>
                                            ))
                                        }
                                        </div>
                                        <div className={ActiveStyles.groups}>
                                        {
                                            (orderItem["groups"].length === 0) ? <></> : orderItem["groups"].map(group => (
                                                <div key={`order-${openOrder["orderId"]}-orderitem-${orderItem["orderItemId"]}-group-${group["group"]["groupId"]}-groupOption-${group["groupOption"]["groupOptionId"]}`}>
                                                    <span className={ActiveStyles.modifier_label}>{group["group"]["name"]}</span>
                                                    <span className={ActiveStyles.modifier_text}>{getModifierLabel(group, "groupOption")}</span>
                                                </div>
                                            ))
                                        }
                                        </div>
                                    </div>
                                ))
                            }
                            <div>
                                <div className={ActiveStyles.total_item}>
                                    <span className={ActiveStyles.total_label}>Subtotal</span>
                                    <span className={ActiveStyles.total_label}>{`$${openOrder["subtotal"].toFixed(2)}`}</span>
                                </div>
                                <div className={ActiveStyles.total_item}>
                                    <span cclassName={ActiveStyles.item_label}>Subtotal Tax</span>
                                    <span className={ActiveStyles.total_value}>{`$${openOrder["subtotalTax"].toFixed(2)}`}</span>
                                </div>
                                <div className={ActiveStyles.total_item}>
                                    <span className={ActiveStyles.total_label} style={{fontSize: "16px", fontWeight: "700"}}>Total</span>
                                    <span className={ActiveStyles.total_value} style={{fontSize: "16px", fontWeight: "700", margin: "6px 0 0 0"}}>{`$${openOrder["total"].toFixed(2)}`}</span>
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
        <div className={ActiveStyles.orders}>
                <OpenItem />
                <h1 className={ActiveStyles.header}>Orders</h1>
                <div className={ActiveStyles.tab_buttons}>
                    <button type="button" className={ActiveStyles.tab_button__current}>Active</button>
                    <button type="button" className={ActiveStyles.tab_button}>History</button>
                </div>
                <div className={ActiveStyles.controls}>
                    <div className={ActiveStyles.search_wrapper}>
                        <input type="text" className={ActiveStyles.search} placeholder="Search Orders..." />
                    </div>
                </div>
                <div>
                    <StickyBox style={{zIndex: "1"}} offsetTop={-41}>
                        <table className={ActiveStyles.sticky_table}>
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
                    <table className={ActiveStyles.table}>
                        <tbody>
                        {
                            (isLoading) ? <></> : orders.map(order => (
                                    <tr key={"orderid-" + order.orderId} onClick={() => handleOrderItemClick(order.orderId)}>
                                        <td>{order.orderId}</td>
                                        <td>{order.status}</td>
                                        <td>{new Date(order.orderDate).toLocaleDateString()}</td>
                                        <td>{formatTime(order.orderDate)}</td>
                                        <td style={{ textDecoration: "capitalize" }}>{`${(order.firstName)} ${order.lastName}`}</td>
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

export default Active;