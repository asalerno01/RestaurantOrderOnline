import React from 'react';
import ItemModifiersStyles from './css/ItemModifiers.module.css';

const ItemModifiers = ({ orderItem }) => {
    function getModifierLabel(modifier, type) {
        if (!isNaN(modifier[type]["price"]) && modifier[type]["price"] > 0)
            if (type === "noOption") return `${modifier[type]["name"]} (-$${modifier[type]["price"].toFixed(2)})`;
            else return `${modifier[type]["name"]} ($${modifier[type]["price"].toFixed(2)})`;
        return modifier[type]["name"];
    }
    return (
        <div>
        {
            orderItem["addons"].map(addon => (
                <div key={`addon-${addon["addon"]["addonId"]}`}>
                    <span className={ItemModifiersStyles.label}>Addon:</span>
                    <span className={ItemModifiersStyles.name}>{getModifierLabel(addon, "addon")}</span>
                </div>
            ))
        }
        {
            orderItem["noOptions"].map(noOption => (
                <div key={`noOption-${noOption["noOption"]["noOptionId"]}`}>
                    <span className={ItemModifiersStyles.label}>Remove:</span>
                    <span className={ItemModifiersStyles.name}>{getModifierLabel(noOption, "noOption")}</span>
                </div>
            ))
        }
        {
            (orderItem["groups"].length === 0) ? <></> : orderItem["groups"].map(group => (
                <div key={`group-${group["group"]["groupId"]}-groupOption-${group["groupOption"]["groupOptionId"]}`}>
                    <span className={ItemModifiersStyles.label}>{group["group"]["name"]}</span>
                    <span className={ItemModifiersStyles.name}>{getModifierLabel(group, "groupOption")}</span>
                </div>
            ))
        }
        </div>
    )
}

export default ItemModifiers;