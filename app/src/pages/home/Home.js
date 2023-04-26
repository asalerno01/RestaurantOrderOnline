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
                <h2  className={css.number}> 630-383-8983</h2>
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
            <footer>
                
            </footer>
        </div>
    )
}

export default Home;