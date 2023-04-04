import React from 'react';
import './orderdetails.css';
import OrderItemSummaryStyles from './css/OrderItemSummary.module.css';

import CheeseDogImage from "../../imgs/items/cheesedog (Small).jpeg";
import ChicagoPolishImage from "../../imgs/items/chicagopolish (Small).jpeg";
import ChickenImage from "../../imgs/items/chicken (Small).jpeg";
import ChiliCheeseDogImage from "../../imgs/items/chilicheesedog (Small).jpeg";
import ChiliDogImage from "../../imgs/items/chilidog (Small).jpeg";
import CokeCanImage from "../../imgs/items/coke_can (Small).jpeg";
import CornDogImage from "../../imgs/items/corndog (Small).jpeg";
import DogDealImage from "../../imgs/items/dogdeal (Small).jpeg";
import FriesImage from "../../imgs/items/fries (Small).jpeg";
import ItalianBeefImage from "../../imgs/items/italianbeef (Small).jpeg";
import JumboImage from "../../imgs/items/jumbo (Small).jpeg";
import MaxwellPolishImage from "../../imgs/items/maxwellpolish (Small).jpeg";
import PizzaPuffImage from "../../imgs/items/pizzapuff (Small).jpeg";
import TomTomTamaleImage from "../../imgs/items/tamale (Small).jpeg";
import PlaceholderImage from "../../imgs/items/placeholder_image.png";
import HotDogImage from "../../imgs/items/hotdog.webp";
import OrderDetailsModifiers from './OrderDetailsModifiers';

const OrderItemSummary = ({ order, handleItemClick, handleRemoveItemClick, type }) => {
    function getImage(name) {
        switch(name) {
            case "Chicago Style Hot Dog":
                return HotDogImage;
            case "Cheese Dog":
                return CheeseDogImage;
            case "Chicago Polish":
                return ChicagoPolishImage;
            case "Chicken Strips - 3 Piece":
                return ChickenImage;
            case "Chicken Strips - 5 Piece":
                return ChickenImage;
            case "Chili Cheese Dog":
                return ChiliCheeseDogImage;
            case "Chili Dog":
                return ChiliDogImage;
            case "12 oz Can":
                return CokeCanImage;
            case "Corn Dog":
                return CornDogImage;
            case "2 DOG DEAL- 2 Dogs Fries & 12oz Can -":
                return DogDealImage;
            case "Fresh Cut Fries - Regular":
                return FriesImage;
            case "Fresh Cut Fries - Large":
                return FriesImage;
            case "Italian Beef Sandwich":
                return ItalianBeefImage;
            case "Jumbo Dog":
                return JumboImage;
            case "Maxwell Street Polish":
                return MaxwellPolishImage;
            case "Chicago Pizza Puff":
                return PizzaPuffImage;
            case "Tom Tom Tamale":
                return TomTomTamaleImage;
            default:
                return PlaceholderImage;
        }
    }
    return (
        <>
        {
            order.orderItems.map((orderItem, index) => (
                <div key={index}>
                    <div className={OrderItemSummaryStyles.item_card}>
                        <button className={OrderItemSummaryStyles.item_button} onClick={() => handleItemClick(orderItem.itemId, index)}>
                            <div className={OrderItemSummaryStyles.item_image_wrapper}>
                                <img className={OrderItemSummaryStyles.item_image} src={getImage(orderItem.name)} alt={`${orderItem.name}`} />
                            </div>
                            <div className={OrderItemSummaryStyles.item_details}>
                                <h3 className={OrderItemSummaryStyles.item_header}>{orderItem.name}</h3>
                                <OrderDetailsModifiers 
                                    groups={orderItem.modifier.groups} 
                                    addons={orderItem.modifier.addons} 
                                    noOptions={orderItem.modifier.noOptions} 
                                />
                                <span className={OrderItemSummaryStyles.item_price}>{`$${orderItem.price}`}</span>
                            </div>
                            <div className={OrderItemSummaryStyles.delete_button_wrapper} onClick={e => e.stopPropagation()}>
                                <span className={OrderItemSummaryStyles.delete_button} onClick={() => handleRemoveItemClick(index)}>Remove</span>
                            </div>
                        </button>
                    </div>
                    <div className={OrderItemSummaryStyles.border}></div>
                </div>
            ))
        }
        </>
    )
}

export default OrderItemSummary;