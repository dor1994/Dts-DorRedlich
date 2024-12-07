import React, { useState } from "react";
import { useLocation } from "react-router-dom";
import { CustomerService } from "../../apiService/customerService";
import { CustomerModel } from "../../models/customerModel";
import { Table } from "../Table/Table";
import { Modal } from "../PopUp/Modal";
import FilterList from "../filters/FilterList";
import useApi from "../../apiService/api";
import './CustomersListPage.css'

export default function CustomersListPage() {
    const [rows, setRows] = useState<CustomerModel[]>([]);
    const [message, setMessage] = useState("");
    
    const [modalOpen, setModalOpen] = useState(false);
    const [rowToEdit, setRowToEdit] = useState(null);
    const [modelToEdit, setModelToEdit] = useState(null);
    const [showDetails, setShowDetails] = useState(false);

    const { postAsync, deleteAsync, putAsync } = useApi();

    const location = useLocation();
    const { id } = location.state || {};

    const handleDeleteRow = async (targetIndex) => {
        const rawToDelete = rows.find(row => row.id === targetIndex);
        if (rawToDelete) {
            try {
                var data = await deleteAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.DELETE_QUEUE, rawToDelete.id);
                
                if (data.status) {
                    setRows(rows.filter((raw, idx) => raw.id !== targetIndex));
                    setMessage(data.message);
                }
            } catch (err) {
                console.error("Error delete data:", err);
            }
        }
    };

    const handleEditRow = (idx, row) => {
        setRowToEdit(idx);
        setModelToEdit(row);
        setModalOpen(true);
    };

    const handleSubmit = async (newRow, isChange) => {
        if (newRow.requestedTime !== "") {
            const customer = new CustomerModel(newRow.customerId, newRow.customerName, newRow.requestedTime, null, newRow.id);
            try {
                var data = !isChange
                    ? await postAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.ADD_NEW_QUEUE, customer)
                    : await putAsync(CustomerService.CUSTOMER_CONTROLLER, CustomerService.UPDATE_QUEUE, customer);

                if (data.status) {
                    rowToEdit === null
                        ? setRows([...rows, data.data])
                        : setRows(rows.map((currRow, idx) => (idx !== rowToEdit ? currRow : newRow)));
                    setMessage(data.message);
                } else {
                    console.error(data.message);
                }
            } catch (err) {
                console.error("Error fetching data:", err);
            }
        } else {
            console.error("Must Add requested Time!");
        }
    };

    const handleFilters = async (filteredRows) => {
        setRows(filteredRows);
    };

    const handleShowDetail = (idx, row) => {
        setRowToEdit(idx);
        setModelToEdit(row);
        setModalOpen(true);
        setShowDetails(true);
    };

    return (
        <div className="form-div">
            <h1>Barbershop Queue</h1>
            <FilterList rowsFilters={handleFilters} />
            <div className="table-wrapper">
                <Table rows={rows} id={id} deleteRow={handleDeleteRow} editRow={handleEditRow} showDetail={handleShowDetail} />
                <button onClick={() => { setModelToEdit(null); setModalOpen(true); }} className="btn">
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
                    defaultValue={modelToEdit || null}
                />
            )}
        </div>
    );
}
