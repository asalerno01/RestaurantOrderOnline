import React, { useState } from 'react';
import useAuth from '../../hooks/useAuth';
import CreateReviewStyles from './css/CreateReview.module.css';
import { IoMdClose } from 'react-icons/io';
import { BsStarFill, BsStar } from 'react-icons/bs';
import axios from 'axios';

const CreateReview = ({ setCreateReviewOpen, createReviewOpen }) => {
    const { auth } = useAuth();
    const [message, setMessage] = useState("");
    const [hoverStar, setHoverStar] = useState(-1);
    const handleMessageChange = event => setMessage(event.target.value);

    const [currentStars, setCurrentStars] = useState(5);

    const handleSubmitReview = async event => {
        event.preventDefault();
        await axios({
            method: "POST",
            url: "https://localhost:7089/api/review/create",
            data: {
                "rating": currentStars,
                "message": message,
                "date": new Date().toISOString()
            },
            withCredentials: true
        })
        .then(res => {
            console.log(res);
        })
        .catch(err => {
            console.log(err);
        });
    }
    const handleStarClick = (star) => {
        setHoverStar(star);
        setCurrentStars(star);
    }
    const Stars = () => {
        const stars = [1, 2, 3, 4, 5];
        return (
            <>
            {
                stars.map(star => {
                    if (star <= hoverStar)
                        return <BsStarFill size={"30px"} onClick={() => handleStarClick(star)} onMouseEnter={() => setHoverStar(star)} onMouseLeave={() => setHoverStar(currentStars)} style={{margin: "0 2px", color: "#E8C500", cursor: "pointer"}}/>
                    return <BsStar size={"30px"} onMouseEnter={() => setHoverStar(star)} style={{margin: "0 2px", color: "rgb(71, 71, 71)", cursor: "pointer"}}/>
                })
            }
            </>
        );
    }
    if (!createReviewOpen) return <></>
    return (
        <div className={CreateReviewStyles.backdrop} onClick={() => setCreateReviewOpen(false)}>
            <div className={CreateReviewStyles.container} onClick={e => e.stopPropagation()}>
                <div className={CreateReviewStyles.close_button_wrapper}>
                    <button type="button" className={CreateReviewStyles.close_button} onClick={() => setCreateReviewOpen(false)}><IoMdClose size={"1.5em"}/></button>
                </div>
                <h1 className={CreateReviewStyles.header}>Add a Review</h1>
                <div className={CreateReviewStyles.content_container}>
                    <span>Anthony S.</span>
                    <div className={CreateReviewStyles.stars_wrapper}>
                        <Stars />
                    </div>
                    <textarea className={CreateReviewStyles.input} rows="4" value={message} onChange={handleMessageChange} id="message" placeholder="Helpful reviews mention specific items and describe their quality and taste."/>
                </div>
                <span className={CreateReviewStyles.min_characters}>Min characters: 10</span>
                <button type="button" className={CreateReviewStyles.submit_button} onClick={handleSubmitReview}>Submit</button>
            </div>
        </div>
    )
}

export default CreateReview;