
import React, { ChangeEvent, useEffect, useRef, useState } from "react";
import useApi from "../../apiService/api";
import { useLocation, useNavigate } from "react-router-dom";
import './FilterList.css';
import { UserModel } from "../../models/userModel";
import { EnumError, UserService } from "../../apiService/userService";
import { Table } from "../Table/Table";
import { Modal } from "../PopUp/Modal";
import { CustomerService } from "../../apiService/customerService";
import { CustomerModel } from "../../models/customerModel";

export default function FilterList({ rowsFilters }) {
    const [message, setMessage] = useState("");
    
    const [customerNameFilter, setCustomerNameFilter] = useState("");
    const [requestedTimeFilter, setRequestedTimeFilter] = useState("");

    const hasFetched = useRef(false);
    const { getAsync } = useApi();


    useEffect(() => {
      if (!hasFetched.current) {
          fetchFilteredData();
          hasFetched.current = true;
      }
  }, [customerNameFilter, requestedTimeFilter]);

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

          if (data.status) {
              rowsFilters(data.data);
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
        <div className="filters">
        <input
            type="text"
            placeholder="Filter by Customer Name"
            value={customerNameFilter}
            onChange={(e) => handleFilterChange(e, "customerName")}
        />
        <input
            type="date"
            value={requestedTimeFilter}
            onChange={(e) => handleFilterChange(e, "requestedTime")}
        />
        <button onClick={fetchFilteredData}>Search</button>
    </div>
    );

}