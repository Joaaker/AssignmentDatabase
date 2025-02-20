import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AddEmployeeBtn from '../components/AddEmployeeBtn';
import HomeBtn from '../components/HomeBtn'

const EmployeePage = () => {
  const [employees, setEmployees] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const response = await fetch('/api/Employee');
        if (!response.ok) {
          throw new Error(`Failed to fetch employees: ${response.statusText}`);
        }
        const result = await response.json();
        const employeeData = result.data || result;
        setEmployees(employeeData);
      } catch (err) {
        console.error('Error fetching employees:', err);
        setError(err.message);
      }
    };

    fetchEmployees();
  }, []);

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <>
      <div>
        <HomeBtn/>
        <AddEmployeeBtn />
      </div>

      <table>
        <thead>
          <tr>
            <th className="tableTitle">Anställningsnummer</th>
            <th className="tableTitle">Förnamn</th>
            <th className="tableTitle">Efternamn</th>
            <th className="tableTitle">Roll</th>
            <th className="tableTitle">Redigera anställd</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((employee) => (
            <tr key={employee.id}>
              <td className='align-center'>{employee.id}</td>
              <td>{employee.firstName}</td>
              <td>{employee.lastName}</td>
              <td>{employee.roleName}</td>
              <td>
                <button onClick={() => navigate(`/EditEmployee/${employee.id}`)}>Redigera {employee.firstName} {employee.lastName}</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}

export default EmployeePage