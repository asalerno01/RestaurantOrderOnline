import axios from '../api/axios';
import useAuth from './useAuth';

const useRefreshToken = () => {
    const { setAuth } = useAuth();

    const refresh = async () => {
        const response = await axios({
            method: 'GET',
            url: 'https://localhost:7074/api/auth/refresh',
            withCredentials: true
        })
        setAuth(prev => {
            console.log("refreshing auth...")
            console.log(response);
            return {
                ...prev,
                email: response.data.email,
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                accessToken: response.data.accessToken
            }
        });
        return response.data.accessToken;
    }
    return refresh;
};

export default useRefreshToken;