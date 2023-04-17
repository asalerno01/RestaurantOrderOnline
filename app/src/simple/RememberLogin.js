import { Outlet } from "react-router-dom";
import { useState, useEffect } from "react";
import useRefreshToken from '../hooks/useRefreshToken';
import useAuth from '../hooks/useAuth';
import { isEmptyObject } from "../pages/order/functions/OrderFunctions";

const RememberLogin = () => {
    const [isLoading, setIsLoading] = useState(true);
    const refresh = useRefreshToken();
    const { auth, remember } = useAuth();

    useEffect(() => {
        if (!isEmptyObject(auth)) {
            console.log("Attempting refresh.");
            let isMounted = true;
            const verifyRefreshToken = async () => {
                try {
                    await refresh();
                }
                catch (err) {
                    console.error(err);
                }
                finally {
                    isMounted && setIsLoading(false);
                }
            }

            !auth?.access_token && remember ? verifyRefreshToken() : setIsLoading(false);

            return () => isMounted = false;
        } else {
            setIsLoading(false);
        }
    }, [])

    return (
        <>
            {!remember
                ? <Outlet />
                : isLoading
                    ? <p>Loading...</p>
                    : <Outlet />
            }
        </>
    )
}

export default RememberLogin;