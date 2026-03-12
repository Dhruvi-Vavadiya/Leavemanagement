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

        public async Task updateLeavebalcnce(LeaveBalance employee)
        {
            var balance = await _context.leaveBalances
   .FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);

            if (balance.UsedLeaveCount >= balance.TotalLeaveCount)
            {
                throw new Exception("Annual leave limit reached");
            }
            _context.leaveBalances.Update(employee);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task AddLeaveRequest(LeaveRequest leaveRequest)
        {
            
            await _context.leaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task DeleteProofMapping(ProofMapping proofMapping)
        {
             _context.ProofMappings.Remove(proofMapping);
            await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task<int> existingLeave(int empid, int month, int year)
        {

           
            //var startMonth = leaveRequest.StartDate.Month;
            //var startYear = leaveRequest.StartDate.Year;

            var existingLeave = await _context.leaveRequests
                .Where(x => x.EmployeeId == empid &&
                            x.StartDate.Month == month &&
                            x.StartDate.Year == year)
                .CountAsync();
            return existingLeave;
            //throw new NotImplementedException();
        }

        public async Task<Employee> getemployeeIdById(int employeeId)
        {
            return await _context.Employees.FirstOrDefaultAsync(s => s.Id == employeeId);
            //throw new NotImplementedException();
        }

        public async Task<LeaveBalance> getemployeeleavebalance(int employeeId)
        {
            return await _context.leaveBalances.FirstOrDefaultAsync(s => s.EmployeeId == employeeId);
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
