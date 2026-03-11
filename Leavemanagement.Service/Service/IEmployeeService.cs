using Leavemanagement.Service.ReqestDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leavemanagement.Service.Service
{
    public interface IEmployeeService
    {
        Task AddStudent(EmpRequestDTO empRequestDTO);
    }
}
