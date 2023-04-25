import React, { useState } from 'react';
import css from './css/Home.css'
import {Link} from "react-router-dom";
import myImage from '../../imgs/logo.png';

const Home = () => {

    return (
        <div>
            <header className={css.header}>
                <img className={css.logo} src={myImage} alt="My Image" />
            </header>

            <nav className={css.navbar}>
                <ul>
                    <li>
                        <Link to={'/salerno/home'}>Home</Link>
                    </li>
                    <li>
                        <Link to={'/salerno/order'}>Online Ordering</Link>
                    </li>
                    <li>

                    </li>

                </ul>
            </nav>
        </div>
    )
}

export default Home;