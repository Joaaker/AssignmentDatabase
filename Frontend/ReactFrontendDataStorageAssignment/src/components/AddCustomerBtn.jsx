import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddCustomerBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewCustomer');
  };

  return (
    <button onClick={handleClick}>Add New Customer</button>
  );
}

export default AddCustomerBtn;