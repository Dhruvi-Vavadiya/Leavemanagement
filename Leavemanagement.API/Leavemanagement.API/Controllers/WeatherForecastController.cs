using Leavemanagement.Service.ReqestDTO;
using Leavemanagement.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Leavemanagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public WeatherForecastController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

       
    }
}
