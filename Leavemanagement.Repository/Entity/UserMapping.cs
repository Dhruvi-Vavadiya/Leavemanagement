using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Leavemanagement.Repository.Entity
{
    public class UserMapping
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }

        public Employee Employee { get; set; }
        public Roles Role { get; set; }
    }
}
