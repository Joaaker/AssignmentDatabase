import React from 'react';
import { useNavigate } from 'react-router-dom';

function CreateProjectsBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewProject');
  };

  return (
    <button className='btn' onClick={handleClick}>Skapa nytt projekt</button>
  );
}

export default CreateProjectsBtn;