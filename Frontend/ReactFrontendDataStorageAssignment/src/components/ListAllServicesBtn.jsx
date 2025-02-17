import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllServicesBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/Service');};

  return (
    <button onClick={handleClick}>Visa och hantera tjänster</button>
  );
}

export default ListAllServicesBtn;