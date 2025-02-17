import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddCustomerBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/');
  };

  return (
    <button className='btn' onClick={handleClick}>Gå tillbaka till startsidan</button>
  );
}

export default AddCustomerBtn;