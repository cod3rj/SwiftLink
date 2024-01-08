import React, {useState} from "react";
import axios from "axios";
import {useAuth} from "../../../app/hooks/AuthContext.tsx";

const HomePage = () => {
    const [input, setInput] = useState("")
    const [response, setResponse] = useState("")
    const [showCopyNotification, setShowCopyNotification] = useState(false)
    const { isAuthenticated } = useAuth();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setInput(e.target.value)
    }

    const handleCopy = () => {
        // Copy the URL to the clipboard
        navigator.clipboard.writeText(response)
        // Show the copy notification
        setShowCopyNotification(true)
        // Hide the notification after 2 seconds
        setTimeout(() => setShowCopyNotification(false), 2000)
    }

    const handleSubmit = async (urlType: string) => {
        try {
            console.log('URL Type:', urlType);
            let endpoint = "";

            if (urlType === 'authenticated')
            {
                endpoint = 'http://localhost:5000/url'; // Default to the authenticated and limited version
            }

            if (urlType === 'unauthenticated') {
                endpoint = 'http://localhost:5000/free/url'; // Use the unauthenticated version
            }

            const options = {
                method: 'POST',
                url: endpoint,
                data: { OriginalUrl: input },
            };

            const response = await axios(options);
            setResponse(response.data);
        } catch (error) {
            console.error('Error:', error);
        }
    }

    return (
        <>
            <div className="glass-container mt-[6%] flex flex-col gap-12">
                <h1 className="text-3xl font-bold w-[50%] text-center">SwiftLink: Elevate Your Links, Simplify Your
                    Sharing. Your Swift
                    Journey
                    Starts Here!</h1>
                <div className="flex flex-col gap-12 w-1/2">
                    <input className="p-2 rounded-xl text-dark-1" type="text" name="OriginalUrl" id="OriginalUrl" value={input}
                           onChange={handleChange}/>

                    {isAuthenticated ? (
                        <button
                            className="border-2 p-3 text-white hover:bg-cyan-300 hover:text-dark-1 transition"
                            onClick={() => handleSubmit('authenticated')}
                        >
                            Generate Custom URL (Authenticated)
                        </button>
                    ) : (
                        <button
                            className="border-2 p-3 text-white hover:bg-cyan-300 hover:text-dark-1 transition"
                            onClick={() => handleSubmit('unauthenticated')}
                        >
                            Generate Custom URL
                        </button>
                    )}
                </div>
            {response && (
                <div className="flex flex-col gap-5 w-1/2">
                    <label className="text-center text-2xl font-bold">Your Tiny URL</label>
                    <div className="flex items-center w-full justify-center">
                            <input
                                className="p-2 rounded-xl text-dark-1 w-full"
                                type="text"
                                name="output"
                                id="output"
                                value={response}
                                readOnly // Set the field as readonly
                            />
                            <button
                                className="ml-2 p-2 rounded-md text-white bg-blue-500 hover:bg-blue-600"
                                onClick={handleCopy}
                            >
                                Copy
                            </button>
                        </div>
                        {showCopyNotification && (
                            <div className="text-green-500 text-sm mt-2">
                                URL Copied to Clipboard!
                            </div>
                        )}
                    </div>
                )}
            </div>
        </>
    )
}

export default HomePage