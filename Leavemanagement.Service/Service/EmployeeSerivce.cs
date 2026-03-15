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

            getleaveblance.UsedLeaveCount = getleaveblance.UsedLeaveCount + totalDays;

            await _employeeRepository.updateLeavebalcnce(getleaveblance);

            await _employeeRepository.AddLeaveRequest(leave);
            //await _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public async Task UpdateLeave(LeaveapprovedRequestDTO leaveRequestRequestDTO)
        {
            //List<string> jwtroles = new List<string> { "Admin", "HR", "Manager" };
            //List<string> jwtroles = new List<string> { "Manager" };
            //List<string> jwtroles = new List<string> { "HR" };
            List<string> jwtroles = new List<string> { "Admin" };

            List<int> jwtroleid = new List<int>();

            foreach (var role in jwtroles)
            {
                // Use a fully-qualified reference to System.Enum to avoid conflict with the project's Enum namespace.
                if (global::System.Enum.TryParse<RoleEnum>(role, true, out RoleEnum roleEnum))
                {
                    jwtroleid.Add((int)roleEnum);
                }
            }
            int nextRole = 0;

            var getleavereqest = await _employeeRepository.GetLeaveRequestsByEmployeeId(leaveRequestRequestDTO.Id);

            if (getleavereqest == null) throw new Exception("Leave request not found.");

            var getleaveblance = await _employeeRepository.getemployeeleavebalance(getleavereqest.EmployeeId);
            if (getleavereqest.Status == (int)StatusEnum.Rejected)
            {
                nextRole = getleavereqest.RoleId;
                getleaveblance.UsedLeaveCount = (getleavereqest.EndDate - getleavereqest.StartDate).Days + 1;
            }
            else
            {
                nextRole = getleavereqest.RoleId + 1;
            }
            //status
            bool isRejected = false;
            if (leaveRequestRequestDTO.isApproved)
            {
                getleavereqest.Status = (int)StatusEnum.Approved;
            }
            else
            {
                isRejected = true;
                getleavereqest.Status = (int)StatusEnum.Rejected;
                getleavereqest.Resoan = leaveRequestRequestDTO.Reject_Reason;
            }

            //role


            if (jwtroleid.Contains(nextRole))
            {
                getleavereqest.RoleId = nextRole;

                if (nextRole == (int)RoleEnum.Manager)
                {
                    if (!isRejected)
                    {
                        getleavereqest.ManagerapprovalDate = DateTime.UtcNow;
                        getleavereqest.ManagerId = nextRole; //jwt useid
                        //send email to all hr for approval
                        Console.WriteLine("send email to all hr for approval");
                    }

                }
                else if (nextRole == (int)RoleEnum.HR)
                {
                    if (!isRejected)
                    {
                        getleavereqest.HRapprovalDate = DateTime.UtcNow;
                        getleavereqest.HRId = nextRole;//jwt useid
                        //send email to all admin for approval
                        Console.WriteLine("send email to all admin for approval");
                    }
                }
                else if (nextRole == (int)RoleEnum.Admin)
                {
                    if (!isRejected)
                    {
                        getleavereqest.AdminApprovalDate = DateTime.UtcNow;
                        getleavereqest.AdminId = nextRole;//jwt useid
                        //send email to emp for approval
                        Console.WriteLine("send email to emp for approval");
                    }
                }
                else
                {
                    throw new Exception("you are not a part of approval proccess");
                }
                if (isRejected)
                {
                    //send email to emp for rejection
                    Console.WriteLine("Email sent to employee for rejection.");
                }
            }
            else
            {
                if (getleavereqest.RoleId == (int)RoleEnum.Admin)
                {
                    Console.WriteLine("Yor leave is completely confirm");
                }
                else
                    throw new Exception("Your are not aurthority");
            }


            if (isRejected)
            {
                getleaveblance.UsedLeaveCount = getleaveblance.UsedLeaveCount - getleavereqest.TotalLeaveDays;
            }

            //var updatelevaerequest =await CallCommonFunction(jwtroletoenumid,nextRole,getleavereqest);

            //getleavereqest.RoleId = updatelevaerequest.RoleId;


            await _employeeRepository.updateLeavebalcnce(getleaveblance);

            await _employeeRepository.UpdateLeaveRequest(getleavereqest);
        }

        private async Task<LeaveRequest> CallCommonFunction(List<int> roles, int nextRole, LeaveRequest upleavereqest)
        {
            if (roles.Contains(nextRole))
            {
                upleavereqest.RoleId = nextRole;

                if (nextRole == (int)RoleEnum.Manager)
                {
                    upleavereqest.ManagerapprovalDate = DateTime.UtcNow;
                    upleavereqest.ManagerId = nextRole;
                }
                else if (nextRole == (int)RoleEnum.HR)
                {
                    upleavereqest.HRapprovalDate = DateTime.UtcNow;
                    upleavereqest.HRId = nextRole;
                }
                else if (nextRole == (int)RoleEnum.Admin)
                {
                    upleavereqest.AdminApprovalDate = DateTime.UtcNow;
                    upleavereqest.AdminId = nextRole;
                }
            }

            return upleavereqest;
            // Common logic that needs to be executed before updating the leave request
            //await Task.Delay(100); // Simulating some asynchronous work
        }
    }
}
