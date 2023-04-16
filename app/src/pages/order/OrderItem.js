import React, { useEffect, useState } from 'react';
import { IoMdClose } from 'react-icons/io';
import OrderItemAddons from './OrderItem_Addons';
import OrderItemNoOptions from './OrderItem_NoOptions';
import OrderItemGroups from './OrderItem_Groups';
import ItemImage from '../../components/ItemImage';
import OrderItemStyles from './css/OrderItem.module.css';
import { getOrderItemPrice } from './functions/OrderFunctions';
import OrderItemSummaryStyles from './css/OrderItemSummary.module.css';
import { FiMinus, FiPlus } from 'react-icons/fi';


const OrderItem = ({ selectedItemData, setSelectedItemData, order, setOrder, cartIsOpen }) => {
    const [optionsSelected, setOptionsSelected] = useState({ groups: [], addons: [], noOptions: [] });
    const [count, setCount] = useState(1);

    useEffect(() => {
        if (selectedItemData.index !== null) {
            const selectedItem = order.orderItems[selectedItemData.index];
            setOptionsSelected({ groups: selectedItem.modifier.groups, addons: selectedItem.modifier.addons, noOptions: selectedItem.modifier.noOptions });
            setCount(selectedItem.count);
        }
    }, [selectedItemData.index]);

    const handleCountClick = (type) => {
        if (type === "increment") {
            setCount(prev => prev + 1);
        } else if (type === "decrement") {
            if (count === 1) {
                return;
            } else {
                setCount(prev => prev - 1);
            }
        }
    }

    const handleAddToCartClick = event => {
        // If not all required groups are selected, prevent add to cart.
        if (optionsSelected.groups.length < selectedItemData.item.modifier.groups.length) return;
        const orderItem = {
            "itemId": selectedItemData.item.itemId,
            "name": selectedItemData.item.name,
            "price": getOrderItemPrice(selectedItemData.item.price, 1, optionsSelected.addons, optionsSelected.noOptions, optionsSelected.groups),
            "count": count,
            "modifier": {
                "addons": optionsSelected.addons,
                "noOptions": optionsSelected.noOptions,
                "groups": optionsSelected.groups
            }
        }
        const temp = Object.assign({}, order);
        if (selectedItemData.index !== null) {
            temp.orderItems[selectedItemData.index] = orderItem;
        } else {
            temp["orderItems"].push(orderItem);
        }
        setOrder(temp);
        setSelectedItemData({ item: null, index: null })
        setOptionsSelected({ groups: [], addons: [], noOptions: [] });
        setCount(1);
        cartIsOpen(true);
    }
    const handleEditItemClick = event => {
        const orderItem = {
            "itemId": selectedItemData.item.itemId,
            "name": selectedItemData.item.name,
            "price": selectedItemData.item.price,
            "count": count,
            "modifier": {
                "addons": optionsSelected.addons,
                "noOptions": optionsSelected.noOptions,
                "groups": optionsSelected.groups
            }
        }
        const temp = Object.assign({}, order);
        temp.orderItems[selectedItemData.index] = orderItem;
        setOrder(temp);
        setCount(1);
        setSelectedItemData({ item: null, index: null })
        setOptionsSelected({ groups: [], addons: [], noOptions: [] });
    }

    const handleClose = event => {
        setSelectedItemData({ item: null, index: null })
        setOptionsSelected({ groups: [], addons: [], noOptions: [] });
    }
    const OrderItemButton = () => {
        const orderItemPrice = getOrderItemPrice(selectedItemData.item.price, 1, optionsSelected.addons, optionsSelected.noOptions, optionsSelected.groups)
        if (selectedItemData.index !== null)
            return <button className={OrderItemStyles.add_button} onClick={handleEditItemClick}>Update item - ${orderItemPrice.toFixed(2)}</button>
        return <button className={OrderItemStyles.add_button} onClick={handleAddToCartClick}>Add to cart - ${orderItemPrice.toFixed(2)}</button>
    }
    function isDrink(itemName) { return ["Diet Coke", "Sprite", "Coke", "Root Beer", "Dr Pepper", "Mountain Dew", "Pepsi", "Orange Crush", "Dasani Water"].includes(itemName); }
    
    if (selectedItemData.item === null) return <></>
    else return (
        <div className={OrderItemStyles.backdrop} onClick={handleClose}>
            <div className={OrderItemStyles.container} onClick={e => e.stopPropagation()}>
                <div className={OrderItemStyles.header}>
                    <button className={OrderItemStyles.close_button} onClick={handleClose}><IoMdClose size={"1.5em"}/></button>
                </div>
                <div className={OrderItemStyles.content}>
                    <h1 className={OrderItemStyles.content_header}>{selectedItemData.item.name}</h1>
                    <span className={OrderItemStyles.description}>{selectedItemData.item.description}</span>
                    <div className={isDrink(selectedItemData.item.name) ? OrderItemStyles.image_fit_wrapper : OrderItemStyles.image_wrapper}>
                        <ItemImage itemName={selectedItemData.item.name}/>
                    </div>
                    <OrderItemGroups
                        groups={selectedItemData.item.modifier.groups}
                        optionsSelected={optionsSelected}
                        setOptionsSelected={setOptionsSelected}
                    />
                    <OrderItemAddons 
                        itemName={selectedItemData.item.name}
                        addons={selectedItemData.item.modifier.addons}
                        optionsSelected={optionsSelected}
                        setOptionsSelected={setOptionsSelected}
                    />
                    <OrderItemNoOptions 
                        itemName={selectedItemData.item.name}
                        noOptions={selectedItemData.item.modifier.noOptions}
                        optionsSelected={optionsSelected}
                        setOptionsSelected={setOptionsSelected}
                    />
                </div>
                <div className={OrderItemStyles.footer} style={{ padding: "15px 25px 15px 25px" }}>
                    <div className={OrderItemSummaryStyles.count_wrapper} style={{ width: "auto", marginRight: "20px", height: "36px" }}>
                        <button type="button" className={OrderItemSummaryStyles.count_button} style={{borderRadius: "25px 0 0 25px", height: "36px", width: "36px" }} onClick={() => handleCountClick("increment")}><FiPlus size={"20px"} style={{borderRadius: "25% 0 0 25%"}}/></button>
                        <span className={OrderItemSummaryStyles.count_label} style={{ width: "20px", margin: "0 7px 0 5px" }}>{`${count}x`}</span>
                        <button type="button" className={(count === 1) ? OrderItemSummaryStyles.count_button__disabled : OrderItemSummaryStyles.count_button} style={{borderRadius: "0 25px 25px 0", height: "36px", width: "36px" }} onClick={() => handleCountClick("decrement")}><FiMinus size={"20px"}/></button>
                    </div>
                    <OrderItemButton />
                </div>
            </div>
        </div>
    )
}

export default OrderItem;