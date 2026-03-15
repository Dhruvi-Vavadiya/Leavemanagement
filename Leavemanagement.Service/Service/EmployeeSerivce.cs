using Leavemanagement.Repository.Entity;
using Leavemanagement.Repository.Repository;
using Leavemanagement.Service.Enum;
using Leavemanagement.Service.ReqestDTO;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System;
using System.Collections.Generic;
using Leavemanagement.Service.Enum;

namespace Leavemanagement.Service.Service
{
    public class EmployeeSerivce : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly IWebHostEnvironment _environment;

        public EmployeeSerivce(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            //_environment = environment;

        }

        public async Task AddEmployee(EmpRequestDTO empRequestDTO)
        {
            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var employee = await _employeeRepository.getemployeeIdById(empRequestDTO.Id);

            if (employee == null)
            {
                employee = new Employee();
            }

            // Remove images
            //if (empRequestDTO.RemoveImages != null && empRequestDTO.RemoveImages.Any())
            //{
            //    foreach (var img in empRequestDTO.RemoveImages)
            //    {
            //        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.TrimStart('/'));

            //        if (File.Exists(imagePath))
            //        {
            //            File.Delete(imagePath);
            //        }

            //        //var removeMapping = employee.proofMappings
            //        //    .FirstOrDefault(x => x.ProofPhoto == img);

            //        //if (removeMapping != null)
            //        //{
            //        //    employee.proofMappings.Remove(removeMapping);
            //        //}
            //        var removemapping = await _employeeRepository.IsImageExsist(img);

            //        if (removemapping != null)
            //        {
            //            await _employeeRepository.DeleteProofMapping(removemapping);
            //        }
            //    }
            //}

            //// Upload new images
            //if (empRequestDTO.ProofPhotos != null)
            //{
            //    foreach (var file in empRequestDTO.ProofPhotos)
            //    {
            //        if (file.Length > 0)
            //        {
            //            string fileName = Guid.NewGuid() + "_" + file.FileName;
            //            string filePath = Path.Combine(uploadPath, fileName);

            //            using (var stream = new FileStream(filePath, FileMode.Create))
            //            {
            //                await file.CopyToAsync(stream);
            //            }

            //            string dbPath = "/Images/" + fileName;

            //            // Check duplicate
            //            bool exists = employee.proofMappings.Any(x => x.ProofPhoto == dbPath);

            //            if (!exists)
            //            {
            //                employee.proofMappings.Add(new ProofMapping
            //                {
            //                    EmployeeId = employee.Id,
            //                    ProofPhoto = dbPath
            //                });
            //            }
            //        }
            //    }
            //}

            //employee.Name = empRequestDTO.Name;

            //if (empRequestDTO.Id == 0)
            //{
            //    await _employeeRepository.AddEmployee(employee);
            //}
            //else
            //{
            //    await _employeeRepository.UpdateEmployee(employee);
            //}
        }

        public async Task AddEmployeewithleavebalance(EmpRequestDTO empRequestDTO)
        {
            var leavebalnce = new LeaveBalance
            {
                EmployeeId = empRequestDTO.Id,
                TotalLeaveCount = 20,
                UsedLeaveCount = 0,
            };
            var newemp = new Employee
            {
                Name = empRequestDTO.Name,
                proofMappings = new List<ProofMapping>(),
                leaveBalances = leavebalnce != null ? new List<LeaveBalance> { leavebalnce } : new List<LeaveBalance>(),
            };
            await _employeeRepository.AddEmployee(newemp);

            //throw new NotImplementedException();
        }

        public async Task ApplyLeave(LeaveRequestRequestDTO leaveRequestRequestDTO)
        {
            //var startMonth = leaveRequestRequestDTO.StartDate.Month;
            //var startYear = leaveRequestRequestDTO.StartDate.Year;

            // Check leave already taken in current month
            //var existingLeave = await _context.LeaveRequests
            //    .Where(x => x.EmpId == dto.EmpId &&
            //                x.StartDate.Month == startMonth &&
            //                x.StartDate.Year == startYear)
            //    .CountAsync();

            //var existingLeave = await _employeeRepository.existingLeave(leaveRequestRequestDTO.EmployeeId,startMonth,startYear);
            //if (existingLeave >= 1)
            //{
            //    throw new Exception("Your current month leave is already used");
            //}

            int totalDays = (leaveRequestRequestDTO.EndDate - leaveRequestRequestDTO.StartDate).Days + 1;

            var leave = new LeaveRequest
            {
                EmployeeId = leaveRequestRequestDTO.EmployeeId,
                StartDate = leaveRequestRequestDTO.StartDate,
                EndDate = leaveRequestRequestDTO.EndDate,
                TotalLeaveDays = (leaveRequestRequestDTO.EndDate - leaveRequestRequestDTO.StartDate).Days + 1,
                Status = (int)StatusEnum.Pending,
                Resoan = leaveRequestRequestDTO.Resoan,
                RoleId = (int)RoleEnum.Defalt

            };

            var getleaveblance = await _employeeRepository.getemployeeleavebalance(leaveRequestRequestDTO.EmployeeId);

            getleaveblance.UsedLeaveCount = totalDays;

            await _employeeRepository.updateLeavebalcnce(getleaveblance);

            await _employeeRepository.AddLeaveRequest(leave);
            //await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task UpdateLeave(LeaveapprovedRequestDTO leaveRequestRequestDTO)
        {
            List<string> jwtroles = new List<string> { "Admin", "HR", "Manager", "Employee" };

            List<int> jwtroletoenumid = new List<int>();

            foreach (var role in jwtroles)
            {
                // Use a fully-qualified reference to System.Enum to avoid conflict with the project's Enum namespace.
                if (global::System.Enum.TryParse<RoleEnum>(role, true, out RoleEnum roleEnum))
                {
                    jwtroletoenumid.Add((int)roleEnum);
                }
            }

            var getleavereqest = await _employeeRepository.GetLeaveRequestsByEmployeeId(leaveRequestRequestDTO.Id);

            if (getleavereqest == null) throw new Exception("Leave request not found.");

            if (getleavereqest.RoleId == (int)RoleEnum.Defalt)
            {
                if (jwtroles.Contains(RoleEnum.Manager.ToString()))
                {
                    getleavereqest.ManagerapprovalDate = DateTime.UtcNow;
                    getleavereqest.RoleId = (int)RoleEnum.Manager;
                    //getleavereqest.ManagerId = leaveRequestRequestDTO.EmployeeId;
                }
            }
            else if (getleavereqest.RoleId == (int)RoleEnum.Manager)
            {
                if (jwtroles.Contains(RoleEnum.HR.ToString()))
                {
                    getleavereqest.HRapprovalDate = DateTime.UtcNow;
                    getleavereqest.RoleId = (int)RoleEnum.HR;
                    //getleavereqest.HRId = leaveRequestRequestDTO.EmployeeId;
                }
            }
            else if (getleavereqest.RoleId == (int)RoleEnum.HR)
            {
                if (jwtroles.Contains(RoleEnum.Admin.ToString()))
                {
                    getleavereqest.AdminApprovalDate = DateTime.UtcNow;
                    getleavereqest.RoleId = (int)RoleEnum.Admin;
                    //getleavereqest.AdminId = leaveRequestRequestDTO.EmployeeId;
                }

            }


            await _employeeRepository.UpdateLeaveRequest(getleavereqest);
        }
    }
}
