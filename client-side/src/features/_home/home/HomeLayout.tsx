import {Navigate, Outlet} from "react-router-dom";
import NavigationBar from "../../../app/layout/NavigationBar.tsx";

const HomeLayout = () => {
    const isAuthenticated = false;

    return (
        <>
            {isAuthenticated ? (
                <Navigate to={"/"} />
            ) : (
                <>
                    <NavigationBar/>
                    <section className="flex flex-1 justify-center items-center">
                        <Outlet/>
                    </section>
                </>
            )}
        </>
    )
}

export default HomeLayout