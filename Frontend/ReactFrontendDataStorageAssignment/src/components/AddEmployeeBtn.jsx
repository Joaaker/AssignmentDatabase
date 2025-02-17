import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddEmployeeBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewEmployee');
  };

  return (
    <button className='btn' onClick={handleClick}>Lägg till ny anställd</button>
  );
}

export default AddEmployeeBtn;