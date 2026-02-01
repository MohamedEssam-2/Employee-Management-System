using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo_BLL.DTOs.DepartmentDto;
using Demo_BLL.Factories;
using Demo_BLL.Services.Interfaces;
using Demo_DAL.Data.Repositories.Interfaces;
using Demo_DAL.Models.Department;

namespace Demo_BLL.Services.Classes
{
    public class Department_Service(IUnitOfWork unitOfWork) : IDepartment_Service
    {
        //private readonly IDepartment_Repository _Repository = Repository;


        //old way in manual mapping
        //public IEnumerable<Department_Dto> GetAllDepartmnets()
        //{
        //    var depts=_Repository.GetAll();
        //    var DepartmentToReturn = depts.Select(d => new Department_Dto()
        //    {
        //        Dept_Id = d.Id,
        //        Name = d.Name,
        //        Code = d.Code,
        //        Description = d.Description,
        //        DateOfCreation = DateOnly.FromDateTime(d.Created_On)
        //    }); return DepartmentToReturn;
        //}

        #region Extension Method
        public IEnumerable<Department_Dto> GetAllDepartmnets(string? DepartmenSearchName)
        {
            IEnumerable<Department> depts;
            if (!String.IsNullOrEmpty(DepartmenSearchName))
            {
                depts = unitOfWork.Department_Repository.GetAll(d => d.Name.ToLower().Contains(DepartmenSearchName.ToLower()));
            }
            else 
            {
                depts = unitOfWork.Department_Repository.GetAll(); 
            }
             var DepartmentToReturn = depts.Select(d => d.CreateDepartmentDto());
            return DepartmentToReturn;
        }
        #endregion

        public Department_Details_Dto? GetDepartmentById(int id)
        {
            #region Old Way in Manual Mappping
            //var dept = _Repository.GetById(id);
            //if (dept is null) return null;
            //else
            //{
            //    var DepartmentToReturn = new Department_Details_Dto()
            //    {
            //        Id = dept.Id,
            //        Name = dept.Name,
            //        Code = dept.Code,
            //        Description = dept.Description,
            //        DateOfCreation = DateOnly.FromDateTime(dept.Created_On),
            //        LastMdifedOn = DateOnly.FromDateTime(dept.Last_Mdifed_On),
            //        CreatedBY = dept.Created_By,
            //        LastModifiedBY = dept.Last_Modified_By,
            //        Is_Deleted = dept.IsDeleted,
            //    };
            //    return DepartmentToReturn;
            //} 
            #endregion
            #region Constructor Mapping 
            //var dept = _Repository.GetById(id);
            //return dept is null ? null : new Department_Details_Dto(dept);
            #endregion
            #region Extension Method
            var dept = unitOfWork.Department_Repository.GetById(id);
            return dept is null ? null : dept.CreateDepartmentDetailsDto();
            #endregion

        }

        public int AddDepartment(Add_Department_Dto dept)
        {
            var entity = dept.ToEntity();
            unitOfWork.Department_Repository.Add(entity);
            return unitOfWork.SaveChanges();

        }
        public int UpdateDepartment(Update_Department_Dto dept)
        {
            unitOfWork.Department_Repository.Update(dept.ToEntity());
            return unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var dept = unitOfWork.Department_Repository.GetById(id);
            if (dept is null) return false;
            else
            {
                unitOfWork.Department_Repository.Remove(dept);
                return unitOfWork.SaveChanges() > 0 ? true : false;

            }


        }
    }
}
