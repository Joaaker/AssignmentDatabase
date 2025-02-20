import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const EditProjectPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [projectStatusId, setProjectStatusId] = useState('');
  const [projectStatusName, setProjectStatusName] = useState('');
  const [customerId, setCustomerId] = useState('');
  const [projectManagerId, setProjectManagerId] = useState('');
  const [projectManagerName, setProjectManagerName] = useState('');
  const [selectedServices, setSelectedServices] = useState([]);
  const [selectedServiceIds, setSelectedServiceIds] = useState([]);
  const [serviceSelection, setServiceSelection] = useState('');
  const [error, setError] = useState(null);
  const [incomingCustomerName, setIncomingCustomerName] = useState('');
  const [customers, setCustomers] = useState([]);
  const [employees, setEmployees] = useState([]);
  const [services, setServices] = useState([]);

  const projectStatuses = [
    { id: 1, statusName: 'Ej påbörjat' },
    { id: 2, statusName: 'Pågående' },
    { id: 3, statusName: 'Avslutat' },
  ];

  useEffect(() => {
    const fetchDropdownData = async () => {
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
    fetchDropdownData();
  }, []);

  useEffect(() => {
    const fetchProject = async () => {
      try {
        const response = await fetch(`/api/Project/${id}`);
        if (!response.ok) {
          throw new Error(`Fel vid hämtning av projekt: ${response.statusText}`);
        }
        const projectData = await response.json();
        const incomingData = projectData.data;
        setTitle(incomingData.title || '');
        setDescription(incomingData.description || '');
        setStartDate(incomingData.startDate || '');
        setEndDate(incomingData.endDate || '');
        setProjectStatusName(incomingData.statusType || '');
        if (incomingData.services && incomingData.services.length > 0) {
          setSelectedServices(incomingData.services);
          setSelectedServiceIds(incomingData.services.map(service => service.id));
        }
        setIncomingCustomerName(incomingData.customerName);
        setProjectManagerName(incomingData.projectManagerName || '');
      } catch (err) {
        console.error('Error fetching project:', err);
        setError(err.message);
      }
    };
    fetchProject();
  }, [id]);

  useEffect(() => {
    if (customers.length > 0 && incomingCustomerName) {
      const matchedCustomer = customers.find(customer => customer.customerName === incomingCustomerName);
      if (matchedCustomer) {
        const custId = matchedCustomer.customerId || matchedCustomer.id;
        if (custId !== undefined) {
          setCustomerId(custId.toString());
        } else {
          console.warn('Kunde inte hitta ett kund-id för kunden:', incomingCustomerName);
        }
      } else {
        console.warn('Kunde inte hitta en kund med namnet:', incomingCustomerName);
      }
    }
  }, [customers, incomingCustomerName]);

  useEffect(() => {
    if (employees.length > 0 && projectManagerName) {
      const matchedEmployee = employees.find(employee => `${employee.firstName} ${employee.lastName}` === projectManagerName);
      if (matchedEmployee) {
        setProjectManagerId(matchedEmployee.id.toString());
      } else {
        console.warn('Kunde inte matcha projektansvarig med en anställd:', projectManagerName);
      }
    }
  }, [employees, projectManagerName]);

  useEffect(() => {
    if (projectStatusName) {
      const matchedStatus = projectStatuses.find(status => status.statusName === projectStatusName);
      if (matchedStatus) {
        setProjectStatusId(matchedStatus.id.toString());
      } else {
        console.warn('Kunde inte matcha statusnamn:', projectStatusName);
      }
    }
  }, [projectStatusName, projectStatuses]);

  useEffect(() => {
    if (services.length > 0) {
      const selected = services.filter(service => selectedServiceIds.includes(service.id));
      setSelectedServices(selected);
    }
  }, [services, selectedServiceIds]);

  const handleServiceSelect = (e) => {
    const serviceId = parseInt(e.target.value);
    if (!serviceId) return;
    const selectedService = services.find(service => service.id === serviceId);
    if (selectedService && !selectedServices.find(s => s.id === serviceId)) {
      setSelectedServices(prev => [...prev, selectedService]);
      setSelectedServiceIds(prev => [...prev, serviceId]);
    }
    setServiceSelection('');
  };

  const handleRemoveService = (id) => {
    setSelectedServices(prev => prev.filter(service => service.id !== id));
    setSelectedServiceIds(prev => prev.filter(sId => sId !== id));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const updatedProject = {
      title,
      description: description === '' ? null : description,
      startDate,
      endDate: endDate === '' ? null : endDate,
      projectStatusId: parseInt(projectStatusId),
      customerId: parseInt(customerId),
      projectManagerId: parseInt(projectManagerId),
      serviceIds: selectedServices.map(service => service.id),
    };
    try {
      const response = await fetch(`/api/Project/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(updatedProject),
      });
      if (!response.ok) {
        throw new Error(`Fel vid uppdatering av projekt: ${response.statusText}`);
      }
      navigate('/Project');
    } catch (err) {
      console.error('Error updating project:', err);
      setError(err.message);
    }
  };

  const handleDelete = async () => {
    if (!window.confirm('Är du säker på att du vill radera detta projekt?')) return;
    try {
      const response = await fetch(`/api/Project/${id}`, { method: 'DELETE' });
      if (!response.ok) {
        throw new Error(`Fel vid radering av projekt: ${response.statusText}`);
      }
      navigate('/Project');
    } catch (err) {
      console.error('Error deleting project:', err);
      setError(err.message);
    }
  };

  return (
    <div className="container">
      <h1>Redigera projekt</h1>
      <HomeBtn />
      <p>Projektnummer: {id}</p>
      <form id="projectForm" onSubmit={handleSubmit}>
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
          <label htmlFor="projectStatusId">Projekstatus:</label>
          <select id="projectStatusId" value={projectStatusId} onChange={(e) =>{ 
            const selectedId = e.target.value; setProjectStatusId(selectedId); 
            const selectedStatus = projectStatuses.find(status => status.id.toString() === selectedId); 
            if (selectedStatus) { setProjectStatusName(selectedStatus.statusName); } }} required>
            <option value="">Välj status</option>
            {projectStatuses.map(status => <option key={status.id} value={status.id.toString()}>{status.statusName}</option>)}
          </select>
        </div>
        <div>
          <label htmlFor="customerId">Kund:</label>
          <select id="customerId" value={customerId} onChange={(e) => setCustomerId(e.target.value)} required>
            <option value="">Välj kund</option>
            {customers.map(customer => <option key={customer.id} value={customer.customerId || customer.id}>{customer.customerName}</option>)}
          </select>
        </div>
        <div>
          <label htmlFor="projectManagerId">Projektansvarig: </label>
          <select id="projectManagerId" value={projectManagerId} onChange={(e) => { 
            const selectedId = e.target.value; setProjectManagerId(selectedId); 
            const selectedEmployee = employees.find(emp => emp.id.toString() === selectedId); 
            if (selectedEmployee) { setProjectManagerName(`${selectedEmployee.firstName} ${selectedEmployee.lastName}`); } }} required>
            <option value="">Välj projektansvarig</option>
            {employees.map(employee => <option key={employee.id} value={employee.id.toString()}>{employee.firstName} {employee.lastName}</option>)}
          </select>
        </div>
        <div>
          <label htmlFor="serviceDropdown">Välj tjänst:</label>
          <select id="serviceDropdown" value={serviceSelection} onChange={handleServiceSelect}>
            <option value="">Välj tjänst</option>
            {services.map(service => <option key={service.id} value={service.id.toString()}>{service.serviceName}, {service.unitType}</option>)}
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
              {selectedServices.map(service => (
                <tr key={service.id}>
                  <td>{service.serviceName}</td>
                  <td className="align-center">{service.price}kr</td>
                  <td>{service.unitType}</td>
                  <td><button type="button" onClick={() => handleRemoveService(service.id)}>Ta bort</button></td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
        {error && <div className="error">{error}</div>}
        <button type="submit">Uppdatera projekt</button>
      </form>
      <button className="deleteBtn" onClick={handleDelete} type="button">Radera projekt</button>
    </div>
  )
}

export default EditProjectPage