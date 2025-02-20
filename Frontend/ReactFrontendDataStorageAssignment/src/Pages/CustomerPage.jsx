import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import AddCustomerBtn from '../components/AddCustomerBtn';
import HomeBtn from '../components/HomeBtn'

const CustomerPage = () => {
  const [customers, setCustomers] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCustomers = async () => {
      try {
        const response = await fetch('/api/Customer');
        if (!response.ok) {
          throw new Error(`Failed to fetch customers: ${response.statusText}`);
        }
        const result = await response.json();
        const customerData = result.data || result;
        setCustomers(customerData);
      } catch (err) {
        console.error('Error fetching customers:', err);
        setError(err.message);
      }
    };

    fetchCustomers();
  }, []);

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <>
      <div>
        <HomeBtn/>
        <AddCustomerBtn />
      </div>

      <table>
        <thead>
          <tr>
            <th className="tableTitle">Kundnummer</th>
            <th className="tableTitle long">Kundnamn</th>
            <th className="tableTitle">Redigera kund</th>
          </tr>
        </thead>
        <tbody>
          {customers.map((customer) => (
            <tr key={customer.id}>
              <td className='align-center'>{customer.id}</td>
              <td>{customer.customerName}</td>
              <td>
                <button onClick={() => navigate(`/EditCustomer/${customer.id}`)}>Redigera {customer.customerName}</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  )
}

export default CustomerPage