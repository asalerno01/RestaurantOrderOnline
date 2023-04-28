import React, { useState, useEffect } from 'react';
import MiniCalendar from '../../../components/MiniCalendar';
import ReportsStyles from './css/Reports.module.css';
import OrdersStyles from './css/Orders.module.css';
import axios from 'axios';
import { getLastSevenDays, monthStrings } from '../../../components/functions/DateInfo';
import { BsCalendarWeek } from 'react-icons/bs';

const Reports = () => {
    const [orders, setOrders] = useState([]);
    const [miniCalendarOpen, setMiniCalendarOpen] = useState(false);
    const [filterDate, setFilterDate] = useState(getLastSevenDays());
    const [grossSales, setGrossSales] = useState(0);
    const [netSales, setNetSales] = useState(0);
    const [tax, setTax] = useState(0);
    const [orderItemTotals, setOrderItemTotals] = useState({});
    const [items, setItems] = useState([]);
    const [customerTotal, setCustomerTotal] = useState(0);
    const [cogs, setCogs] = useState(0);

    const getOrders = async () => {
        await axios({
            method: "POST",
            url: "https://localhost:7074/api/orders/date/full",
            data: {
                startDate: new Date(filterDate[0].Year, filterDate[0].Month - 1, filterDate[0].Day).toISOString(),
                endDate: new Date(filterDate[1].Year, filterDate[1].Month - 1, filterDate[1].Day).toISOString()
            }
        })
        .then(res => {
            console.log(res.data)
            setOrders(res.data);
        })
        .catch(err => {
            console.log(err);
        });
    }

    const getItems = async () => {
        await axios({
            method: "GET",
            url: "https://localhost:7074/api/items"
        })
        .then(res => {
            console.log(res.data)
            setItems(res.data);
        })
        .catch(err => {
            console.log(err);
        });
    }
    useEffect(() => { getOrders(); getItems(); }, [filterDate]);
    useEffect(() => {
        calculateGrossSales();
        calculateNetSales();
        calculateTax();
        calculateOrderItemTotals();
        calcCustomerCount();
    }, [orders]);

    const CalendarButton = () => {
        return (
            <>
                <button className={OrdersStyles.calendar_button} onClick={() => setMiniCalendarOpen(prev => !prev)}>
                    <BsCalendarWeek size={"22px"} style={{ verticalAlign: 'middle', marginRight: "5px" }}/>
                    <span>{`${monthStrings[filterDate[0].Month - 1]} ${filterDate[0].Day}, ${filterDate[0].Year} - ${monthStrings[filterDate[1].Month - 1]} ${filterDate[1].Day}, ${filterDate[1].Year}`}</span>
                </button>
                <MiniCalendar miniCalendarOpen={miniCalendarOpen} setMiniCalendarOpen={setMiniCalendarOpen} setFilterDate={setFilterDate} filterDate={filterDate}/>
            </>
        )
    }
    function calculateGrossSales() {
        let grossSales = 0;
        orders.forEach(order => { order.orderItems.forEach(orderItem => { grossSales += calcOrderItemTotalPrice(orderItem) }); });
        setGrossSales(grossSales);
    }
    function calculateTax() { setTax(grossSales * 0.0825); }
    function calculateNetSales() { setNetSales(grossSales * (1 - 0.0825)); }
    function calculateOrderItemTotals() {
        let orderItems = {};
        let cogs = 0;
        orders.forEach(order => {
            order.orderItems.forEach(orderItem => {
                if (Object.keys(orderItems).includes(orderItem.name))
                    orderItems[orderItem.name] = { count: (orderItems[orderItem.name].count + orderItem.count), sales: (orderItems[orderItem.name].sales + calcOrderItemTotalPrice(orderItem)) }
                else if (!Object.keys(orderItems).includes(orderItem.name))
                    orderItems[orderItem.name] = { count: orderItem.count, sales: calcOrderItemTotalPrice(orderItem)}
            })
        });
        Object.entries(orderItems).forEach(orderItem => {
            cogs += (orderItem[1].count * items.find(item => item.name === orderItem[0])?.assignedCost);
        })
        setOrderItemTotals(orderItems);
        setCogs(cogs);
    }
    function calcOrderItemTotalPrice(orderItem) {
        let orderItemTotal = 0;
        orderItem.addons.forEach(addon => { orderItemTotal += addon.addon.price });
        orderItem.noOptions.forEach(noOption => { orderItemTotal -= noOption.noOption.price });
        orderItem.groups.forEach(group => { orderItemTotal += group.group.price });
        orderItemTotal += orderItem.price;
        return orderItemTotal * orderItem.count;
    }
    function calcCustomerCount() {
        // Just get count of unique customers.
        // If customer has two orders, they only count as a single customer.
        let customerTotal = new Set();
        orders.forEach(order => customerTotal.add(order.account.accountId));
        setCustomerTotal(customerTotal.size);
    }

    console.log(orderItemTotals)
    return (
        <main className={ReportsStyles.main}>
            <div className={ReportsStyles.calendar_container}>
                <CalendarButton />
            </div>
            <div className={ReportsStyles.items}>
                <div className={ReportsStyles.item}>
                    <span>Gross Sales</span>
                    <span>${grossSales.toFixed(2)}</span>
                </div>
                <div className={ReportsStyles.item}>
                    <span>Tax</span>
                    <span>${tax.toFixed(2)}</span>
                </div>
                <div className={ReportsStyles.item}>
                    <span>Net Sales</span>
                    <span>${netSales.toFixed(2)}</span>
                </div>
                <div className={ReportsStyles.item}>
                    <span>Orders</span>
                    <span>{orders.length}</span>
                </div>
                <div className={ReportsStyles.item}>
                    <span>Customers</span>
                    <span>{customerTotal}</span>
                </div>
                <div className={ReportsStyles.item}>
                    <span>Cost of Goods</span>
                    <span>${cogs.toFixed(2)}</span>
                </div>
                <div style={{display: "flex"}}>
                    <div className={ReportsStyles.item} style={{flexDirection: "column"}}>
                        <h1>Item Count</h1>
                        <ul>
                        {
                            Object.entries(orderItemTotals).map(orderItemTotal => (
                                <li><span>{orderItemTotal[0]}</span><span>{orderItemTotal[1].count}</span></li>
                            ))
                        }
                        </ul>
                    </div>
                    <div className={ReportsStyles.item} style={{flexDirection: "column"}}>
                        <h1>Item Sales</h1>
                        <ul>
                        {
                            Object.entries(orderItemTotals).map(orderItemTotal => (
                                <li><span>{orderItemTotal[0]}</span><span>${orderItemTotal[1].sales.toFixed(2)}</span></li>
                            ))
                        }
                        </ul>
                    </div>
                </div>
            </div>
        </main>
    )
}

export default Reports;