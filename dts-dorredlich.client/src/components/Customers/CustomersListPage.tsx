
import React, { useEffect, useState } from "react";
import useApi from "../../apiService/api";
import { useLocation, useNavigate } from "react-router-dom";
import './CustomersListPage.css';
import { UserModel } from "../../models/userModel";
import { EnumError, UserService } from "../../apiService/userService";
import { Table } from "../Table/Table";
import { Modal } from "../PopUp/Modal";
import { CustomerService } from "../../apiService/customerService";
import { CustomerModel } from "../../models/customerModel";

export default function CustomersListPage() {
    const [queue, setQueue] = useState([]); // Holds the queue data
    const [customerName, setCustomerName] = useState(""); // Input for customer name
    const [requestedTime, setRequestedTime] = useState(""); // Input for requested time
    const [loading, setLoading] = useState(false); // Loading state for API calls

    const [message, setMessage] = useState("");
    const [error, setError] = useState(false);
    const navigate = useNavigate();

    const { getAsync, postAsync } = useApi();

    const location = useLocation();
    const { id } = location.state || {};
    useEffect(() => {
        
      }, []);


    const [modalOpen, setModalOpen] = useState(false);
  const [rows, setRows] = useState([
    {
      id: "1",
      customerId: "1",
      customerName: "dor",
      requestedTime: "11/12/2024"
    },
    {
      id: "2",
      customerId: "2",
      customerName: "dor1",
      requestedTime: "11/12/2024"
    },
    {
      id: "3",
      customerId: "3",
      customerName: "dor2",
      requestedTime: "11/12/2024"
    },
  ]);
  const [rowToEdit, setRowToEdit] = useState(null);

  const handleDeleteRow = (targetIndex) => {
    setRows(rows.filter((_, idx) => idx !== targetIndex));
  };

  const handleEditRow = (idx) => {
    setRowToEdit(idx);

    setModalOpen(true);
  };

  const handleSubmit = async (newRow) => {

    const customer = new CustomerModel(newRow.customerId, newRow.customerName, newRow.requestedTime);
    
    try {
      const data = await postAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.ADD_NEW_QUEUE, customer); // Use postAsync directly
      console.log("data: ", data)
      if(data.status){
        rowToEdit === null
        ? setRows([...rows, newRow])
        : setRows(
            rows.map((currRow, idx) => {
              if (idx !== rowToEdit) return currRow;
  
              return newRow;
            })
          );
      }

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
            <h1>Barbershop Queue</h1>
            <div className="table-wrapper">
              <Table rows={rows} id={id} deleteRow={handleDeleteRow} editRow={handleEditRow} />
              <button onClick={() => setModalOpen(true)} className="btn">
                Add
              </button>
            </div>
            {modalOpen && (
              <Modal
                id={id}
                closeModal={() => {
                  setModalOpen(false);
                  setRowToEdit(null);
                }}
                onSubmit={handleSubmit}
                defaultValue={rowToEdit !== null && rows[rowToEdit]}
              />
            )}
        </div>
    );

}