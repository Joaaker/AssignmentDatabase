import React from 'react';
import { useNavigate } from 'react-router-dom';

function ListAllServicesBtn() {
  const navigate = useNavigate();

  const handleClick = () => {navigate('/ServiceList');};

  return (
    <button onClick={handleClick}>Se alla och hantera tjänster</button>
  );
}

export default ListAllServicesBtn;