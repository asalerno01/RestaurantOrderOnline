import React, { useEffect, useRef, useState } from 'react';
import axios from 'axios';
import OrderItem from './OrderItem';
import './order.css';
import OrderStyles from './css/Order.module.css';
import CartStyles from './css/Cart.module.css';
import MenuItem from '../../raquel/components/menu/MenuItem';
import Cart from './Cart';
import Banner from '../../imgs/banner.webp';
import StickyBox from 'react-sticky-box';
import { ImCart } from 'react-icons/im';
import { createEmptyOrder } from './functions/OrderFunctions';
import SavedOrder from './SavedOrder';
import useAuth from '../../hooks/useAuth';

const Order = () => {
    const customerAccountId = 1;
    const auth = useAuth();

    const [items, setItems] = useState([]);
    const [categories, setCategories] = useState([]);
    const [cats, setCats] = useState([]);
    const [order, setOrder] = useState(createEmptyOrder());
    const [savedOrders, setSavedOrders] = useState([]);

    const [selectedItemData, setSelectedItemData] = useState({ item: null, index: null });

    const [cartOpen, setCartOpen] = useState(false);
    useEffect(() => {
        const order = localStorage.getItem("order");
        if (order !== null && order.length > 0)
            setOrder(JSON.parse(order));
    }, []);
    const [response, setResponse] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    const getItems = async () => {
        console.log("Executing getItems()");
        await axios.get("https://localhost:7074/api/category")
        .then(res => {
            console.log(res);
            let items = [];
            let categories = res.data;
            categories.forEach(category => {
                category.items.forEach(item => {
                    items.push(item);
                });
            });
            setItems(items);
            setCats(categories);
        })
        .catch(err => {
            console.log(err);
        });
    }
    const getSavedOrders = async () => {
        console.log("Executing getSavedOrders()");
        console.log(categories);
        await axios({
            method: "GET",
            url: `https://localhost:7074/api/orders/savedorders/${customerAccountId}`,
            withCredentials: true,
            headers: {
                Authorization: `Bearer ${auth?.accessToken}`
            }
        })
        .then(res => {
            console.log(res.data);
            if (res.data.length > 0) {
                let data = res.data;
                data.forEach(savedOrder => {
                    savedOrder.type = "SavedOrder";
                });
                setCategories([{ categoryId: 0, name: "Saved Orders", description: "", items: data }, ...cats]);
                setIsLoading(false);
            }
        })
        .catch(err => {
            console.log(err);
            setCategories(cats);
        });
    }
    useEffect(() => {
        getItems();
    }, []);
    useEffect(() => {
        console.log(categories);
        getSavedOrders();
    }, [cats])

    const handleEditItemClick = (itemId, index) => setSelectedItemData({ item: items.find(item => item.itemId === itemId), index: index });
    const handleOpenItem = (itemId) => setSelectedItemData({ item: items.find(i => i["itemId"] === itemId), index: null });
    const handleRemoveItemClick = (index) => {
        const temp = Object.assign({}, order);
        let subtotal = 0;
        temp["orderItems"].splice(index, 1);
        if (isNaN(subtotal)) subtotal = 0;
        temp["subtotal"] = subtotal;
        setOrder(temp);
    }

    function getScroll() {
        for (let i = categories.length - 1; i > -1; i--) {
            if ((document.getElementById(i).getBoundingClientRect().top - 106) < 0) {
                setCurrentScroll(i);
                break;
            }
        }
    }
    const [currentScroll, setCurrentScroll] = useState(0);
    const handleScrollClick = event => document.getElementById(event.target.value).scrollIntoView({ block: "start", behavior: "smooth" });

    function cartIsOpen(type) {
        if (type) {
            setCartOpen(true);
            cartSliderRef.current.style.width = "1100px";
        } else {
            cartSliderRef.current.style.width = "0";
            setCartOpen(false);
        }
    }
    const cartSliderRef = useRef();

    return (
        <div className={OrderStyles.order} onScroll={() => getScroll()}>
            <div className={CartStyles.backdrop} style={(cartOpen) ? {width: "100%"} : {width: "0"}} onClick={() => cartIsOpen(false)}></div>
            <div className={CartStyles.slider} ref={cartSliderRef}>
                <div className={CartStyles.container}>  
                    <Cart
                        order={order}
                        setOrder={setOrder}
                        handleEditItemClick={handleEditItemClick}
                        handleRemoveItemClick={handleRemoveItemClick}
                        cartOpen={cartOpen}
                        cartIsOpen={cartIsOpen}
                    />
                </div>
            </div>
            <OrderItem 
                selectedItemData={selectedItemData}
                setSelectedItemData={setSelectedItemData}
                order={order}
                setOrder={setOrder}
                cartIsOpen={cartIsOpen}
            />
            { (response === "Success!") ? <h3 style={{color: "green"}}>{response}</h3> : (response !== null) ? <h3 style={{color: "red"}}>{response}</h3> : <></> }
            <div className={OrderStyles.header}>
                <div className={OrderStyles.banner_wrapper}>
                    <img src={Banner}/>
                </div>
                <h1>Salerno's Red Hots</h1>
                <span>197 E Veterans Pkwy, Yorkville, IL 60560, USA</span>
                <span>Open Hours: 11:00 AM - 8:00 PM</span>
            </div>
            <StickyBox style={{zIndex: "2"}} offsetTop={55}>
                <div className={OrderStyles.cart_button_wrapper}>
                    <button type="button" className={OrderStyles.cart_button} onClick={() => cartIsOpen(true)}><ImCart size={"24px"}/><span>Cart</span></button>
                </div>
            </StickyBox>
            <main>
                <div className={OrderStyles.nav}>
                    <StickyBox style={{zIndex: "1"}} offsetTop={-16}>
                        <h2 className={OrderStyles.nav_header}>Categories</h2>
                        <div className={OrderStyles.border}></div>
                        <div className={OrderStyles.category_buttons}>
                        {
                            categories.map((category, index) => {
                                if (index === currentScroll)
                                    return <button type="button" className={OrderStyles.category_button__selected} value={index} onClick={handleScrollClick}><span className={OrderStyles.button_content}>{category.name}<span className={OrderStyles.button_border}></span></span></button>
                                return <button type="button" className={OrderStyles.category_button} value={index} onClick={handleScrollClick}><span className={OrderStyles.button_content}>{category.name}<span className={OrderStyles.button_border}></span></span></button>
                            })
                        }
                        </div>
                    </StickyBox>
                </div>
                <div className={OrderStyles.categories}>
                {
                    categories.map((category, index) => (
                        <div className={OrderStyles.category} id={index}>
                            <StickyBox style={{zIndex: "1", backgroundColor: "rgb(255, 255, 255)"}} offsetTop={-16}>
                                <h1 className={OrderStyles.category_header}>{category.name}</h1>
                                <div className={OrderStyles.border}></div>
                            </StickyBox>
                            <div className={OrderStyles.items}>
                            {
                                category.items.map(item => {
                                    if (item?.type === "SavedOrder")
                                        return (
                                            <SavedOrder
                                                savedOrder={item}
                                                order={order}
                                                setOrder={setOrder}
                                            />
                                        )
                                    else
                                        return (
                                            <MenuItem
                                                key={item.itemId}
                                                itemId={item.itemId}
                                                name={item.name}
                                                price={item.price}
                                                description={item.description}
                                                handleOpenItem={handleOpenItem}
                                            />
                                        )
                                })
                            }
                            </div>
                        </div>
                    ))
                }
                </div>
            </main>
        </div>
    );
}

export default Order;