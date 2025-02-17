import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllCustomersBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/Customer');};

  return (
    <button onClick={handleClick}>Visa och hantera kunder</button>
  );
}

export default ListAllCustomersBtn;