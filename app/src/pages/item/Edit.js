import React, { useState, useEffect } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import FilterButton from '../../components/FilterButton';
import QuestionIcon from "../../components/QuestionIcon";
import ButtonTabs from "../../components/ButtonTabs";
import DeleteItemModal from "../../components/DeleteItemModal";
import axios from 'axios';
import './css/edit.css';
import EditStyles from './css/Edit.module.css';

const Edit = () => {
    const { itemId } = useParams();
    const location = useLocation();

    const [createNewCategory, setCreateNewCategory] = useState(false);
    const [newCategory, setNewCategory] = useState("");

    const [item, setItem] = useState({
        "itemId": "",
        "name": "",
        "description": "",
        "department": "",
        "categoryId": 0,
        "upc": "",
        "sku": "",
        "price": 0,
        "discountable": false,
        "taxable": false,
        "trackingInventory": false,
        "cost": 0,
        "assignedCost": 0,
        "quantity": 0,
        "reorderTrigger": 0,
        "recommendedOrder": 0,
        "supplier": "",
        "liabilityItem": false,
        "liabilityRedemptionTender": "",
        "taxGroupOrRate": "0",
        "isEnabled": true,
        "categoryName": ""
    });
    const [categories, setCategories] = useState([]);

    const [dialogOpen, setDialogOpen] = useState(false);
    const navigate = useNavigate();

    const getItem = async () => {
        await axios.get(`https://localhost:7074/api/items/${itemId}`)
        .then(res => {
            console.log(res.data);
            setItem(res.data);
        })
        .catch(function (err) {
            console.log(err.message);
        });
    }
    const getCategories = async () => {
        await axios.get(`https://localhost:7074/api/category/simple`)
        .then(res => {
            if (location.pathname === "/salerno/items/new")
                item.categoryId = res.data[0].categoryId;
            setCategories(res.data);
        })
        .catch(function (err) {
            console.log(err.message);
        });
    }
    useEffect(() => {
        if (itemId !== undefined && location.pathname !== "/salerno/items/new") getItem();
        getCategories();
    }, []);

    const handleSave = async event => {
        if (itemId === undefined) {
            await axios.post("https://localhost:7074/api/items", item)
            .then(res => { console.log(res); navigate("/salerno/items"); })
            .catch(err => console.log(err));
        }
        else {
            let fixedItem = addCategoryObjectToState();
            console.log("putting")
            console.log(JSON.stringify(fixedItem));
            await axios.put(`https://localhost:7074/api/items/${item.itemId}`, fixedItem)
            .then(res => {
                console.log(res);
                navigate("/salerno/items");
            })
            .catch(err => {
                console.log(err);
            });
        }
    }

    function addCategoryObjectToState() {
        console.log("Adding category object")
        let tempItem = Object.assign({}, item);
        let category = {
            "categoryId": item["categoryId"],
            "name": ""
        };
        if (item["categoryId"] !== 0) {
            console.log(item["categoryId"])
            console.log(categories)
            // num to string here...
            category["name"] = categories.find(c => c["categoryId"] == item["categoryId"])["name"];
            
        } else {
            category["name"] = item["categoryName"];
        }
        tempItem["category"] = category;
        return tempItem;
    }

    const handleInputChange = event => {
        // TODO: Validate input number vs string.
        if (event.target.value === "_newCategory") {
            setCreateNewCategory(true);
        } else {
            let value = event.target.value;
            let attributeType = event.target.attributes.attributeType.value;
            let tempItem = Object.assign({}, item);
            console.log(event.target.value)
            if (attributeType === "isEnabled") tempItem.isEnabled = !item.isEnabled;
            else tempItem[attributeType] = value;
            setItem(tempItem);
        }
    }

    if ((itemId === undefined || itemId === "undefined") && location.pathname !== "/salerno/items/new") return navigate("/salerno/items");

    const CategoryDiv = () => {
        if (!createNewCategory) {
            return (
                <div>
                    <select className={EditStyles.input} value={item.categoryId} attributeType="categoryId" onChange={handleInputChange} id='category-input'>
                    <option value="_newCategory">Add a new Category.</option>
                    {
                        categories.map(category => (
                            <option key={`category-${category.categoryId}`} value={category.categoryId}>{category.name}</option>
                        ))
                    }
                    </select>
                </div>
            )
        } else {
            return (
                <div>
                    <input type='text' className={EditStyles.input} value={newCategory} onChange={e => setNewCategory(e.target.value)} />
                    <button type="button">Save</button>
                    <button type="button" onClick={() => setCreateNewCategory(false)}>Cancel</button>
                </div>
            )
        }
    }
    return (
        <main className={EditStyles.edit}>
            <div className={EditStyles.header}>
                <span className={EditStyles.title}>Edit Item</span>
                <div className={EditStyles.header_buttons}>
                    <FilterButton />
                </div>
            </div>
            <DeleteItemModal setDialogOpen={setDialogOpen} itemId={itemId} dialogOpen={dialogOpen} />
            <section className={EditStyles.container}>
                <ButtonTabs itemId={itemId} />
                <div className={EditStyles.grid}>
                    <div>
                            <h3>Item Details</h3>
                        <div className={EditStyles.inputs_container}>
                            <div>
                                <label htmlFor='name-input' className={EditStyles.required_label}>Name</label>
                                <input className={EditStyles.input} type='text' value={item['name']} attributeType="name" onChange={handleInputChange} id='name-input'/>
                            </div>
                            <div style={{display: "flex", flexDirection: "column", alignItems: "flex-start", justifyContent: "flex-start"}}>
                                <label htmlFor='status'>Item Status <QuestionIcon className={EditStyles.question_icon}/></label>
                                <div className={EditStyles.status_wrapper}>
                                    <input type='checkbox' checked={item.isEnabled} onChange={handleInputChange} attributeType="isEnabled" id='status'/>
                                    <label htmlFor='status' className={EditStyles.register_label}>Active</label>
                                </div>
                            </div>
                            <div>
                                <label htmlFor='department-input'>Department <QuestionIcon /></label>
                                <select className={EditStyles.input} value={item["department"]} attributeType="department" onChange={handleInputChange}  id='department-input'>
                                    <option value={item.department}>{item['department']}</option>
                                    <option value='2'>2</option>
                                    <option value='3'>3</option>
                                </select>
                            </div>
                            <div>
                                <label>SKU<QuestionIcon /></label>
                                <div style={{textAlign: 'left', fontSize: '14px', fontWeight: '300', height: '39px', padding: '6px 0'}}>{item['sku']}</div>
                            </div>
                            <div>
                                <label htmlFor='category-input'>Category <QuestionIcon /></label>
                                <CategoryDiv />
                            </div>
                            <div>
                                <label htmlFor='upc-input'>UPC <QuestionIcon /></label>
                                <input type='text' className={EditStyles.input} value={item['upc']} attributeType="upc" onChange={handleInputChange} id='upc-input'/>
                            </div>
                            <div>
                                <label htmlFor='description-input'>Description</label>
                                <textarea id="description-input" value={item.description} attributeType="description" onChange={handleInputChange} placeholder="Describe the item for customers..."/>
                            </div>
                        </div>
                    </div>
                    <div>
                        <h3>Pricing</h3>
                        <div className={EditStyles.inputs_container}>
                            <div>
                                <label htmlFor='sale-price-input' className={EditStyles.required_label}>Sales Price</label>
                                <input type='text' className={EditStyles.input} value={item.price} attributeType="price" onChange={handleInputChange} id='sale-price-input'/>
                            </div>
                            <div>
                                <label htmlFor='price-type-input'>Price Type</label>
                                <select className={EditStyles.input} defaultValue="fixed" id='price-type-input'>
                                    <option value='fixed'>Fixed</option>
                                    <option value='at-the-register'>At the Register</option>
                                    <option value='unit-price'>Unit Price (lb, oz, etc.)</option>
                                </select>
                                <span className={EditStyles.description}>
                                    <b>Fixed</b> prices are set from this screen and can't be 
                                    changed at the Register, only adjusted through discounts.
                                </span>
                            </div>
                            <div>
                                <label htmlFor='discounts-input'>Discounts <QuestionIcon /></label>
                                <select className={EditStyles.input} value={true} attributeType="discountable" onChange={handleInputChange} id='discounts-input'>
                                    <option value={true}>Discountable</option>
                                    <option value={false}>Non-discountable</option>
                                </select>
                            </div>
                            <div>
                                <label htmlFor='cost-per-item-input'>Cost per Item <QuestionIcon /></label>
                                <div className={EditStyles.cost_wrapper}>
                                    <span>{item["cost"].toFixed(2)}</span>
                                    <span style={{lineHeight: '21px'}}>
                                        <button type='button' className='EditItem_Override_Cost_Button'>Override</button>
                                    </span>
                                </div>
                            </div>
                            <div>
                                <label htmlFor='taxable-input'>Taxable</label>
                                <select className={EditStyles.input} attributeType="taxable" onChange={handleInputChange} id='taxable-input'>
                                    <option value={true}>Yes</option>
                                    <option value={false}>No</option>
                                </select>
                            </div>
                            <div>
                                <label htmlFor='select-tax-input'>Select Tax <QuestionIcon /></label>
                                <select className={EditStyles.input} attributeType="taskGroupRate" onChange={handleInputChange} id='select-tax-input'>
                                    <option value='test'>Add Dynamic Options</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div>
                            <h3>Advanced</h3>
                        <div className={EditStyles.inputs_container}>
                            <div>
                                <label htmlFor='supplier-input'>Supplier <QuestionIcon /></label>
                                <select className={EditStyles.input} value={item["supplier"]} attributeType="supplier" onChange={handleInputChange} id='supplier-input'>
                                    <option value={item["supplier"]}>{item['supplier']}</option>
                                </select>
                            </div>
                            <div>
                                <label htmlFor='supplier-item-id-input'>Supplier's Item ID <QuestionIcon /></label>
                                <input type='text' className={EditStyles.input} id='supplier-item-id-input'/>
                            </div>
                            <div>
                                <label htmlFor='ticket-printer-group-input'>Ticket Printer Group <QuestionIcon /></label>
                                <select className={EditStyles.input} id='ticket-printer-group-input'>
                                    <option value='none'>None</option>    
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <footer>
                    <div className='EditItem_Submit_Footer_Container'>
                            <button type='button' className='EditItem_Submit_Footer_Button_Delete' onClick={() => setDialogOpen(true)}>
                                Delete
                            </button>
                            <div style={{flexGrow: '1'}}></div>
                            <button onClick={() => navigate("/salerno/items")} className='EditItem_Submit_Footer_Button_Cancel'>
                                Cancel
                            </button>
                            <button onClick={handleSave} className='EditItem_Submit_Footer_Button_Save'>
                                Save and close
                            </button>
                    </div>
                </footer>
            </section>
        </main>
    )
}

function CategoryNameInput() {
    const [categoryName, setCategoryName] = useState("");
    return (
        <div>
            <label htmlFor='category-input'>Category <QuestionIcon /></label>
            <input type='text' className={EditStyles.input} value={categoryName} onChange={e => setCategoryName(e.target.value)} />
            <button type="button">Save</button>
            <button type="button">Cancel</button>
        </div>
    )
}

export default Edit;