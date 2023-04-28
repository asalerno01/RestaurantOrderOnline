import React, { useState, useRef, useEffect } from 'react';
import axios from 'axios';
import { Navigate, useLocation } from 'react-router-dom';
import Logo from '../../imgs/logo.jpg';
import RegisterStyles from './css/Register.module.css';

const Register = () => {

    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const firstNameRef = useRef();
    const lastNameRef = useRef();
    const emailRef = useRef();
    const phoneNumberRef = useRef();
    const passwordRef = useRef();
    const confirmPasswordRef = useRef();

    const errRef = useRef();
    const [errMsg, setErrMsg] = useState("");

    useEffect(() => {
        setErrMsg("");
        // dont want to show the error message after its been seen
    }, []);

    // TODO: verify password on submit, maybe even on change...
    // TODO: err ref for failed response account exists for this user
    // TODO: "success, click here to log in"...
    

    const register = async event => {
        event.preventDefault();
        if (firstName.length === 0
            || lastName.length === 0
            || email.length === 0
            || phoneNumber.length < 10
            || password.length === 0) {
                console.log("missing data");
                return;
            }
        if (password !== confirmPassword) {
            console.log("bad password");
            return;
        }
        axios.post("https://localhost:7089/api/auth/register", {
            firstName: firstName,
            lastName: lastName,
            email: email,
            phoneNumber: phoneNumber,
            password: password
        })
        .then(res => {
            console.log(res);
        })
        .catch(err => {
            console.log(err);
        });
    }

    const handleFirstNameChange = event => { setFirstName(event.target.value); }
    const handleLastNameChange = event => { setLastName(event.target.value); }
    const handleEmailChange = event => { setEmail(event.target.value); }
    const handlePhoneNumberChange = event => {
        if (/^\d{0,10}$/.test(event.target.value)
            || /^\d{3}-\d{0,7}$/.test(event.target.value)
            || /^\d{3}-\d{3}-\d{0,4}$/.test(event.target.value)
            || /^\d{6}-\d{0,4}$/.test(event.target.value)
            || /^\(\d{0,3}-\d{0,3}-\d{0,4}$/.test(event.target.value)
            || event.target.value.length < phoneNumber.length)
            setPhoneNumber(event.target.value);
        console.log("bad phone number input");
    }
    const handlePasswordChange = event => { setPassword(event.target.value); }
    const handleConfirmPasswordChange = event => { setConfirmPassword(event.target.value); }

    return (
        <div className={RegisterStyles.register}>
            <div className={RegisterStyles.container}>
                <div className={RegisterStyles.logo_wrapper}>
                    <img src={Logo} className={RegisterStyles.logo_image} />
                </div>
                <h1 className={RegisterStyles.header}>Create Account</h1>
                <div className={RegisterStyles.form_container}>
                    <div className={RegisterStyles.input_wrapper}>
                        <label className={RegisterStyles.label} htmlFor='firstName'>First Name</label>
                        <input className={RegisterStyles.input} type='text' id='firstName' ref={firstNameRef} onChange={handleFirstNameChange} value={firstName} />
                    </div>
                    <div className={RegisterStyles.input_wrapper}>
                        <label className={RegisterStyles.label} htmlFor='lastName'>Last Name</label>
                        <input className={RegisterStyles.input} type='text' id='lastName' ref={lastNameRef} onChange={handleLastNameChange} value={lastName} />
                    </div>
                    <div className={RegisterStyles.input_wrapper}>
                        <label className={RegisterStyles.label} htmlFor='email'>Email</label>
                        <input className={RegisterStyles.input} type='text' id='email' ref={emailRef} onChange={handleEmailChange} value={email} />
                    </div>
                    <div className={RegisterStyles.input_wrapper}>
                        <label className={RegisterStyles.label} htmlFor='phoneNumber'>Phone Number</label>
                        <input className={RegisterStyles.input} type='text' id='phoneNumber' ref={phoneNumberRef} onChange={handlePhoneNumberChange} value={phoneNumber} />
                    </div>
                    <div className={RegisterStyles.input_wrapper}>
                        <label className={RegisterStyles.label} htmlFor='password'>Password</label>
                        <input className={RegisterStyles.input} type='password' id='password' ref={passwordRef} onChange={handlePasswordChange} value={password} />
                    </div>
                    <div className={RegisterStyles.input_wrapper}>
                        <label className={RegisterStyles.label} htmlFor='confirmPassword'>Confirm Password</label>
                        <input className={RegisterStyles.input} type='password' id='confirmPassword' ref={confirmPasswordRef} onChange={handleConfirmPasswordChange} value={confirmPassword} />
                    </div>
                    <button type='button' className={RegisterStyles.register_button} onClick={register}>Create Account</button>
                </div>
            </div>
        </div>
      );
}

export default Register;