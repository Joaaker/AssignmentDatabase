import React from 'react';
import { useNavigate } from 'react-router-dom';

function AddServiceBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewService');
  };

  return (
    <button className='btn' onClick={handleClick}>Lägg till ny tjänst</button>
  );
}

export default AddServiceBtn;