import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllCustomersBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/CustomerList');};

  return (
    <button onClick={handleClick}>Se alla och hantera kunder</button>
  );
}

export default ListAllCustomersBtn;