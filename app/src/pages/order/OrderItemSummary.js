import React from 'react';
import OrderItemSummaryStyles from './css/OrderItemSummary.module.css';
import { SlPencil } from 'react-icons/sl';
import { RiDeleteBin6Line } from 'react-icons/ri';
import { FiMinus, FiPlus } from 'react-icons/fi';
import CartModifiers from './CartModifiers';
import ItemImage from '../../components/ItemImage';
import { getOrderItemPrice, isDrink } from './functions/OrderFunctions';

const OrderItemSummary = ({ order, setOrder, items, setSelectedItemData }) => {
    const handleCountClick = (type, index) => {
        let tempOrder = [...order];
        const currentCount = tempOrder[index].count;
        if (type === "increment") {
            tempOrder[index].count = currentCount + 1;
        } else if (type === "decrement") {
            if (currentCount === 1) {
                tempOrder = tempOrder.filter((orderItem, thisIndex) => thisIndex !== index);
            } else {
                tempOrder[index].count = currentCount - 1;
            }
        }
        console.log(tempOrder)
        setOrder(tempOrder);
    }
    
    const handleEditItemClick = (itemId, index) => {
        console.log(items);
        setSelectedItemData({ item: items.find(item => item.itemId === itemId), index: index });
    }
    const handleRemoveItemClick = (index) => {
        let newOrder = order.toSpliced(index, 1);
        setOrder(newOrder);
    }
    console.log(order)
    return (
        <>
        {
            order.map((orderItem, index) => (
                <div key={index}>
                    <div className={OrderItemSummaryStyles.card}>
                        <div className={OrderItemSummaryStyles.item_button} onClick={() => handleEditItemClick(orderItem.itemId, index)}>
                            <div className={isDrink(orderItem.name) ? OrderItemSummaryStyles.image_fit_wrapper : OrderItemSummaryStyles.image_wrapper}>
                                <ItemImage name={orderItem.name}/>
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