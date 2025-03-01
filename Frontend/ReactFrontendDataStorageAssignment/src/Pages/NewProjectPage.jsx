import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const NewProject = () => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [projectStatusId, setProjectStatusId] = useState('');
  const [customerId, setCustomerId] = useState('');
  const [projectManagerId, setProjectManagerId] = useState('');
  const [error, setError] = useState(null);

  const projectStatuses = [
    { id: 1, statusName: 'Ej påbörjat' },
    { id: 2, statusName: 'Pågående' },
    { id: 3, statusName: 'Avslutat' },
  ];

  const [customers, setCustomers] = useState([]);
  const [employees, setEmployees] = useState([]);
  const [services, setServices] = useState([]);

  const [serviceSelection, setServiceSelection] = useState('');
  const [selectedServices, setSelectedServices] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [customersRes, employeesRes, servicesRes] = await Promise.all([
          fetch('/api/Customer'),
          fetch('/api/Employee'),
          fetch('/api/Service'),
        ]);

        if (!customersRes.ok || !employeesRes.ok || !servicesRes.ok) {
          throw new Error('Fel vid hämtning av data för kund, anställd eller tjänster.');
        }

        const customersData = await customersRes.json();
        const employeesData = await employeesRes.json();
        const servicesData = await servicesRes.json();

        setCustomers(customersData.data || customersData);
        setEmployees(employeesData.data || employeesData);
        setServices(servicesData.data || servicesData);
      } catch (err) {
        console.error('Error fetching dropdown data:', err);
        setError(err.message);
      }
    };

    fetchData();
  }, []);

  const handleServiceSelect = (e) => {
    const serviceId = parseInt(e.target.value);
    if (!serviceId) return;

    const selectedService = services.find((service) => service.id === serviceId);

    if (selectedService && !selectedServices.find((s) => s.id === serviceId)) {
      setSelectedServices((prev) => [...prev, selectedService]);
    }
    setServiceSelection('');
  };

  const handleRemoveService = (id) => {
    setSelectedServices((prev) => prev.filter((service) => service.id !== id));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newProject = {
      title,
      description: description === '' ? null : description,
      startDate,
      endDate: endDate === '' ? null : endDate,
      projectStatusId: parseInt(projectStatusId),
      customerId: parseInt(customerId),
      projectManagerId: parseInt(projectManagerId),
      serviceIds: selectedServices.map((service) => service.id),
    };

    try {
      const response = await fetch('/api/Project', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newProject),
      });

      if (!response.ok) {
        throw new Error(`Fel vid skapande av projekt: ${response.statusText}`);
      }

      navigate('/Project');
    } catch (err) {
      console.error('Error creating project:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Skapa ett nytt projekt</h1>
      <HomeBtn />
      <form className='m-5' id="projectForm" onSubmit={handleSubmit}>
        <div>
          <label htmlFor="title">Projekttitel: </label>
          <input type="text" id="title" value={title} onChange={(e) => setTitle(e.target.value)} required />
        </div>
        <div>
          <label htmlFor="description">Beskrivning av projektet: </label>
          <textarea id="description" value={description} onChange={(e) => setDescription(e.target.value)}></textarea>
        </div>
        <div>
          <label htmlFor="startDate">Välj startdatum: </label>
          <input type="date" id="startDate" value={startDate} onChange={(e) => setStartDate(e.target.value)} required />
        </div>
        <div>
          <label htmlFor="endDate">Välj slutdatum: </label>
          <input type="date" id="endDate" value={endDate} onChange={(e) => setEndDate(e.target.value)} />
        </div>
        <div>
          <label htmlFor="projectStatusId">Projekstatus: </label>
          <select id="projectStatusId" value={projectStatusId} onChange={(e) => setProjectStatusId(e.target.value)} required>
            <option value="">Välj status</option>
            {projectStatuses.map((status) => <option key={status.id} value={status.id}>{status.statusName}</option>)}
          </select>
        </div>
        <div>
          <label htmlFor="customerId">Kund: </label>
          <select id="customerId" value={customerId} onChange={(e) => setCustomerId(e.target.value)} required>
            <option value="">Välj kund</option>
            {customers.map((customer) => <option key={customer.id} value={customer.id}>{customer.customerName}</option>)}
          </select>
        </div>
        <div>
          <label htmlFor="projectManagerId">Projektansvarig: </label>
          <select id="projectManagerId" value={projectManagerId} onChange={(e) => setProjectManagerId(e.target.value)} required>
            <option value="">Välj projektansvarig</option>
            {employees.map((employee) => <option key={employee.id} value={employee.id}>{employee.firstName} {employee.lastName}</option>)}
          </select>
        </div>
        <div>
          <label htmlFor="serviceDropdown">Välj tjänst: </label>
          <select id="serviceDropdown" value={serviceSelection} onChange={handleServiceSelect}>
            <option value="">Välj tjänst</option>
            {services.map((service) => <option key={service.id} value={service.id}>{service.serviceName}, {service.unitType}</option>)}
          </select>
        </div>
        {selectedServices.length > 0 && (
          <table>
            <thead>
              <tr>
                <th className="tableTitle">Tjänstnamn</th>
                <th className="tableTitle">Pris</th>
                <th className="tableTitle">Enhetstyp</th>
                <th className="tableTitle">Ta bort</th>
              </tr>
            </thead>
            <tbody>
              {selectedServices.map((service) => (
                <tr key={service.id}>
                  <td>{service.serviceName}</td>
                  <td className="align-center">{service.price}kr</td>
                  <td>{service.unitType}</td>
                  <td>
                    <button type="button" onClick={() => handleRemoveService(service.id)}>Ta bort</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
        {error && <div className="error">{error}</div>}
        <button type="submit">Skapa projekt</button>
      </form>
    </div>
  )
}

export default NewProject