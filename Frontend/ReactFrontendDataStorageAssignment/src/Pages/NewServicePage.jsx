import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const NewService = () => {
  const [serviceName, setServiceName] = useState('');
  const [price, setPrice] = useState('');
  const [unitType, setUnitType] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newService = { 
      serviceName, 
      price: parseFloat(price),
      unitType 
    };

    try {
      const response = await fetch('/api/Service', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newService),
      });

      if (!response.ok) {
        throw new Error(`Fel vid skapande av tjänst: ${response.statusText}`);
      }

      navigate('/Service');
    } catch (err) {
      console.error('Error creating service:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Skapa en ny tjänst</h1>
      <HomeBtn />
      <form className='m-5' id="serviceForm" onSubmit={handleSubmit}>
        <div>
          <label htmlFor="serviceName">Tjänstnamn: </label>
          <input type="text" id="serviceName" value={serviceName} onChange={(e) => setServiceName(e.target.value)} required />
        </div>
        <div>
          <label htmlFor="price">Pris: </label>
          <input type="number" id="price" value={price} onChange={(e) => setPrice(e.target.value)} required />
        </div>
        <div>
          <label htmlFor="unitType">Enhetstyp: </label>
          <input type="text" id="unitType" value={unitType} onChange={(e) => setUnitType(e.target.value)} required />
        </div>{error && <div className="error">{error}</div>}
      </form>
      <button form="serviceForm" type="submit">Skapa tjänst</button>
    </div>
  )
}

export default NewService