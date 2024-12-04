import { useEffect, useState } from 'react';
import './App.css';
import SignUpPage from './components/signUp/SignUp';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LoginPage from './components/login/LoginPage';
import CustomersListPage from './components/Customers/CustomersListPage';

function App() {

    return (
        <Router>
            <Routes>
                <Route path="/" element={<LoginPage />} />
                <Route path="/signup" element={<SignUpPage />} />
                <Route path="/customersList" element={<CustomersListPage />}/>
            </Routes>
        </Router>
    );
}

export default App;