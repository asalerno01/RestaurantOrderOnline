import React from 'react';
import SavedOrderStyles from './css/SavedOrder.module.css';

const SavedOrder = (props) => {
    const handleSelectOrderClick = event => {
        const x = props.savedOrder.orderItems.map(orderItem => ({
            itemId: orderItem.itemId,
            name: orderItem.name,
            description: orderItem.description,
            price: orderItem.price,
            count: orderItem.count,
            addons: props.items.find(item => item.itemId === orderItem.itemId).modifier.addons,
            noOptions: props.items.find(item => item.itemId === orderItem.itemId).modifier.noOptions,
            groups: props.items.find(item => item.itemId === orderItem.itemId).modifier.groups,
            selectedAddons: orderItem.addons.map(addon => addon.addon),
            selectedNoOptions: orderItem.noOptions.map(noOption => noOption.noOption),
            selectedGroups: orderItem.groups.map(group => group.group)
        }));
        props.setSavedOrderName(props.savedOrder.name)
        props.setSelectedItemData(x);
    }
    const OrderItems = ({ orderItems }) => {
        let orderItemString = "";
        orderItems.forEach(orderItem => {
            if (orderItemString.length > 0) orderItemString += " \u2022 ";
            orderItemString += orderItem.name;
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
        <div className={SavedOrderStyles.savedorder}>
            <button onClick={handleSelectOrderClick}>
                <div className={SavedOrderStyles.details}>
                    <h3 className={SavedOrderStyles.header}>{props.savedOrder.name}</h3>
                    <OrderItems orderItems={props.savedOrder.orderItems}/>
                </div>
            </button>
        </div>
    )
}

export default SavedOrder;