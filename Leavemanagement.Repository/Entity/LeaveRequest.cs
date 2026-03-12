using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leavemanagement.Repository.Entity
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        public Employee Employees { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalLeaveDays { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
