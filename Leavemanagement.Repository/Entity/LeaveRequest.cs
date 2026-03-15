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

        public string Resoan { get; set; }

        public int RoleId { get; set; }

        public DateTime? ManagerapprovalDate { get; set; }

        [ForeignKey("ManagerId")]
        public int? ManagerId { get; set; } //1
        public Employee Managers { get; set; }


        public DateTime? HRapprovalDate { get; set; }

        [ForeignKey("HRId")]
        public int? HRId { get; set; } //2
        public Employee HRs { get; set; }    

        public DateTime? AdminApprovalDate { get; set; }

        [ForeignKey("AdminId")]
        public int? AdminId { get; set; } //3
        public Employee Admins { get; set; }

        public int Status { get; set; }
    }
}
