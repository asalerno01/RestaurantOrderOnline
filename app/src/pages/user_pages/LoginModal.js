import React, { useState, useEffect, useRef, useContext } from "react";
import { Navigate, Link, useNavigate, useLocation } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import './css/login.css';
import axios from 'axios';
import Logo from '../../imgs/logo.png';

const LoginModal = ({ loginModalOpen, setLoginModalOpen }) => {
    const { auth, setAuth, remember, setRemember } = useAuth();
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [errMsg, setErrMsg] = useState("");
    const emailRef = useRef();

    const handleEmailChange = event => {
        event.preventDefault();
        setEmail(event.target.value);
    };
    const handlePasswordChange = event => {
        event.preventDefault();
        setPassword(event.target.value);
    };

    useEffect(() => {
        localStorage.setItem("remember", remember);
    }, [remember]);

    const handleLogin = async event => {
        event.preventDefault();
        console.log("submitting...");
        await axios({
            method: "POST",
            url: "https://localhost:7074/api/auth/login",
            data: { 
                email: email,
                password: password
            },
            header: {
                "Content-Type": "application/json"
            },
            withCredentials: true
        })
        .then(res => {
            console.log(res);
            console.log(res.data);
            const accessToken = res.data?.accessToken;
            const firstName = res.data?.firstName;
            const lastName = res.data?.lastName;
            const email = res.data?.email;
            setAuth({ firstName, lastName, email, accessToken });
            setEmail("");
            setPassword("");
            setLoginModalOpen(false);
        })
        .catch(err => {
            console.log(err);
            if (!err?.response) {
                setErrMsg('No Server Response');
            } else if (err.response?.status === 400) {
                setErrMsg('Missing EmployeeID or Password');
            } else if (err.response?.status === 401) {
                setErrMsg('No account exists for this employeeID');
            } else if (err.response?.status === 404) {
                setErrMsg(err.response.data.message);
            } else {
                setErrMsg('Login Failed');
            }
        });
    }

    if (!loginModalOpen) {
        return <></>
    }
    return (
        <div className="login_modal" onClick={() => setLoginModalOpen(false)}>
            <div className="Login_Container" onClick={e => e.stopPropagation()}>
                <div className="Login_Logo_Wrapper">
                    <img src={Logo} className="Login_Logo" />
                </div>
                <h1 className="Login_Header">Sign In</h1>
                <div className="Login_Form">
                    <label className="Login_Form_Input_Label" htmlFor='email'>Email</label>
                    <input className="Login_Input_Text" type='text' id='email' ref={emailRef} onChange={handleEmailChange} value={email} />
                    <label className="Login_Form_Input_Label" htmlFor='password-input'>Password</label>
                    <input className="Login_Input_Text" type='password' id='password-input' placeholder="Password" onChange={handlePasswordChange} value={password}/>
                    <label htmlFor='remember-me-input' className="Login_Remember_Checkbox_Label">
                        Remember Me
                        <input type='checkbox' name='remember-me-input' id='remember-me-input' className="Login_Remember_Checkbox" />
                    </label>
                    <button type='submit' className="Login_Login_Button" onClick={handleLogin}>Submit</button>
                </div>
            </div>
        </div>
    );
}

export default LoginModal;