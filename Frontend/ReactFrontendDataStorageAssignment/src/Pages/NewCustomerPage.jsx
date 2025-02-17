import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const NewCustomer = () => {
  const [customerName, setCustomerName] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const newCustomer = { customerName };

    try {
      const response = await fetch('/api/Customer', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newCustomer),
      })

      if (!response.ok) {
        throw new Error(`Fel vid skapande av kund: ${response.statusText}`);
      }

      navigate('/Customer');
    } catch (err) {
      console.error('Error creating customer:', err);
      setError(err.message);
    }
  }

  return (
    <div className="container">
      <h1>Skapa en ny kund</h1>
      <HomeBtn />
      <form id="customerForm" onSubmit={handleSubmit}>
        <div>
          <label htmlFor="customerName">Kundnamn:</label>
          <input type="text" id="customerName" value={customerName} onChange={(e) => setCustomerName(e.target.value)} required/>
        </div>{error && <div className="error">{error}</div>}
      </form>
      <button form="customerForm" type="submit">Skapa kund</button>
    </div>
  )
}

export default NewCustomer