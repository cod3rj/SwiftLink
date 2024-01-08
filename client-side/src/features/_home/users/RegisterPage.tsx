import {Link, useNavigate} from "react-router-dom";
import {useAuth} from "../../../app/hooks/AuthContext.tsx";

const RegisterPage = () => {
    const {register} = useAuth()
    const navigate = useNavigate()

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()

        const email = event.currentTarget.email.value;
        const username  = event.currentTarget.userName.value;
        const displayname = event.currentTarget.displayName.value;
        const password = event.currentTarget.password.value;

        await register(email, password, username, displayname)
        // Redirect to a protected route after successful login
        navigate("/auth/home")
    }

    return (
        <section className="w-full">
            <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
                <Link to="/" className="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-white">
                    <img className="w-30 h-50 mb-[-60px]" src="/assets/logo.svg" alt="logo"/>
                </Link>
                <div
                    className="w-full glass-container h-[60%] rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
                    <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                        <h1 className="text-xl font-bold leading-tight tracking-tight text-white-900 md:text-2xl dark:text-white">
                            Create and account
                        </h1>
                        <form onSubmit={handleSubmit} className="space-y-4 md:space-y-6" action="#">
                            <div>
                                <label htmlFor="email"
                                       className="block mb-2 text-sm font-medium text-white-900 dark:text-white">Email</label>
                                <input type="email" name="email" id="email"
                                       className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                       placeholder="name@company.com" required=""/>
                            </div>
                            <div>
                                <label htmlFor="userName" className="block mb-2 text-sm font-medium text-white-900 dark:text-white">
                                    Username
                                </label>
                                <input
                                    type="text"
                                    name="userName"
                                    id="userName"
                                    placeholder="TestUser001"
                                       className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                       required=""/>
                            </div>
                            <div>
                                <label htmlFor="displayName" className="block mb-2 text-sm font-medium text-white-900 dark:text-white">
                                    Display Name
                                </label>
                                <input
                                    type="text"
                                    name="displayName"
                                    id="displayName"
                                    placeholder="Test User 001"
                                       className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                       required=""/>
                            </div>
                            <div>
                                <label htmlFor="password"
                                       className="block mb-2 text-sm font-medium text-white-900 dark:text-white">
                                    Password
                                </label>
                                <input
                                    type="password"
                                    name="password"
                                    id="password"
                                    placeholder="••••••••"
                                    className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                    required=""/>
                            </div>


                            <button type="submit"
                                    className="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">Create
                                an account
                            </button>
                            <p className="text-sm font-light text-white-500 dark:text-white-400">
                                Already have an account? <Link to="/login"
                                                               className="font-medium text-primary-600 hover:underline dark:text-primary-500">Login
                                here</Link>
                            </p>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    )
}

export default RegisterPage