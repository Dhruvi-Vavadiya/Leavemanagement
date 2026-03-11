using Leavemanagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leavemanagement.Repository.Repository
{
    public interface IEmployeeRepository
    {
        Task AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);

        Task DeleteProofMapping(ProofMapping proofMapping);

        Task<Employee> getemployeeIdById(int employeeId);

        Task<ProofMapping> IsImageExsist(string imageName);


    }
}
