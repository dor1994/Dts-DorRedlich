import { useState, useCallback } from "react";
import axios from "axios";

const useApi = () => {
  const [error, setError] = useState(null);
    const baseApiUrl =  "https://localhost:7168/api"
  /**
   * Perform a GET request.
   * @param {string} controller - The controller name.
   * @param {string} endpoint - The API endpoint (optional).
   * @param {object} params - Query parameters (optional).
   * @returns {Promise<any>}
   */
  const getAsync = useCallback(async (controller, endpoint = "", params = {}) => {

    const url = `${baseApiUrl}/${controller}${endpoint}`;
    try {
      const response = await axios.get(`${baseApiUrl}/${controller}${endpoint}`, { params });
      
      return response.data;
    } catch (err) {
      setError(err);
      throw err;
    } 
  });

  /**
   * Perform a POST request.
   * @param {string} controller - The controller name.
   * @param {string} endpoint - The API endpoint (optional).
   * @param {object} body - Request body.
   * @returns {Promise<any>}
   */
  const postAsync = useCallback(async (controller, endpoint = "", body = {}) => {
    
    try {
      const response = await axios.post(`${baseApiUrl}/${controller}${endpoint}`, body);

      return response.data;
    } catch (err) {
      setError(err);
      throw err;
    } 
  });

   /**
   * Perform a PUT request.
   * @param {string} controller - The controller name.
   * @param {string} endpoint - The API endpoint (optional).
   * @param {object} body - Request body.
   * @returns {Promise<any>}
   */
  const putAsync = useCallback(async (controller, endpoint = "", body = {}) => {

    try {
      const response = await axios.put(`${baseApiUrl}/${controller}${endpoint}`, body);
      return response.data;
    } catch (err) {
      setError(err);
      throw err;
    } 
  }, [baseApiUrl]);


 /**
   * Perform a DELETE request.
   * @param {string} controller - The controller name.
   * @param {string} endpoint - The API endpoint (optional).
   * @param {object} params - Query parameters (optional).
   * @returns {Promise<any>}
   */
 const deleteAsync = useCallback(async (controller, endpoint = "", params = {}) => {

    try {
      const response = await axios.delete(`${baseApiUrl}/${controller}${endpoint}/${params}`);
      return response.data;
    } catch (err) {
      setError(err);
      throw err;
    } 
  }, [baseApiUrl]);

  return {
    getAsync,
    postAsync,
    putAsync,
    deleteAsync,
  };
}

export default useApi;


