import React, { useRef, useState, useEffect } from 'react';
import CreditDebitModalStyles from './css/CreditDebitModal.module.css';
import { IoMdClose } from 'react-icons/io';

const CreditDebitModal = ({ inputData, setInputData, creditDebitModalIsOpen, setCreditDebitModalIsOpen }) => {
    const [cardDetails, setCardDetails] = useState("");
    const [cvc, setCVC] = useState("");
    const [expiration, setExpiration] = useState("");
    const [zipCode, setZipCode] = useState("");
    const [cardErrorMessage, setCardErrorMessage] = useState("Bad Card");
    const [expirationErrorMessage, setExpirationErrorMessage] = useState("");
    const [cvcErrorMessage, setCVCErrorMessage] = useState("");
    const cardInvalidMessageRef = useRef();
    const expirationInvalidMessageRef = useRef();
    const cvcInvalidMessageRef = useRef();

    useEffect(() => {
        setCardErrorMessage("");
    }, [cardDetails]);
    useEffect(() => {
        setExpirationErrorMessage("");
    }, [expiration]);
    useEffect(() => {
        setCVCErrorMessage("");
    }, [cvc]);

    const handleCardDetailsChange = event => {
        if (!isNaN(event.target.value) || event.target.value < cardDetails)
            if (event.target.value.length < 12)
                setCardDetails(event.target.value)
    }
    const handleCVCChange = event => {
        if (!isNaN(event.target.value) || event.target.value < cardDetails)
            if (event.target.value.length < 5)
                setCVC(event.target.value)
    }
    const handleExpirationChange = event => {
        if (event.target.value.match(/^\d{2} \/ \d{2}$/)) {
            return setExpiration(event.target.value)
        } else if (event.target.value.match(/^\d{2}\/\d{2}$/)) {
            return setExpiration(event.target.value.substring(0, 2) + " / " + event.target.value.substring(3, 5));
        } else if (event.target.value < expiration) {
            console.log("Were backspacing. Less than expiration=>" + event.target.value + "<" + expiration);
            if (event.target.value.length > 7) {
                return setExpiration("");
            }
            if (event.target.value.length === 4) {
                return setExpiration(event.target.value[0]);
            } else {
                return setExpiration(event.target.value);
            }
        } else if (event.target.value[(event.target.value.length - 1)] !== " " && event.target.value[(event.target.value.length - 1)] !== ".") {
            if (event.target.value.length > 7) {
                return;
            }
            if (expiration.length === 1) {
                if (event.target.value[1] === "/" && event.target.value[0] !== "0") {
                    return setExpiration("0" + event.target.value[0] + " / ");
                } else if (event.target.value[1] === "0" && event.target.value[0] === "0") {
                    return;
                } else if (!isNaN(event.target.value)) {
                    return setExpiration(event.target.value + " / ");
                }
            } else if (expiration.length === 0) {
                if (!isNaN(event.target.value)) {
                    return setExpiration(event.target.value);
                }
            } else if (expiration.length === 5) {
                if (!isNaN(event.target.value[5]) && Number(event.target.value[5]) < 4) {
                    return setExpiration(event.target.value);
                }
            } else if (expiration.length === 6) {
                if (!isNaN(event.target.value[6])) {
                    if (event.target.value[5] === "0") {
                        if (event.target.value[6] !== "0") {
                            return setExpiration(event.target.value);
                        }
                    } else if (event.target.value[5] === "3") {
                        if (Number(event.target.value[6]) < 2) {
                            return setExpiration(event.target.value);
                        }
                    } else {
                        return setExpiration(event.target.value);
                    }
                }
            }
        }
        return;
    }
    const handleZipCodeChange = event => {
        if (!isNaN(event.target.value) || event.target.value < cardDetails)
            setZipCode(event.target.value)
    }
    const handleAddCard = event => {
        let temp = Object.assign({}, inputData);
        temp.paymentType = "CreditDebit";
        setInputData(temp);
        setCreditDebitModalIsOpen(false);

    }
    if (!creditDebitModalIsOpen) return <></>
    return (
        <div className={CreditDebitModalStyles.backdrop} onClick={() => setCreditDebitModalIsOpen(false)}>
            <div className={CreditDebitModalStyles.container} onClick={e => e.stopPropagation()}>
                <button type="button" className={CreditDebitModalStyles.close_button}><IoMdClose size={"1.5em"}/></button>
                <h1 className={CreditDebitModalStyles.header}>Add New Card</h1>
                <div className={CreditDebitModalStyles.content_container}>
                    <div className={CreditDebitModalStyles.input_wrapper}>
                        <label className={CreditDebitModalStyles.label} htmlFor="card-details">Card Details</label>
                        <input type="text" className={CreditDebitModalStyles.input} value={cardDetails} onChange={handleCardDetailsChange} id="card-details" placeholder="XXXX XXXX XXXX XXXX"/>
                        <span className={CreditDebitModalStyles.error_message} ref={cardInvalidMessageRef}>{cardErrorMessage}</span>
                    </div>
                    <div className={CreditDebitModalStyles.input_wrapper}>
                        <label className={CreditDebitModalStyles.label} htmlFor="cvc">CVC</label>
                        <input type="text" className={CreditDebitModalStyles.input} value={cvc} onChange={handleCVCChange} id="cvc" placeholder="CVC"/>
                        <span className={CreditDebitModalStyles.error_message} ref={cvcInvalidMessageRef}>{cvcErrorMessage}</span>
                    </div>
                    <div className={CreditDebitModalStyles.input_wrapper}>
                        <label className={CreditDebitModalStyles.label} htmlFor="expiration">Expiration</label>
                        <input type="text" className={CreditDebitModalStyles.input} value={expiration} onChange={handleExpirationChange} id="expiration" placeholder="MM / YY"/>
                        <span className={CreditDebitModalStyles.error_message} ref={expirationInvalidMessageRef}>{expirationErrorMessage}</span>
                    </div>
                    <div className={CreditDebitModalStyles.input_wrapper}>
                        <label className={CreditDebitModalStyles.label} htmlFor="zip-code">Zip Code</label>
                        <input type="text" className={CreditDebitModalStyles.input} value={zipCode} onChange={handleZipCodeChange} id="zip-code" placeholder="Zip Code"/>
                    </div>
                </div>
                <div className={CreditDebitModalStyles.button_container}>
                    <button type="button" className={CreditDebitModalStyles.back_button} onClick={() => setCreditDebitModalIsOpen(false)}>Back</button>
                    <button type="button" className={CreditDebitModalStyles.add_card_button} onClick={handleAddCard}>Add Card</button>
                </div>
            </div>
        </div>
    )
}

export default CreditDebitModal;