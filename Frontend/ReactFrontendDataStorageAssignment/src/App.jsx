import React from 'react'; 
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './Pages/Home';
import NewProject from './Pages/NewProjectPage';
import NewEmployee from './Pages/NewEmployeePage';
import NewCustomer from './Pages/NewCustomerPage';
import NewService from './Pages/NewServicePage';
import Project from './Pages/ProjectPage';
import Employee from './Pages/EmployeePage';
import Customer from './Pages/CustomerPage';
import Service from './Pages/ServicePage';
import EditProjectPage from './Pages/EditProjectPage';
import EditCustomerPage from './Pages/EditCustomerPage';
import EditEmployeePage from './Pages/EditEmployeePage';
import EditServicePage from './Pages/EditServicePage';
import DetailedProjectView from './Pages/DetailedProjectViewPage'

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />

          <Route path="/Project" element={<Project />} />
          <Route path="/AddNewProject" element={<NewProject />} />
          <Route path="/EditProject/:id" element={<EditProjectPage />} />
          <Route path='/DetailedProjectView/:id' element={<DetailedProjectView/>} />

          <Route path="/Employee" element={<Employee />} />
          <Route path="/AddNewEmployee" element={<NewEmployee />} />
          <Route path="/EditEmployee/:id" element={<EditEmployeePage />} />

          <Route path="/Customer" element={<Customer />} />
          <Route path="/AddNewCustomer" element={<NewCustomer />} />
          <Route path="/EditCustomer/:id" element={<EditCustomerPage />} />

          <Route path="/Service" element={<Service />} />
          <Route path="/AddNewService" element={<NewService />} />
          <Route path="/EditService/:id" element={<EditServicePage />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;