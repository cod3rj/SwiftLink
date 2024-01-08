import {Outlet} from "react-router-dom";

const RootLayout = () => {

    return (
        <>
            <section className="flex flex-1 justify-center items-center">
                <Outlet/>
            </section>
        </>
    )
}

export default RootLayout