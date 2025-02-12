import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllProjectsBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/ProjectList');};

  return (
    <button onClick={handleClick}>Se alla och hantera projekt</button>
  );
}

export default ListAllProjectsBtn;