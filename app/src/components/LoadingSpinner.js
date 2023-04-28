import React, { CSSProperties } from "react";
import ClipLoader from 'react-spinners/ClipLoader';
import LoadingSpinnerStyles from './css/LoadingSpinner.module.css';

const LoadingSpinner = () => {

    return (
        <div className={LoadingSpinnerStyles.LoadingSpinner}>
            <ClipLoader
                color="rgb(0, 187, 225)"
                size={50}
            />
        </div>
    )
}

export default LoadingSpinner;