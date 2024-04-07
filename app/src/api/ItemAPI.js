import { axiosPrivate } from "./axios";

const ItemAPI = {
    get: async function (itemId, accessToken) {
        const response = await axiosPrivate({
            method: 'GET',
            url: `https://localhost:7074/api/items/${itemId}`,
            headers: {
                'Authorization': `Bearer ${accessToken}`
            }
        });

        return response.data;
    },
    getAll: async function (accessToken) {
        const response = await axiosPrivate({
            method: 'GET',
            url: 'https://localhost:7074/api/items',
            headers: {
                'Authorization': `Bearer ${accessToken}`
            }
        });

        return response.data;
    },
    add: async function (a)
}