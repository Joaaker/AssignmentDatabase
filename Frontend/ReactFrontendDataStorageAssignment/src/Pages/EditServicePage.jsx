import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const EditServicePage = () => {
  const { id } = useParams();
  const [serviceName, setServiceName] = useState('');
  const [price, setPrice] = useState('');
  const [unitType, setUnitType] = useState('');
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchService = async () => {
      try {
        const response = await fetch(`/api/Service/${id}`);
        if (!response.ok) {
          throw new Error(`Fel vid hämtning av tjänst: ${response.statusText}`);
        }
        const result = await response.json();
        const serviceData = result.data || result;
        setServiceName(serviceData.serviceName);
        setPrice(serviceData.price);
        setUnitType(serviceData.unitType);
      } catch (err) {
        console.error('Error fetching service:', err);
        setError(err.message);
      }
    };
    fetchService();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updateService = {
      serviceName,
      price: parseFloat(price),
      unitType
    };

    console.log("Data sent:", updateService); 

    try {
      const response = await fetch(`/api/Service/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updateService),
      });
      if (!response.ok) {
        throw new Error(`Fel vid uppdatering av tjänst: ${response.statusText}`);
      }
      navigate('/Service');
    } catch (err) {
      console.error('Error updating service:', err);
      setError(err.message);
    }
  };

  const handleDelete = async () => {
    if (!window.confirm('Är du säker på att du vill radera den här tjänsten?')) return;
    try {
      const response = await fetch(`/api/Service/${id}`, { method: 'DELETE' });
      if (!response.ok) {
        throw new Error(`Fel vid radering av tjänst: ${response.statusText}`);
      }
      navigate('/Service');
    } catch (err) {
      console.error('Error deleting service:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Redigera tjänst</h1>
      <HomeBtn />
      <p>Tjänstens ID: {id}</p>
      <form id="serviceForm" onSubmit={handleSubmit}>
        <div className="flex-column align-center">
          <div className="padding-1">
            <label htmlFor="serviceName">Tjänstnamn: </label>
            <input type="text" id="serviceName" value={serviceName} onChange={(e) => setServiceName(e.target.value)} required />
          </div>
          <div className="padding-1">
            <label htmlFor="price">Pris: </label>
            <input type="number" id="price" value={price} onChange={(e) => setPrice(e.target.value)} required />
          </div>
        </div>
      </form>
      <p>Enhetstyp: {unitType}</p>
      {error && <div className="error">{error}</div>}
      <button form="serviceForm" type="submit">Uppdatera tjänst</button>
      <button className="deleteBtn" onClick={handleDelete} type="button">Radera tjänst</button>
    </div>
  )
}

export default EditServicePage