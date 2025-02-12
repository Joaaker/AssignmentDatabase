import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllEmployeesBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/EmployeeList');};

  return (
    <button onClick={handleClick}>Se alla och hantera anställda</button>
  );
}

export default ListAllEmployeesBtn;