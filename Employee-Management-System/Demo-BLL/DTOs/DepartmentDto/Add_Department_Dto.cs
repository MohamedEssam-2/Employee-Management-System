using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_BLL.DTOs.DepartmentDto
{
    public class Add_Department_Dto
    {

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly DataOfCreation { get; set; }
    }
}
