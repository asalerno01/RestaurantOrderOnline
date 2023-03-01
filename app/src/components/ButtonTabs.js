import { useNavigate } from "react-router-dom";
import './css/buttontabs.css';

const ButtonTabs = ({guid}) => {
    const navigate = useNavigate();
    console.log(window.location.href);

    const GeneralButton = () => {
        if (window.location.href.match('localhost:3000\/salerno\/items\/.*\/edit') || window.location.href.match('localhost:3000\/salerno\/items\/.*\/edit'))
            return (
                <div className='ButtonTabs_Current ButtonTabs_General'>
                    <span className='ButtonTabs_Span_Text'>General</span>
                </div>
            )
        return (
            <div className='ButtonTabs_Not_Current ButtonTabs_General'>
                <a href={`/salerno/items/${guid}/edit`} className="ButtonTabs_Link">General</a>
            </div>
        )
    }
    const ModifiersButton = () => {
        if (window.location.href.match('localhost:3000\/salerno\/items\/.*\/modifiers'))
            return (
                <div className='ButtonTabs_Current ButtonTabs_Modifiers'>
                    <span className='ButtonTabs_Span_Text'>Modifiers</span>
                </div>
            )
        return (
            <div className='ButtonTabs_Not_Current ButtonTabs_Modifiers'>
                <a href={`/salerno/items/${guid}/modifiers`} className="ButtonTabs_Link">Modifiers</a>
            </div>
        )
    }
    const LabelPrintingButton = () => {
        return (
            <div className='ButtonTabs_Not_Current ButtonTabs_LabelPrinting'>
                <span className='ButtonTabs_Span_Text'>Label Printing</span>
            </div>
        )
    }

    return (
        <>
            <GeneralButton />
            <ModifiersButton />
            <LabelPrintingButton />
        </>
    )
}

export default ButtonTabs;