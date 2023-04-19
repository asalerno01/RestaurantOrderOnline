import React from 'react';
import './cart.css';
import OrderItemSummaryStyles from './css/OrderItemSummary.module.css';
import { SlPencil } from 'react-icons/sl';
import { RiDeleteBin6Line } from 'react-icons/ri';
import { FiMinus, FiPlus } from 'react-icons/fi';
import CartModifiers from './CartModifiers';
import ItemImage from '../../components/ItemImage';
import { getOrderItemPrice, isDrink } from './functions/OrderFunctions';

const OrderItemSummary = ({ order, setOrder, handleEditItemClick, handleRemoveItemClick }) => {
    const handleCountClick = (type, index) => {
        let temp = Object.assign({}, order);
        const currentCount = temp.orderItems[index].count;
        if (type === "increment") {
            temp.orderItems[index].count = currentCount + 1;
        } else if (type === "decrement") {
            if (currentCount === 1) {
                temp.orderItems = temp.orderItems.filter((orderItem, thisIndex) => thisIndex !== index);
            } else {
                temp.orderItems[index].count = currentCount - 1;
            }
        }
        setOrder(temp);
        localStorage.setItem("order", JSON.stringify(temp));
    }
    return (
        <>
        {
            order.orderItems.map((orderItem, index) => (
                <div key={index}>
                    <div className={OrderItemSummaryStyles.card}>
                        <div className={OrderItemSummaryStyles.item_button} onClick={() => handleEditItemClick(orderItem.itemId, index)}>
                            <div className={isDrink(orderItem.name) ? OrderItemSummaryStyles.image_fit_wrapper : OrderItemSummaryStyles.image_wrapper}>
                                <ItemImage itemName={orderItem.name}/>
                            </div>
                            <div className={OrderItemSummaryStyles.details}>
                                <span className={OrderItemSummaryStyles.name}>{orderItem.name}</span>
                                <CartModifiers
                                    addons={orderItem.addons}
                                    noOptions={orderItem.noOptions}
                                    groups={orderItem.groups}
                                />
                                <span className={OrderItemSummaryStyles.price}>{`$${getOrderItemPrice(orderItem.price, orderItem.count, orderItem.addons, orderItem.noOptions, orderItem.groups).toFixed(2)}`}</span>
                            </div>
                            <div className={OrderItemSummaryStyles.buttons} onClick={e => e.stopPropagation()}>
                                <div className={OrderItemSummaryStyles.count_wrapper}>
                                    <button type="button" className={OrderItemSummaryStyles.count_button} style={{borderRadius: "25px 0 0 25px"}} onClick={() => handleCountClick("increment", index)}><FiPlus size={"20px"} style={{borderRadius: "25% 0 0 25%"}}/></button>
                                    <span className={OrderItemSummaryStyles.count_label}>{`${orderItem.count}x`}</span>
                                    <button type="button" className={OrderItemSummaryStyles.count_button}style={{borderRadius: "0 25px 25px 0"}} onClick={() => handleCountClick("decrement", index)}><FiMinus size={"20px"}/></button>
                                </div>
                                <div className={OrderItemSummaryStyles.controls}>
                                    <button className={OrderItemSummaryStyles.delete_icon} onClick={() => handleRemoveItemClick(index)}>
                                        <RiDeleteBin6Line size='24px' style={{minWidth: "24px"}}/>
                                        <span className={OrderItemSummaryStyles.delete_label}>Delete</span>
                                    </button>
                                    <button className={OrderItemSummaryStyles.edit_icon} onClick={() => handleEditItemClick(orderItem.itemId, index)}>
                                        <SlPencil size='20px' style={{minWidth: "20px"}}/>
                                        <span className={OrderItemSummaryStyles.edit_label}>Edit</span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className={OrderItemSummaryStyles.border}></div>
                </div>
            ))
        }
        </>
    )
}

export default OrderItemSummary;