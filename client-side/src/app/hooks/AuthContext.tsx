import {createContext, useContext, useEffect, useState} from "react"
import axios from "axios"

interface AuthContextProps{
    isAuthenticated: boolean;
    login:(email: string, password: string) => void;
    register: (email: string, password: string, userName: string, displayName: string) => void;
    logout: () => void;
    loading: boolean;
}

interface AuthProviderProps {
    children: React.ReactNode
}

interface User {
    username: string,
    email: string,
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined)


export const  AuthProvider: React.FC<AuthProviderProps> = ({children}) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false)
    const [user, setUser] = useState<User | null>(null)
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        async function getMe() {
            try {
                const response = await axios.get(
                    'http://localhost:5000/api/account/getuser',
                    { withCredentials: true }
                )

                if (response.status === 200) {
                    setUser(response.data)
                    setIsAuthenticated(true)
                    console.log('User is authenticated:', response.data)
                }

                setLoading(false)
            } catch (error) {
                if (error.response && error.response.status === 401) {
                    // Only log if the status is not 401 (Unauthorized)
                    console.log('User is not authenticated');
                } else {
                    // Log other errors
                    console.error('Error:', error);
                }

                setLoading(false)
            }
        }

        getMe()
    }, [])

    const login = async (email: string, password: string) => {
        try {
            await axios.post(
                'http://localhost:5000/api/account/login/',
                { email: email, password: password },
                { withCredentials: true }
            )

            setIsAuthenticated(true)

        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                // Axios error with response (HTTP error)
                console.error('Login failed with HTTP error:', error.response.data);
            } else {
                // Non-Axios error (network error, etc.)
                console.error('Login failed:', error.message);
            }
        }
    }

    const register = async (email: string, password: string, username: string, displayname: string) => {
        try {
            await axios.post(
                'http://localhost:5000/api/account/register/',
                { email: email, password: password, username: username, displayname: displayname },
                { withCredentials: true }
            )

            setIsAuthenticated(true)

        } catch (error) {
            if (axios.isAxiosError(error) && error.response) {
                // Axios error with response (HTTP error)
                console.error('Login failed with HTTP error:', error.response.data);
            } else {
                // Non-Axios error (network error, etc.)
                console.error('Login failed:', error.message);
            }
        }
    }

    const logout = async () => {
        try {
            await axios.post(
                'http://localhost:5000/api/account/logout/',
                {}, // You may need to pass any required data here
                { withCredentials: true }
            )
            // Expire the existing authentication cookie in the frontend
            document.cookie = 'AuthToken=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';
            setIsAuthenticated(false)
        } catch (error) {
            console.error('Logout failed:', error.message)
        }
    }

    return (
        <AuthContext.Provider value={{ isAuthenticated, loading, login, register, logout }}>
            {loading ? <p>Loading...</p> : children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext)

    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider')
    }

    return context
}