import React, { useEffect } from 'react';
import CartStyles from './css/Cart.module.css';
import { IoMdClose } from 'react-icons/io';
import OrderItemSummary from './OrderItemSummary';
import { useNavigate } from 'react-router-dom';
import { getOrderSubtotal, saveToLocalStorage } from './functions/OrderFunctions';

const Cart = ({ order, setOrder, items, setSelectedItemData, cartOpen, cartIsOpen }) => {
    const navigate = useNavigate();
    const handleCheckoutClick = event => {
        if (order.length > 0) {
            navigate("/salerno/order/checkout");
        }
    }
    if (!cartOpen) return <></>
    return (
        <div className={CartStyles.cart}>
            <div className={CartStyles.close_button_wrapper}>
                <button type="button" onClick={() => cartIsOpen(false)}>
                    <IoMdClose size={"26px"}/>
                </button>
            </div>
            <h2 className={CartStyles.header}>Order Details</h2>
            <div className={CartStyles.border}></div>
                <button
                    type="button"
                    className={CartStyles.checkout_button}
                    onClick={handleCheckoutClick}
                >
                    <span>Checkout</span>
                    <span>{`$${getOrderSubtotal(order, items).toFixed(2)}`}</span>
                </button>
            <div className={CartStyles.items}>
                <OrderItemSummary
                    order={order}
                    setOrder={setOrder}
                    items={items}
                    setSelectedItemData={setSelectedItemData}
                />
            </div>
        </div>
    )
}

export default Cart;