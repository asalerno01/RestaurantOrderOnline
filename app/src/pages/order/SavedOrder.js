import React from 'react';
import MenuItemStyles from './../../raquel/components/menu/MenuItem.module.css';

const SavedOrder = ({ savedOrder, order, setOrder }) => {
    console.log(savedOrder);
    const handleSelectOrderClick = event => {
        let temp = Object.assign({}, order);
        temp.orderItems = savedOrder.orderItems;
        setOrder(temp);
    }
    const OrderItems = ({ orderItems }) => {
        console.log(orderItems)
        let orderItemString = "";
        orderItems.forEach(orderItem => {
            if (orderItemString.length > 0) orderItemString += " \u2022 ";
            orderItemString += orderItem.itemName;
        });
        if (orderItemString.charAt(orderItemString.length - 1) === "\u2022")
            orderItemString = orderItemString.slice(0, orderItemString.length - 1);
        if (orderItemString.length === 0) return <></>
        return (
            <span className={MenuItemStyles.modifiers}>
                {orderItemString}
            </span>
        )
    }
    return (
        <div className={MenuItemStyles.menuitem}>
            <button onClick={handleSelectOrderClick}>
                <div className={MenuItemStyles.details}>
                    <h3 className={MenuItemStyles.header}>{savedOrder.savedOrderName}</h3>
                    <OrderItems orderItems={savedOrder.orderItems}/>
                </div>
            </button>
        </div>
    )
}

export default SavedOrder;