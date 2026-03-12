using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Leavemanagement.Repository.Entity
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public IList<ProofMapping> proofMappings { get; set; }  = new List<ProofMapping>();
        public IList<LeaveRequest> leaveRequests { get; set; } = new List<LeaveRequest>();
        public IList<LeaveBalance> leaveBalances { get; set; } = new List<LeaveBalance>();
    }
}
