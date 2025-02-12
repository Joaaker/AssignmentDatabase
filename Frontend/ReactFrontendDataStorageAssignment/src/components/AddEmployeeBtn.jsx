import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddEmployeeBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewEmployee');
  };

  return (
    <button onClick={handleClick}>Add New Employee</button>
  );
}

export default AddEmployeeBtn;