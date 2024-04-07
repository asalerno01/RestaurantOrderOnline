import useAuth from "../hooks/useAuth";
import axios from "./axios";

export const increaseShoppingCartItemCount = async (shoppingCartItemId, accessToken, setOrder) => {
    await axios({
        method: "PUT",
        headers: {
            "Authorization": `Bearer ${accessToken}`
        },
        url: `https://localhost:7074/api/ShoppingCart/increase/${shoppingCartItemId}`
    })
    .then (res => {
        console.log(res);
        setOrder(res.data);
    })
}

export const decreaseShoppingCartItemCount = async (shoppingCartItemId, accessToken, setOrder) => {
    await axios({
        method: "PUT",
        headers: {
            "Authorization": `Bearer ${accessToken}`
        },
        url: `https://localhost:7074/api/ShoppingCart/decrease/${shoppingCartItemId}`
    })
    .then (res => {
        console.log(res);
        setOrder(res.data);
    })
}

export const deleteShoppingCartItem = async (shoppingCartItemId, accessToken, setOrder) => {
    await axios({
        method: "DELETE",
        headers: {
            "Authorization": `Bearer ${accessToken}`
        },
        url: `https://localhost:7074/api/ShoppingCart/${shoppingCartItemId}`
    })
    .then(res => {
        console.log(res);
        setOrder(res.data);
    })
}

export const updateShoppingCartItem = async (shoppingCartItem, accessToken, setOrder) => {
    await axios({
        method: "PUT",
        headers: {
            "Authorization": `Bearer ${accessToken}`
        },
        url: `https://localhost:7074/api/ShoppingCart/${shoppingCartItem.shoppingCartItemId}`,
        data: shoppingCartItem
    })
    .then(res => {
        console.log(res);
        setOrder(res.data);
    })
}