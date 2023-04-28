import React from "react";
import classes from './OrderData.module.css'

function OrderData({dataReport}){
    const {
        grossSales,
        netSales,
        salesTax,
        orders,
        customerTotal,
        cogs
    } = dataReport;
    return (
        <div className={classes.dataContainer}>
            <div className={classes.data}>Gross Sales ${grossSales}</div>
            <div className={classes.data}>Net Sales: ${netSales}</div>
            <div className={classes.data}>Sales Tax : ${salesTax}</div>
            <div className={classes.data}>Orders : {orders}</div>
            <div className={classes.data}>Customers : {customerTotal}</div>
            <div className={classes.data}>Cost of Goods : ${cogs}</div>
        </div>
    )
}export default OrderData;