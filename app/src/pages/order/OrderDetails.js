import React from 'react';
import './orderdetails.css';
import OrderDetailsStyles from './css/OrderDetails.module.css';

import OrderItemSummary from './OrderItemSummary';
import { useNavigate } from 'react-router-dom';

const OrderDetails = ({ order, handleRemoveItemClick, handleItemClick, orderDetailsOpen, setOrderDetailsOpen, detailsOpen, openDetails }) => {
    const navigate = useNavigate();

    const handleCheckoutClick = event => {
        event.preventDefault();
        if (order.orderItems.length > 0) {
            localStorage.setItem("order", JSON.stringify(order));
            console.log("stored")
            navigate("/salerno/order/checkout");
        }
    }
    function getSubtotal() {
        let subtotal = 0;
        order.orderItems.forEach(orderItem => {
            subtotal += Number(orderItem.price);
        });
        console.log(subtotal)
        return Number(subtotal);
    }
    if (!detailsOpen) return <></>
    return (
        <div>
            <button type="button" onClick={() => openDetails(false)}>X</button>
            <h2 className={OrderDetailsStyles.header}>Order Details</h2>
            <div className={OrderDetailsStyles.border}></div>
                <button type="button" className={OrderDetailsStyles.checkout_button} onClick={handleCheckoutClick}><span>Checkout</span><span>{`$${getSubtotal().toFixed(2)}`}</span></button>
            <div className={OrderDetailsStyles.items}>
                <OrderItemSummary order={order} handleItemClick={handleItemClick} handleRemoveItemClick={handleRemoveItemClick} />
            </div>
        </div>
    )
}

export default OrderDetails;