import React, { useState } from 'react';
import css from './Home.module.css';
import {Link} from "react-router-dom";
import myImage from '../../imgs/logo.png';
import coverImg from '../../imgs/food.png';

const Home = () => {

    return (
        <div>
            <header className={css.header}>
                <img className={css.logo} src={myImage} alt="My Image" />
                <h3  className={css.number}> 630-383-8983</h3>
                <h3  className={css.address}>197 E Veterans Pkwy - Yorkville, Illinois </h3>
            </header>

            <nav className={css.navbar}>
                <ul>
                    <li>
                        <Link to={'/salerno/home'}>Home</Link>
                    </li>
                    <li>
                        <Link to={'/salerno/order'}>Online Ordering</Link>
                    </li>

                </ul>
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