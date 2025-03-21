import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddCustomerBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewCustomer');
  };

  return (
    <button className='btn' onClick={handleClick}>Lägg till ny kund</button>
  );
}

export default AddCustomerBtn;