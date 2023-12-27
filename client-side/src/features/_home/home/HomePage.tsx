
const HomePage = () => {
    return (
        <>
            <div className="glass-container mt-[6%] flex flex-col gap-12">
                <h1 className="text-3xl font-bold w-[50%] text-center">SwiftLink: Elevate Your Links, Simplify Your Sharing. Your Swift
                    Journey
                    Starts Here!</h1>
                <div className="flex flex-col gap-12 w-1/2">
                    <input className="p-2 rounded-xl text-dark-1"/>
                    <button className="border-2 p-3 text-white hover:bg-cyan-300 hover:text-dark-1 transition">
                        Generate Custom URL!
                    </button>
                </div>
            </div>
        </>
    )
}

export default HomePage