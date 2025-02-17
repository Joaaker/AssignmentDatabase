import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AddServiceBtn from '../components/AddServiceBtn';
import HomeBtn from '../components/HomeBtn'

const ServicePage = () => {
  const [services, setServices] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchServices = async () => {
      try {
        const response = await fetch('/api/Service');
        if (!response.ok) {
          throw new Error(`Kunde inte hämta tjänster: ${response.statusText}`);
        }
        const result = await response.json();
        const serviceData = result.data || result;
        setServices(serviceData);
      } catch (err) {
        console.error('Fel vid hämtning av tjänster:', err);
        setError(err.message);
      }
    };

    fetchServices();
  }, []);

  if (error) {
    return <div>Fel: {error}</div>;
  }

  return (
    <>
      <div>
        <HomeBtn/>
        <AddServiceBtn />
      </div>

      <table>
        <thead>
          <tr>
            <th className="tableTitle">Tjänstnamn</th>
            <th className="tableTitle">Pris</th>
            <th className="tableTitle">Enhetstyp</th>
            <th className="tableTitle">Redigera tjänst</th>
          </tr>
        </thead>
        <tbody>
          {services.map((service) => (
            <tr key={service.id}>
              <td>{service.serviceName}</td>
              <td className='align-center'>{service.price}kr</td>
              <td>{service.unitType}</td>
              <td>
                <button onClick={() => navigate(`/EditService/${service.id}`)}>Redigera {service.serviceName}</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};

export default ServicePage;