import React, { useEffect, useState } from 'react';
import axios from 'axios';
import OrderItem from './OrderItem';
import './order.css';

// const items = require('./test_items.json');

const Order = () => {
    const customerAccountId = 1;

    const [items, setItems] = useState([]);
    const [order, setOrder] = useState({"subtotal": 0, "orderItems": [] });
    const [orderItem, setOrderItem] = useState({});
    const [response, setResponse] = useState(null);
    const [openItem, setOpenItem] = useState("");
    const [isLoading, setIsLoading] = useState(true);

    const getItems = async () => {
        await axios.get("https://localhost:7074/api/items")
            .then(res => {
                console.log(res);
                setItems(res.data);
                console.log(res.data)
                setIsLoading(false)
            })
            .catch(err => {
                console.log(err);
            });
    }

    useEffect(() => {
        getItems();
    }, []);
    
    const handleOpenItem = event => {
        event.preventDefault();
        if (orderItem["itemId"] === event.target.value) setOrderItem({})
        else {
            const item = items.find(i => i["itemId"] === event.target.value);
            let orderItem = {
                "itemId": item["itemId"],
                "name": item["name"],
                "price": item["price"],
                "modifier": {
                    "addons": [],
                    "noOptions": [],
                    "groups": []
                }
            }
            console.log(JSON.stringify(orderItem))
            setOrderItem(item);
        }
    }
    const handleAddAddonCheckboxChange = event => {
        event.preventDefault();
        const modifierId = event.target.attributes.modifierid.value;
        console.log("modifierid => " + modifierId);
        const addonId = event.target.attributes.addonid.value;
        console.log("addonid => " + addonId);
        const addonModifierParent = orderItem["basemodifier"].find(bm => bm["modifierId"] == modifierId);
        console.log(JSON.stringify(addonModifierParent))
        const addonToAdd = addonModifierParent["addons"].find(a => a["addonId"] == addonId);
        const tempOrderItem = Object.assign({}, orderItem);
        console.log(JSON.stringify(tempOrderItem))
        if (event.target.checked)
            tempOrderItem["modifier"]["addons"].push(addonToAdd);
        else if (!event.target.checked)
            tempOrderItem["modifier"]["addons"] = tempOrderItem["modifier"]["addons"].filter(a => a["addonId"] != addonId);
        setOrderItem(tempOrderItem);
    }
    const handleAddItem = event => {
        event.preventDefault();
        const temp = Object.assign({}, order);
        temp["orderItems"].push(orderItem);
        temp["subtotal"] = temp["subtotal"] + orderItem['price']
        setOrder(temp);
        setOrderItem({});
    }
    const handleRemoveItem = event => {
        event.preventDefault();
        const temp = Object.assign({}, order);
        temp["orderItems"].splice(event.target.value, 1);
        let subtotal = temp["subtotal"] - orderItem['price'];
        if (isNaN(subtotal)) subtotal = 0;
        temp["subtotal"] = subtotal;
        setOrder(temp);
    }
    const handleCheckout = async event => {
        event.preventDefault();
        let orderItems = [];
        order["orderItems"].forEach(orderItem => {
            let addons = [];
            let noOptions = [];
            let groupOptions = [];
            orderItem["modifier"]["addons"].forEach(addon => {
                addons.push({
                    "addonId": addon["addonId"]
                });
            });
            orderItem["modifier"]["noOptions"].forEach(noOption => {
                noOptions.push({
                    "noOptionId": noOption["noOptionId"]
                });
            });
            // TODO: Handle groups.
            orderItems.push({
                "itemId": orderItem["itemId"],
                "groupOptions": groupOptions,
                "addons": addons,
                "noOptions": noOptions
            })
        });
        // const orderForAPI = {
        //     "customerAccountId": customerAccountId,
        //     "subtotal": order["subtotal"],
        //     "tax": (order["subtotal"] * 0.0825).toFixed(2),
        //     "net": ((order["subtotal"] * 0.0825) + order["subtotal"]).toFixed(2),
        //     "orderItems": orderItems
        // };
        // console.log(JSON.stringify(orderForAPI));
        
        await axios.post("https://localhost:7074/api/order",
            {
                "customerAccountId": customerAccountId,
                "subtotal": order["subtotal"],
                "tax": (order["subtotal"] * 0.0825).toFixed(2),
                "net": ((order["subtotal"] * 0.0825) + order["subtotal"]).toFixed(2),
                "orderItems": orderItems
            }
        )
        .then(res => {
            setResponse("Success!");
            console.log(res);
        })
        .catch(err => {
            setResponse(err.message + ". \n" + err.response.data);
            console.log(err);
        });

    }

    const OpenItem = () => {
        console.log(orderItem)
        if (orderItem === {}) return <></>
        else if (isLoading) return <></>
        else {
            const item = items.find(i => i["itemId"] === openItem);
            console.log("hey")
            return (
                <OrderItem item={orderItem} setOrder={setOrder} setOrderItem={setOrderItem} order={order} />
            )
        }
        // if (item["modifier"].length === 0)
        // return (
        //     <div className="Order_ItemList_Grid_Options_Wrapper">
        //         <button type="button" className="Order_ItemList_Grid_AddItem_Button" onClick={handleAddItem}>Add to cart</button>
        //     </div>
        // )
        // return (
        //     <div className="Order_ItemList_Grid_Options_Wrapper">
        //         <div className="Order_ItemList_Options_Container">
        //             <div className="Order_Item_Options_Addons_Container">
        //                 <div className="Order_Item_Options_Addon_Label">Add-Ons</div>
        //                 <div className="Order_Item_Options_Addons_Wrapper">
        //                     <ul className="Order_Item_Options_Addons_List">
        //                         {
        //                             item['modifier']["addons"].map(addon => (
        //                                     <li key={`addon-id-${addon["addonId"]}`} value={addon['addonId']} className="Order_Item_Options_Addon_Item">
        //                                         <label className="Order_Item_Options_Addon_Item_Label" htmlFor={`addon-checkbox-${addon["addonId"]}`}>
        //                                             <input type="checkbox" id={`addon-checkbox-${addon["addonId"]}`}
        //                                                 className="Order_Item_Options_Addon_Item_Checkbox"
        //                                                 checked={orderItem["modifier"]["addons"].some(a => a["addonId"] === addon["addonId"])}
        //                                                 addonid={addon["addonId"]}
        //                                                 onChange={handleAddAddonCheckboxChange}
        //                                             />
        //                                             {addon["name"]}
        //                                         </label>
        //                                         <div>${addon["price"].toFixed(2)}</div>
        //                                     </li>
        //                                 )
        //                             )
        //                         }
        //                     </ul>

        //                 </div>
        //             </div>
        //         </div>
        //         <button type="button" className="Order_ItemList_Grid_AddItem_Button" onClick={handleAddItem}>Add to cart</button>
        //     </div>
        // )
    }
    console.log(order)
    return (
        <div className="Order">
            <OrderItem item={orderItem} setOrder={setOrder} setOrderItem={setOrderItem} order={order} />
            { (response === "Success!") ? <h3 style={{color: "green"}}>{response}</h3> : (response !== null) ? <h3 style={{color: "red"}}>{response}</h3> : <></> }
            <div className="Order_Container">
                <div className="Order_ItemList">
                    <h2 className="Order_Details_Header">Items</h2>
                    <table className="Order_ItemList_Table">
                        <tbody>
                            {
                                items.map(item => (
                                    <tr key={item['itemId']}>
                                        <td>
                                            <div className="Order_ItemList_Grid">
                                                <div className="Order_ItemList_Grid_Button_Wrapper">
                                                    <button className="Order_ItemList_Button" onClick={handleOpenItem} value={item['itemId']} type="button">{item['name']}</button>
                                                </div>
                                                <div className="Order_ItemList_Grid_Price_Wrapper">
                                                    <div>${item['price'].toFixed(2)}</div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </table>
                </div>
                <div className="Order_Details">
                    <h2 className="Order_Details_Header">Order Details</h2>
                    <table className="Order_Details_Table">
                        <tbody>
                        {
                            order["orderItems"].map((orderItem, index) => (
                                <tr key={index}>
                                    <td>
                                        <div className="Order_Details_Item">
                                            <div className="Order_Details_Name_Wrapper">
                                                <div>{orderItem['name']}</div>
                                            </div>
                                            <div className="Order_Details_Price_Wrapper">
                                                <div>${orderItem['price']}</div>
                                            </div>
                                            <div>
                                                <button type="button" value={index} onClick={handleRemoveItem} className="Order_Details_Remove_Button">X</button>
                                            </div>
                                            <div className="Order_Details_modifier_Wrapper">
                                                {
                                                    (orderItem["modifier"]["addons"].length === 0) ? <></>
                                                    :
                                                    <div className="Order_Details_modifier_Addons_Wrapper">
                                                        <div className="Order_Details_modifier_Addons_Header">Add-Ons</div>
                                                        <ul className="Order_Details_modifier_Addons_List">
                                                            {
                                                                orderItem["modifier"]["addons"].map(addon => (
                                                                    <li key={`orderitem-${orderItem["itemId"]}-addon-${addon["addonId"]}`} className="Order_Details_modifier_List_Item">
                                                                        <div className="Order_Details_modifier_List_Item_Name">{addon["name"]}</div>
                                                                        <div className="Order_Details_modifier_List_Item_Price">${addon["price"]}</div>
                                                                    </li>
                                                                ))
                                                            }
                                                        </ul>
                                                    </div>
                                                }
                                            </div>
                                            <div className="Order_Details_modifier_Wrapper">
                                                {
                                                    (orderItem["modifier"]["noOptions"].length === 0) ? <></>
                                                    :
                                                    <div className="Order_Details_modifier_Addons_Wrapper">
                                                        <div className="Order_Details_modifier_Addons_Header">Remove from {orderItem["name"]}</div>
                                                        <ul className="Order_Details_modifier_Addons_List">
                                                            {
                                                                orderItem["modifier"]["noOptions"].map(addon => (
                                                                    <li key={`orderitem-${orderItem["itemId"]}-addon-${addon["addonId"]}`} className="Order_Details_modifier_List_Item">
                                                                        <div className="Order_Details_modifier_List_Item_Name">{addon["name"]}</div>
                                                                        {/* <div className="Order_Details_modifier_List_Item_Price">${addon["price"]}</div> */}
                                                                    </li>
                                                                ))
                                                            }
                                                        </ul>
                                                    </div>
                                                }
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            ))
                        } 
                        </tbody>
                    </table>
                    <div className="Order_Details_Pricing_Wrapper">
                        <ul className="Order_Details_Pricing_List">
                            <li className="Order_Details_Pricing_List_Item">
                                <div className="Order_Details_Pricing_Label">Subtotal:</div>
                                <div className="Order_Details_Pricing_Price">${order["subtotal"].toFixed(2)}</div>
                            </li>
                            <li className="Order_Details_Pricing_List_Item">
                                <div className="Order_Details_Pricing_Label">Tax:</div>
                                <div className="Order_Details_Pricing_Price">${(order["subtotal"] * 0.0825).toFixed(2)}</div>
                            </li>
                            <li className="Order_Details_Pricing_List_Item">
                                <div className="Order_Details_Pricing_Label">Total:</div>
                                <div className="Order_Details_Pricing_Price">${(order["subtotal"] * 1.0825).toFixed(2)}</div>
                            </li>
                        </ul>
                    </div>
                    <div className="Order_Details_Checkout_Wrapper">
                        <button type="button" className="Order_Details_Checkout_Button" onClick={handleCheckout}>Checkout</button>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Order;