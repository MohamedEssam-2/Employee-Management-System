using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_BLL.DTOs.DepartmentDto;
using Demo_BLL.DTOs.EmployeeDto;
using Demo_DAL.Data.Migrations;

namespace Demo_BLL.Services.Interfaces
{
    public interface IEmployee_Services
    {
            
        IEnumerable<EmployeeDto> GetAllEmployee(string? EmployeeSearchName,bool Withtracking=false);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int AddEmployee(CreateEmployeeDto dept);
        int UpdateEmployee(UpdateEmployeeDto dept);
        bool DeleteEmployee(int id);
    }
}
