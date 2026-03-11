using Leavemanagement.Repository.Entity;
using Leavemanagement.Repository.Repository;
using Leavemanagement.Service.ReqestDTO;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;

namespace Leavemanagement.Service.Service
{
    public class EmployeeSerivce:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly IWebHostEnvironment _environment;

        public EmployeeSerivce(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            //_environment = environment;
        
        }

        public async Task AddStudent(EmpRequestDTO empRequestDTO)
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
            if (empRequestDTO.RemoveImages != null && empRequestDTO.RemoveImages.Any())
            {
                foreach (var img in empRequestDTO.RemoveImages)
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", img.TrimStart('/'));

                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }

                    //var removeMapping = employee.proofMappings
                    //    .FirstOrDefault(x => x.ProofPhoto == img);

                    //if (removeMapping != null)
                    //{
                    //    employee.proofMappings.Remove(removeMapping);
                    //}
                    var removemapping = await _employeeRepository.IsImageExsist(img);

                    if (removemapping != null)
                    {
                        await _employeeRepository.DeleteProofMapping(removemapping);
                    }
                }
            }

            // Upload new images
            if (empRequestDTO.ProofPhotos != null)
            {
                foreach (var file in empRequestDTO.ProofPhotos)
                {
                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        string dbPath = "/Images/" + fileName;

                        // Check duplicate
                        bool exists = employee.proofMappings.Any(x => x.ProofPhoto == dbPath);

                        if (!exists)
                        {
                            employee.proofMappings.Add(new ProofMapping
                            {
                                EmployeeId = employee.Id,
                                ProofPhoto = dbPath
                            });
                        }
                    }
                }
            }

            employee.Name = empRequestDTO.Name;

            if (empRequestDTO.Id == 0)
            {
                await _employeeRepository.AddEmployee(employee);
            }
            else
            {
                await _employeeRepository.UpdateEmployee(employee);
            }
        }
    }
}
