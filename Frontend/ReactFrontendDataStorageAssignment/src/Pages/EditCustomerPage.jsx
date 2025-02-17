import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const EditCustomer = () => {
  const { id } = useParams();
  const [customerName, setCustomerName] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCustomer = async () => {
      try {
        const response = await fetch(`/api/Customer/${id}`);
        if (!response.ok) {
          throw new Error(`Fel vid hämtning av kund: ${response.statusText}`);
        }
        const result = await response.json();
        const customerData = result.data || result;
        setCustomerName(customerData.customerName);
      } catch (err) {
        console.error('Error fetching customer:', err);
        setError(err.message);
      }
    };

    fetchCustomer();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updateForm = { customerName };

    try {
      const response = await fetch(`/api/Customer/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updateForm),
      });
      if (!response.ok) {
        throw new Error(`Fel vid uppdatering av kund: ${response.statusText}`);
      }
      navigate('/Customer');
    } catch (err) {
      console.error('Error updating customer:', err);
      setError(err.message);
    }
  };

  const handleDelete = async () => {
    if (!window.confirm('Är du säker på att du vill radera den här kunden?')) return;
    try {
      const response = await fetch(`/api/Customer/${id}`, { method: 'DELETE' });
      if (!response.ok) {
        throw new Error(`Fel vid radering av kund: ${response.statusText}`);
      }
      navigate('/Customer');
    } catch (err) {
      console.error('Error deleting customer:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Redigera kund</h1>
      <HomeBtn />
      <p>Kund-ID: {id}</p>
      <form id="customerForm" onSubmit={handleSubmit}>
        <div>
          <label htmlFor="customerName">Kundnamn:</label>
          <input type="text" id="customerName" value={customerName} onChange={(e) => setCustomerName(e.target.value)} required/>
        </div>{error && <div className="error">{error}</div>}
      </form>
      <button form="customerForm" type="submit">Uppdatera kund</button>
      <button className='deleteBtn' onClick={handleDelete} type="button">Radera kund</button>
    </div>
  )
}

export default EditCustomer