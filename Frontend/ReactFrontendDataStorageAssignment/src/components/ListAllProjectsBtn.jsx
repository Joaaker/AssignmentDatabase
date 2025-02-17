import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllProjectsBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/Project');};

  return (
    <button onClick={handleClick}>Visa och hantera projekt</button>
  );
}

export default ListAllProjectsBtn;