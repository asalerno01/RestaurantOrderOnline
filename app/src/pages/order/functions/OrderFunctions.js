export function removeOrderItem(order, index) {
    return order.toSplice(index, 1);
}
export function canSubmitOrder(inputObject) {
    return (inputObject.firstName.length > 0
        && inputObject.firstName.length > 0
        && inputObject.phoneNumber.length > 0
        && inputObject.email.length > 0
        && inputObject.paymentType.length > 0
        && inputObject.pickupType.length > 0);
}
export function generateUUIDUsingMathRandom() { 
    // wow
    // https://qawithexperts.com/article/javascript/generating-guiduuid-using-javascript-various-ways/372#:~:text=Generating%20GUID%2FUUID%20using%20Javascript%20%28Various%20ways%29%201%20Generate,is%20fast%20to%20generate%20an%20ASCII-safe%20GUID%20
    var d = new Date().getTime();//Timestamp
    var d2 = (performance && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if(d > 0){ //Use timestamp until depleted
            r = (d + r)%16 | 0;
            d = Math.floor(d/16);
        } else { //Use microseconds since page-load if supported
            r = (d2 + r)%16 | 0;
            d2 = Math.floor(d2/16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}
export function getOrderSubtotal(order, items) {
    let subtotal = 0;
    order.forEach(orderItem => {
        let { count, addons, noOptions, groups } = orderItem;
        subtotal += getOrderItemPrice(orderItem.price, count, addons, noOptions, groups);
    });
    return subtotal;
}
export function getOrderItemPrice(basePrice, count, addons, noOptions, groups) {
    let orderItemPrice = basePrice;
    addons.forEach(addon => { orderItemPrice += Number(addon.price); });
    noOptions.forEach(noOption => { orderItemPrice -= Number(noOption.price); });
    groups.forEach(group => { orderItemPrice += Number(group.price); });
    return Number(orderItemPrice * count);
}
export function createEmptyOrderItem() {
    return ({
        "itemId": "",
        "name": "",
        "price": 0,
        "count": 0,
        "modifier": {
            "addons": [],
            "noOptions": [],
            "groups": []
        }
    });
}
export function createEmptyBaseItem() {
    return ({
        "itemId": "",
        "name": "",
        "price": 0,
        "modifier": {
            "addons": [],
            "noOptions": [],
            "groups": []
        }
    });
}
export function isEmptyObject(obj) {
    for(var prop in obj) {
        if(obj.hasOwnProperty(prop))
            return false;
    }
    return true;
}
export function isDrink(name) { return ["Diet Coke", "Sprite", "Coke", "Root Beer", "Dr Pepper", "Mountain Dew", "Pepsi", "Orange Crush", "Dasani Water"].includes(name) };