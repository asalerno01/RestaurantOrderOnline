import React, { useState } from 'react';
import ReviewsStyles from './css/Reviews.module.css';
import { BsStarFill, BsStar } from 'react-icons/bs';
import ItemImage from '../../components/ItemImage';
import { RxCaretRight, RxCaretLeft } from 'react-icons/rx';
import reviews from './testdata.json';
import CreateReview from './CreateReview';

const Reviews = () => {
    const [createReviewOpen, setCreateReviewOpen] = useState(false);

    const ReviewStars = ({ count }) => {
        let stars = [0,1,2,3,4];
        return (
            <span>
                {
                    stars.map((star) => {
                        if (star < count)
                            return (<BsStarFill style={{margin: "0 2px", color: "rgb(71, 71, 71)"}} size={"0.75em"}/>)
                        else
                            return (<BsStar style={{margin: "0 2px"}} size={"0.75em"}/>)
                    })
                }
            </span>
        )
    }
    const getExampleImages = (index) => {
        if (index === 0)
            return (
                <>
                    <div className={ReviewsStyles.items}>
                        <div className={ReviewsStyles.item}>
                            <div className={ReviewsStyles.item_details}>
                                <h6 className={ReviewsStyles.item_title}>Chicken Strips - 5 Piece</h6>
                                <span className={ReviewsStyles.item_price}>$5.61</span>
                                <span className={ReviewsStyles.vote_wrapper}>Disliked</span>
                            </div>
                            <div className={ReviewsStyles.item_image_wrapper}>
                                <ItemImage itemName="Chicken Strips - 5 Piece"/>
                            </div>
                        </div>
                        <div className={ReviewsStyles.item}>
                            <div className={ReviewsStyles.item_details}>
                                <h6 className={ReviewsStyles.item_title}>Fresh Cut Fries - Large</h6>
                                <span className={ReviewsStyles.item_price}>$3.79</span>
                                <span className={ReviewsStyles.vote_wrapper}>Liked</span>
                            </div>
                            <div className={ReviewsStyles.item_image_wrapper}>
                                <ItemImage itemName="Fresh Cut Fries - Large"/>
                            </div>
                        </div>
                    </div>
                    <div className={ReviewsStyles.controls}>
                        <span className={ReviewsStyles.control}>L</span>
                        <span className={ReviewsStyles.control}>R</span>
                    </div>
                </>
            )
        return <></>
    }
    const ReviewItems = () => {
        return (
            <>
                {
                    reviews.map((review, index) => (
                        <div className={ReviewsStyles.review}>
                            <h3 className={ReviewsStyles.review_header}>{review.name}</h3>
                                <div className={ReviewsStyles.review_data}>
                                    <ReviewStars count={review.rating} />
                                    <span>{review.date}</span>
                                </div>
                                <span className={ReviewsStyles.message}>{review.message}</span>
                                {getExampleImages(index)}
                            </div>
                    ))
                }
            </>
        )
    }
    return (
        <div className={ReviewsStyles.reviews}>
            <CreateReview createReviewOpen={createReviewOpen} setCreateReviewOpen={setCreateReviewOpen} />
            <div className={ReviewsStyles.container}>
                <span className={ReviewsStyles.back_button_wrapper}>
                    <a href="/salerno/order"><RxCaretLeft style={{position: "relative", bottom: "1px", verticalAlign: "middle"}}/>Back to Menu</a>
                </span>
                <h1 className={ReviewsStyles.header}>Ratings & Reviews</h1>
                <div className={ReviewsStyles.content_container}>
                    <div className={ReviewsStyles.left}>
                        <h1 className={ReviewsStyles.rating_overall}>4.2 <BsStarFill size={"0.85em"} style={{color: "#E8C500"}}/></h1>
                        <span className={ReviewsStyles.count_summary}>262 ratings, 21 public reviews</span>
                        <button type="button" className={ReviewsStyles.add_review_button} onClick={() => setCreateReviewOpen(true)}>Add Review</button>
                    </div>
                    <div className={ReviewsStyles.right}>
                        <ReviewItems />
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Reviews;