import axios from "./axios";

export const ShoppingCartAPI = {
    get: async function (accessToken) {
        const response = await axios.request({
            url: 'https://localhost:7074/api/ShoppingCart/User',
            method: 'GET',
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        });

        return response.data;
    },
    increaseCount: async function (shoppingCartItemId, accessToken) {
        let response;
        await axios({
            method: 'PUT',
            url: `https://localhost:7074/api/ShoppingCart/increase/${shoppingCartItemId}`,
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        }).then(res => console.log(res));

        return response.data;
    },
    decreaseCount: async function (shoppingCartItemId, accessToken) {
        const response = await axios.request({
            url: `https://localhost:7074/api/ShoppingCart/decrease/${shoppingCartItemId}`,
            method: 'PUT',
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        });

        return response.data;
    },
    add: async function (shoppingCartItem, accessToken) {
        const response = await axios.request({
            url: 'https://localhost:7074/api/ShoppingCart/',
            method: 'POST',
            headers: {
                Authorization: `Bearer ${accessToken}`
            },
            data: shoppingCartItem
        });

        return response.data;
    },
    delete: async function (shoppingCartItemId, accessToken) {
        const response = await axios.request({
            url: `https://localhost:7074/api/ShoppingCart/${shoppingCartItemId}`,
            method: 'DELETE',
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        });

        return response.data;
    },
    update: async function (shoppingCartItem, accessToken) {
        const response = await axios.request({
            url: `https://localhost:7074/api/ShoppingCart/${shoppingCartItem.shoppingCartItemId}`,
            method: 'PUT',
            headers: {
                Authorization: `Bearer ${accessToken}`
            },
            data: shoppingCartItem
        });

        return response.data;
    }
}