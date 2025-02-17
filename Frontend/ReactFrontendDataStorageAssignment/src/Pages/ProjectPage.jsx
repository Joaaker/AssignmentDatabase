import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AddCustomerBtn from '../components/AddCustomerBtn';
import AddEmployeeBtn from '../components/AddEmployeeBtn';
import AddServiceBtn from '../components/AddServiceBtn';
import CreateProjectBtn from '../components/CreateProjectsBtn';
import HomeBtn from '../components/HomeBtn'

const ProjectPage = () => {
  const [projects, setProjects] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const response = await fetch('/api/Project');
        if (!response.ok) {
          throw new Error(`Failed to fetch projects: ${response.statusText}`);
        }
        const result = await response.json();
        const projectsData = result.data || result;
        setProjects(projectsData);
      } catch (err) {
        console.error('Error fetching projects:', err);
        setError(err.message);
      }
    };

    fetchProjects();
  }, []);

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <>
    <div>
      <HomeBtn/>
      <CreateProjectBtn/>
      <AddCustomerBtn/>
      <AddEmployeeBtn/>
      <AddServiceBtn/>
    </div>
    
      <table>
        <thead>
          <tr>
            <th className="tableTitle">Projektnummer</th>
            <th className="tableTitle long">Kundnamn</th>
            <th className="tableTitle long">Titel</th>
            <th className="tableTitle long ">Projektansvarig</th>
            <th className="tableTitle">Status</th>
            <th className="tableTitle">Startdatum</th>
            <th className="tableTitle">Slutdatum</th>
            <th className="tableTitle">Totalbelopp</th>
            <th className="tableTitle">Redigera projekt</th>
            <th className="tableTitle">Detaljerad vy</th>
          </tr>
        </thead>
        <tbody>
          {projects.map((project) => {
            const totalBelopp =
              project.services?.reduce((sum, service) => sum + service.price, 0) || 0;

            return (
              <tr key={project.id}>
                <td className='align-center'>P-{project.id}</td>
                <td>{project.customerName}</td>
                <td>{project.title}</td>
                <td>{project.projectManagerName}</td>
                <td >{project.statusType}</td>
                <td className='align-center'>{project.startDate}</td>
                <td className='align-center'>{project.endDate}</td>
                <td className='align-center'>{totalBelopp}kr</td>
                <td>
                  <button onClick={() => navigate(`/EditProject/${project.id}`)}>Redigera P-{project.id}</button>
                </td>
                <td>
                  <button onClick={() => navigate(`/DetailedProjectView/${project.id}`)}>Gå till P-{project.id}</button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
};

export default ProjectPage;