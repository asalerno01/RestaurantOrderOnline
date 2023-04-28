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
                        <button type="button" className={isCurrentPage("report")} onClick={() => navigate('/salerno/report')}><div>Reports</div></button>
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
                        <button type="button" className={isCurrentPage("items")} onClick={() => navigate('/salerno/items')}><div>Item List</div></button>
                        <button type='button' className={isCurrentPage("itemshortcuts")}><div>Item Shortcuts</div></button>
                        <button type='button' className={isCurrentPage("updateinventory")}><div>Update Inventory</div></button>
                        <button type='button' className={isCurrentPage("bulkmanage")}><div>Bulk Manage Items</div></button>
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
                        <button type='button' className={isCurrentPage("employees")} onClick={() => navigate('/salerno/employees')}><div>Staff List</div></button>
                        <button type='button' className={isCurrentPage("timeclock")} onClick={() => navigate('/salerno/employees/timeclock')}><div>Time Clock</div></button>
                        <button type='button' className='NavBar_Inner_Button'><div>Labor Tracking</div></button>
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
                        <button type='button' className={isCurrentPage("order")} onClick={() => navigate('/salerno/order')}><div>Menu Page</div></button>
                        <button type='button' className={isCurrentPage("orders")} onClick={() => navigate('/salerno/orders')}><div>Order Summary</div></button>
                        <button type='button' className={isCurrentPage("checkout")} onClick={() => navigate('/salerno/order/checkout')}><div>Checkout</div></button>
                    </div>
                </div>
            );
        default:
            return (
                <div>hey</div>
            )

    }
}

export default InnerNavBar;