using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Models.Shared;
using Demo_DAL.Models.Shared.Enums;

namespace Demo_DAL.Models.Employee
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; } 
        public string? Address { get; set; } = null!;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department.Department? Department { get; set; }
        public string ?ImageName { get; set; }

    }
}
