
import React, { useState } from "react";
import useApi from "../../apiService/api";
import { useNavigate } from "react-router-dom";
import './Login.css';
import { UserModel } from "../../models/userModel";
import { EnumError, UserService } from "../../apiService/userService";

export default function LoginPage() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");
    const [error, setError] = useState(false);
    const navigate = useNavigate();

    const { postAsync } = useApi();

    const handleLogin = async () => {
        const user = new UserModel(username, password, "");
        
        try {
        const data = await postAsync(UserService.USER_CONTROLLER, UserService.LOGIN, user); // Call the API
        if(data.enumMessage === EnumError.WorngPassword){
            console.log(data.enumMessage)
            setMessage(data.message);
            setError(true);
        }
        else if(data.enumMessage === EnumError.UserFound){
            navigate("/customersList");
        }
        } catch (err) {
        setMessage("Login failed. Please check your credentials.");
        }
    };

    const goToSignUp = () => {
        navigate("/signup");
      };

    return (
        <div className="form-div">
        <h1>Login</h1>
        <div className="form">
                <label>UserName</label>
                <input
                    type="text"
                    placeholder="Username"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    />
            </div>
            <div className="form">
                <label>Password</label>
                <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                />
            </div>          
            <button onClick={handleLogin}>
                Login
            </button>
        <span>Don't have an account? <a onClick={goToSignUp}>Sign up</a></span>
        {error && <p style={{ color: "red" }}>Error: {message}</p>}
      </div>
    );

}