
import React, { useState } from "react";
import useApi from "../../apiService/api";
import { UserModel } from "../../models/userModel";
import './SignUp.css';
import { UserService } from "../../apiService/userService";

export default function SignUpPage() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [firstName, setFirstName] = useState("");
    const [message, setMessage] = useState("");

    const { postAsync, loading, error } = useApi(); // Replace with your API base URL

    const handleSignUp = async () => {
        const user = new UserModel(username, password, firstName);
        
        try {
            const data = await postAsync(UserService.USER_CONTROLLER, UserService.SIGNUP, user); // Use postAsync directly
        
            setMessage(`Sign Up successful! Welcome, ${data.firstName}`);
        } catch (err) {
            setMessage("Sign Up failed. Please check your credentials.");
        }
    };


    return (
        <div className="form-div">
        <h1>SignUp</h1>
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
            <div className="form">
                <label>FirstName</label>
                <input
                    type="text"
                    placeholder="firstName"
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                />
            </div >           
            <button onClick={handleSignUp}>
                SignUp
            </button>
        {/* {error && <p style={{ color: "red" }}>Error: {error.message}</p>}
        {message && <p>{message}</p>} */}
      </div>
    );

}