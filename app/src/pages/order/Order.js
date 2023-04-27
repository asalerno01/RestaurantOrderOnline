import React, { useEffect, useRef, useState } from 'react';
import axios from 'axios';
import OrderItem from './OrderItem';
import OrderStyles from './css/Order.module.css';
import CartStyles from './css/Cart.module.css';
import MenuItem from '../../raquel/components/menu/MenuItem';
import Cart from './Cart';
import Banner from '../../imgs/banner.webp';
import StickyBox from 'react-sticky-box';
import { ImCart } from 'react-icons/im';
import { createEmptyOrder, isEmptyObject } from './functions/OrderFunctions';
import SavedOrder from './SavedOrder';
import useAuth from '../../hooks/useAuth';
import LoadingSpinner from '../../components/LoadingSpinner';
import SavedOrderItem from './SavedOrderItem';

const Order = () => {
    // Should store a key to tie order to auth account
    const { auth } = useAuth();
    const [order, setOrder] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [menu, setMenu] = useState([]);
    const [selectedItemData, setSelectedItemData] = useState({ item: null, index: null });
    const cartSliderRef = useRef();
    const [cartOpen, setCartOpen] = useState(false);
    
    function getLocalStorageOrder() {
        let order = localStorage.getItem("order");
        if (order === undefined || order === null || order.length === 0) return;
        setOrder(JSON.parse(order));
    }
    
    function saveToLocalStorage() {
        localStorage.setItem("order", JSON.stringify(order));
        // localStorage.setItem("order", JSON.stringify(order.map(orderItem => (
        //     {
        //         itemId: orderItem.itemId,
        //         count: orderItem.count,
        //         addons: [...orderItem.addons.map(addon => addon?.addonId)],
        //         noOptions: [...orderItem.noOptions.map(noOption => noOption?.noOptionId)],
        //         groups: [...orderItem.groups.map(group => ({ groupId: group?.groupId, groupOptionId: group?.groupOptionId }))]
        //     }
        // ))));
    }
    useEffect(() => {
        if (!isLoading) saveToLocalStorage();
    }, [order])
    
    const getMenu = async () => {
        await axios.get("https://localhost:7074/api/menu")
        .then(response => {
            setMenu(response.data);
        })
        .catch(err => console.log(err));
        if (auth.accountId !== undefined) {
            console.log(auth);getSavedOrders();
        }
        setIsLoading(false);
    }
    const getSavedOrders = async () => {
        if (auth.accountId === undefined) return;
        await axios({
        method: "GET",
        url: `https://localhost:7074/api/orders/savedorders/${auth.accountId}`,
        })
        .then(res => {
            if (res.data.length > 0) {
                setMenu(prev => {
                    if (prev[0]?.name === "Saved Orders") return prev;
                    return[{ name: "Saved Orders", items: [...res.data] }, ...prev];
                })
            }
        })
        .catch(err => console.log(err));
    }
    useEffect(() => { getLocalStorageOrder(); }, []);
    useEffect(() => { getMenu(); }, [auth]);

    function getScroll() {
        for (let i = menu.length - 1; i > -1; i--) {
            if ((document.getElementById(i).getBoundingClientRect().top - 106) < 0) {
                setCurrentScroll(i);
                break;
            }
        }
    }
    const [currentScroll, setCurrentScroll] = useState(0);
    const handleScrollClick = event => document.getElementById(event.target.value).scrollIntoView({ block: "start", behavior: "smooth" });
    const handleOpenItem = (itemId) => setSelectedItemData({ item: menu.map(item => item.items).reduce((a, c) => a.concat(c), []).find(i => i.itemId === itemId), index: null });

    function cartIsOpen(type) {
        if (type) {
            setCartOpen(true);
            cartSliderRef.current.style.width = "1100px";
        } else {
            setCartOpen(false);
            cartSliderRef.current.style.width = "0";
        }
    }
    const [savedOrderName, setSavedOrderName] = useState("");
    const Test = () => {
        if (selectedItemData === { item: null, index: null }) return <></>
        if (Array.isArray(selectedItemData)) return <SavedOrderItem selectedItemData={selectedItemData} setSelectedItemData={setSelectedItemData} setOrder={setOrder} cartIsOpen={cartIsOpen} savedOrderName={savedOrderName} setSavedOrderName={setSavedOrderName} />
        else return <OrderItem 
            selectedItemData={selectedItemData}
            setSelectedItemData={setSelectedItemData}
            order={order}
            setOrder={setOrder}
            cartIsOpen={cartIsOpen}
            saveToLocalStorage={saveToLocalStorage}
        />
    }
    const MenuItems = () => {
        if (isLoading) return <LoadingSpinner />
        return (
            <div className={OrderStyles.categories}>
            {
                menu.map((category, index) => (
                    <div className={OrderStyles.category} id={index} key={`category-${category.name}-${index}`}>
                        <StickyBox style={{zIndex: "1", backgroundColor: "rgb(255, 255, 255)"}} offsetTop={-16}>
                            <h1 className={OrderStyles.category_header}>{category.name}</h1>
                            <div className={OrderStyles.border}></div>
                        </StickyBox>
                        <div className={OrderStyles.items}>
                        {
                            category.items.map((item, index) => {
                                if (category.name === "Saved Orders")
                                    return (
                                        <SavedOrder
                                            key={`SavedOrderKey-${index}`}
                                            savedOrder={item}
                                            setSelectedItemData={setSelectedItemData}
                                            items={menu.map(item => item.items).reduce((a, c) => a.concat(c), [])}
                                            setSavedOrderName={setSavedOrderName}
                                        />
                                    )
                                else 
                                    return (
                                        <MenuItem
                                            key={`MenuItemKey-${item.itemId}`}
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
        )
    }
    const cartCount = () => { return (0 + order.length); }

    return (
        <div className={OrderStyles.order} onScroll={() => getScroll()}>
            <div className={CartStyles.backdrop} style={(cartOpen) ? {width: "100%"} : {width: "0"}} onClick={() => cartIsOpen(false)}></div>
            <div className={CartStyles.slider} ref={cartSliderRef}>
                <div className={CartStyles.container}>  
                    <Cart
                        order={order}
                        setOrder={setOrder}
                        items={menu.map(item => item.items).reduce((a, c) => a.concat(c), [])}
                        setSelectedItemData={setSelectedItemData}
                        cartOpen={cartOpen}
                        cartIsOpen={cartIsOpen}
                        saveToLocalStorage={saveToLocalStorage}
                    />
                </div>
            </div>
            <Test />
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
                    <button type="button" className={OrderStyles.cart_button} onClick={() => cartIsOpen(true)}>
                        <ImCart size={"24px"}/>
                        <span>{`Cart (${cartCount()})`}</span>
                    </button>
                </div>
            </StickyBox>
            <main>
                <div className={OrderStyles.nav}>
                    <StickyBox style={{zIndex: "1"}} offsetTop={-16}>
                        <h2 className={OrderStyles.nav_header}>Categories</h2>
                        <div className={OrderStyles.border}></div>
                        <div className={OrderStyles.category_buttons}>
                        {
                            menu.map((category, index) => {
                                if (index === currentScroll)
                                    return <button type="button" key={`scroll-index-${index}`} className={OrderStyles.category_button__selected} value={index} onClick={handleScrollClick}><span className={OrderStyles.button_content}>{category.name}<span className={OrderStyles.button_border}></span></span></button>
                                return <button type="button" key={`scroll-index-${index}`} className={OrderStyles.category_button} value={index} onClick={handleScrollClick}><span className={OrderStyles.button_content}>{category.name}<span className={OrderStyles.button_border}></span></span></button>
                            })
                        }
                        </div>
                    </StickyBox>
                </div>
                <MenuItems />
            </main>
        </div>
    );
}

export default Order;