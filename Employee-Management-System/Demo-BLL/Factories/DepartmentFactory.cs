using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Demo_BLL.DTOs.DepartmentDto;
using Demo_DAL.Models.Department;

namespace Demo_BLL.Factories
{
    internal static class DepartmentFactory
    {
        public static Department_Dto CreateDepartmentDto(this Department d )
        {
            return new Department_Dto
            {
                Dept_Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = DateOnly.FromDateTime(d.Created_On)
            };
        }


        public static Department_Details_Dto CreateDepartmentDetailsDto(this Department d)
        {
            return new Department_Details_Dto()
            {
                Id = d.Id,
                Name = d.Name,
                Code = d.Code,
                Description = d.Description,
                DateOfCreation = DateOnly.FromDateTime(d.Created_On),
                LastModifedOn = DateOnly.FromDateTime(d.Last_Mdifed_On),
                CreatedBY = d.Created_By,
                ModifiedBY = d.Last_Modified_By,
                Is_Deleted = d.IsDeleted,
            };
        }

        public static Department ToEntity(this Add_Department_Dto dto)
        {
            return new Department
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Created_On = dto.DataOfCreation.ToDateTime(new TimeOnly()),

            };
        }
        public static Department ToEntity(this Update_Department_Dto dto)
        {
            return new Department
            {
                Id = dto.Id,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                Created_On = dto.DateOfCreation.ToDateTime(new TimeOnly()),

            };
        }
    }
}
