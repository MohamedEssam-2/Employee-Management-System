using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_DAL.Models;

namespace Demo_BLL.DTOs.DepartmentDto
{
    public class Department_Details_Dto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly DateOfCreation { get; set; }
        public DateOnly LastModifedOn { get; set; }

        public int CreatedBY { get; set; }

        public int ModifiedBY { get; set; }

        public bool Is_Deleted { get; set; }

        #region Constructor Mappping
        //public Department_Details_Dto(Department dept)
        //{
        //    Id = dept.Id;
        //    Name = dept.Name;
        //    Code = dept.Code;
        //    Description = dept.Description;
        //    DateOfCreation = DateOnly.FromDateTime(dept.Created_On);
        //    LastMdifedOn = DateOnly.FromDateTime(dept.Last_Mdifed_On);
        //    CreatedBY = dept.Created_By;
        //    LastModifiedBY = dept.Last_Modified_By;
        //    Is_Deleted = dept.IsDeleted;
        //} 
        #endregion

    }
}
