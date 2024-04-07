import React from 'react';
import OrderItemSummaryStyles from './css/OrderItemSummary.module.css';
import { SlPencil } from 'react-icons/sl';
import { RiDeleteBin6Line } from 'react-icons/ri';
import { FiMinus, FiPlus } from 'react-icons/fi';
import CartModifiers from './CartModifiers';
import ItemImage from '../../components/ItemImage';
import { getOrderItemPrice, isDrink } from './functions/OrderFunctions';
import { ShoppingCartAPI } from '../../api/ShoppingCartAPI';
import useAuth from '../../hooks/useAuth';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';

const OrderItemSummary = ({ order, setOrder, items, setSelectedItemData }) => {
    const { auth } = useAuth();
    const axiosPrivate = useAxiosPrivate();
    
    const handleEditItemClick = (itemId, index) => {
        console.log(items)
        setSelectedItemData({ 
            item: items.find(item => item.itemId === itemId),
            index: index
        });
    }

    const handleIncreaseCountClick = async (orderItem) => {
        await axiosPrivate({
            method: 'PUT',
            url: `https://localhost:7074/api/ShoppingCart/increase/${orderItem.shoppingCartItemId}`,
            headers: {
                Authorization: `Bearer ${auth?.accessToken}`
            }
        }).then(res => setOrder(res.data)).catch(err => console.log(err));
    }
    
    const handleDecreaseCountClick = (orderItem) => {
        ShoppingCartAPI.decreaseCount(orderItem.shoppingCartItemId, auth?.accessToken)
        .then((shoppingCart) => {
            setOrder(shoppingCart)
        });
    }

    const handleDeleteClick = (orderItem) => {
        ShoppingCartAPI.delete(orderItem.shoppingCartItemId, auth?.accessToken)
        .then((shoppingCart) => {
            setOrder(shoppingCart)
        });
    }
    
    return (
        <>
        {
            order.map((orderItem, index) => (
                <div key={index}>
                    <div className={OrderItemSummaryStyles.card}>
                        <div className={OrderItemSummaryStyles.item_button} onClick={() => handleEditItemClick(orderItem.item.itemId, index)}>
                            <div className={isDrink(orderItem.item.name) ? OrderItemSummaryStyles.image_fit_wrapper : OrderItemSummaryStyles.image_wrapper}>
                                <ItemImage name={orderItem.item.name}/>
                            </div>
                            <div className={OrderItemSummaryStyles.details}>
                                <span className={OrderItemSummaryStyles.name}>{orderItem.item.name}</span>
                                <CartModifiers
                                    addons={orderItem.addons}
                                    noOptions={orderItem.noOptions}
                                    groups={orderItem.groups}
                                />
                                <span className={OrderItemSummaryStyles.price}>{`$${orderItem.price.toFixed(2)}`}</span>
                            </div>
                            <div className={OrderItemSummaryStyles.buttons} onClick={e => e.stopPropagation()}>
                                <div className={OrderItemSummaryStyles.count_wrapper}>
                                    <button type="button" className={OrderItemSummaryStyles.count_button} style={{borderRadius: "25px 0 0 25px"}} onClick={() => handleIncreaseCountClick(orderItem)}><FiPlus size={"20px"} style={{borderRadius: "25% 0 0 25%"}}/></button>
                                    <span className={OrderItemSummaryStyles.count_label}>{`${orderItem.count}x`}</span>
                                    <button type="button" className={OrderItemSummaryStyles.count_button}style={{borderRadius: "0 25px 25px 0"}} onClick={() => handleDecreaseCountClick(orderItem)}><FiMinus size={"20px"}/></button>
                                </div>
                                <div className={OrderItemSummaryStyles.controls}>
                                    <button className={OrderItemSummaryStyles.delete_icon} onClick={() => handleDeleteClick(orderItem)}>
                                        <RiDeleteBin6Line size='24px' style={{minWidth: "24px"}}/>
                                        <span className={OrderItemSummaryStyles.delete_label}>Delete</span>
                                    </button>
                                    <button className={OrderItemSummaryStyles.edit_icon} onClick={() => handleEditItemClick(orderItem.item.itemId, index)}>
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