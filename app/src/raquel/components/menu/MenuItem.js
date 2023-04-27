import React from 'react';
import MenuItemStyles from './MenuItem.module.css';

import ItemImage from '../../../components/ItemImage';
import SavedOrder from '../../../pages/order/SavedOrder';

const MenuItem = (props) => {
    function isDrink(name) { return ["Diet Coke", "Sprite", "Coke", "Root Beer", "Dr Pepper", "Mountain Dew", "Pepsi", "Orange Crush", "Dasani Water"].includes(name) };
    if (props.item?.savedOrder) return <SavedOrder savedOrder={props.savedOrder} />
    return (
        <div className={MenuItemStyles.menuitem}>
            <button onClick={() => props.handleOpenItem(props.itemId)}>
                <div className={MenuItemStyles.details}>
                    <h3 className={MenuItemStyles.header}>{props.name}</h3>
                    <span className={MenuItemStyles.description}>{props.description}</span>
                    <span className={MenuItemStyles.price}>{`$${props.price.toFixed(2)}`}</span>
                </div>
                <div className={isDrink(props.name) ? MenuItemStyles.image_fit_wrapper : MenuItemStyles.image_wrapper}>
                    <ItemImage name={props.name}/>
                </div>
            </button>
        </div>
    );
}

export default MenuItem;