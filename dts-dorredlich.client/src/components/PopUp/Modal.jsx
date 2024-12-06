import React, { useState } from "react";
import { CustomerModel } from "../../models/customerModel";
import "./Modal.css";

export const Modal = ({ closeModal, id, onSubmit, defaultValue, showDetails }) => {
        const [customer, setCustomer] = useState(
            defaultValue || new CustomerModel(id, "", "")
        );

        const [errors, setErrors] = useState("");
        const [isChange, setIsChange] = useState(defaultValue != null ? true : false);
        const [dateRequested, timeRequested] = defaultValue?.requestedTime.split("T") || [];
        const [dateCreate, timeCreate] = defaultValue?.createdAt.split("T") || [];

        const handleNameChange = (event) => {
            setCustomer({ ...customer, customerName: event.target.value });
            
        };
        
        const handleTimeChange = (event) => {
            setCustomer({ ...customer, requestedTime: event.target.value });
        };
    
        const handleSubmit = (e) => {
            e.preventDefault();

            onSubmit(customer, isChange);

            closeModal();
        };

    return (
        <div
            className="modal-container"
            onClick={(e) => {
                if (e.target.className === "modal-container") closeModal();
            }}
            >
            <div className="modal">
                {showDetails ? (
                        <div>      
                            <div className="form-group">
                                <label className="label-detail">CustomerName</label>
                                <span>{customer.customerName}</span>
                            </div>
                            <div className="form-group">
                                <label className="label-detail">Requested Time</label>
                                <span>{dateRequested} : {timeRequested}</span>
                            </div>
                            <div className="form-group">
                                <label className="label-detail">CustomerName</label>
                                <span>{dateCreate}</span>
                            </div>
                            <button  className="btn" onClick={() => closeModal()}>
                                Close
                            </button>
                        </div>
                ): (
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
                )}
                
            </div>
        </div>
    );
};