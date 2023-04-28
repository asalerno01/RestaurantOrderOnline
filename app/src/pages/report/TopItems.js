import React from "react";
import classes from './TopItems.module.css'

function TopItems({ orderItemTotals }){

    const itemCounts = {};
    let orderItems = orderItemTotals;
    // Iterate through the orders and items to count the number of times each item has been ordered
    // orders.forEach(order => {
    //     order.orderItems.forEach(item =>{
    //         const itemName = item.itemName;
    //         const count = item.count;

    //         if (itemCounts[itemName]){
    //             itemCounts[itemName] += count;
    //         }else{
    //             itemCounts[itemName] = count;
    //         }
    //     })
    // })

    // Sort the items by their count in descending order
    // const itemList = Object.keys(itemCounts).sort((a, b) => itemCounts[b] - itemCounts[a]);
    // const sortedList = orderItems.sort((a, b) => orderItems[b].count - orderItems[a].count);
    let itemArray = Object.entries(orderItems).map(orderItem => {
        return { name: orderItem[0], count: orderItem[1].count };
    })
    let sortedArray = itemArray.sort((a, b) => b.count - a.count);
    console.log(itemArray)
    return (
        <div className={classes.listContainer}>
            <ol className={classes.order}>
                {/* {itemList.map((itemName) => (
                    <li key={itemName}>
                        {itemName}  <span className={classes.count}>{itemCounts[itemName]} times ordered</span>
                    </li>
                ))} */}
                {
                    sortedArray.map(item => (
                        <li key={item.name}>{item.name}<span className={classes.count}>{item.count}</span></li>
                    ))
                }
            </ol>
        </div>
    )
}export default TopItems;