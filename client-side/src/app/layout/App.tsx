import './global.css'
import {Route, Routes} from "react-router-dom";
import HomeLayout from "../../features/_home/home/HomeLayout.tsx";
import HomePage from "../../features/_home/home/HomePage.tsx";
import LoginPage from "../../features/_home/users/LoginPage.tsx";
import RegisterPage from "../../features/_home/users/RegisterPage.tsx";
import { motion } from 'framer-motion';
import Cursor from "../common/cursor/Cursor.tsx";
import {useEffect, useState} from "react";
import Loader from "../common/loader/Loader.tsx";

const App = () => {
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        // Simulate an asynchronous operation (e.g., fetching data) that takes time
        const fetchData = async () => {
            await new Promise((resolve) => setTimeout(resolve, 4000)) // Simulate a 2-second delay
            setIsLoading(false)
        };

        fetchData()
    }, [])

    return (
        <motion.div initial={{opacity: 0}} animate={{opacity: 1}} transition={{duration: 0.5}}>
            {isLoading ? (
                // Display loader while the app is loading
                <Loader/>
            ) : (
                <main className="flex h-screen">
                    <Cursor />
                    <Routes>
                        {/* public routes */}
                        <Route element={<HomeLayout />}>
                            <Route index element={<HomePage />} />
                            <Route path="/login" element={<LoginPage />} />
                            <Route path="/register" element={<RegisterPage />} />
                        </Route>

                        {/* private routes */}
                    </Routes>
                </main>
            )}
        </motion.div>
    )
}

export default App