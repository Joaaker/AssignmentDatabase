import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllEmployeesBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/Employee');};

  return (
    <button onClick={handleClick}>Visa och hantera anställda</button>
  );
}

export default ListAllEmployeesBtn;