import {Link, useNavigate} from "react-router-dom";
import {useEffect, useState} from "react";
import {GrClose, GrMenu} from "react-icons/gr";
import {useAuth} from "../hooks/AuthContext.tsx";

const NavigationBar = () => {
    const [openMenu, setOpenMenu] = useState(false)
    const [isLoggedOut, setIsLoggedOut] = useState(false)
    const {isAuthenticated, logout} = useAuth()
    const navigate = useNavigate()
    const handleOpenMenu = () => {
        setOpenMenu(!openMenu)
    }

    const handleLogout = async () => {
        try {
            await logout();
            setIsLoggedOut(true);
            navigate("/login");
        } catch (error) {
            console.error("Logout failed:", error.message);
        }
    }

    useEffect(() => {
        setIsLoggedOut(false)
    }, [isAuthenticated])

    return (
        <nav className="flex items-center m-auto fixed top-5 left-1/2 translate-x-[-50%] h-20 bg-opacity-80 bg-white p-4 rounded-xl backdrop-blur-md
                        justify-between w-[89%] shadow-2xl
        ">
            <Link to="/" className="flex items-center">
                <img src="/assets/logo.svg" alt="Logo" className="h-[15%] w-[30%]"/>
                <h1 className="text-2xl font-bold italic uppercase text-gray-700">Swift Link</h1>
            </Link>

            <ul className={`md:flex gap-6 [&>li]:cursor-pointer font-medium absolute md:static top-20 max-md:p-3 text-center
                            ${openMenu ? 'flex flex-col justify-center w-full h-screen bg-opacity-70 bg-white' : 'hidden'}
            `}>
                {isAuthenticated ? (
                    <>
                        <li>
                            <Link
                                to="/dashboard"
                                className="flex align-middle justify-center text-dark-1 text-2xl font-semibold hover:text-gray-700 hover:bg-gray-200 px-4 py-2 rounded transition duration-300 transform hover:scale-105"
                            >
                                DASHBOARD
                            </Link>
                        </li>
                        <li>
                            <button
                                onClick={handleLogout}
                                className="text-dark-1 text-2xl font-semibold hover:text-gray-700 hover:bg-gray-200 px-4 py-2 rounded transition duration-300 transform hover:scale-105"
                            >
                                LOGOUT
                            </button>
                        </li>
                    </>
                ) : (
                    <>
                        <li>
                            <Link
                                to="/login"
                                className="flex text-dark-1 text-2xl font-semibold hover:text-gray-700 hover:bg-gray-200 px-4 py-2 rounded transition duration-300 transform hover:scale-105"
                            >
                                LOGIN
                            </Link>
                        </li>
                        <li>
                            <Link
                                to="/register"
                                className="flex text-dark-1 text-2xl font-semibold hover:text-gray-700 hover:bg-gray-200 px-4 py-2 rounded transition duration-300 transform hover:scale-105"
                            >
                                REGISTER
                            </Link>
                        </li>
                    </>
                    )}

            </ul>
            <div className="md:hidden">
                <button onClick={handleOpenMenu}>
                    {openMenu ? <GrClose size={25}/> : <GrMenu size={25}/>}
                </button>
            </div>
        </nav>
    );
};

export default NavigationBar;
