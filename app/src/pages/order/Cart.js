import React, { useEffect } from 'react';
import './cart.css';
import CartStyles from './css/Cart.module.css';
import { IoMdClose } from 'react-icons/io';
import OrderItemSummary from './OrderItemSummary';
import { useNavigate } from 'react-router-dom';
import { getOrderSubtotal } from './functions/OrderFunctions';

const Cart = ({ order, setOrder, handleEditItemClick, handleRemoveItemClick, cartOpen, cartIsOpen }) => {
    const navigate = useNavigate();

    const handleCheckoutClick = event => {
        if (order.orderItems.length > 0) {
            localStorage.setItem("order", JSON.stringify(order));
            navigate("/salerno/order/checkout");
        }
    }
    useEffect(() => {
        if (cartOpen) localStorage.setItem("order", JSON.stringify(order));
    }, [order]);

    if (!cartOpen) return <></>
    return (
        <div className={CartStyles.cart}>
            <div className={CartStyles.close_button_wrapper}>
                <button type="button" onClick={() => cartIsOpen(false)}><IoMdClose size={"26px"}/></button>
            </div>
            <h2 className={CartStyles.header}>Order Details</h2>
            <div className={CartStyles.border}></div>
                <button
                    type="button"
                    className={CartStyles.checkout_button}
                    onClick={handleCheckoutClick}
                >
                    <span>Checkout</span>
                    <span>{`$${getOrderSubtotal(order).toFixed(2)}`}</span>
                </button>
            <div className={CartStyles.items}>
                <OrderItemSummary
                    order={order}
                    setOrder={setOrder}
                    handleEditItemClick={handleEditItemClick}
                    handleRemoveItemClick={handleRemoveItemClick}
                />
            </div>
        </div>
    )
}

export default Cart;