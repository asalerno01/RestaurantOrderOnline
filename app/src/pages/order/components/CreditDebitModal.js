import React, { useRef, useState, useEffect } from 'react';
import CreditDebitModalStyles from './css/CreditDebitModal.module.css';
import { IoMdClose } from 'react-icons/io';

const CreditDebitModal = ({ setCreditDebit, creditDebitModalIsOpen, setCreditDebitModalIsOpen }) => {
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
        if (!isNaN(event.target.value) || event.target.value < cardDetails)
            if (event.target.value.length < 5 || event.target.value < cardDetails)
                setExpiration(event.target.value)
    }
    const handleZipCodeChange = event => {
        if (!isNaN(event.target.value) || event.target.value < cardDetails)
            setZipCode(event.target.value)
    }
    const handleAddCard = event => {
        event.preventDefault();
        if (!(/^\d{12}/.test(cardDetails))) setCardErrorMessage("Invalid card number");

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