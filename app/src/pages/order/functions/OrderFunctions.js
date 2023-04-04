export function removeOrderItem(order, index) {
    console.log("removeOrderItem=>" + order);
    console.log("Removing order item at index: " + index + ", " + "\n" + JSON.stringify(order.orderItems[index]));
    const temp = Object.assign({}, order);
    if (temp.orderItems.length === 1) {
        temp.orderItems = [];
        temp.subtotal = 0;
    } else {
        temp.orderItems.splice(index, 1);
        temp.subtotal = temp.subtotal - order.orderItems[index].price;
    }
    return temp;
}