import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const DetailedProjectViewPage = () => {
  const { id } = useParams();
  const [project, setProject] = useState(null);
  const [customers, setCustomers] = useState([]);
  const [employees, setEmployees] = useState([]);
  const [services, setServices] = useState([]);
  const [error, setError] = useState(null);

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
        setProject(projectData.data || projectData);
      } catch (err) {
        console.error('Error fetching project:', err);
        setError(err.message);
      }
    };
    fetchProject();
  }, [id]);

  let projectStatusName = '';
  let customerName = '';
  let projectManagerName = '';
  let selectedServices = [];

  if (project) {
    const statusObj = projectStatuses.find(status => status.id === project.projectStatusId);
    projectStatusName = statusObj ? statusObj.statusName : project.projectStatusId;

    const customer = customers.find(c => c.id === project.customerId);
    customerName = customer ? customer.customerName : project.customerId;

    const manager = employees.find(e => e.id === project.projectManagerId);
    projectManagerName = manager ? `${manager.firstName} ${manager.lastName}` : project.projectManagerId;

    selectedServices = services.filter(service => project.serviceIds && project.serviceIds.includes(service.id));
  }

  if (error) return <div className="error">{error}</div>;
  if (!project) return <div>Laddar projektdata...</div>;

  return (
    <div className="container">
      <h1>Detaljerad vy för projekt</h1>
      <HomeBtn />
      <p><strong>Projektnummer:</strong> {id}</p>
      <p><strong>Projekttitel:</strong> {project.title}</p>
      <p><strong>Beskrivning:</strong> {project.description}</p>
      <p><strong>Startdatum:</strong> {project.startDate}</p>
      <p><strong>Slutdatum:</strong> {project.endDate}</p>
      <p><strong>Status:</strong> {projectStatusName}</p>
      <p><strong>Kund:</strong> {customerName}</p>
      <p><strong>Projektansvarig:</strong> {projectManagerName}</p>
      <h2>Tjänster</h2>
      {selectedServices.length > 0 ? (
        <table>
          <thead>
            <tr>
              <th>Tjänstnamn</th>
              <th>Pris</th>
              <th>Enhetstyp</th>
            </tr>
          </thead>
          <tbody>
            {selectedServices.map(service => (
              <tr key={service.id}>
                <td>{service.serviceName}</td>
                <td>{service.price} kr</td>
                <td>{service.unitType}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p>Inga tjänster kopplade.</p>
      )}
    </div>
  )
}

export default DetailedProjectViewPage