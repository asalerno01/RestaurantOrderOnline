import React from 'react';
import { BsRecordCircle, BsCircle } from 'react-icons/bs';
import { FiAlertTriangle } from 'react-icons/fi';
import { ImCheckboxChecked, ImCheckboxUnchecked } from 'react-icons/im';
import ModifierStyles from './css/OrderItemModifier.module.css';

const Addons = ({ name, baseItemAddons, optionsSelected, setOptionsSelected }) => {

    function formatPrice(price) {
        return (!isNaN(price) && price > 0) ? `+$${price.toFixed(2)}` : "";
    }

    const handleAddonClick = (addon) => {
        const tempOptionsSelected = Object.assign({}, optionsSelected);
        if (tempOptionsSelected.addons.find(a => a.addonId === addon.addonId) !== undefined)
            tempOptionsSelected.addons = tempOptionsSelected.addons.filter(a => a.addonId !== addon.addonId);
        else
            tempOptionsSelected.addons.push(addon);
        setOptionsSelected(tempOptionsSelected);
    }
    const CheckBox = ({ addonId }) => {
        if (optionsSelected.addons.find(addon => addon.addonId === addonId)) {
            return <ImCheckboxChecked className={ModifierStyles.checkbox} size={"18px"} style={{color: "rgb(0, 187, 225)"}} />
        } else {
            return <ImCheckboxUnchecked className={ModifierStyles.checkbox} size={"18px"} />
        }
    }
    if (baseItemAddons.length === 0) return <></>
    return (
        <div className={ModifierStyles.container}>
            <div className={ModifierStyles.header}>
                <span className={ModifierStyles.title}>Add to {name}</span>
                <span className={ModifierStyles.select_type}>(Optional)</span>
            </div>
            <span className={ModifierStyles.max_select_label}>Select up to {baseItemAddons.length}</span>
            <ul className={ModifierStyles.options}>
            {
                baseItemAddons.map(addon => (
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
const Groups = ({ baseItemGroups, optionsSelected, setOptionsSelected }) => {
    function formatPrice(price) {
        if (!isNaN(price) && price > 0)
            return `+$${price.toFixed(2)}`;
        else
            return "";
    }

    const handleGroupClick = (option) => {
        const tempOptionsSelected = Object.assign({}, optionsSelected);
        if (tempOptionsSelected.groups.find(group => group.groupId === option.groupId) !== undefined) {
            tempOptionsSelected.groups = tempOptionsSelected.groups.filter(group => group.groupId !== option.groupId);
            tempOptionsSelected.groups.push(option);
        } else {
            tempOptionsSelected.groups.push(option);
        }
        setOptionsSelected(tempOptionsSelected);
    }
    const CheckBox = ({ groupId, groupOptionId }) => {
        if (optionsSelected.groups.find(group => (group.groupId === groupId && group.groupOptionId === groupOptionId)))
            return <BsRecordCircle size={"17px"} style={{color: "rgb(0, 187, 225)", verticalAlign: "middle"}}/>
        else
            return <BsCircle size={"17px"} style={{verticalAlign: "middle", color: "rgb(118, 118, 118)"}}/>
    }

    if (baseItemGroups.length === 0) return <></>
    return (
        <div className="orderitem__modifier">
        {
            baseItemGroups.map(group => (
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
const NoOptions = ({ name, baseItemNoOptions, optionsSelected, setOptionsSelected }) => {

    function formatPrice(price) {
        if (!isNaN(price) && price > 0)
            return `-$${price.toFixed(2)}`;
        else
            return "";
    }

    const handleNoOptionClick = (noOption) => {
        const tempOptionsSelected = Object.assign({}, optionsSelected);
        if (tempOptionsSelected.noOptions.find(n => n.noOptionId === noOption.noOptionId) !== undefined)
            tempOptionsSelected.noOptions = tempOptionsSelected.noOptions.filter(n => n.noOptionId !== noOption.noOptionId);
        else
            tempOptionsSelected.noOptions.push(noOption);
        setOptionsSelected(tempOptionsSelected);
    }
    const CheckBox = ({ noOptionId }) => {
        if (optionsSelected.noOptions.find(noOption => noOption.noOptionId === noOptionId)) {
            return <ImCheckboxChecked className={ModifierStyles.checkbox} size={"18px"} style={{color: "rgb(0, 187, 225)"}} />
        } else {
            return <ImCheckboxUnchecked className={ModifierStyles.checkbox}size={"18px"} />
        }
    }
    if (baseItemNoOptions.length === 0) return <></>
    return (
        <div className={ModifierStyles.container}>
            <div className={ModifierStyles.header}>
                <span className={ModifierStyles.title}>Remove from {name}</span>
                <span className={ModifierStyles.select_type}>(Optional)</span>
            </div>
            <span className={ModifierStyles.max_select_label}>Select up to {baseItemNoOptions.length}</span>
            <ul className={ModifierStyles.options}>
            {
                baseItemNoOptions.map(noOption => (
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

export { Addons, NoOptions, Groups };