using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leavemanagement.Repository.Entity
{
    public class ProofMapping
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        public string ProofPhoto { get; set; }

        public Employee Employees { get; set; }



    }
}
