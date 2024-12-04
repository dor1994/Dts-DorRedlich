
import React, { useEffect, useState } from "react";
import useApi from "../../apiService/api";
import { useNavigate } from "react-router-dom";
import './CustomersListPage.css';
import { UserModel } from "../../models/userModel";
import { EnumError, UserService } from "../../apiService/userService";

export default function CustomersListPage() {
    const [queue, setQueue] = useState([]); // Holds the queue data
    const [customerName, setCustomerName] = useState(""); // Input for customer name
    const [requestedTime, setRequestedTime] = useState(""); // Input for requested time
    const [loading, setLoading] = useState(false); // Loading state for API calls

    const [message, setMessage] = useState("");
    const [error, setError] = useState(false);
    const navigate = useNavigate();

    const { getAsync, postAsync } = useApi();

    useEffect(() => {
        
      }, []);

      const handleSignUp = async () => {
        // const user = new UserModel(username, password, firstName);
        
        try {
            const data = await postAsync(UserService.USER_CONTROLLER, UserService.SIGNUP, {}); // Use postAsync directly
        
            setMessage(`Sign Up successful! Welcome, ${data.firstName}`);
        } catch (err) {
            setMessage("Sign Up failed. Please check your credentials.");
        }
    };

    const addToQueue = async () => {
        // if (!customerName || !requestedTime) {
        //   setError("Customer Name and Requested Time are required");
        //   return;
        // }
    
        // try {
        //   setLoading(true);
        //   const response = await axios.post("/api/queue", {
        //     customerName,
        //     requestedTime,
        //   });
        //   setQueue([...queue, response.data]); // Update the queue locally
        //   setCustomerName(""); // Clear input fields
        //   setRequestedTime("");
        // } catch (err) {
        //   setError("Failed to add to queue");
        // } finally {
        //   setLoading(false);
        // }
      };

    return (
        <div className="form-div">
            <h1>Login</h1>
            <div>
      <h1>Barbershop Queue</h1>
      {error && <p style={{ color: "red" }}>{error}</p>}
      {loading && <p>Loading...</p>}

      <table style={{ width: "100%", marginTop: "20px" }}>
        <thead>
          <tr>
            <th>Customer Name</th>
            <th>Requested Time</th>
          </tr>
        </thead>
        <tbody>
          {queue.length > 0 ? (
            queue.map((entry, index) => (
              <tr key={index}>
                <td>{}</td>
                <td>{}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td  style={{ textAlign: "center" }}>
                No requests in the queue
              </td>
            </tr>
          )}
        </tbody>
      </table>

            <h2>Add New Queue Request</h2>
            <form
                onSubmit={(e) => {
                e.preventDefault();
                addToQueue();
                }}
            >
                <div>
                <label>Customer Name: </label>
                <input
                    type="text"
                    value={customerName}
                    onChange={(e) => setCustomerName(e.target.value)}
                />
                </div>
                <div>
                <label>Requested Time: </label>
                <input
                    type="text"
                    value={requestedTime}
                    onChange={(e) => setRequestedTime(e.target.value)}
                />
                </div>
                <button type="submit">Add to Queue</button>
            </form>
            </div>
        </div>
    );

}