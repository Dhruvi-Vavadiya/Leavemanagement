using Leavemanagement.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leavemanagement.Service.Service
{
    public class EmployeeSerivce:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeSerivce(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
    }
}
