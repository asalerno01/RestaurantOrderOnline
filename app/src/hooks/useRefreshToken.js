import axios from '../api/axios';
import useAuth from './useAuth';

const useRefreshToken = () => {
    const { setAuth } = useAuth();

    const refresh = async () => {
        const response = await axios({
            method: 'GET',
            url: 'https://localhost:7074/api/auth/refresh',
            withCredentials: true
        });
        setAuth(prev => {
            if (response.status === 204) return {};
            return {
                ...prev,
                accountId: response.data.accountId,
                email: response.data.email,
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                phoneNumber: response.data.phoneNumber,
                accessToken: response.data.accessToken
            }
        });
        return response.data.accessToken;
    }
    return refresh;
};

export default useRefreshToken;