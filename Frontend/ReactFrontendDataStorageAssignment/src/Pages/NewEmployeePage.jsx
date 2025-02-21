import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const NewEmployee = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [roleName, setRoleName] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newEmployee = { firstName, lastName, roleName };

    try {
      const response = await fetch('/api/Employee', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newEmployee),
      });
      if (!response.ok) {
        throw new Error(`Fel vid skapande av anställd: ${response.statusText}`);
      }
      navigate('/Employee');
    } catch (err) {
      console.error('Error creating employee:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Skapa en ny anställd</h1>
      <HomeBtn />
      <form className='m-5' id="employeeForm" onSubmit={handleSubmit}>
        <div>
          <label htmlFor="firstName">Förnamn: </label>
          <input type="text" id="firstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} required />
        </div>
        <div>
          <label htmlFor="lastName">Efternamn: </label>
          <input type="text" id="lastName" value={lastName} onChange={(e) => setLastName(e.target.value)} required />
        </div>
        <div>
          <label htmlFor="roleName">Roll: </label>
          <input type="text" id="roleName" value={roleName} onChange={(e) => setRoleName(e.target.value)} required />
        </div>
        {error && <div className="error">{error}</div>}
      </form>
      <button form="employeeForm" type="submit">Skapa anställd</button>
    </div>
  )
}

export default NewEmployee