import "./loader.css"

const Loader = () => {
    return (
        <div className="typewriter-container">
            <div className="typewriter">
                <div className="slide">
                    <i></i>
                </div>
                <div className="paper"></div>
                <div className="keyboard"></div>
            </div>
            <div className="loader-text">Loading the app just for you :)</div>
        </div>
    )
}

export default Loader