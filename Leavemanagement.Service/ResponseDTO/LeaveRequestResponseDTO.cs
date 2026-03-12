using Leavemanagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leavemanagement.Service.ResponseDTO
{
    public class LeaveRequestResponseDTO
    {
        public int Id { get; set; }

       
        public int EmployeeId { get; set; }

       

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalLeaveDays { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
