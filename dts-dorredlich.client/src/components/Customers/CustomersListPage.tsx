
import React, { ChangeEvent, useEffect, useRef, useState } from "react";
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
    const [rows, setRows] = useState<CustomerModel[]>([]);
    const [message, setMessage] = useState("");
    
    const [modalOpen, setModalOpen] = useState(false);
    const [customerNameFilter, setCustomerNameFilter] = useState("");
    const [requestedTimeFilter, setRequestedTimeFilter] = useState("");
    const [rowToEdit, setRowToEdit] = useState(null);
    const [modelToEdit, setModelToEdit] = useState(null);
    const [showDetails, setShowDetails] = useState(false);

    const hasFetched = useRef(false); // Tracks if the fetch has occurred
    const { getAsync, postAsync, deleteAsync, putAsync } = useApi();

    const location = useLocation();
    const { id } = location.state || {};

    useEffect(() => {
      if (!hasFetched.current) {
          fetchFilteredData();
          hasFetched.current = true;
      }
  }, [customerNameFilter, requestedTimeFilter]); // Refetch when filters change

  const handleDeleteRow = async (targetIndex) => {
    
    const rawToDelete = rows.find(row => row.id === targetIndex);  // Find by condition
    if(rawToDelete) {
      try {
        var data = await deleteAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.DELETE_QUEUE, rawToDelete.id); // Use postAsync directly
        
        if(data.status){
          setRows(rows.filter((raw, idx) => raw.id !== targetIndex));
          setMessage(data.message);
        }
  
      } catch (err) {
        console.error("Error delete data:", err);
        console.error("Error Message:", data.message);
      }
    }
    
  };

  const handleEditRow = (idx, row) => {
    setRowToEdit(idx);
    setModelToEdit(row);
    setModalOpen(true);
  };

  const handleSubmit = async (newRow, isChange) => {

    if(newRow.requestedTime != ""){
      const customer = new CustomerModel(newRow.customerId, newRow.customerName, newRow.requestedTime, null,newRow.id);
      try {
        var data = !isChange ? await postAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.ADD_NEW_QUEUE, customer) 
                              : await putAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.UPDATE_QUEUE, customer);
  
        if(data.status){
          rowToEdit === null
          ? setRows([...rows, data.data])
          : setRows(
              rows.map((currRow, idx) => {
                if (idx !== rowToEdit) return currRow;
    
                return newRow;
              })
            );
            setMessage(data.message);
        }
        else {
          console.error(data.message);
        }
  
        } catch (err) {
          console.error("Error fetching data:", err);
          console.error("Error Message:", data.message);
        }

    }

    else {
      console.error("Must Add requested Time!")
    }
      
    };

    const handleShowDetail = (idx, row) => {
      setRowToEdit(idx);
      setModelToEdit(row);
      setModalOpen(true);
      setShowDetails(true);
    }


    const handleFilterChange = (e: ChangeEvent<HTMLInputElement>, filterType: string) => {
      const { value } = e.target;
      if (filterType === "customerName") {
          setCustomerNameFilter(value);
      } else if (filterType === "requestedTime") {
          setRequestedTimeFilter(value);
      }
  };

  const fetchFilteredData = async () => {
      try {
          const queryParams = new URLSearchParams();
          if (customerNameFilter) queryParams.append("customerName", customerNameFilter);
          if (requestedTimeFilter) queryParams.append("requestedTime", requestedTimeFilter);
          const data = await getAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.GET_FILTER_CUSTOMERS, queryParams);

          // const data = await getAsync(`${CustomerService.CUSTOMER_CONTROLLER}?${queryParams.toString()}`);
          if (data.status) {
              setRows(data.data);
          } else {
              console.error(data.message);
              setMessage("Failed to load filtered customers.");
          }
      } catch (err) {
          console.error("Error fetching filtered customers:", err);
          setMessage("Failed to load filtered customers. Please try again later.");
      }
  };


    return (
        <div className="form-div">
            <h1>Barbershop Queue</h1>
            <div className="filters">
                <input
                    type="text"
                    placeholder="Filter by Customer Name"
                    value={customerNameFilter}
                    onChange={(e) => handleFilterChange(e, "customerName")}
                />
                <input
                    type="datetime-local"
                    value={requestedTimeFilter}
                    onChange={(e) => handleFilterChange(e, "requestedTime")}
                />
                <button onClick={fetchFilteredData}>Search</button>
            </div>
            <div className="table-wrapper">
              <Table rows={rows} id={id} deleteRow={handleDeleteRow} editRow={handleEditRow} showDetail={handleShowDetail}/>
              <button onClick={() => {setModelToEdit(null); setModalOpen(true);}} className="btn">
                Add
              </button>
            </div>
            {modalOpen && (
              <Modal
                id={id}
                showDetails={showDetails}
                closeModal={() => {
                  setModalOpen(false);
                  setRowToEdit(null);
                  setShowDetails(false);
                }}
                onSubmit={handleSubmit}
                defaultValue={modelToEdit != null ? modelToEdit : null}
              />
            )}
        </div>
    );

}