import React from 'react'; 
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import NewProject from './Pages/NewProject';
import NewEmployee from './Pages/NewEmployee';
import NewCustomer from './Pages/NewCustomer';
import NewService from './Pages/NewService';
import ProjectList from './Pages/ProjectPage';
import EmployeeList from './Pages/EmployeePage';
import CustomerList from './Pages/CustomerPage';
import ServiceList from './Pages/ServicePage';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/AddNewProject" element={<NewProject />} />
          <Route path="/ProjectList" element={<ProjectList />} />
          <Route path="/AddNewEmployee" element={<NewEmployee />} />
          <Route path="/EmployeeList" element={<EmployeeList />} />
          <Route path="/AddNewCustomer" element={<NewCustomer />} />
          <Route path="/CustomerList" element={<CustomerList />} />
          <Route path="/AddNewService" element={<NewService />} />
          <Route path="/ServiceList" element={<ServiceList />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;