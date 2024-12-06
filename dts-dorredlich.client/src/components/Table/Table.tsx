import React from "react";

import { BsFillTrashFill, BsFillPencilFill } from "react-icons/bs";

import "./Table.css";

export const Table = ({ rows, id, deleteRow, editRow, showDetail }) => {
  return (
    <div>
      <table className="table">
        <thead>
          <tr>
            <th>Customer Name</th>
            <th className="expand">Requested Date</th>
            <th className="expand">Requested Time</th>
          </tr>
        </thead>
        <tbody>
          {rows.length > 0 &&
            rows.map((row, idx) => {
              const [date, time] = row.requestedTime.split("T");
              return (
                <tr
                  key={idx}
                  onClick={() => showDetail(idx, row)}
                  className={`content ${id != row.customerId ? "disabled" : ""}`}
                >
                  <td>{row.customerName}</td>
                  <td className="expand">{date}</td>
                  <td className="expand">{time}</td>
                  <td
                    className="fit"
                    onClick={(e) => e.stopPropagation()} // Prevent click event from propagating to <tr>
                  >
                    {row.customerId === id && (
                      <span className="actions">
                        <BsFillTrashFill
                          className="delete-btn"
                          onClick={() => deleteRow(row.id)}
                        />
                        <BsFillPencilFill
                          className="edit-btn"
                          onClick={() => editRow(idx, row)}
                        />
                      </span>
                    )}
                  </td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};
