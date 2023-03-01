import React, { useEffect, useRef, useState } from "react";
import { IconContext } from "react-icons/lib";
import { BsPlusSquareFill } from 'react-icons/bs';
import { RiDeleteBinFill } from 'react-icons/ri';
import { TiArrowSortedDown, TiArrowSortedUp } from 'react-icons/ti';
import axios from 'axios';
import ButtonTabs from "../../components/ButtonTabs";
import { useParams } from "react-router-dom";
import FilterButton from "../../components/FilterButton";
import './css/modifiers.css';

export default function Modifiers() {

    const [modifiers, setModifiers] = useState(null);
    const { guid } = useParams();

    useEffect(() => {
        if (guid !== undefined) {
            axios.get(`https://localhost:7074/api/item/${guid}/modifiers`)
            .then(res => {
                console.log(res.data);
                setModifiers(res.data);
            })
            .catch(function (err) {
                console.log(err.message);
            });
        }
    }, []);

    const handleAddGroup = () => {
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['groups'].push(
            {
                "name": "New Group",
                "groupId": generateUUIDUsingMathRandom(),
                "groupOptions": []
            }
        )
        setModifiers(temp);
    }
    const handleAddGroupOption = (group) => {
        console.log(group)
        console.log('adding group option to: ' + group['name'])
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['groups'].find(g => g['groupId'] === group['groupId'])['groupOptions'].push({ "name": "New Option", "price": 0.00, "groupOptionId": generateUUIDUsingMathRandom() });
        setModifiers(temp);
    }
    const handleRemoveGroupOption = event => {
        event.preventDefault();
        console.log(event.target.parentElement.parentElement.parentElement.attributes.groupid.value);
        console.log(event.target.parentElement.parentElement.parentElement.attributes.groupoptionid.value);
        let groupId = event.target.parentElement.parentElement.parentElement.attributes.groupid.value;
        let groupOptionId = event.target.parentElement.parentElement.parentElement.attributes.groupoptionid.value;
        let temp = Object.assign({}, modifiers);
        console.log(temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId))
        console.log(temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId)['groupOptions'])
        temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId)['groupOptions'] = temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId)['groupOptions'].filter(go => go['groupOptionId'] != groupOptionId);
        setModifiers(temp);
    }
    const handleRemoveGroup = (groupInput) => {
        let temp = Object.assign({}, modifiers);
        let newGroups = temp['modifiers'][0]['groups'].filter(group => group['groupId'] !== groupInput['groupId']);
        temp['modifiers'][0]['groups'] = newGroups;
        setModifiers(temp);
    }
    const handleRemoveAddOn = (addOnInput) => {
        let temp = Object.assign({}, modifiers);
        let newAddons = temp['modifiers'][0]['addons'].filter(addOn => addOn['addonId'] !== addOnInput['addonId']);
        temp['modifiers'][0]['addons'] = newAddons;
        setModifiers(temp);
    }
    const handleRemoveNoOption = (noOptionInput) => {
        let temp = Object.assign({}, modifiers);
        let newNoOptions = temp['modifiers'][0]['noOptions'].filter(noOption => noOption['noOptionId'] !== noOptionInput['noOptionId']);
        temp['modifiers'][0]['noOptions'] = newNoOptions;
        setModifiers(temp);
    }

    const handleAddAddOn = () => {
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['addons'].push({ "name": "New Option", "price": 0.00, "addonId": generateUUIDUsingMathRandom()});
        setModifiers(temp);
    }
    const handleAddNoOption = () => {
        console.log('x')
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['noOptions'].push({ "name": "New Option", "price": 0.00, "noOptionId": generateUUIDUsingMathRandom() });
        setModifiers(temp);
    }

    const [copyDropdown, setCopyDropdown] = useState({ "type": "", "value": "Copy/Import from another item" });

    const testRef = useRef();

    const handleCopyOpenDropdown = event => {
        setCopyDropdown(event.target.value);
    }

    function generateUUIDUsingMathRandom() { 
        // wow
        // https://qawithexperts.com/article/javascript/generating-guiduuid-using-javascript-various-ways/372#:~:text=Generating%20GUID%2FUUID%20using%20Javascript%20%28Various%20ways%29%201%20Generate,is%20fast%20to%20generate%20an%20ASCII-safe%20GUID%20
        var d = new Date().getTime();//Timestamp
        var d2 = (performance && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = Math.random() * 16;//random number between 0 and 16
            if(d > 0){//Use timestamp until depleted
                r = (d + r)%16 | 0;
                d = Math.floor(d/16);
            } else {//Use microseconds since page-load if supported
                r = (d2 + r)%16 | 0;
                d2 = Math.floor(d2/16);
            }
            return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
    }


    const handleGroupNameChange = event => {
        event.preventDefault();
        let groupId = event.target.attributes.groupid.value;
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId)['name'] = event.target.value;
        setModifiers(temp);
    }
    const handleGroupOptionNameChange = event => {
        event.preventDefault();
        console.log(event.target.attributes)
        let groupId = event.target.attributes.groupid.value;
        let groupOptionId = event.target.attributes.groupoptionid.value;
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId)['groupOptions'].find(go => go['groupOptionId'] == groupOptionId)['name'] = event.target.value;
        setModifiers(temp);
    }
    const handleAddonNameChange = event => {
        event.preventDefault();
        let addonId = event.target.attributes.addonid.value;
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['addons'].find(a => a['addonId'] == addonId)['name'] = event.target.value;
        setModifiers(temp);
    }
    const handleNoOptionNameChange = event => {
        event.preventDefault();
        let noOptionId = event.target.attributes.nooptionid.value;
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['noOptions'].find(no => no['noOptionId'] == noOptionId)['name'] = event.target.value;
        setModifiers(temp);
    }

    const handleInputChange = event => {
        event.preventDefault();
        let type = event.target.attributes.thistype.value;
        let id = event.target.attributes.thisid.value;
        console.log("e")
        let thisattr = event.target.attributes.thisattr.value;
        let tempModifier = Object.assign({}, modifiers);
        
        console.log(event.target)
        switch (type) {
            case "addons":
                console.log('a');
                tempModifier['modifiers'][0]['addons'].find(a => a['addonId'] == id)[thisattr] = event.target.value;
                break;
            case "noOptions":
                console.log('b');
                tempModifier['modifiers'][0]['noOptions'].find(no => no['noOptionId'] == id)[thisattr] = event.target.value;
                break;
            case "groupOptions":
                console.log('c');
                tempModifier['modifiers'][0]['groups'].find(g => g['groupId'] == event.target.attributes.groupid.value)['groupOptions'].find(go => go['groupOptionId'] == id)[thisattr] = event.target.value;
                break;
            case "groups":
                console.log('d');
                tempModifier['modifiers'][0]['groups'].find(g => g['groupId'] == id)[thisattr] = event.target.value;
                break;
            default:
                break;
        }
        setModifiers(tempModifier);
    }
    
    const handleGroupOptionPriceChange = event => {
        event.preventDefault();
        let groupId = event.target.attributes.groupid.value;
        let groupOptionId = event.target.attributes.groupoptionid.value;
        let temp = Object.assign({}, modifiers);
        temp['modifiers'][0]['groups'].find(g => g['groupId'] == groupId)['groupOptions'].find(go => go['groupOptionId'] == groupOptionId)['price'] = event.target.value;
        setModifiers(temp);
    }
    const copyInputRef = useRef();

    const GroupContent = () => {
        if (modifiers === null || modifiers['modifiers'][0]['groups'].length === 0) {
            return (
                <div className='Modifiers_Groups_Empty_Container'>
                    <ul className='Modifiers_Groups_Empty_List'>
                        <li className='Modifiers_Groups_Empty_List_Item'>Group modifiers allow you to select one option from a list</li>
                        <li className='Modifiers_Groups_Empty_List_Item'>The base option is activated by default and included in the base price</li>
                    </ul>
                    <div className='Modifiers_Groups_Empty_Example_Container'>
                        <div className='Modifiers_Groups_Empty_Example'>Example: Small, Medium, or Large</div>
                        
                        <button type='button' className='Modifiers_Add_Option_Button_First' onClick={handleAddGroup}>
                            <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                            </IconContext.Provider>
                            Add a group
                        </button>
                    </div>
                </div>
            )
        } else
        return (
            <div>
                {
                modifiers['modifiers'][0]['groups'].map((group, index) => {
                    return (
                        <div key={`groupId${group['groupId']}`}>
                            <div className='Modifiers_Group_Container'>
                                <div className='Modifiers_Group_Name_Container'>
                                    <span className='Modifiers_Group_Name_Label'>Group Name</span>
                                    <input type='text' className='Modifiers_Group_Name_Input' value={group['name']} groupid={group['groupId']} onChange={handleInputChange} />
                                    <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveGroup(group)}>
                                        <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.25em' }}>
                                            <RiDeleteBinFill />
                                        </IconContext.Provider>
                                        Delete
                                    </button>
                                </div>
                                <div className='Modifiers_Group_List_Container'>
                                    <div className='Modifiers_Group_List_Header'>
                                        <span className='Modifiers_Group_List_Header_Name'>Option name</span>
                                        <span className='Modifiers_Group_List_Header_Add_Price'>Add to price</span>
                                        <span className='Modifiers_Group_List_Header_Base'>Base</span>
                                    </div>
                                    
                                    <ul className='Modifiers_Group_List'>
                                    {
                                        group['groupOptions'].map((option, index) => (
                                                <li key={option + index}>
                                                    <input type='text' className='Modifiers_Option_Name_Input' defaultValue={option['name']} />
                                                    <input type='text' className='Modifiers_Option_Price_Input' defaultValue={option['price']} />
                                                    <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveGroupOption(group, option)}>
                                                        <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.5em' }}>
                                                            <RiDeleteBinFill />
                                                        </IconContext.Provider>
                                                </button>
                                            </li>
                                            )
                                        )
                                    }
                                        <li>
                                            <button type='button' className='Modifiers_Add_Option_Button' onClick={() => handleAddGroupOption(group)}>
                                                <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                    <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                </IconContext.Provider>
                                                Add another option
                                            </button>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    )
                                }
                )}
                <div className='Modifiers_Group_Footer'>
                    <button type='button' className='Modifiers_Add_Option_Button' onClick={handleAddGroup}>
                        <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                            <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                        </IconContext.Provider>
                        Add another group
                    </button>
                </div>
            </div>
        )
    }
        
    const NoOptions = () => {
        if (modifiers === null || modifiers['modifiers'][0]['noOptions'].length === 0)
            return (
                <div className='Modifiers_Groups_Empty_Container'>
                    <ul className='Modifiers_Groups_Empty_List'>
                        <li className='Modifiers_Groups_Empty_List_Item'>"NO" Options are activated by default and do not print on receipts</li>
                        <li className='Modifiers_Groups_Empty_List_Item'>If pressed, they will deactivate and print "No option name" on all receipts</li>
                    </ul>
                    <div className='Modifiers_Groups_Empty_Example_Container'>
                        <div className='Modifiers_Groups_Empty_Example'>Example: Cheeseburger, No bun</div>
                        
                        <button type='button' className='Modifiers_Add_Option_Button_First' onClick={handleAddNoOption}>
                            <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                            </IconContext.Provider>
                            Add an option
                        </button>
                    </div>
                </div>
            )
        else
            return (
                <div className='Modifiers_NoOptions_Container'>
                    <div className='Modifiers_NoOptions_List_Container'>
                        <div className='Modifiers_NoOptions_List_Header'>
                            <span className='Modifiers_NoOptions_List_Header_Name'>Option name</span>
                            <span className='Modifiers_NoOptions_List_Header_Add_Price'>Discount when OFF</span>
                        </div>
                        <ul className='Modifiers_NoOptions_List'>
                            {
                                modifiers['modifiers'][0]['noOptions'].map(noOption => (
                                    <li key={`noOption-${noOption['noOptionId']}`}>
                                        <input type='text' className='Modifiers_Option_Name_Input' value={noOption['name']} thistype="noOptions" thisattr="name" thisid={noOption['noOptionId']} onChange={handleInputChange} />
                                        <input type='text' className='Modifiers_Option_Price_Input' value={noOption['price']} thistype="noOptions" thisattr="price" thisid={noOption['noOptionId']} onChange={handleInputChange} />
                                        <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveNoOption(noOption)}>
                                            <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.5em' }}>
                                                <RiDeleteBinFill />
                                            </IconContext.Provider>
                                        </button>
                                    </li>
                                ))
                            }
                            <li>
                                <button type='button' className='Modifiers_Add_Option_Button' onClick={handleAddNoOption}>
                                    <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                        <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                    </IconContext.Provider>
                                    Add another option
                                </button>
                            </li>
                        </ul>
                    </div>
                </div>
            )
    }

    // useEffect(() => {
    //     copyInputRef.current.focus();
    //   }, [copyDropdown]);

    const AddOns = () => {
        if (modifiers === null || modifiers['modifiers'][0]['addons'].length === 0) {
            return (
                <div className='Modifiers_Groups_Empty_Container'>
                    <ul className='Modifiers_Groups_Empty_List'>
                        <li className='Modifiers_Groups_Empty_List_Item'>Add-on Options will print on all receipts when activated</li>
                        <li className='Modifiers_Groups_Empty_List_Item'>If activated, option price will be added to base price</li>
                    </ul>
                    <div className='Modifiers_Groups_Empty_Example_Container'>
                        <div className='Modifiers_Groups_Empty_Example'>Example: Extra Cheese (add 0.50 to price)</div>
                        
                        <button type='button' className='Modifiers_Add_Option_Button_First' onClick={handleAddAddOn}>
                            <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                            </IconContext.Provider>
                            Add an add-on
                        </button>
                    </div>
                </div>
            )
        } else
        return (
            <div className='Modifiers_AddOn_Container'>
                <div className='Modifiers_AddOn_List_Container'>
                    <div className='Modifiers_AddOn_List_Header'>
                        <span className='Modifiers_AddOn_List_Header_Name'>Option name</span>
                        <span className='Modifiers_AddOn_List_Header_Add_Price'>Add to price</span>
                    </div>
                    <ul className='Modifiers_AddOn_List'>
                        {
                            modifiers['modifiers'][0]['addons'].map((addOn, index) => (
                                <li key={addOn + index}>
                                    <input type='text' className='Modifiers_Option_Name_Input' value= {addOn['name']} thistype="addons" thisattr="name" thisid={addOn["addonId"]} onChange={handleInputChange} />
                                    <input type='text' className='Modifiers_Option_Price_Input' value={addOn['price']} thistype="addons" thisattr="price" thisid={addOn["addonId"]} onChange={handleInputChange} />
                                    <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveAddOn(addOn)}>
                                        <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.5em' }}>
                                            <RiDeleteBinFill />
                                        </IconContext.Provider>
                                    </button>
                                </li>
                            ))
                        }
                        <li>
                            <button type='button' className='Modifiers_Add_Option_Button' onClick={handleAddAddOn}>
                                <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                    <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                </IconContext.Provider>
                                Add another option
                            </button>
                        </li>
                    </ul>
                </div>
            </div>
        )
    }

    return (
        <div className='Modifiers'>
            <div className='PageLayout_Header'>
                <div className='PageLayout_Header_Title'>Edit Item</div>
                <div className='PageLayout_Header_Button_Container'>
                    <FilterButton />
                </div>
            </div>
            <div className='Modifiers_Container'>
                <ButtonTabs guid={guid}/>
                <div className='Modifiers_Content_Border_Fix'>    
                    <div className='Modifiers_Content'>    
                        <div className='Modifiers_Item_Name_Container' id='item-name-row'>
                            <div className='Modifiers_Item_Name'>{modifiers === null ? "hey" : modifiers['name']}</div>
                        </div>
                        <div className='Modifiers_Grid'>
                            <div className='Modifiers_Base_Sale_Header'>
                                <div className='Modififers_Base_Sale_Container'>
                                    <span className='Modififers_Base_Sale_Label'>
                                        <b>Base Sales Price</b>
                                    </span>
                                    <span className='Modififers_Base_Sale_Value'>0.00</span>
                                </div>
                                <div className='Modifiers_Show_Checkbox_Container'>
                                    <input type='checkbox' className='Modifiers_Show_Checkbox' id='Modifiers_Show_Checkbox' />
                                    <label htmlFor='Modifiers_Show_Checkbox' className='Modifiers_Show_Checkbox_Label'>Show modifiers when I sell this item</label>
                                </div>
                            </div>
                            <div className='Modifiers_Groups_Header_Container'>
                                    <span className='Modifiers_Groups_Header_Label'>
                                        <b>Groups - Single Choice</b>
                                    </span>
                                <div className="Modifiers_Copy_Dropdown_Container">
                                    <button 
                                        className={`Modifiers_Groups_Copy_Import_Select_Dropdown_Selected ${(copyDropdown["type"] === "Groups") ? "Modifiers_Groups_Copy_Import_Select_Dropdown_Selected_Is_Open" : ""}`}
                                        onClick={() => (copyDropdown["type"] === "Groups") ? setCopyDropdown({ "type": "", "value": "Copy/Import from another item"}) : setCopyDropdown({ "type": "Groups", "value": "" })}>
                                        {(copyDropdown["type"] === "Groups") ? copyDropdown["value"] : "Copy/Import from another item"}
                                        <IconContext.Provider value={{ style: { verticalAlign: 'middle', height: '100%', float: 'right', color: 'rgb(139, 139, 139)' }, size: '1.25em' }}>
                                            { (copyDropdown["type"] === "Groups") ? <TiArrowSortedUp className='Modifiers_Groups_Copy_Import_Arrow' /> : <TiArrowSortedDown className='Modifiers_Groups_Copy_Import_Arrow' /> }
                                        </IconContext.Provider>
                                    </button>
                                    <div className={`Modifiers_Groups_Copy_Import_Dropdown_Container`} style={(copyDropdown["type"] === "Groups") ? {display: "block"} : {display: "none"}} onClick={e => e.stopPropagation()}>
                                        <div className={(copyDropdown["type"] === "Groups") ? 'Modifiers_Groups_Copy_Import_Select_Dropdown' : "Modifiers_Groups_Copy_Import_Select_Dropdown"}>
                                            <div className='Modifiers_Groups_Copy_Import_Select_Dropdown_Search_Wrapper'>
                                                <input type='text' className='Modifiers_Groups_Copy_Import_Select_Dropdown_Search' autoComplete="off" />
                                            </div>
                                            <div className='Modifiers_Groups_Copy_Import_Select_Dropdown_Scroll_Container'>
                                                <ul className='Modifiers_Groups_Copy_Import_Select_Dropdown_List'>
                                                    <li className='Modifiers_Groups_Copy_Import_Select_Dropdown_List_Item'>Chicago Style Hot Dog</li>
                                                    <li className='Modifiers_Groups_Copy_Import_Select_Dropdown_List_Item'>Maxwell Street Polish</li>
                                                    <li className='Modifiers_Groups_Copy_Import_Select_Dropdown_List_Item'>Chicken Tenders</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className='Modififers_Groups_Wrapper'>
                                {
                                    (modifiers === null || modifiers['modifiers'][0]['groups'].length === 0) ?
                                        <div className='Modifiers_Groups_Empty_Container'>
                                            <ul className='Modifiers_Groups_Empty_List'>
                                                <li className='Modifiers_Groups_Empty_List_Item'>Group modifiers allow you to select one option from a list</li>
                                                <li className='Modifiers_Groups_Empty_List_Item'>The base option is activated by default and included in the base price</li>
                                            </ul>
                                            <div className='Modifiers_Groups_Empty_Example_Container'>
                                                <div className='Modifiers_Groups_Empty_Example'>Example: Small, Medium, or Large</div>
                                                
                                                <button type='button' className='Modifiers_Add_Option_Button_First' onClick={handleAddGroup}>
                                                    <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                        <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                    </IconContext.Provider>
                                                    Add a group
                                                </button>
                                            </div>
                                        </div>
                                        :
                                        <div>
                                        {
                                            modifiers['modifiers'][0]['groups'].map((group) => {
                                                return (
                                                    <div key={`groupId${group['groupId']}`}>
                                                        <div className='Modifiers_Group_Container'>
                                                            <div className='Modifiers_Group_Name_Container'>
                                                                <span className='Modifiers_Group_Name_Label'>Group Name</span>
                                                                <input type='text' className='Modifiers_Group_Name_Input' value={group['name']} thistype="groups" thisid={group['groupId']} thisattr="name" onChange={handleInputChange} />
                                                                <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveGroup(group)}>
                                                                    <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.25em' }}>
                                                                        <RiDeleteBinFill />
                                                                    </IconContext.Provider>
                                                                    Delete
                                                                </button>
                                                            </div>
                                                            <div className='Modifiers_Group_List_Container'>
                                                                <div className='Modifiers_Group_List_Header'>
                                                                    <span className='Modifiers_Group_List_Header_Name'>Option name</span>
                                                                    <span className='Modifiers_Group_List_Header_Add_Price'>Add to price</span>
                                                                    <span className='Modifiers_Group_List_Header_Base'>Base</span>
                                                                </div>
                                                                <ul className='Modifiers_Group_List'>
                                                                {
                                                                    group['groupOptions'].map((option) => (
                                                                            <li key={`groupId-${group['groupId']}-groupOptionId-${option['groupOptionId']}`}>
                                                                                <input type='text' className='Modifiers_Option_Name_Input' value={option['name']} thistype="groupOptions" groupid={group['groupId']} thisattr="name" thisid={option['groupOptionId']} onChange={handleInputChange} />
                                                                                <input type='text' className='Modifiers_Option_Price_Input' value={option['price']} thistype="groupOptions" groupid={group['groupId']} thisattr="price" thisid={option['groupOptionId']} onChange={handleInputChange} />
                                                                                <button type='button' className='Modifiers_Delete_Button' groupoptionid={option['groupOptionId']} groupid={group['groupId']} onClick={handleRemoveGroupOption}>
                                                                                    <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.5em' }}>
                                                                                        <RiDeleteBinFill />
                                                                                    </IconContext.Provider>
                                                                            </button>
                                                                        </li>
                                                                        )
                                                                    )
                                                                }
                                                                    <li>
                                                                        <button type='button' className='Modifiers_Add_Option_Button' onClick={() => handleAddGroupOption(group)}>
                                                                            <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                                                <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                                            </IconContext.Provider>
                                                                            Add another option
                                                                        </button>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                )
                                            }
                                        )}
                                        <div className='Modifiers_Group_Footer'>
                                            <button type='button' className='Modifiers_Add_Option_Button' onClick={handleAddGroup}>
                                                <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                    <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                </IconContext.Provider>
                                                Add another group
                                            </button>
                                        </div>
                                    </div>
                                }
                            </div>
                            <div className='Modifiers_Groups_Header_Container'>
                                <span className='Modifiers_Groups_Header_Label'>
                                    <b>Options - Multiple Choice</b>
                                </span>
                                <div className="Modifiers_Copy_Dropdown_Container">
                                    <button 
                                        className={`Modifiers_Groups_Copy_Import_Select_Dropdown_Selected ${(copyDropdown["type"] === "Options") ? "Modifiers_Groups_Copy_Import_Select_Dropdown_Selected_Is_Open" : ""}`}
                                        onClick={() => (copyDropdown["type"] === "Options") ? setCopyDropdown({ "type": "", "value": "Copy/Import from another item"}) : setCopyDropdown({ "type": "Options", "value": "" })}>
                                        {(copyDropdown["type"] === "Options") ? copyDropdown["value"] : "Copy/Import from another item"}
                                        <IconContext.Provider value={{ style: { verticalAlign: 'middle', height: '100%', float: 'right', color: 'rgb(139, 139, 139)' }, size: '1.25em' }}>
                                            { (copyDropdown["type"] === "Options") ? <TiArrowSortedUp className='Modifiers_Groups_Copy_Import_Arrow' /> : <TiArrowSortedDown className='Modifiers_Groups_Copy_Import_Arrow' /> }
                                        </IconContext.Provider>
                                    </button>
                                    <div className={`Modifiers_Groups_Copy_Import_Dropdown_Container`} style={(copyDropdown["type"] === "Options") ? {display: "block"} : {display: "none"}} onClick={e => e.stopPropagation()}>
                                        <div className={(copyDropdown["type"] === "Options") ? 'Modifiers_Groups_Copy_Import_Select_Dropdown' : "Modifiers_Groups_Copy_Import_Select_Dropdown"}>
                                            <div className='Modifiers_Groups_Copy_Import_Select_Dropdown_Search_Wrapper'>
                                                <input type='text' className='Modifiers_Groups_Copy_Import_Select_Dropdown_Search' autoComplete="off" />
                                            </div>
                                            <div className='Modifiers_Groups_Copy_Import_Select_Dropdown_Scroll_Container'>
                                                <ul className='Modifiers_Groups_Copy_Import_Select_Dropdown_List'>
                                                    <li className='Modifiers_Groups_Copy_Import_Select_Dropdown_List_Item'>Chicago Style Hot Dog</li>
                                                    <li className='Modifiers_Groups_Copy_Import_Select_Dropdown_List_Item'>Maxwell Street Polish</li>
                                                    <li className='Modifiers_Groups_Copy_Import_Select_Dropdown_List_Item'>Chicken Tenders</li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className='Modififers_AddOns_Wrapper'>
                                <div className='Modifiers_AddOns_Header'><b>Add-ons</b></div>
                                {
                                    (modifiers === null || modifiers['modifiers'][0]['addons'].length === 0) ?
                                        <div className='Modifiers_Groups_Empty_Container'>
                                            <ul className='Modifiers_Groups_Empty_List'>
                                                <li className='Modifiers_Groups_Empty_List_Item'>Add-on Options will print on all receipts when activated</li>
                                                <li className='Modifiers_Groups_Empty_List_Item'>If activated, option price will be added to base price</li>
                                            </ul>
                                            <div className='Modifiers_Groups_Empty_Example_Container'>
                                                <div className='Modifiers_Groups_Empty_Example'>Example: Extra Cheese (add 0.50 to price)</div>
                                                
                                                <button type='button' className='Modifiers_Add_Option_Button_First' onClick={handleAddAddOn}>
                                                    <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                        <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                    </IconContext.Provider>
                                                    Add an add-on
                                                </button>
                                            </div>
                                        </div>
                                        :
                                        <div className='Modifiers_AddOn_Container'>
                                            <div className='Modifiers_AddOn_List_Container'>
                                                <div className='Modifiers_AddOn_List_Header'>
                                                    <span className='Modifiers_AddOn_List_Header_Name'>Option name</span>
                                                    <span className='Modifiers_AddOn_List_Header_Add_Price'>Add to price</span>
                                                </div>
                                                <ul className='Modifiers_AddOn_List'>
                                                    {
                                                        modifiers['modifiers'][0]['addons'].map(addOn => (
                                                            <li key={`addonId-${addOn['addonId']}`}>
                                                                <input type='text' className='Modifiers_Option_Name_Input' value={addOn['name']} thistype="addons" thisattr="name" thisid={addOn["addonId"]} onChange={handleInputChange} />
                                                                <input type='text' className='Modifiers_Option_Price_Input' value={addOn['price']} thistype="addons" thisattr="price" thisid={addOn["addonId"]} onChange={handleInputChange} />
                                                                <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveAddOn(addOn)}>
                                                                    <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.5em' }}>
                                                                        <RiDeleteBinFill />
                                                                    </IconContext.Provider>
                                                                </button>
                                                            </li>
                                                        ))
                                                    }
                                                    <li>
                                                        <button type='button' className='Modifiers_Add_Option_Button' onClick={handleAddAddOn}>
                                                            <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                                <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                            </IconContext.Provider>
                                                            Add another option
                                                        </button>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                }
                            </div>
                            <div className='Modififers_NoOptions_Wrapper'>
                                <div className='Modifiers_NoOptions_Header'><b>"NO" Options</b></div>
                                {
                                    (modifiers === null || modifiers['modifiers'][0]['noOptions'].length === 0) ?
                                    <div className='Modifiers_Groups_Empty_Container'>
                                        <ul className='Modifiers_Groups_Empty_List'>
                                            <li className='Modifiers_Groups_Empty_List_Item'>"NO" Options are activated by default and do not print on receipts</li>
                                            <li className='Modifiers_Groups_Empty_List_Item'>If pressed, they will deactivate and print "No option name" on all receipts</li>
                                        </ul>
                                        <div className='Modifiers_Groups_Empty_Example_Container'>
                                            <div className='Modifiers_Groups_Empty_Example'>Example: Cheeseburger, No bun</div>
                                            
                                            <button type='button' className='Modifiers_Add_Option_Button_First' onClick={handleAddNoOption}>
                                                <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                    <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                </IconContext.Provider>
                                                Add an option
                                            </button>
                                        </div>
                                    </div>
                                    :
                                    <div className='Modifiers_NoOptions_Container'>
                                        <div className='Modifiers_NoOptions_List_Container'>
                                            <div className='Modifiers_NoOptions_List_Header'>
                                                <span className='Modifiers_NoOptions_List_Header_Name'>Option name</span>
                                                <span className='Modifiers_NoOptions_List_Header_Add_Price'>Discount when OFF</span>
                                            </div>
                                            <ul className='Modifiers_NoOptions_List'>
                                                {
                                                    modifiers['modifiers'][0]['noOptions'].map(noOption => (
                                                        <li key={`noOption-${noOption['noOptionId']}`}>
                                                            <input type='text' className='Modifiers_Option_Name_Input' value={noOption['name']} thistype="noOptions" thisattr="name" thisid={noOption['noOptionId']} onChange={handleInputChange} />
                                                            <input type='text' className='Modifiers_Option_Price_Input' value={noOption['price']} thistype="noOptions" thisattr="price" thisid={noOption['noOptionId']} onChange={handleInputChange} />
                                                            <button type='button' className='Modifiers_Delete_Button' onClick={() => handleRemoveNoOption(noOption)}>
                                                                <IconContext.Provider value={{ style: { verticalAlign: 'top' },  size: '1.5em' }}>
                                                                    <RiDeleteBinFill />
                                                                </IconContext.Provider>
                                                            </button>
                                                        </li>
                                                    ))
                                                }
                                                <li>
                                                    <button type='button' className='Modifiers_Add_Option_Button' onClick={handleAddNoOption}>
                                                        <IconContext.Provider value={{ style: { verticalAlign: 'middle' },  size: '1.25em' }}>
                                                            <BsPlusSquareFill className='Modifiers_Button_Icon Modifiers_Button_Icon_Empty_Green' />
                                                        </IconContext.Provider>
                                                        Add another option
                                                    </button>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                }
                                <div className='Modifiers_Submit_Container'>
                                    <button type='button' className='Modifiers_Submit_Button'>Ok</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}