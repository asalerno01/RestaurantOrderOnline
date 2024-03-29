import { Routes, Route } from 'react-router-dom';
import PageLayout from './container/PageLayout';
import ItemList from './pages/item/ItemList';
import Dashboard from './pages/dashboard/Dashboard';
import Edit from './pages/item/Edit';
import Modifiers from './pages/item/Modifiers';
import TimeClock from './pages/staff/TimeClock';
import NewEmployee from './pages/staff/NewEmployee';
import EmployeeList from './pages/staff/EmployeeList';
import EditEmployee from './pages/staff/EditEmployee';
import Login from './pages/user_pages/Login';
import Order from './pages/order/Order';
import OrderItem from './pages/order/OrderItem';
import Orders from './pages/order/Backoffice/Orders';
import Checkout from './pages/order/Checkout';
import Register from './pages/user_pages/Register';
import RememberLogin from './components/RememberLogin';
import Reviews from './pages/review/Reviews';
import Home from './pages/home/Home';
import Reports from './pages/order/Backoffice/Reports';
import Report from './pages/report/Report';

function App() {
    return (
        <Routes>
            <Route path="/" element={<Login />}/>
            <Route path="salerno/home" element={<Home />} />
            <Route path="salerno/login" element={<Login />} />
            <Route path="salerno/register" element={<Register />} />
            <Route path="salerno/reviews" element={<Reviews />}/>
            <Route element={<RememberLogin />}>
                <Route path="salerno" element={<Dashboard />}>
                    <Route element={<PageLayout />}>
                        <Route path='order' element={<Order />} />
                        <Route path='orders' element={<Orders />} />
                        <Route path='order/item' element={<OrderItem />} />
                        <Route path='order/checkout' element={<Checkout />} />
                        <Route path='items' element={<ItemList />} />
                        <Route path='items/:itemId/edit' element={<Edit />} />
                        <Route path='items/new' element={<Edit />} />
                        <Route path='items/:itemId/modifiers' element={<Modifiers />} />

                        <Route path='home' element={<Home />} />
                        <Route path='employees' element={<EmployeeList />} />
                        <Route path='employees/timeclock' element={<TimeClock />} />
                        <Route path='employees/new' element={<NewEmployee />} />
                        <Route path='employees/:id/edit' element={<EditEmployee />} />
                        
                        <Route path='reports' element={<Reports />} />
                        <Route path='report' element={<Report />} />
                    </Route>
                </Route>
            </Route>
        </Routes>
    )
}

export default App;

            
            /* <Route path='/' element={<Layout />}>
                <Route index element={<Public />} />
                <Route path='login' element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="unauthorized" element={<Unauthorized />} />
                <Route path="sessionexpired" element={<SessionExpired />}/>
                <Route element={<RememberLogin />}>
                    <Route element={<Status />}>
                        <Route element={<RequireAuth allowedEmployeeType={["Employee", "Manager"]}/>}>
                            <Route path="dashboard" element={<Dashboard />}>
                                <Route index element={<Welcome />} />
                                <Route path="newmanager" element={<NewManager />}/>
                                <Route element={<RequestContainer />}>
                                    <Route path="pending" element={<PendingRequests />}/>
                                    <Route path="closed" element={<ClosedRequests />}/>
                                    <Route path="create" element={<CreateRequestWithCalendar />}/>
                                    <Route path="manager" element={<RequestManager />}/>
                                </Route>
                            </Route>
                        </Route>
                        
                    </Route>
                </Route>
            </Route> */