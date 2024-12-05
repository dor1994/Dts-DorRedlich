import React, { useState } from "react";
import { CustomerModel } from "../../models/customerModel";
import "./Modal.css";

export const Modal = ({ closeModal, id, onSubmit, defaultValue }) => {
    const [customer, setCustomer] = useState(
        new CustomerModel(id, "", "")
      );

      const [errors, setErrors] = useState("");
        const [selectedDate, setSelectedDate] = useState("");
        const [firstName, setFirstName] = useState("");
    
      // Handler to update the customer's name
      const handleNameChange = (event) => {
        setCustomer({ ...customer, customerName: event.target.value });
      };
    
      // Handler to update the requested time
      const handleTimeChange = (event) => {
        setCustomer({ ...customer, requestedTime: event.target.value });
      };
  
  
//   const validateForm = () => {
//     if (formState.page && formState.description && formState.status) {
//       setErrors("");
//       return true;
//     } else {
//       let errorFields = [];
//       for (const [key, value] of Object.entries(formState)) {
//         if (!value) {
//           errorFields.push(key);
//         }
//       }
//       setErrors(errorFields.join(", "));
//       return false;
//     }
//   };

  const handleChange = (e) => {
    setFormState({ ...formState, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    onSubmit(customer);

    closeModal();
  };

  const handleDateChange = (event) => {
    setSelectedDate(event.target.value); // Capture the selected date
  };

  return (
    <div
      className="modal-container"
      onClick={(e) => {
        if (e.target.className === "modal-container") closeModal();
      }}
    >
      <div className="modal">
        <form>
          <div className="form-group">
            <label htmlFor="customerName">CustomerName</label>
            <input
                type="text"
                value={customer.customerName}
                onChange={handleNameChange}
            />
          </div>
          <div className="form-group">
            <label htmlFor="description">Select Date</label>
            <input
                type="datetime-local"
                value={customer.requestedTime}
                onChange={handleTimeChange}
            />
          </div>
          {errors && <div className="error">{`Please include: ${errors}`}</div>}
          <button type="submit" className="btn" onClick={handleSubmit}>
            Submit
          </button>
        </form>
      </div>
    </div>
  );
};