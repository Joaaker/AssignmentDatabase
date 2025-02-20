import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import HomeBtn from '../components/HomeBtn';

const DetailedProjectViewPage = () => {
  const { id } = useParams();
  const [project, setProject] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProject = async () => {
      try {
        const response = await fetch(`/api/Project/${id}`);
        if (!response.ok) {
          throw new Error(`Fel vid hämtning av projekt: ${response.statusText}`);
        }
        const projectData = await response.json();
        setProject(projectData.data);
        console.log(projectData.data)
      } catch (err) {
        console.error('Error fetching project:', err);
        setError(err.message);
      }
    };
    fetchProject();
  }, [id]);


  if (error) {
    return <div className="error">Fel: {error}</div>;
  }

  if (!project) {
    return <div>Laddar...</div>;
  }

  return (
    <div className="container">
      <h1>Projekt detaljer</h1>
      <HomeBtn />
      <p><strong>Projektnummer:</strong> P-{id}</p>
      <p><strong>Projekttitel:</strong> {project.title}</p>
      <p><strong>Beskrivning:</strong> {project.description || 'Ingen beskrivning angiven'}</p>
      <p><strong>Kund:</strong> {project.customerName}</p>
      <p><strong>Projektansvarig:</strong> {project.projectManagerName}</p>
      <p><strong>Projekstatus:</strong> {project.statusType}</p>
      <p><strong>Startdatum:</strong> {project.startDate}</p>
      <p><strong>Slutdatum:</strong> {project.endDate || 'Ej angivet'}</p>

      {project.services && project.services.length > 0 && (
        <div>
          <h2>Tjänster</h2>
          <table>
            <thead>
              <tr>
                <th className="tableTitle">Tjänstnamn</th>
                <th className="tableTitle">Pris</th>
                <th className="tableTitle">Enhet</th>
              </tr>
            </thead>
            <tbody>
              {project.services.map(service => (
                <tr key={service.id}>
                  <td>{service.serviceName}</td>
                  <td className="align-center">{service.price} kr</td>
                  <td className="align-center">{service.unitType}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  )
}

export default DetailedProjectViewPage