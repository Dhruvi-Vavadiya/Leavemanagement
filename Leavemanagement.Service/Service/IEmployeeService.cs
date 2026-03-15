using Leavemanagement.Service.ReqestDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leavemanagement.Service.Service
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmpRequestDTO empRequestDTO);

        Task AddEmployeewithleavebalance(EmpRequestDTO empRequestDTO);

        Task ApplyLeave(LeaveRequestRequestDTO leaveRequestRequestDTO);

        Task UpdateLeave(LeaveapprovedRequestDTO leaveRequestRequestDTO);

    }
}
