using Leavemanagement.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Leavemanagement.Repository.Repository
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly LeaveManagementsDbContext _context;

        public EmployeeRepository(LeaveManagementsDbContext dbStudentManagementContext)
        {
            _context = dbStudentManagementContext;
        }

        public async Task AddEmployee(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task DeleteProofMapping(ProofMapping proofMapping)
        {
             _context.ProofMappings.Remove(proofMapping);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task<Employee> getemployeeIdById(int employeeId)
        {
            return await _context.Employees.FirstOrDefaultAsync(s => s.Id == employeeId);
            //throw new NotImplementedException();
        }

        public async Task<ProofMapping> IsImageExsist(string imageName)
        {
            return await _context.ProofMappings.FirstOrDefaultAsync(p => p.ProofPhoto == imageName);
            //throw new NotImplementedException();
        }

        public async Task UpdateEmployee(Employee employee)
        {
             _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }
    }
}
