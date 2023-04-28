import React, {useEffect, useState} from "react";
import Graph from './Graph';
import OrderData from './OrderData';
import TopItems from './TopItems';
import classes from './ReportPage.module.css';
import {BsCalendarWeek} from "react-icons/bs";
import MiniCalendar from "../../components/MiniCalendar";
import OrdersStyles from "../order/Backoffice/css/Orders.module.css";
import axios from "axios";
import { getLastSevenDays } from "../../components/functions/DateInfo";

const Report = () => {
    const [isLoading, setIsLoading] = useState(true)
    // const [menuItems, setMenuItems] = useState([])
    // const [orders, setOrders] = useState([])
    // const [reportDaySelected, setReportDaySelected] = useState([]);
    // const [miniCalendarOpen, setMiniCalendarOpen] = useState(false);
    // const [filterDate, setFilterDate] = useState([{ Day: 1, Month: 1, Year: 2023 }, { Day: 1, Month: 1, Year: 2023 }]);

    // const handleFilterDate = (dateArr) => {
    //     console.log(dateArr);
    // }
    const monthStrings = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
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
            setItems(res.data);
        })
        .catch(err => {
            console.log(err);
        });
    }
    useEffect(() => { getOrders(); getItems(); setIsLoading(false); }, [filterDate]);
    useEffect(() => {
        calculateGrossSales();
        calculateNetSales();
        calculateTax();
        calculateOrderItemTotals();
        calcCustomerCount();
    }, [orders]);
    const [daySpan, setDaySpan] = useState(0)

    
    let startDate = new Date();
    startDate.setFullYear(filterDate[0].Year);
    startDate.setMonth(filterDate[0].Month - 1);
    startDate.setDate(filterDate[0].Day);
    console.log(startDate.toDateString())
    let endDate = new Date();
    endDate.setFullYear(filterDate[1].Year);
    endDate.setMonth(filterDate[1].Month - 1);
    endDate.setDate(filterDate[1].Day);
    useEffect(() => { setDaySpan(Math.floor((endDate.getTime() - startDate.getTime()) / (1000 * 3600 * 24))); }, [filterDate]);
    // useEffect(() => {
    //     setIsLoading(true);
        
    //     fetch('https://localhost:7089/api/items')
    //         .then((response) => {
    //             return response.json();
    //         })
    //         .then((data) => {
    //             const menuItems = [];

    //             for (const key in data) {
    //                 const menuItem = {
    //                     id: key,
    //                     ...data[key]
    //                 };
    //                 menuItems.push(menuItem);
    //             }
    //             setMenuItems(menuItems)
    //             console.log("menuItems:", menuItems);

    //         })
    //         .then(() => fetch('https://localhost:7089/api/orders'))
    //                 .then((response) => {
    //                     return response.json();
    //                 })
    //                 .then((data) => {
    //                     const orders = [];

    //                     for (const key in data) {
    //                         const order = {
    //                             id: key,
    //                             ...data[key]
    //                         };
    //                         orders.push(order);
    //                     }
    //                     setIsLoading(false)
    //                     setOrders(orders)
    //                     console.log("orders:", orders);
    //                 });
    //     }, []);

    if (isLoading) {
        return (
            <section>
                <p>Loading...</p>
            </section>
        );
    }
    //net sales = subtota
    //gross sales = subtotalTax

    // const grossSales = orders.reduce((total,order) => total + order.subtotalTax, 0)
    // const netSales = orders.reduce((total,order) => total + order.subtotal, 0)
    // const salesTax = orders.reduce((total,order)=> total+ order.total,0)

    function calculateGrossSales() {
        let grossSales = 0;
        orders.forEach(order => { order.orderItems.forEach(orderItem => { grossSales += calcOrderItemTotalPrice(orderItem) }); });
        setGrossSales(grossSales.toFixed(2));
    }
    function calculateTax() { setTax((grossSales * 0.0825).toFixed(2)); }
    function calculateNetSales() { setNetSales((grossSales * (1 - 0.0825)).toFixed(2)); }
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
        setCogs(cogs.toFixed(2));
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

    function dayOfWeek(dateString) {
        const date = new Date(dateString);
        return date.getDay();
    }
    //     const weeklyNetSales = new Array(7).fill(0);
    // const weeklyTransactions = new Array(7).fill(0);
    // const weeklyDates = new Array(7).fill('');

        // orders.forEach((order) => {
        //     const weekDay = dayOfWeek(order.orderDate);
        //     weeklyNetSales[weekDay] += order.subtotal;
        //     weeklyOrders[weekDay] ++;
        //     weeklyDates[weekDay] = new Date(order.orderDate).toLocaleDateString('en-US', {
        //         month: '2-digit',
        //         day: '2-digit'
        //     });
        // });

    let orderDays = [];
    let tempStartDate = new Date(startDate);
    orderDays.push(tempStartDate.toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' }))
    for (let i = 0; i < daySpan; i++) {
        orderDays.push(new Date(tempStartDate.setDate(tempStartDate.getDate() + 1)).toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' }));
    }
    const rangeNetSales = new Array(orderDays.length).fill(0);
    const rangeOrders = new Array(orderDays.length).fill(0);
    console.log(rangeOrders)
    const rangeDates = Array.from(orderDays).map(day => new Date(day).toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' }));
    // const rangeNetSales = new Set();
    // const rangeOrders = new Set();
    // const rangeDates = new Set();
    orders.forEach(order => {
        let orderTotal = 0;
        order.orderItems.forEach(orderItem => {
            let orderItemTotal = orderItem.price;
            orderItem.addons.forEach(addon => { orderItemTotal += addon.addon.price });
            orderItem.noOptions.forEach(noOption => { orderItemTotal -= noOption.noOption.price });
            orderItem.groups.forEach(group => { orderItemTotal += group.group.price });
            orderTotal += (orderItemTotal * orderItem.count)
        });
        console.log(order.orderDate)
        rangeNetSales[orderDays.indexOf(new Date(order.orderDate).toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' }))] += orderTotal;
        rangeOrders[orderDays.indexOf(new Date(order.orderDate).toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' }))] ++;
    });
    
    

    const title = `Sales through ${startDate.toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' })} - ${endDate.toLocaleDateString('en-US', { month: '2-digit', day: '2-digit' })}`


    const dataReport = {
        grossSales: grossSales,
        netSales: netSales,
        salesTax: tax,
        customerTotal: customerTotal,
        cogs: cogs,
        orders: orders.length,
        weeklyNetSales: rangeNetSales,
        weeklyOrders: rangeOrders,
        weeklyDates: rangeDates,
        title: title
    };
    console.log(dataReport)
    console.log(orders)


    return(
        <div style={{overflowY: "scroll"}}>
        <section className={classes.report}>
            <div className={classes.header}><CalendarButton/><h1>Reports</h1>
            </div>
            {/*order data*/}
                <OrderData dataReport={dataReport} className={classes.reportData}/>
                <Graph orders={orders} dataReport={dataReport} className={classes.reportData} />
                <TopItems orderItemTotals={orderItemTotals} className={classes.reportData} />
        </section>
        </div>
    )
} 

export default Report;