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
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserMapping> UserMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Employees)
                .WithMany(e => e.leaveRequestsEmployee)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Managers)
                .WithMany(e => e.leaveRequestsManager)
                .HasForeignKey(l => l.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.HRs)
                .WithMany(e => e.leaveRequestsHr)
                .HasForeignKey(l => l.HRId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.Admins)
                .WithMany(e => e.leaveRequestsAdmin)
                .HasForeignKey(l => l.AdminId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
