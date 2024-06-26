import React, { useState, useEffect } from 'react';
import AddNewItemDialog from '../../components/AddNewItem';
import FilterItemList from '../../components/FilterItemList';
import FilterButton from '../../components/FilterButton';
import { HiFilter } from 'react-icons/hi';
import { AiOutlineVerticalLeft, AiOutlineLeft, AiOutlineRight, AiOutlineVerticalRight} from 'react-icons/ai';
import SortButton from '../../components/SortButton';
import axios from 'axios';
import './css/itemlist.css';
import LoadingSpinner from '../../components/LoadingSpinner';

const ItemList = () => {
    const [addNewItemOpen, setAddNewItemOpen] = useState(false);
    const [addBasicItemOpen, setAddBasicItemOpen] = useState(false);
    const [dialogOpen, setDialogOpen] = useState(false);
    const [isFilterOpen, setIsFilterOpen] = useState(false);
    const [initialItems, setInitialItems] = useState([]);

    const [items, setItems] = useState(null);
    const [currentFilterDropdown, setCurrentFilterDropdown] = useState(null);
    
    const [sortType, setSortType] = useState({ 'Value': '', 'Type': 'None' });

    const [itemsPerPage, setItemsPerPage] = useState({ 'Per Page': 50, 'Current Page': 1 });

    const [isLoading, setIsLoading] = useState(true);

    const getItems = async () => {
        await axios.get('https://localhost:7074/api/items')
        .then(res => {
            let items = res.data;
            items.forEach(item => {
                item.markup = (item.assignedCost === 0) ? 0 : (item['price'] / item['assignedCost']) * 100;
                item.margin = (item.price === 0) ? 0 : (item['price'] - item['assignedCost']) / item['price'] * 100;
            })
            console.log(items)
            setInitialItems(items);
            setItems(items);
            setIsLoading(false);
        })
        .catch(function (err) {
            console.log(err.message);
        });
    }
    useEffect(() => {
        console.log('Requesting items');
        getItems();
    }, []);
    useEffect(() => {
        if (sortType['Type'] === 'Ascending') {
            Array.prototype.clone = function() {
                return this.slice(0);
            }
            var tmp = items.clone();
            const sorted = tmp.sort((a, b) => {
                if (sortType['Value'] !== 'Price' || sortType['Value'] !== 'Cost' || sortType['Value'] !== 'Quantity') return a[sortType['Value']] - b[sortType['Value']];
                else if (sortType['Value'] === 'Margin') {
                    let itemA = (a['price'] - a['cost']) / a['price'], itemB = (b['price'] - b['cost']) / b['price'];
                    return itemA - itemB;
                } else if (sortType['Value'] === 'Markup') {
                    let itemA = (Number(a['price']) / Number(a['cost'])), itemB = (Number(b['price']) / Number(b['cost']));
                    return itemA - itemB;
                } else if (sortType['Value'] === 'Item') {
                    let itemA = a['name'].toLowerCase(), itemB = b['name'].toLowerCase();
                    if (itemA > itemB) {
                        return -1;
                    }
                    if (itemA < itemB) {
                        return 1;
                    }
                    return 0;
                }
            });
            setItems(sorted);
        } else if (sortType['Type'] === 'Descending') {
            Array.prototype.clone = function() {
                return this.slice(0);
            }
            var tmp = items.clone();
            const sorted = tmp.sort((a, b) => {
                if (sortType['Value'] === 'Price' || sortType['Value'] === 'Cost' || sortType['Value'] === 'Quantity') return b[sortType['Value']] - a[sortType['Value']];
                else if (sortType['Value'] === 'Margin') {
                    let itemA = ((Number(a['price']) - Number(a['cost'])) / Number(a['price'])), itemB = ((Number(b['price']) - Number(b['cost'])) / Number(b['price']));
                    return itemB - itemA;
                } else if (sortType['Value'] === 'Markup') {
                    let itemA = (Number(a['price']) / Number(a['cost'])), itemB = (Number(b['price']) / Number(b['cost']));
                    return itemB - itemA;
                } else if (sortType['Value'] === 'Item') {
                    let itemA = a['name'].toLowerCase(), itemB = b['name'].toLowerCase();
                    if (itemA < itemB) {
                        return -1;
                    }
                    if (itemA > itemB) {
                        return 1;
                    }
                    return 0;
                }
            });
            setItems(sorted);
        } else {
            setItems(initialItems);
        }
    }, [sortType]);



    const [showHideValue, setShowHideValue] = useState(['Item', 'Margin', 'Markup', 'Quantity', 'Total Value']);
    const [priceValue, setPriceValue] = useState({ 'Value': '', 'Type': 'Equals' });
    const [costValue, setCostValue] = useState({ 'Value': '', 'Type': 'Equals' });
    const [itemValue, setItemValue] = useState({ 'Value': '', 'Type': '' });
    const [marginValue, setMarginValue] = useState({ 'Value': '', 'Type': 'Equals' });
    const [markupValue, setMarkupValue] = useState({ 'Value': '', 'Type': 'Equals' });
    const [quantityValue, setQuantityValue] = useState({ 'Value': '', 'Type': 'Equals' });

    const handleShowHideClick = (type) => {
        if (showHideValue.includes(type)) {
            setShowHideValue(prev => prev.filter(val => val !== type));
        } else {
            setShowHideValue(prev => [...prev, type]);
        }
    }
    const handlePriceValueChange = event => {
        event.preventDefault();
        const x = event.target.value;
        if (event.target.value.length === priceValue.length - 1) {
            setPriceValue(prev => ({ ...prev, 'Value': event.target.value}))
        } else if ((!isNaN(event.target.value.charAt(event.target.value.length - 1)) 
                || (event.target.value.charAt(event.target.value.length - 1) === "." && event.target.value.substring(0, event.target.value.length - 1).indexOf('.') < 0) 
                || (event.target.value.charAt(event.target.value.length - 1) === "-" && event.target.value.length === 1)) 
                    && event.target.value.charAt(event.target.value.length - 1) !== " ")
            setPriceValue(prev => ({ ...prev, 'Value': event.target.value}));

            // WOW!
    }
    const handleCostValueChange = event => {
        event.preventDefault();
        if (event.target.value.length === costValue.length - 1) {
            setCostValue(prev => ({ ...prev, 'Value': event.target.value}))
        } else if ((!isNaN(event.target.value.charAt(event.target.value.length - 1)) 
                || (event.target.value.charAt(event.target.value.length - 1) === "." && event.target.value.substring(0, event.target.value.length - 1).indexOf('.') < 0) 
                || (event.target.value.charAt(event.target.value.length - 1) === "-" && event.target.value.length === 1)) 
                    && event.target.value.charAt(event.target.value.length - 1) !== " ")
            setCostValue(prev => ({ ...prev, 'Value': event.target.value}));
    }
    const handleItemInput = event => {
        event.preventDefault();
        setItemValue(event.target.value);
    }
    const handleMarginValueChange = event => {
        event.preventDefault();
        if (isNaN(event.target.value.charAt(event.target.value.length - 1))) return;
        if (event.target.value.length === marginValue.length - 1) {
            setMarginValue(prev => ({ ...prev, 'Value': event.target.value}))
        } else if ((!isNaN(event.target.value.charAt(event.target.value.length - 1)) 
                || (event.target.value.charAt(event.target.value.length - 1) === "." && event.target.value.substring(0, event.target.value.length - 1).indexOf('.') < 0) 
                || (event.target.value.charAt(event.target.value.length - 1) === "-" && event.target.value.length === 1)) 
                    && event.target.value.charAt(event.target.value.length - 1) !== " ")
                    setMarginValue(prev => ({ ...prev, 'Value': event.target.value}));
    }
    const handleMarkupValueChange = event => {
        event.preventDefault();
        if (event.target.value.length === markupValue.length - 1) {
            setMarkupValue(prev => ({ ...prev, 'Value': event.target.value}))
        } else if ((!isNaN(event.target.value.charAt(event.target.value.length - 1)) 
                || (event.target.value.charAt(event.target.value.length - 1) === "." && event.target.value.substring(0, event.target.value.length - 1).indexOf('.') < 0) 
                || (event.target.value.charAt(event.target.value.length - 1) === "-" && event.target.value.length === 1)) 
                    && event.target.value.charAt(event.target.value.length - 1) !== " ")
            setMarkupValue(prev => ({ ...prev, 'Value': event.target.value}));
    }
    const handleQuantityValueChange = event => {
        event.preventDefault();
        if (event.target.value.length === quantityValue.length - 1) {
            setQuantityValue(prev => ({ ...prev, 'Value': event.target.value}))
        } else if ((!isNaN(event.target.value.charAt(event.target.value.length - 1)) 
                || (event.target.value.charAt(event.target.value.length - 1) === "." && event.target.value.substring(0, event.target.value.length - 1).indexOf('.') < 0) 
                || (event.target.value.charAt(event.target.value.length - 1) === "-" && event.target.value.length === 1)) 
                    && event.target.value.charAt(event.target.value.length - 1) !== " ")
            setQuantityValue(prev => ({ ...prev, 'Value': event.target.value}));
    }

    const isItemFiltered = (item) => {
        if (itemValue.length > 0) {
            if (item['name'].toLowerCase().indexOf(itemValue.toLowerCase()) < 0) {
                return false
            }
        }
        if (priceValue['Value'] !== '') {
            switch (priceValue['Type']) {
                case 'Equals':
                    if (item['price'] !== Number(priceValue['Value'])) return false;
                    break;
                case 'Greater_Equal':
                    if (item['price'] < Number(priceValue['Value'])) return false;
                    break;
                case 'Greater':
                    if (item['price'] <= Number(priceValue['Value'])) return false;
                    break;
                case 'Less_Equal':
                    if (item['price'] > Number(priceValue['Value'])) return false;
                    break;
                case 'Less':
                    if (item['price'] >= Number(priceValue['Value'])) return false;
                    break;
                default:
                    console.log('uh oh oreos');
                    
            }
        }
        if (costValue['Value'] !== '') {
            switch (costValue['Type']) {
                case 'Equals':
                    if (item['cost'] !== Number(costValue['Value'])) return false;
                    break;
                case 'Greater_Equal':
                    if (item['cost'] < Number(costValue['Value'])) return false;
                    break;
                case 'Greater':
                    if (item['cost'] <= Number(costValue['Value'])) return false;
                    break;
                case 'Less_Equal':
                    if (item['cost'] > Number(costValue['Value'])) return false;
                    break;
                case 'Less':
                    if (item['cost'] >= Number(costValue['Value'])) return false;
                    break;
                default:
                    console.log('uh oh oreos Cost');
                    
            }
        }
        if (marginValue['Value'] !== '') {
            switch (marginValue['Type']) {
                case 'Equals':
                    if (item['margin'] !== Number(marginValue['Value'])) return false;
                    break;
                case 'Greater_Equal':
                    if (item['margin'] < Number(marginValue['Value'])) return false;
                    break;
                case 'Greater':
                    if (item['margin'] <= Number(marginValue['Value'])) return false;
                    break;
                case 'Less_Equal':
                    if (item['margin'] > Number(marginValue['Value'])) return false;
                    break;
                case 'Less':
                    if (item['margin'] >= Number(marginValue['Value'])) return false;
                    break;
                default:
                    console.log('uh oh oreos Margin');
                    
            }
        }
        if (markupValue['Value'] !== '') {
            switch (markupValue['Type']) {
                case 'Equals':
                    if (item['markup'] !== Number(markupValue['Value'])) return false;
                    break;
                case 'Greater_Equal':
                    if (item['markup'] < Number(markupValue['Value'])) return false;
                    break;
                case 'Greater':
                    if (item['markup'] <= Number(markupValue['Value'])) return false;
                    break;
                case 'Less_Equal':
                    if (item['markup'] > Number(markupValue['Value'])) return false;
                    break;
                case 'Less':
                    if (item['markup'] >= Number(markupValue['Value'])) return false;
                    break;
                default:
                    console.log('uh oh oreos Markup');
                    
            }
        }
        if (quantityValue['Value'] !== '') {
            switch (quantityValue['Type']) {
                case 'Equals':
                    if (item['quantity'] !== Number(quantityValue['Value'])) return false;
                    break;
                case 'Greater_Equal':
                    if (item['quantity'] < Number(quantityValue['Value'])) return false;
                    break;
                case 'Greater':
                    if (item['quantity'] <= Number(quantityValue['Value'])) return false;
                    break;
                case 'Less_Equal':
                    if (item['quantity'] > Number(quantityValue['Value'])) return false;
                    break;
                case 'Less':
                    if (item['quantity'] >= Number(quantityValue['Value'])) return false;
                    break;
                default:
                    console.log('uh oh oreos Quantity');
            }
        }
        return true;
    }

    const handleFilterSlideInClick = event => {
        event.preventDefault();
        setCurrentFilterDropdown('');
        setIsFilterOpen(prev => !prev);
    }

    const handleHeaderFilterDropdownClick = (type) => {
        if (currentFilterDropdown === type) setCurrentFilterDropdown('');
        else setCurrentFilterDropdown(type);
    }

    const DataRows = () => {
        if (isLoading) return <div style={{width: "100%", height: "100%", display: "flex", justifyContent: "center", alignItems: "center" }}><LoadingSpinner /></div>
        return (
            <div className='ItemList_Table_Content_Container'>
                {   
                    items.map(item => {
                        if (isItemFiltered(item))
                            return (
                                <div key={item['itemId']} className='ItemList_Table_Content_Row'>
                                    <div className='ItemList_Table_Content_Body_Cell' style={showHideValue.includes('Price') ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'>{item['price'].toFixed(2)}</span>
                                        </div>
                                    </div>
                                    <div className='ItemList_Table_Content_Body_Cell' style={(showHideValue.includes('Cost')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'>{item['cost'].toFixed(2)}</span>
                                        </div>
                                    </div>
                                    <div className='ItemList_Table_Content_Body_Cell_Item_Col' style={(showHideValue.includes('Item')) ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <a href={`/salerno/items/${item['itemId']}/edit`} className='ItemList_ItemName_Button'>{item['name']}</a>
                                        </div>
                                    </div>
                                    {/* <div className='ItemList_Table_Content_Body_Cell' style={{display: 'block', width: `calc(100% / ${showHideValue.length + 1}`}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'>{(item.isEnabled) ? "Active" : "Inactive"}</span>
                                        </div>
                                    </div> */}
                                    <div className='ItemList_Table_Content_Body_Cell' style={(showHideValue.includes('Margin')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'>{item.margin.toFixed(2)}%</span>
                                        </div>
                                    </div>
                                    <div className='ItemList_Table_Content_Body_Cell' style={(showHideValue.includes('Markup')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'>{item.markup.toFixed(2)}%</span>
                                        </div>
                                    </div>
                                    <div className='ItemList_Table_Content_Body_Cell' style={(showHideValue.includes('Quantity')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'>{item['quantity']}</span>
                                        </div>
                                    </div>
                                    <div className='ItemList_Table_Content_Body_Cell' style={(showHideValue.includes('Total Value')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                        <div className='ItemList_Table_Body_Data_Container'>
                                            <span className='ItemList_ItemDetails'></span>
                                        </div>
                                    </div>
                                </div>
                            )
                    })
                }
            </div>
        )
    }

    return (
        <div className='ItemList'>
            <div className='PageLayout_Header'>
                <div className='PageLayout_Header_Title'>Item List</div>
                <div className='PageLayout_Header_Button_Container'>
                    <button className='ItemList_Header_AddNewItem_Button' onClick={() => setDialogOpen(true)}>
                        <div className='ItemList_Header_AddNewItem_Text'>Add New</div>
                    </button>
                    <FilterButton handleClick={handleFilterSlideInClick} />
                </div>
            </div>
            { dialogOpen && <AddNewItemDialog
                                setAddNewItemOpen={setAddNewItemOpen}
                                addNewItemOpen={addNewItemOpen}
                                addBasicItemOpen={addBasicItemOpen}
                                setAddBasicItemOpen={setAddBasicItemOpen}
                                setDialogOpen={setDialogOpen}
                            />
            }
                <div className='ItemList_Container'>
                    <FilterItemList isFilterOpen={isFilterOpen} 
                        showHideValue={showHideValue} handleShowHideClick={handleShowHideClick} 
                        priceValue={priceValue} handlePriceValueChange={handlePriceValueChange} 
                        costValue={costValue} handleCostValueChange={handleCostValueChange} 
                        itemValue={itemValue} handleItemInput={handleItemInput} 
                        marginValue={marginValue} handleMarginValueChange={handleMarginValueChange} 
                        markupValue={markupValue} handleMarkupValueChange={handleMarkupValueChange} 
                        quantityValue={quantityValue} handleQuantityValueChange={handleQuantityValueChange} 
                        setPriceValue={setPriceValue} setCostValue={setCostValue} setItemValue={setItemValue}
                        setMarginValue={setMarginValue} setMarkupValue={setMarkupValue} setQuantityValue={setQuantityValue}
                        />
                        <div className='ItemList_Scrollable_List'>
                            <div className='ItemList_Header_Container'>
                                <div className='ItemList_Header_Control_Buttons'>
                                    <button type='button' className='ItemList_Header_Button ItemList_Header_Button_BulkAction_Dropdown'>
                                        Bulk action 
                                    </button>
                                    <button type='button' disabled className='ItemList_Header_Button_Disabled ItemList_Header_Button ItemList_Header_Button_ExpandAll'>
                                        Expand all
                                    </button>
                                    <button type='button' disabled className='ItemList_Header_Button_Disabled ItemList_Header_Button ItemList_Header_Button_CollapseAll'>
                                        Collapse all
                                    </button>
                                    <button type='button' className='ItemList_Header_Button ItemList_Header_Button_Export_CSV'>
                                        Export to CSV
                                    </button>
                                    <button type='button' className='ItemList_Header_Button ItemList_Header_Button_Export_Excel'>
                                        Export to Excel
                                    </button>
                                </div>
                            </div>
                    
                    <div className='ItemList_Table_Container_Scroll'>
                            <div className='ItemList_Table'>
                                <div className='ItemList_Table_Header_Col' style={(showHideValue.includes('Price')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'>
                                                <span>Price</span>
                                            </div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                <SortButton sortType={sortType} setSortType={setSortType} value='Price' />
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' value={priceValue['Value']} onChange={handlePriceValueChange} className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button' onClick={() => handleHeaderFilterDropdownClick('Price')}>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' />
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='ItemList_Header_Filter_Dropdown_Wrapper' style={(currentFilterDropdown === 'Price') ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Open_Wrapper'>
                                            <div className='ItemList_Select_Wrapper'>
                                                <select className='ItemList_Filter_Select' value={priceValue['Type']} onChange={(event) => setPriceValue(prev => ({...prev, 'Type': event.target.value}))}>
                                                    <option className='ItemList_Filter_Select_Option' value='Equals'>Equals</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater'>Greater than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater_Equal'>Greater than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less'>Less than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less_Equal'>Less than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Range'>In range</option>
                                                </select>
                                            </div>
                                            <div className='ItemList_Input_Search_Wrapper'>
                                                <input type='text' className='ItemList_Input_Search' value={priceValue['Value']} onChange={handlePriceValueChange} />
                                            </div>
                                            <div className='ItemList_Clear_Wrapper'>
                                                <button type='button' className='ItemList_Clear_Button' onClick={() => setPriceValue({'Value': '', 'Type': 'Equals'})}>Clear filter</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className={(showHideValue.includes('Cost')) ? 'ItemList_Table_Header_Col' : 'ItemList_Table_Header_Col hidden'} style={(showHideValue.includes('Cost')) ? {width: `calc(100% / ${showHideValue.length + 1}`} : {}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'><span>Cost</span></div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                <SortButton sortType={sortType} setSortType={setSortType} value='Cost' />
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' value={costValue['Value']} onChange={handleCostValueChange} className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button'>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' onClick={() => handleHeaderFilterDropdownClick('Cost')}/>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='ItemList_Header_Filter_Dropdown_Wrapper' style={(currentFilterDropdown === 'Cost') ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Open_Wrapper'>
                                            <div className='ItemList_Select_Wrapper'>
                                                <select className='ItemList_Filter_Select' value={costValue['Type']} onChange={(event) => setCostValue(prev => ({...prev, 'Type': event.target.value}))}>
                                                    <option className='ItemList_Filter_Select_Option' value='Equals'>Equals</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater'>Greater than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater_Equal'>Greater than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less'>Less than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less_Equal'>Less than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Range'>In range</option>
                                                </select>
                                            </div>
                                            <div className='ItemList_Input_Search_Wrapper'>
                                                <input type='text' className='ItemList_Input_Search' value={costValue['Value']} onChange={handleCostValueChange} />
                                            </div>
                                            <div className='ItemList_Clear_Wrapper'>
                                                <button type='button' className='ItemList_Clear_Button' onClick={() => setCostValue({'Value': '', 'Type': 'Equals'})}>Clear filter</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className='ItemList_Table_Header_Item_Col' style={(showHideValue.includes('Item')) ? {display: 'block'} : {display: 'none'}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Item'><span>Item</span></div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                <SortButton sortType={sortType} setSortType={setSortType} value='Item' />
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' value={itemValue['Value']} onChange={handleItemInput} className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button'>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' onClick={() => handleHeaderFilterDropdownClick('Item')}/>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='ItemList_Header_Filter_Dropdown_Wrapper' style={(currentFilterDropdown === 'Item') ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Open_Wrapper'>
                                            <div className='ItemList_Input_Search_Wrapper'>
                                                <input type='text' className='ItemList_Input_Search' value={itemValue['Value']} onChange={handleItemInput} />
                                            </div>
                                            <div className='ItemList_Clear_Wrapper'>
                                                <button type='button' className='ItemList_Clear_Button' onClick={() => setItemValue({'Value': '', 'Type': 'Equals'})}>Clear filter</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                {/* <div className='ItemList_Table_Header_Col' style={{display: 'block', width: `calc(100% / ${showHideValue.length + 1}`}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'><span>Active</span></div>
                                        </div>
                                    </div>
                                </div> */}
                                <div className='ItemList_Table_Header_Col' style={(showHideValue.includes('Margin')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'><span>Margin</span></div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                <SortButton sortType={sortType} setSortType={setSortType} value='Margin' />
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' value={marginValue['Value']} onChange={handleMarginValueChange} className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button'>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' onClick={() => handleHeaderFilterDropdownClick('Margin')}/>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='ItemList_Header_Filter_Dropdown_Wrapper' style={(currentFilterDropdown === 'Margin') ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Open_Wrapper'>
                                            <div className='ItemList_Select_Wrapper'>
                                                <select className='ItemList_Filter_Select' value={marginValue['Type']} onChange={(event) => setMarginValue(prev => ({...prev, 'Type': event.target.value}))}>
                                                    <option className='ItemList_Filter_Select_Option' value='Equals'>Equals</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater'>Greater than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater_Equal'>Greater than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less'>Less than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less_Equal'>Less than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Range'>In range</option>
                                                </select>
                                            </div>
                                            <div className='ItemList_Input_Search_Wrapper'>
                                                <input type='text' className='ItemList_Input_Search' value={marginValue['Value']} onChange={handleMarginValueChange} />
                                            </div>
                                            <div className='ItemList_Clear_Wrapper'>
                                                <button type='button' className='ItemList_Clear_Button' onClick={() => setMarginValue({'Value': '', 'Type': 'Equals'})}>Clear filter</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className='ItemList_Table_Header_Col' style={(showHideValue.includes('Markup')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'><span>Markup</span></div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                <SortButton sortType={sortType} setSortType={setSortType} value='Markup' />
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' value={markupValue['Value']} onChange={handleMarkupValueChange} className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button'>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' onClick={() => handleHeaderFilterDropdownClick('Markup')}/>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='ItemList_Header_Filter_Dropdown_Wrapper' style={(currentFilterDropdown === 'Markup') ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Open_Wrapper'>
                                            <div className='ItemList_Select_Wrapper'>
                                                <select className='ItemList_Filter_Select' value={markupValue['Type']} onChange={(event) => setMarkupValue(prev => ({...prev, 'Type': event.target.value}))}>
                                                    <option className='ItemList_Filter_Select_Option' value='Equals'>Equals</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater'>Greater than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater_Equal'>Greater than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less'>Less than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less_Equal'>Less than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Range'>In range</option>
                                                </select>
                                            </div>
                                            <div className='ItemList_Input_Search_Wrapper'>
                                                <input type='text' className='ItemList_Input_Search' value={markupValue['Value']} onChange={handleMarkupValueChange} />
                                            </div>
                                            <div className='ItemList_Clear_Wrapper'>
                                                <button type='button' className='ItemList_Clear_Button' onClick={() => setMarkupValue({'Value': '', 'Type': 'Equals'})}>Clear filter</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className='ItemList_Table_Header_Col' style={(showHideValue.includes('Quantity')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'><span>Quantity</span></div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                <SortButton sortType={sortType} setSortType={setSortType} value='Quantity' />
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' value={quantityValue['Value']} onChange={handleQuantityValueChange} className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button'>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' onClick={() => handleHeaderFilterDropdownClick('Quantity')}/>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div className='ItemList_Header_Filter_Dropdown_Wrapper' style={(currentFilterDropdown === 'Quantity') ? {display: 'block'} : {display: 'none'}}>
                                        <div className='ItemList_Open_Wrapper'>
                                            <div className='ItemList_Select_Wrapper'>
                                                <select className='ItemList_Filter_Select' value={quantityValue['Type']} onChange={(event) => setQuantityValue(prev => ({...prev, 'Type': event.target.value}))}>
                                                    <option className='ItemList_Filter_Select_Option' value='Equals'>Equals</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater'>Greater than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Greater_Equal'>Greater than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less'>Less than</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Less_Equal'>Less than or equal to</option>
                                                    <option className='ItemList_Filter_Select_Option' value='Range'>In range</option>
                                                </select>
                                            </div>
                                            <div className='ItemList_Input_Search_Wrapper'>
                                                <input type='text' className='ItemList_Input_Search' value={quantityValue['Value']} onChange={handleQuantityValueChange} />
                                            </div>
                                            <div className='ItemList_Clear_Wrapper'>
                                                <button type='button' className='ItemList_Clear_Button' onClick={() => setQuantityValue({'Value': '', 'Type': 'Equals'})}>Clear filter</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className='ItemList_Table_Header_Col' style={(showHideValue.includes('Total Value')) ? {display: 'block', width: `calc(100% / ${showHideValue.length + 1}`} : {display: 'none'}}>
                                    <div className='ItemList_Table_Header_Wrapper'>
                                        <div className='ItemList_Table_Header_Grid'>
                                            <div className='ItemList_Table_Header_Title_Not_Item'><span>Total Value</span></div>
                                            <div className='ItemList_Table_Header_Sort_Icon_Wrapper'>
                                                .
                                            </div>
                                            <div className='ItemList_Table_Header_Item_Wrapper'>
                                                <div className='ItemList_Table_Header_Item_Bottom'>
                                                    <div className='ItemList_Table_Header_Input_Wrapper'>
                                                        <input type='text' disabled className='ItemList_Table_Header_Input' />
                                                    </div>
                                                    <div className='ItemList_Table_Header_Filter_Icon_Wrapper'>
                                                        <button className='ItemList_Table_Header_Search_Filter_Button'>
                                                            <HiFilter className='ItemList_Filter_Icon' size='1.1em' onClick={() => handleHeaderFilterDropdownClick('Total Value')}/>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className='xtest'>
                                <DataRows />
                            </div>
                        </div>
                    </div>
                    <div className='ItemList_Table_PageOptions'>
                        <div className='ItemList_Table_PageOptions_Container'>
                            <span className='ItemList_Table_PageOptions_Title'>Page size</span>
                            <button type='button' className='ItemList_Table_PageOptions_Button'>50</button>
                            <button type='button' className='ItemList_Table_PageOptions_Button'>100</button>
                            <button type='button' className='ItemList_Table_PageOptions_Button'>150</button>
                            <button type='button' className='ItemList_Table_PageOptions_Button ItemList_Table_PageOptions_Button--selected'>All</button>
                            <span className='ItemList_PageOptions_Page_Buttons_Container'>
                                <span className='ItemList_Table_PageOptions_Summary'>1 to 83 of 83</span>
                                <button type='button' className='ItemList_PageOptions_Page_Button_Disabled'>
                                    <AiOutlineVerticalRight size='0.8em'/>
                                </button>
                                <button type='button' className='ItemList_PageOptions_Page_Button_Disabled'>
                                    <AiOutlineLeft size='0.8em'/>
                                </button>
                                <span className='ItemList_PageOptions_CurrentPage'>
                                    Page <span style={{margin: '0 7px'}}>1</span> of <span style={{margin: '0 7px 0 0'}}>2</span>
                                </span>
                                <button type='button' className='ItemList_PageOptions_Page_Button'>
                                    <AiOutlineRight size='0.8em'/>
                                </button>
                                <button type='button' className='ItemList_PageOptions_Page_Button'>
                                    <AiOutlineVerticalLeft size='0.8em' />
                                </button>
                            </span>
                        </div>
                    </div>
                </div>
        </div>
    )
}

export default ItemList;