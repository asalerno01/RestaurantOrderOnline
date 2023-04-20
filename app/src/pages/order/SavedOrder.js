import React from 'react';
import SavedOrderStyles from './css/SavedOrder.module.css';
import { getOrderItemPrice } from './functions/OrderFunctions';

const SavedOrder = (props) => {
    const handleSelectOrderClick = event => {
        let temp = Object.assign({}, props.order);
        temp.orderItems = [];
        props.savedOrder.orderItems.forEach(orderItem => {
            console.log(orderItem)
            temp.orderItems.push({
                "itemId": orderItem.itemId,
                "name": orderItem.itemName,
                "price": orderItem.price,
                "count": orderItem.count,
                "addons": orderItem.addons.map(addon => {
                    return addon.addon;
                }),
                "noOptions": orderItem.noOptions.map(noOption => {
                    return noOption.noOption
                }),
                "groups": orderItem.groups.map(group => {
                    return group.group;
                })
            });
        });
        console.log(temp)
        props.setOrder(temp);
        props.cartIsOpen(true);
    }
    const OrderItems = ({ orderItems }) => {
        let orderItemString = "";
        orderItems.forEach(orderItem => {
            if (orderItemString.length > 0) orderItemString += " \u2022 ";
            orderItemString += orderItem.itemName;
        });
        if (orderItemString.charAt(orderItemString.length - 1) === "\u2022")
            orderItemString = orderItemString.slice(0, orderItemString.length - 1);
        if (orderItemString.length === 0) return <></>
        return (
            <span className={SavedOrderStyles.description}>
                {orderItemString}
            </span>
        )
    }
    return (
        <div className={SavedOrderStyles.savedorder}>
            <button onClick={handleSelectOrderClick}>
                <div className={SavedOrderStyles.details}>
                    <h3 className={SavedOrderStyles.header}>{props.savedOrder.savedOrderName}</h3>
                    <OrderItems orderItems={props.savedOrder.orderItems}/>
                </div>
            </button>
        </div>
    )
}

export default SavedOrder;