import React from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './css/navbar.css';

const InnerNavBar = ({ innerNavType }) => {
    const location = useLocation();
    const navigate = useNavigate();
    
    const isCurrentPage = (page) => {
        if (location.pathname.search(`/${page}$`) > -1)
            return "NavBar_Inner_Button NavBar_Inner_Button_Active";
        return "NavBar_Inner_Button";
    }
    switch (innerNavType) {
        case "report":
            return (
                <div className='NavBar_Inner_Container'>
                    <div className='NavBar_Inner_Header'>
                        <span>Reports</span>
                    </div>
                    <div className='NavBar_Inner_Buttons_Container'>
                        <a href="/salerno/report" className={isCurrentPage("report")}><div>Reports</div></a>
                    </div>
                </div>
            );
        case "items":
            return (
                <div className='NavBar_Inner_Container'>
                    <div className='NavBar_Inner_Header'>
                        <span>Items</span>
                    </div>
                    <div className='NavBar_Inner_Buttons_Container'>
                        <a href="/salerno/items" className={isCurrentPage("items")} onClick={() => navigate('/salerno/items')}><div>Item List</div></a>
                        <a href="#" className={isCurrentPage("itemshortcuts")}><div>Item Shortcuts</div></a>
                        <a href="#" className={isCurrentPage("updateinventory")}><div>Update Inventory</div></a>
                        <a href="#" className={isCurrentPage("bulkmanage")}><div>Bulk Manage Items</div></a>
                    </div>
                </div>
            );
        case "staff":
            return (
                <div className='NavBar_Inner_Container'>
                    <div className='NavBar_Inner_Header'>
                        <span>Staff</span>
                    </div>
                    <div className='NavBar_Inner_Buttons_Container'>
                        <a href="/salerno/employees" className={isCurrentPage("employees")} onClick={() => navigate('/salerno/employees')}><div>Staff List</div></a>
                        <a href="/salerno/employees/timeclock" className={isCurrentPage("timeclock")} onClick={() => navigate('/salerno/employees/timeclock')}><div>Time Clock</div></a>
                        <a href="#" className='NavBar_Inner_Button'><div>Labor Tracking</div></a>
                    </div>
                </div>
            );
        case "order":
            return (
                <div className='NavBar_Inner_Container'>
                    <div className='NavBar_Inner_Header'>
                        <span>Order</span>
                    </div>
                    <div className='NavBar_Inner_Buttons_Container'>
                        <a href="/salerno/order" className={isCurrentPage("order")} onClick={() => navigate('/salerno/order')}><div>Menu Page</div></a>
                        <a href="/salerno/orders" className={isCurrentPage("orders")} onClick={() => navigate('/salerno/orders')}><div>Order Summary</div></a>
                        <a href="/salerno/checkout" className={isCurrentPage("checkout")} onClick={() => navigate('/salerno/order/checkout')}><div>Checkout</div></a>
                    </div>
                </div>
            );
        default:
            return <></>

    }
}

export default InnerNavBar;