import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const EditEmployee = () => {
  const { id } = useParams();
  const [firstName, setFirstName] = useState(''); const [lastName, setLastName] = useState(''); const [roleName, setRoleName] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchEmployee = async () => {
      try {
        const response = await fetch(`/api/Employee/${id}`);
        if (!response.ok) {
          throw new Error(`Fel vid hämtning av anställd: ${response.statusText}`);
        }
        const result = await response.json();
        const employeeData = result.data || result;
        setFirstName(employeeData.firstName);
        setLastName(employeeData.lastName);
        setRoleName(employeeData.roleName);
      } catch (err) {
        console.error('Error fetching employee:', err);
        setError(err.message);
      }
    };

    fetchEmployee();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updateForm = { firstName, lastName };

    try {
      const response = await fetch(`/api/Employee/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updateForm),
      });
      if (!response.ok) {
        throw new Error(`Fel vid uppdatering av anställd: ${response.statusText}`);
      }
      navigate('/Employee');
    } catch (err) {
      console.error('Error updating employee:', err);
      setError(err.message);
    }
  };

  const handleDelete = async () => {
    if (!window.confirm('Är du säker på att du vill radera den här anställda?')) return;
    try {
      const response = await fetch(`/api/Employee/${id}`, { method: 'DELETE' });
      if (!response.ok) {
        throw new Error(`Fel vid radering av anställd: ${response.statusText}`);
      }
      navigate('/Employee');
    } catch (err) {
      console.error('Error deleting employee:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Redigera anställd</h1>
      <HomeBtn />
      <p>Anställd-ID: {id}</p>
      <form id="employeeForm" onSubmit={handleSubmit}>
        <div className='flex-column align-center'>
          <div className='padding-1'>
            <label htmlFor="firstName">Förnamn: </label>
            <input type="text" id="firstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} required/>
          </div>
          <div className='padding-1'>
            <label htmlFor="lastName">Efternamn: </label>
            <input type="text" id="lastName" value={lastName} onChange={(e) => setLastName(e.target.value)} required/>
          </div>
        </div>
      </form>
      <p>Roll: {roleName}</p>
      {error && <div className="error">{error}</div>}
      <button form="employeeForm" type="submit">Uppdatera anställd</button>
      <button className="deleteBtn" onClick={handleDelete} type="button">Radera anställd</button>
    </div>
  )
}

export default EditEmployee