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
            await _employeeService.AddEmployee(dto);
            return Ok("Employee Added");
        }

        [HttpPost("AddEmployeewithleavebalance")]
        public async Task<IActionResult> AddEmployeewithleavebalance([FromForm] EmpRequestDTO dto)
        {
            await _employeeService.AddEmployeewithleavebalance(dto);
            return Ok("Employee Added");
        }
        [HttpPost("ApplyLeave")]
        public async Task<IActionResult> ApplyLeave([FromForm] LeaveRequestRequestDTO dto)
        {
            await _employeeService.ApplyLeave(dto);
            return Ok("leave Added");
        }

        [HttpPost("UpdateLeave")]
        public async Task<IActionResult> UpdateLeave([FromForm] LeaveapprovedRequestDTO dto)
        {
            await _employeeService.UpdateLeave(dto);
            return Ok("UpdateLeave");
        }
    }
}
