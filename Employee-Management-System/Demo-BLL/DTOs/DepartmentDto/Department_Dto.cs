using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.DTOs.DepartmentDto
{
    public class Department_Dto
    {
        public int Dept_Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateOnly DateOfCreation { get; set; }

    }
}
