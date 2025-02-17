import React from 'react'
import ListAllCustomersBtn from './ListAllCustomersBtn'
import ListAllEmployeesBtn from './ListAllEmployeesBtn'
import ListAllProjectsBtn from './ListAllProjectsBtn'
import ListAllServicesBtn from './ListAllServicesBtn'

const NavigationField = () => {
  return (
    <>
        <div className='btnContainer'> 
            <ListAllProjectsBtn/>
            <ListAllEmployeesBtn/>
            <ListAllCustomersBtn/>
            <ListAllServicesBtn/>
        </div>
    </>
  )
}

export default NavigationField