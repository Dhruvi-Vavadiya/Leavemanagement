using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Leavemanagement.Repository.Entity
{
    public class LeaveManagementsDbContext:DbContext
    {
        public LeaveManagementsDbContext(DbContextOptions<LeaveManagementsDbContext> options)
           : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProofMapping> ProofMappings { get; set; }
        public DbSet<LeaveBalance> leaveBalances { get; set; }
        public DbSet<LeaveRequest> leaveRequests { get; set; }


    }
}
