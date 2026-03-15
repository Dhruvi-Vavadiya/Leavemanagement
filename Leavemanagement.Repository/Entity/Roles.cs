using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Leavemanagement.Repository.Entity
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<UserMapping> userMappings { get; set; } = new List<UserMapping>();
    }
}
