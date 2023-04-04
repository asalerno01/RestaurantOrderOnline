import React, { useEffect, useState } from 'react';
import axios from 'axios';
import OrderItem from './OrderItem';
import './order.css';

import MenuItem from '../../raquel/components/menu/MenuItem';
import OrderDetailsModifiers from './OrderDetailsModifiers';
import { IoMdClose } from 'react-icons/io';
import OrderDetails from './OrderDetails';
import Banner from '../../imgs/banner.webp';

const Order = () => {
    const customerAccountId = 1;

    const [items, setItems] = useState([]);
    const [categories, setCategories] = useState([]);
    const [order, setOrder] = useState({
        "orderId": generateUUIDUsingMathRandom(),
        "subtotal": 0,
        "subtotalTax": 0,
        "total": 0,
        "orderItems": []
    });
    useEffect(() => {
        const order = localStorage.getItem("order");
        if (order !== null && order.length > 0)
            setOrder(JSON.parse(order));
    }, []);
    
    // TODO: drop subtotal.
    const [orderItem, setOrderItem] = useState({});
    const [editItemIndex, setEditItemIndex] = useState(null);
    const [response, setResponse] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    const getItems = async () => {
        await axios.get("https://localhost:7074/api/Category")
        .then(res => {
            console.log(res);
            let items = [];
            let categories = res.data;
            categories.forEach(category => {
                category.items.forEach(item => {
                    items.push(item);
                });
            });
            console.log(items);
            setCategories(res.data);
            setItems(items);
            setIsLoading(false)
        })
        .catch(err => {
            console.log(err);
        });
    }

    useEffect(() => {
        getItems();
    }, []);

    function generateUUIDUsingMathRandom() { 
        // wow
        // https://qawithexperts.com/article/javascript/generating-guiduuid-using-javascript-various-ways/372#:~:text=Generating%20GUID%2FUUID%20using%20Javascript%20%28Various%20ways%29%201%20Generate,is%20fast%20to%20generate%20an%20ASCII-safe%20GUID%20
        var d = new Date().getTime();//Timestamp
        var d2 = (performance && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random() * 16;//random number between 0 and 16
            if(d > 0){ //Use timestamp until depleted
                r = (d + r)%16 | 0;
                d = Math.floor(d/16);
            } else { //Use microseconds since page-load if supported
                r = (d2 + r)%16 | 0;
                d2 = Math.floor(d2/16);
            }
            return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
    }

    const handleItemClick = (itemId, index) => {
        setEditItemIndex(index);
        const item = items.find(i => i["itemId"] === itemId);
        setOrderItem(item);
    }
    const handleOpenItem = (itemId) => {
        console.log("items=>" + items);
        if (orderItem["itemId"] === itemId) setOrderItem({})
        else {
            const item = items.find(i => i["itemId"] === itemId);
            setOrderItem(item);
        }
    }
    const handleRemoveItemClick = (index) => {
        console.log("hey")
        const temp = Object.assign({}, order);
        let subtotal = temp["subtotal"] - orderItem['price'];
        temp["orderItems"].splice(index, 1);
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
            orderItem["modifier"]["noOptions"].forEach(group => {
                groupOptions.push({
                    "groupId": group["groupId"],
                    "groupOptionId": group["groupOptionId"]
                });
            });
            orderItems.push({
                "itemId": orderItem["itemId"],
                "groupOptions": groupOptions,
                "addons": addons,
                "noOptions": noOptions
            })
        });
        
        await axios.post("https://localhost:7074/api/orders",
            {
                "customerAccountId": customerAccountId,
                "subtotal": order["subtotal"],
                "subtotalTax": (order["subtotal"] * 0.0825).toFixed(2),
                "total": ((order["subtotal"] * 0.0825) + order["subtotal"]).toFixed(2),
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

    function formatPrice(price, type) {
        if (!isNaN(price) && price > 0) {
            if (type === "noOptions")
                return `-$${price.toFixed(2)}`;
            return `+$${price.toFixed(2)}`;
        }
            return "";
    }

    console.log(order)
    return (
        <div className="order">
            <OrderItem itemI={orderItem} setOrder={setOrder} setOrderItem={setOrderItem} order={order} editItemIndex={editItemIndex} setEditItemIndex={setEditItemIndex} />
            { (response === "Success!") ? <h3 style={{color: "green"}}>{response}</h3> : (response !== null) ? <h3 style={{color: "red"}}>{response}</h3> : <></> }
                <div className="order_header">
                    <div className="order_header_banner_wrapper">
                        <img src={Banner} className="order_header_banner_image" />
                    </div>
                    <div className="order_header_content_container">
                        <h1 className="order_header_content_header">Salerno's Red Hots</h1>
                        <span className="order_header_content_text">197 E Veterans Pkwy, Yorkville, IL 60560, USA</span>
                        <span className="order_header_content_text">Open Hours: 11:00 AM - 8:00 PM</span>
                    </div>
                </div>
                <hr className="order_border"/>
                <div className="order_content_container">
                    <div className="order__itemlist">
                        {
                            categories.map(category => (
                                <div className="category_container">
                                    <h1 className="category_header">{category.name}</h1>
                                    <div className="order_border"></div>
                                    <div className="itemlist__container">
                                    {
                                        category.items.map(item => (
                                            <MenuItem
                                                key={item.itemId}
                                                itemId={item.itemId}
                                                name={item.name}
                                                price={item.price}
                                                description={item.description}
                                                handleOpenItem={handleOpenItem}
                                            />
                                        ))
                                    }
                                    </div>
                                </div>
                            ))
                        }
                        </div>
                    <OrderDetails order={order} handleItemClick={handleItemClick} handleRemoveItemClick={handleRemoveItemClick} />
            </div>
        </div>
    );
}

export default Order;


/*
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
*/