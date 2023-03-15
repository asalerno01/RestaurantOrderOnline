import React, { useEffect, useState } from 'react';
import { BsSquare } from "react-icons/bs";
import { FcCheckmark } from "react-icons/fc";
import { FaRegCircle } from 'react-icons/fa';
import { GiPlainCircle } from 'react-icons/gi';
import './orderitem.css';

const OrderItem = ({ item, setOrder, setOrderItem, order }) => {
    console.log(item)
    const [optionsSelected, setOptionsSelected] = useState({ groups: [], addons: [], noOptions: [] });
    // const orderItem = {
    //     "itemId": 1,
    //     "name": "anthony",
    //     "price": 10.00,
    //     "modifier": {
    //         "addons": [],
    //         "noOptions": [],
    //         "groups": []
    //     }
    // }

    const getPrice = () => {
        let price = item["price"];
        optionsSelected.groups.forEach(group => {
            price += group.price
        });
        optionsSelected.addons.forEach(addon => {
            price += addon.price
        });
        optionsSelected.noOptions.forEach(noOption => {
            price += noOption.price
        });
        return price.toFixed(2);
    }

    const handleOptionClick = (type, val) => {
        console.log(type);
        console.log(val)
        let tempOptionsSelected = Object.assign({}, optionsSelected);

        switch (type) {
            case "group":
                console.log(optionsSelected.groups)
                if (tempOptionsSelected.groups.find(group => group.groupId === val.groupId) !== undefined) {
                    console.log("found group");
                    tempOptionsSelected.groups = tempOptionsSelected.groups.filter(group => group.groupId !== val.groupId);
                    tempOptionsSelected.groups.push(val);
                } else {
                    tempOptionsSelected.groups.push(val);
                }
                break;
            case "addon":
                if (tempOptionsSelected.addons.find(addon => addon.addonId === val.addonId) !== undefined) {
                    tempOptionsSelected.addons = tempOptionsSelected.addons.filter(addon => addon.addonId !== val.addonId);
                } else {
                    tempOptionsSelected.addons.push(val);
                }
                break;
            case "noOption":
                if (tempOptionsSelected.noOptions.find(noOption => noOption.noOptionId === val.noOptionId) !== undefined) {
                    tempOptionsSelected.noOptions = tempOptionsSelected.noOptions.filter(noOption => noOption.noOptionId !== val.noOptionId);
                } else {
                    tempOptionsSelected.noOptions.push(val);
                }
                break;
            default:
                break;
        }
        setOptionsSelected(tempOptionsSelected);
    }
    const CheckBox = ({ optionType, value }) => {
        switch (optionType)  {
            case "group":
                if (optionsSelected.groups.find(group => (group.groupId === value.groupId && group.groupOptionId === value.groupOptionId))) {
                    return (
                        <span className="OrderItem_CheckBox_Wrapper">
                            <span className="OrderItem_Checkbox_Box_Icon"><GiPlainCircle size={"1.25em"} /></span>
                        </span>
                    )
                } else {
                    return (
                        <span className="OrderItem_CheckBox_Wrapper">
                            <span className="OrderItem_Checkbox_Box_Icon"><FaRegCircle size={"1.25em"} /></span>
                        </span>
                    )
                }
                break;
            case "addon":
                if (optionsSelected.addons.find(addon => addon.addonId === value.addonId)) {
                    return (
                        <span className="OrderItem_CheckBox_Wrapper">
                            <span className="OrderItem_Checkbox_Box_Icon"><BsSquare size={"1.25em"} /></span>
                            <span className="OrderItem_Checkbox_Checked_Icon"><FcCheckmark size={"2em"} /></span>
                        </span>
                    )
                } else {
                    return (
                        <span className="OrderItem_CheckBox_Wrapper">
                            <span className="OrderItem_Checkbox_Box_Icon"><BsSquare size={"1.25em"} /></span>
                        </span>
                    )
                }
                break;
            case "noOption":
                if (optionsSelected.noOptions.find(noOption => noOption.noOptionId === value.noOptionId)) {
                    return (
                        <span className="OrderItem_CheckBox_Wrapper">
                            <span className="OrderItem_Checkbox_Box_Icon"><BsSquare /></span>
                            <span className="OrderItem_Checkbox_Checked_Icon"><FcCheckmark /></span>
                        </span>
                    )
                } else {
                    return (
                        <span className="OrderItem_CheckBox_Wrapper">
                            <span className="OrderItem_Checkbox_Box_Icon"><BsSquare /></span>
                        </span>
                    )
                }
                break;
            default:
                return (
                    <span className="OrderItem_CheckBox_Wrapper">
                        <span className="OrderItem_Checkbox_Box_Icon"><BsSquare /></span>
                    </span>
                )
        }
    }

    const handleAddToCartClick = () => {
        const orderItem = {
            "itemId": item["itemId"],
            "name": item["name"],
            "price": getPrice(),
            "modifier": {
                "addons": optionsSelected.addons,
                "noOptions": optionsSelected.noOptions,
                "groups": optionsSelected.groups
            }
        }
        const temp = Object.assign({}, order);
        temp["orderItems"].push(orderItem);
        temp["subtotal"] = Number(temp["subtotal"]) + Number(orderItem['price'])
        setOrder(temp);
        setOrderItem({});
        console.log(orderItem)
    }
    function isEmpty(obj) {
        for(var prop in obj) {
            if(obj.hasOwnProperty(prop))
                return false;
        }
        return true;
    }

    const handleClose = event => {
        console.log("hey")
        setOrderItem({});
    }
    console.log(item)
    console.log(item == {});

    const Addons = () => {
        if (item["modifier"]["addons"].length === 0) return <></>
        else return (
            <div className="OrderItem_Addons_Container">
                <div className="OrderItem_Addons_Header">
                    <div className="OrderItem_Addons_Title">Add to {item["name"]}</div>
                    <div className="OrderItem_Options_Type">(Optional)</div>
                </div>
                <div className="OrderItem_Options_Max_Select_Label">Select up to 1</div>
                <ul className="OrderItem_Options_List">
                    {
                        item["modifier"]["addons"].map(addon => (
                            <li key={`addon-${addon["addonId"]}`}>
                                <button className="OrderItem_Option_Button" onClick={() => handleOptionClick("addon", { addonId: addon["addonId"], name: addon["name"], price: addon["price"] })}>
                                    <CheckBox value={{ addonId: addon["addonId"], name: addon["name"], price: addon["price"] }} optionType="addon" />
                                    <span className="OrderItem_Option_Label">{addon["name"]}</span>
                                </button>
                            </li>
                        ))
                    }
                </ul>
            </div>
        )
    }
    const NoOptions = () => {
        if (item["modifier"]["noOptions"].length === 0) return <></>
        else return (
            <div className="OrderItem_Addons_Container">
                <div className="OrderItem_Addons_Header">
                    <div className="OrderItem_Addons_Title">Remove from {item["name"]}</div>
                    <div className="OrderItem_Options_Type">(Optional)</div>
                </div>
                <div className="OrderItem_Options_Max_Select_Label">Select up to 1</div>
                <ul className="OrderItem_Options_List">
                    {
                        item["modifier"]["noOptions"].map(noOption => (
                            <li key={`noOption-${noOption["noOptionId"]}`}>
                                <button className="OrderItem_Option_Button" onClick={() => handleOptionClick("noOption", { noOptionId: noOption["noOptionId"], name: noOption["name"], price: noOption["price"] })}>
                                    <CheckBox value={{ noOptionId: noOption["noOptionId"], name: noOption["name"], price: noOption["price"] }} optionType="noOption" />
                                    <span className="OrderItem_Option_Label">{noOption["name"]}</span>
                                </button>
                            </li>
                        ))
                    }
                </ul>
            </div>
        )
    }
    if (isEmpty(item)) {
        return <></>
    } else return (
        <div className="OrderItem" onClick={handleClose}>
            <div className="OrderItem_Container" onClick={e => e.stopPropagation()}>
                <div className="OrderItem_Header">
                    <div className="OrderItem_Header_Title">C</div>
                </div>
                <div className="OrderItem_Content">
                    <div className="OrderItem_Description">Mustard, Relish, Onion, Pickle, Celery Salt and Sport Pepper</div>
                    <div className="OrderItem_Groups_Container">
                        <div className="OrderItem_Addons_Header">
                            <div className="OrderItem_Addons_Title">Size</div>
                            <div className="OrderItem_Options_Type">(Required)</div>
                        </div>
                        <ul className="OrderItem_Options_List">
                            <li>
                                <button className="OrderItem_Option_Button" onClick={() => handleOptionClick("group", { groupId: 1, groupOptionId: 1, price: 2 })}>
                                    <CheckBox value={{ groupId: 1, groupOptionId: 1 }} optionType="group" />
                                    <span className="OrderItem_Option_Label">Regular</span>
                                </button>
                            </li>
                            <li>
                                <button className="OrderItem_Option_Button" onClick={() => handleOptionClick("group", { groupId: 1, groupOptionId: 2, price: 1 })}>
                                    <CheckBox value={{ groupId: 1, groupOptionId: 2 }} optionType="group" />
                                    <span className="OrderItem_Option_Label">Large</span>
                                </button>
                            </li>
                        </ul>
                    </div>
                    <Addons />
                    <NoOptions />
                </div>
                <div className="OrderItem_Footer">
                    <button className="OrderItem_Footer_AddToCart_Button" onClick={handleAddToCartClick}>Add to cart - ${getPrice()}</button>
                </div>
            </div>
        </div>
    )
}

export default OrderItem;