import React, {useEffect, useState} from "react";
import Graph from './Graph';
import OrderData from './OrderData';
import TopItems from './TopItems';
import classes from './ReportPage.module.css';
import {IconContext} from "react-icons/lib";
import {BsCalendarWeek} from "react-icons/bs";
import MiniCalendar from "../../simple/Requests/MiniCalendar";

function Report(){
    const [isLoading, setIsLoading] = useState(true)
    const [menuItems, setMenuItems] = useState([])
    const [orders, setOrders] = useState([])
    const [reportDaySelected, setReportDaySelected] = useState([]);
    const [miniCalendarOpen, setMiniCalendarOpen] = useState(false);
    const [filterDate, setFilterDate] = useState([{ Day: 1, Month: 1, Year: 2023 }, { Day: 1, Month: 1, Year: 2023 }]);

    const handleFilterDate = (dateArr) => {
        console.log(dateArr);
    }
    const monthStrings = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const CalendarButton = () => {
        return (
            <div>
                <button className='calendar-btn' onClick={() => setMiniCalendarOpen(prev => !prev)}>
                    <IconContext.Provider value={{ style: { verticalAlign: 'middle'}, size: '1em' }}>
                    </IconContext.Provider>
                    <div>{`${monthStrings[filterDate[0].Month - 1]} ${filterDate[0].Day}, ${filterDate[0].Year} - ${monthStrings[filterDate[1].Month - 1]} ${filterDate[1].Day}, ${filterDate[1].Year}`}</div>
                </button>
                <MiniCalendar handleFilterDate={handleFilterDate} miniCalendarOpen={miniCalendarOpen} setMiniCalendarOpen={setMiniCalendarOpen} reportDaySelected={reportDaySelected} setReportDaySelected={setReportDaySelected} setFilterDate={setFilterDate} />
            </div>
        )
    }
    useEffect(() => {
        setIsLoading(true);

        fetch('https://localhost:7089/api/items')
            .then((response) => {
                return response.json();
            })
            .then((data) => {
                const menuItems = [];

                for (const key in data) {
                    const menuItem = {
                        id: key,
                        ...data[key]
                    };
                    menuItems.push(menuItem);
                }
                setMenuItems(menuItems)
                console.log("menuItems:", menuItems);

            })
            .then(() => fetch('https://localhost:7089/api/orders'))
                    .then((response) => {
                        return response.json();
                    })
                    .then((data) => {
                        const orders = [];

                        for (const key in data) {
                            const order = {
                                id: key,
                                ...data[key]
                            };
                            orders.push(order);
                        }
                        setIsLoading(false)
                        setOrders(orders)
                        console.log("orders:", orders);
                    });
        }, []);

    if (isLoading) {
        return (
            <section>
                <p>Loading...</p>
            </section>
        );
    }
    //net sales = subtota
    //gross sales = subtotalTax

    const grossSales = orders.reduce((total,order) => total + order.subtotalTax, 0)
    const netSales = orders.reduce((total,order) => total + order.subtotal, 0)
    const salesTax = orders.reduce((total,order)=> total+ order.total,0)


    function dayOfWeek(dateString) {
        const date = new Date(dateString);
        return date.getDay();
    }
        const weeklyNetSales = new Array(7).fill(0);
    const weeklyTransactions = new Array(7).fill(0);
    const weeklyDates = new Array(7).fill('');

        orders.forEach((order) => {
            const weekDay = dayOfWeek(order.orderDate);
            weeklyNetSales[weekDay] += order.subtotal;
            weeklyTransactions[weekDay] ++;
            weeklyDates[weekDay] = new Date(order.orderDate).toLocaleDateString('en-US', {
                month: '2-digit',
                day: '2-digit'
            });
        });




    const dataReport = {
        grossSales: grossSales,
        netSales:netSales,
        salesTax: salesTax,
        weeklyNetSales: weeklyNetSales,
        weeklyTransactions: weeklyTransactions,
        weeklyDates: weeklyDates,
    };


    return(
        <div>
        <section className={classes.report}>
            <h4 className={classes.dashboard}>Dashboard
            <CalendarButton/></h4>
            {/*order data*/}
                <OrderData dataReport={dataReport} className={classes.reportData}/>
                <Graph orders={orders} dataReport={dataReport} className={classes.reportData} />
                <TopItems orders={orders} menuItems={menuItems} className={classes.reportData} />
        </section>
        </div>
    )
} export default Report;