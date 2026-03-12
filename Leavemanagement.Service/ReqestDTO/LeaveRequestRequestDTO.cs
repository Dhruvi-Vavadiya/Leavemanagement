using Leavemanagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leavemanagement.Service.ReqestDTO
{
    public class LeaveRequestRequestDTO
    {
      
       
        public int EmployeeId { get; set; }

        //public Employee Employees { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

       

    }
}
