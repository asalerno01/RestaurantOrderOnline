import React from 'react';
import CheeseDogImage from "../imgs/items/cheesedog (Small).jpeg";
import ChicagoPolishImage from "../imgs/items/chicagopolish (Small).jpeg";
import ChickenImage from "../imgs/items/chicken (Small).jpeg";
import ChiliCheeseDogImage from "../imgs/items/chilicheesedog (Small).jpeg";
import ChiliDogImage from "../imgs/items/chilidog (Small).jpeg";
import CornDogImage from "../imgs/items/corndog (Small).jpeg";
import DogDealImage from "../imgs/items/dogdeal (Small).jpeg";
import FriesImage from "../imgs/items/fries (Small).jpeg";
import ItalianBeefImage from "../imgs/items/italianbeef (Small).jpeg";
import JumboImage from "../imgs/items/jumbo (Small).jpeg";
import MaxwellPolishImage from "../imgs/items/maxwellpolish (Small).jpeg";
import PizzaPuffImage from "../imgs/items/pizzapuff (Small).jpeg";
import TomTomTamaleImage from "../imgs/items/tamale (Small).jpeg";
import PlaceholderImage from "../imgs/items/placeholder_image.png";
import HotDogImage from "../imgs/items/hotdog.webp";

import CokeImage from "../imgs/items/soda/coke.jpeg";
import DietCokeImage from "../imgs/items/soda/diet-coke.jpg";
import SpriteImage from "../imgs/items/soda/sprite.webp";
import PepsiImage from "../imgs/items/soda/pepsi.webp";
import MountainDewImage from "../imgs/items/soda/mountain-dew.webp";
import OrangeCrushImage from "../imgs/items/soda/orange-crush.jpeg";
import GrapeCrushImage from "../imgs/items/soda/grape-crush.jpeg";
import DrPepperImage from "../imgs/items/soda/dr-pepper.jpeg";
import DietMountainDewImage from "../imgs/items/soda/diet-mountain-dew.webp";
import DasaniImage from "../imgs/items/soda/dasani.jpeg";

const ItemImage = ({ itemName }) => {
    function getImage(name) {
        switch(name) {
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
            case "Chicago Style Hot Dog":
                return HotDogImage;
            case "Double Dog":
                return HotDogImage;
            case "Coke":
                return CokeImage;
            case "Sprite":
                return SpriteImage;
            case "Diet Coke":
                return DietCokeImage;
            case "Orange Crush":
                return CokeImage;
            case "Pepsi":
                return PepsiImage;
            case "Mountain Dew":
                return MountainDewImage;
            case "Diet Mountain Dew":
                return DietMountainDewImage;
            case "Dasani Water":
                return DasaniImage;
            default:
                return PlaceholderImage;
        }
    }
    const image = getImage(itemName);
    // if (image === null) return <></>;
    return (
        <img loading="lazy" src={image} alt={`${itemName} Image`} />
    )
}

export default ItemImage;