import React from 'react';
import SavedOrderStyles from './css/SavedOrder.module.css';

const SavedOrder = (props) => {
    const handleSelectOrderClick = event => {
        let temp = Object.assign({}, props.order);
        temp.orderItems = props.savedOrder.orderItems;
        props.setOrder(temp);
    }
    const OrderItems = ({ orderItems }) => {
        let orderItemString = "";
        props.orderItems.forEach(orderItem => {
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
        <div className={SavedOrderStyles.savedorder} key={props.key}>
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