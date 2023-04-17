import React from 'react';
import OrderDetailsModifiersStyles from './css/CartModifiers.module.css';

const CartModifiers = ({ modifier }) => {
    let modifierString = "";

    modifier.groups.forEach(group => {
        if (modifierString.length > 0) modifierString += " \u2022 ";
        modifierString += group.groupOptionName;
    });
    modifier.addons.forEach(addon => {
        if (modifierString.length > 0) modifierString += " \u2022 ";
        modifierString += addon.name;
    });
    modifier.noOptions.forEach(noOption => {
        if (modifierString.length > 0) modifierString += " \u2022 ";
        modifierString += noOption.name;
    });
    if (modifierString.length === 0) return <></>
    return (
        <span className={OrderDetailsModifiersStyles.modifier_text}>
            {modifierString}
        </span>
    )
}

export default CartModifiers;