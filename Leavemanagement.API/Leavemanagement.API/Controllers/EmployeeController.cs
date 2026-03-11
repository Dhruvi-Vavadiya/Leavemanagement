using Leavemanagement.Service.ReqestDTO;
using Leavemanagement.Service.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leavemanagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
  //      SELECT TOP(1000) [Id]
  //    ,[Name]
  //      FROM[db_leavemanagements].[dbo].[Employees];
  //SELECT TOP(1000) [Id]
  //    ,[EmployeeId]
  //    ,[ProofPhoto]
  //      FROM[db_leavemanagements].[dbo].[ProofMappings]




        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromForm] EmpRequestDTO dto)
        {
            await _employeeService.AddStudent(dto);
            return Ok("Employee Added");
        }
    }
}
