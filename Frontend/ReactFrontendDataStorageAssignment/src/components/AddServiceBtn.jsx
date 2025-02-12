import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddServiceBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewService');
  };

  return (
    <button onClick={handleClick}>Add New Service</button>
  );
}

export default AddServiceBtn;