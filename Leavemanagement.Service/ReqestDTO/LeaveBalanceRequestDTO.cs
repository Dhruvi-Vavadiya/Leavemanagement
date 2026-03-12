using Leavemanagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leavemanagement.Service.ReqestDTO
{
    public class LeaveBalanceRequestDTO
    {
        public int EmployeeId { get; set; }
        public Employee Employees { get; set; }
        public int TotalLeaveCount { get; set; } = 12;

        public int UsedLeaveCount { get; set; }
    }
}
