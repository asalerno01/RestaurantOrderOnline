import React from 'react';
import css from './Home.module.css';
import { Link } from "react-router-dom";
import Logo from '../../imgs/logo.jpg';
import coverImg from '../../imgs/food.png';
import Button_Overlay from '../../imgs/button_style.webp';

const Home = () => {
    return (
        <div>
            <header className={css.header}>
                    <img className={css.logo} src={Logo} alt="Salerno's Red Hots Logo" />
                <div className={css.header_details}>
                    <h3  className={css.number}>630-383-8983</h3>
                    <h3  className={css.address}>197 E Veterans Pkwy - Yorkville, Illinois </h3>
                </div>
            </header>

            <nav className={css.navbar}>
                <div>
                    <div className={css.nav_button_wrapper}>
                            <div className={css.nav_button_image_wrapper}>
                                <img src={Button_Overlay} alt="button image overlay" className={css.button_image}/>
                            </div>
                        <button type="button" className={css.nav_button}>Order Now</button>
                    </div>
                </div>
                <div>
                    <div className={css.nav_button_wrapper}>
                            <div className={css.nav_button_image_wrapper}>
                                <img src={Button_Overlay} alt="button image overlay" className={css.button_image}/>
                            </div>
                        <button type="button" className={css.nav_button}>Order Now</button>
                    </div>
                </div>
                <button type="button" className={css.nav_button} style={{ marginLeft: "auto" }}>Register</button>
                <div>
                    <div className={css.nav_button_wrapper}>
                            <div className={css.nav_button_image_wrapper}>
                                <img src={Button_Overlay} alt="button image overlay" className={css.button_image}/>
                            </div>
                        <button type="button" className={css.nav_button}>Login</button>
                    </div>
                </div>
            </nav>
            <section>
                <img className={css.food} src={coverImg} alt="My Image" />
            </section>
            <footer className ={css.footer}>
                <p> Salerno's Red Hots | Copyright 2011-2023 | All Rights Reserved</p>
            </footer>
        </div>
    )
}

export default Home;