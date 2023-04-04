import axios from 'axios';
import useAuth from "./useAuth";

const useLogout = () => {
    const { auth, setAuth } = useAuth();
    const logout = async () => {
        console.log(auth);
        await axios({
            method: "post",
            url: "https://localhost:7074/api/auth/logout",
            withCredentials: true
        })
        .then(res => {
            console.log(res);
        })
        .catch(err => {
            console.log(err);
        })
        setAuth({});
    }
    return logout;
}

export default useLogout