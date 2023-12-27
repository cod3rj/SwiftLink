import { Link } from "react-router-dom";
import {useState} from "react";
import {GrClose, GrMenu} from "react-icons/gr";

const NavigationBar = () => {

    const [openMenu, setOpenMenu] = useState(false);

    const handleOpenMenu = () => {
        setOpenMenu(!openMenu)
    }

    return (
        <nav className="flex items-center m-auto fixed top-5 left-1/2 translate-x-[-50%] h-20 bg-opacity-100 bg-white p-4 rounded-xl backdrop-blur-md
                        justify-between w-[89%] shadow-2xl
        ">
            <Link to="/" className="flex items-center">
                <img src="/assets/logo.svg" alt="Logo" className="h-[15%] w-[30%]"/>
                <h1 className="text-2xl font-bold italic uppercase text-gray-700">Swift Link</h1>
            </Link>

            <ul className={`md:flex gap-12 [&>li]:cursor-pointer font-medium absolute md:static top-20 max-md:p-3 text-center
                            ${openMenu ? 'flex flex-col justify-center w-full h-screen bg-opacity-70 bg-white' : 'hidden'}
            `}>
                <li>
                    <Link
                        to="/"
                        className="text-dark-1 hover:text-gray-700 hover:bg-gray-200 px-4 py-2 rounded transition duration-300 transform hover:scale-105"
                    >
                        Login
                    </Link>
                </li>
                <li>
                    <Link
                        to="/register"
                        className="text-dark-1 hover:text-gray-700 hover:bg-gray-200 px-4 py-2 rounded transition duration-300 transform hover:scale-105"
                    >
                        Register
                    </Link>
                </li>
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
