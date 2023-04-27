import React, { useEffect, useState } from 'react';
import { IoMdClose } from 'react-icons/io';
import OrderItemStyles from './css/OrderItem.module.css';
import { BsRecordCircle, BsCircle } from 'react-icons/bs';
import { FiAlertTriangle } from 'react-icons/fi';
import { ImCheckboxChecked, ImCheckboxUnchecked } from 'react-icons/im';
import ModifierStyles from './css/OrderItemModifier.module.css';


const SavedOrderItem = ({ selectedItemData, setSelectedItemData, savedOrderName, setOrder, cartIsOpen }) => {
    const [newSelectedSavedOrders, setNewSelectedSavedOrders] = useState([...selectedItemData])

    function getOrderSubtotal() {
        let subtotal = 0;
        newSelectedSavedOrders.forEach(orderItem => {
            let { count, selectedAddons, selectedNoOptions, selectedGroups } = orderItem;
            subtotal += getOrderItemPrice(orderItem.price, count, selectedAddons, selectedNoOptions, selectedGroups);
        });
        return subtotal;
    }
    function getOrderItemPrice(basePrice, count, addons, noOptions, groups) {
        let orderItemPrice = basePrice;
        addons.forEach(addon => { orderItemPrice += Number(addon.price); });
        noOptions.forEach(noOption => { orderItemPrice -= Number(noOption.price); });
        groups.forEach(group => { orderItemPrice += Number(group.price); });
        return Number(orderItemPrice * count);
    }

    const handleAddToCartClick = event => {
        // If not all required groups are selected, prevent add to cart.
        newSelectedSavedOrders.forEach(orderItem => {
            if (orderItem.selectedGroups.length < orderItem.groups.length) return;
        })
        const tempOrder = newSelectedSavedOrders.map(orderItem => ({
            itemId: orderItem.itemId,
            name: orderItem.name,
            price: orderItem.price,
            count: orderItem.count,
            addons: orderItem.selectedAddons,
            noOptions: orderItem.selectedNoOptions,
            groups: orderItem.selectedGroups
        }));
        
        setOrder(tempOrder);
        setSelectedItemData({ item: null, index: null })
        setNewSelectedSavedOrders([]);
        cartIsOpen(true);
    }

    const handleClose = event => {
        setSelectedItemData({ item: null, index: null })
        setNewSelectedSavedOrders([])
    }
    const OrderItemButton = () => {
        const tempOrder = newSelectedSavedOrders.map(orderItem => ({
            itemId: orderItem.itemId,
            name: orderItem.name,
            description: orderItem.description,
            price: orderItem.price,
            count: orderItem.count,
            addons: orderItem.selectedAddons,
            noOptions: orderItem.selectedNoOptions,
            groups: orderItem.selectedGroups
        }));
        const subtotal = getOrderSubtotal(tempOrder)
        return <button className={OrderItemStyles.add_button} onClick={handleAddToCartClick}>Add to cart - ${subtotal.toFixed(2)}</button>
    }

    const Addons = ({ newSelectedSavedOrders, setNewSelectedSavedOrders, index }) => {
    
        function formatPrice(price) {
            return (!isNaN(price) && price > 0) ? `+$${price.toFixed(2)}` : "";
        }
    
        const handleAddonClick = (addon) => {
            const temp = [...newSelectedSavedOrders]
            if (temp[index].selectedAddons.find(a => a.addonId === addon.addonId) !== undefined)
                temp[index].selectedAddons = temp[index].selectedAddons.filter(a => a.addonId !== addon.addonId);
            else
                temp[index].selectedAddons.push(addon);
            setNewSelectedSavedOrders(temp);
        }
        const CheckBox = ({ addonId }) => {
            if (newSelectedSavedOrders[index].selectedAddons.find(addon => addon.addonId === addonId)) {
                return <ImCheckboxChecked className={ModifierStyles.checkbox} size={"18px"} style={{color: "rgb(0, 187, 225)"}} />
            } else {
                return <ImCheckboxUnchecked className={ModifierStyles.checkbox} size={"18px"} />
            }
        }
        if (newSelectedSavedOrders[index].addons.length === 0) return <></>
        return (
            <div className={ModifierStyles.container}>
                <div className={ModifierStyles.header}>
                    <span className={ModifierStyles.title}>Add to {newSelectedSavedOrders[index].name}</span>
                    <span className={ModifierStyles.select_type}>(Optional)</span>
                </div>
                <span className={ModifierStyles.max_select_label}>Select up to {newSelectedSavedOrders[index].addons.length}</span>
                <ul className={ModifierStyles.options}>
                {
                    newSelectedSavedOrders[index].addons.map(addon => (
                        <li className={ModifierStyles.option} 
                            key={addon.addonId} 
                            onClick={() => handleAddonClick({ addonId: addon.addonId, name: addon.name, price: addon.price })}
                        >
                            <button className={ModifierStyles.button}>
                                <CheckBox addonId={addon.addonId} />
                                <span className={ModifierStyles.label}>{addon.name}</span>
                                <span className={ModifierStyles.label__price}>{formatPrice(addon.price)}</span>
                            </button>
                        </li>
                    ))
                }
                </ul>
            </div>
        )
    }
    const Groups = ({ newSelectedSavedOrders, setNewSelectedSavedOrders, index }) => {
        console.log(newSelectedSavedOrders)
        function formatPrice(price) {
            if (!isNaN(price) && price > 0)
                return `+$${price.toFixed(2)}`;
            else
                return "";
        }
    
        const handleGroupClick = (option) => {
            const temp = [...newSelectedSavedOrders]
            if (temp[index].selectedGroups.find(group => group.groupId === option.groupId) !== undefined) {
                temp[index].selectedGroups = temp[index].selectedGroups.filter(group => group.groupId !== option.groupId);
                temp[index].selectedGroups.push(option);
            } else {
                temp[index].selectedGroups.push(option);
            }
            setNewSelectedSavedOrders(temp);
        }
        const CheckBox = ({ groupId, groupOptionId }) => {
            if (newSelectedSavedOrders[index].selectedGroups.find(group => (group.groupId === groupId && group.groupOptionId === groupOptionId)))
                return <BsRecordCircle size={"17px"} style={{color: "rgb(0, 187, 225)", verticalAlign: "middle"}}/>
            else
                return <BsCircle size={"17px"} style={{verticalAlign: "middle", color: "rgb(118, 118, 118)"}}/>
        }
    
        if (newSelectedSavedOrders[index].groups.length === 0) return <></>
        return (
            <div className="orderitem__modifier">
            {
                newSelectedSavedOrders[index].groups.map(group => (
                    <div className={ModifierStyles.container} key={group.groupId}>
                        <div className={ModifierStyles.header}>
                            <span className={ModifierStyles.title}>{group.name}</span>
                            <span className={ModifierStyles.select_type__group}>
                                <FiAlertTriangle className={ModifierStyles.alert_icon} size={"1.25em"} />Required</span>
                        </div>
                        <span className={ModifierStyles.max_select_label}>Select 1</span>
                        <ul className={ModifierStyles.options}>
                        {
                            group.groupOptions.map(groupOption => (
                                <li key={`groupId-${group.groupId}-groupOptionId${groupOption.groupOptionId}`}
                                    className={ModifierStyles.option}
                                    onClick={() => handleGroupClick(
                                        { 
                                            groupId: group.groupId,
                                            groupName: group.name,
                                            groupOptionId: groupOption.groupOptionId,
                                            groupOptionName: groupOption.name,
                                            price: groupOption.price
                                        }
                                    )}
                                >
                                    <button className={ModifierStyles.button}>
                                            <span className={ModifierStyles.radio_icon}>
                                                <CheckBox groupId={group.groupId} groupOptionId={groupOption.groupOptionId} />
                                            </span>
                                        <span className={ModifierStyles.label}>{groupOption.name}</span>
                                        <span className={ModifierStyles.label__price}>{formatPrice(groupOption.price)}</span>
                                    </button>
                                </li>
                            ))
                        }
                        </ul>
                    </div>
                ))
            }
            </div>
        )
    }
    const NoOptions = ({ newSelectedSavedOrders, setNewSelectedSavedOrders, index }) => {
        function formatPrice(price) {
            return (!isNaN(price) && price > 0) ? `+$${price.toFixed(2)}` : "";
        }
    
        const handleNoOptionClick = (noOption) => {
            const temp = [...newSelectedSavedOrders]
            if (temp[index].selectedNoOptions.find(n => n.addonId === noOption.noOptionId) !== undefined)
                temp[index].selectedNoOptions = temp[index].selectedNoOptions.filter(n => n.noOptionId !== noOption.noOptionId);
            else
                temp[index].selectedNoOptions.push(noOption);
            setNewSelectedSavedOrders(temp);
        }
        const CheckBox = ({ noOptionId }) => {
            if (newSelectedSavedOrders[index].selectedNoOptions.find(noOption => noOption.noOptionId === noOptionId)) {
                return <ImCheckboxChecked className={ModifierStyles.checkbox} size={"18px"} style={{color: "rgb(0, 187, 225)"}} />
            } else {
                return <ImCheckboxUnchecked className={ModifierStyles.checkbox} size={"18px"} />
            }
        }
        if (newSelectedSavedOrders[index].noOptions.length === 0) return <></>
        return (
            <div className={ModifierStyles.container}>
                <div className={ModifierStyles.header}>
                    <span className={ModifierStyles.title}>Remove from {newSelectedSavedOrders[index].name}</span>
                    <span className={ModifierStyles.select_type}>(Optional)</span>
                </div>
                <span className={ModifierStyles.max_select_label}>Select up to {newSelectedSavedOrders[index].noOptions.length}</span>
                <ul className={ModifierStyles.options}>
                {
                    newSelectedSavedOrders[index].noOptions.map(noOption => (
                        <li key={noOption.noOptionId}
                            className={ModifierStyles.option}
                            onClick={() => handleNoOptionClick(
                                { 
                                    noOptionId: noOption.noOptionId,
                                    name: noOption.name,
                                    price: noOption.price
                                })}
                        >
                            <button className={ModifierStyles.button}>
                                <CheckBox noOptionId={noOption.noOptionId} />
                                <span className={ModifierStyles.label}>{noOption.name}</span>
                                <span className={ModifierStyles.label__price}>{formatPrice(noOption.price)}</span>
                            </button>
                        </li>
                    ))
                }
                </ul>
            </div>
        )
    }

    
    if (selectedItemData.item === null) return <></>
    else return (
        <div className={OrderItemStyles.backdrop} onClick={handleClose}>
            <div className={OrderItemStyles.container} onClick={e => e.stopPropagation()}>
                <div className={OrderItemStyles.header}>
                    <button className={OrderItemStyles.close_button} onClick={handleClose}><IoMdClose size={"1.5em"}/></button>
                </div>
                <h1 className={OrderItemStyles.content_header} style={{paddingLeft: "15px"}}>{savedOrderName}</h1>
                <div className={OrderItemStyles.content}>
                    {
                        newSelectedSavedOrders.map((orderItem, index) => (
                            <>
                            
                <div className={OrderItemStyles.gap}></div>
                            <div key={`orderItemItemId-${orderItem.itemId}-${index}`}>
                                <h1 className={OrderItemStyles.content_header} style={{paddingTop: "15px"}}>{orderItem.name}</h1>
                                <span className={OrderItemStyles.description}>{orderItem.description}</span>
                                <Groups
                                    newSelectedSavedOrders={newSelectedSavedOrders}
                                    setNewSelectedSavedOrders={setNewSelectedSavedOrders}
                                    index={index}
                                />
                                <Addons 
                                    newSelectedSavedOrders={newSelectedSavedOrders}
                                    setNewSelectedSavedOrders={setNewSelectedSavedOrders}
                                    index={index}
                                />
                                <NoOptions 
                                    newSelectedSavedOrders={newSelectedSavedOrders}
                                    setNewSelectedSavedOrders={setNewSelectedSavedOrders}
                                    index={index}
                                />
                            </div>

                            </>
                        ))
                    }
                </div>
                <div className={OrderItemStyles.footer} style={{ padding: "15px 25px 15px 25px" }}>
                        <OrderItemButton />
                </div>
            </div>
        </div>
    )
}

export default SavedOrderItem;