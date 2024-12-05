import React from "react";

import { BsFillTrashFill, BsFillPencilFill } from "react-icons/bs";

import "./Table.css";

export const Table = ({ rows, id, deleteRow, editRow }) => {
  return (
    <div >
      <table className="table">
        <thead>
          <tr>
          <th>Customer Name</th>
          <th className="expand">Requested Time</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((row, idx) => {
            
            return (
              <tr key={idx}>
                <td>{row.customerName}</td>
                <td className="expand">{row.requestedTime}</td>
                <td>
                  {/* <span >
                    {row.firstName}
                  </span> */}
                </td>
                <td  className="fit">
                    {row.customerId == id && (
                        <span className="actions">
                        <BsFillTrashFill
                        className="delete-btn"
                        onClick={() => deleteRow(idx)}
                        />
                        <BsFillPencilFill
                        className="edit-btn"
                        onClick={() => editRow(idx)}
                        />
                  </span>)}
                  
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};