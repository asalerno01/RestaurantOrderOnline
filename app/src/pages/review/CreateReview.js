import React, { useState } from 'react';
import useAuth from '../../hooks/useAuth';
import CreateReviewStyles from './css/CreateReview.module.css';
import { IoMdClose } from 'react-icons/io';
import { BsStarFill, BsStar } from 'react-icons/bs';

const CreateReview = ({ setCreateReviewOpen, createReviewOpen }) => {
    const { auth } = useAuth();
    const [message, setMessage] = useState("");
    const [hoverStar, setHoverStar] = useState(5);
    const handleMessageChange = event => setMessage(event.target.value);

    const [currentStars, setCurrentStars] = useState(5);

    const Stars = () => {
        const stars = [1, 2, 3, 4, 5];
        return (
            <>
            {
                stars.map(star => {
                    if (star <= hoverStar)
                        return <BsStarFill size={"30px"} onClick={() => setHoverStar(star)} onMouseEnter={() => setHoverStar(star)} onMouseLeave={() => setHoverStar(5)} style={{cursor: "pointer"}}/>
                    return <BsStar size={"30px"} onMouseEnter={() => setHoverStar(star)} style={{cursor: "pointer"}}/>
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
                <button type="button" className={CreateReviewStyles.submit_button}>Submit</button>
            </div>
        </div>
    )
}

export default CreateReview;