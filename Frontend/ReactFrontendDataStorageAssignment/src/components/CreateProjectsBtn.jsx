import React from 'react';
import { useNavigate } from 'react-router-dom';

function CreateProjectsBtn() {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate('/AddNewProject');
  };

  return (
    <button onClick={handleClick}>Create New Project</button>
  );
}

export default CreateProjectsBtn;