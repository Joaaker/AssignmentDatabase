import React from 'react'; 
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import NewProject from './Pages/NewProject';
import NewEmployee from './Pages/NewEmployee';
import NewCustomer from './Pages/NewCustomer';
import NewService from './Pages/NewService';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/AddNewProject" element={<NewProject />} />
          <Route path="/AddNewEmployee" element={<NewEmployee />} />
          <Route path="/AddNewCustomer" element={<NewCustomer />} />
          <Route path="/AddNewService" element={<NewService />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;