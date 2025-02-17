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
  }

  return (
    <div className="container">
      <h1>Skapa en ny anställd</h1>
      <HomeBtn />
      <form id="employeeForm" onSubmit={handleSubmit}>
        <div className='padding-1'>
          <label htmlFor="firstName">Förnamn: </label>
          <input type="text" id="firstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} required/>
        </div>
        <div className='padding-1'>
          <label htmlFor="lastName">Efternamn: </label>
          <input type="text" id="lastName" value={lastName} onChange={(e) => setLastName(e.target.value)} required/>
        </div>
        <div className='padding-1'>

        </div>{error && <div className="error">{error}</div>}
      </form>
      <button className='green' form="employeeForm" type="submit">Lägg till anställd</button>
    </div>
  )
}

export default NewEmployee;