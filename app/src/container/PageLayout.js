import { Outlet, useNavigate } from "react-router-dom";
import './css/pagelayout.css';

const PageLayout = () => {
    const navigate = useNavigate();
    const PendingButton = () => {
        if (window.location.href === 'http://localhost:3000/dashboard/pending')
            return (<div className='request-button-current'>Pending</div>)
        return (<div className='request-button-non-current' onClick={() => navigate('/dashboard/pending')}>Pending</div>)
    }
    const ClosedButton = () => {
        if (window.location.href === 'http://localhost:3000/dashboard/closed')
            return (<div className='request-button-current'>Closed</div>)
        return (<div className='request-button-non-current' onClick={() => navigate('/dashboard/closed')}>Closed</div>)
    }
    const CreateButton = () => {
        if (window.location.href === 'http://localhost:3000/dashboard/create')
            return (<div className='request-button-current'>Create</div>)
        return (<div className='request-button-non-current' onClick={() => navigate('/dashboard/create')}>Create</div>)
    }
    const ManageButton = () => {
        if (window.location.href === 'http://localhost:3000/dashboard/manager')
            return (<div className='request-button-current'>Manage Requests</div>)
        return (<div className='request-button-non-current' onClick={() => navigate('/dashboard/manager')}>Manage Requests</div>)
    }
    return (
        <Outlet />
    )
}

export default PageLayout;