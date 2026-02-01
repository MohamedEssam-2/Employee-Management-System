using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Demo_BLL.Attachment.Interface;
using Demo_BLL.DTOs.EmployeeDto;
using Demo_BLL.Profiles;
using Demo_BLL.Services.Interfaces;
using Demo_DAL.Data.Repositories.Interfaces;
using Demo_DAL.Models.Employee;

namespace Demo_BLL.Services.Classes
{
    public class Employee_Service(IUnitOfWork unitOfWork,IMapper _mapper,IAttachment _attachment) : IEmployee_Services
    {
        public IEnumerable<EmployeeDto> GetAllEmployee(string? EmployeeSearchName, bool Withtracking = false)
        {



            #region Old way
            //var emp= employee_Repository.GetAll();

            //  var emplist = emp.Select(e => new EmployeeDto
            //  {
            //      Id = e.Id,
            //      Name = e.Name,
            //      Age = e.Age,
            //      Salary = e.Salary,
            //      IsActive= e.IsActive,
            //      Email = e.Email,
            //      Gender=e.Gender.ToString(),
            //      EmployeeType = e.EmployeeType.ToString()
            //  });
            //  return emplist; 
            #endregion

            #region AutoMapper (Used)
            IEnumerable<Employee> emp;
            if (!String.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                 emp = unitOfWork.Employee_Repository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }
            else
            {
                emp= unitOfWork.Employee_Repository.GetAll(Withtracking);
            }
            var EmplyeeDTo = _mapper.Map<IEnumerable<EmployeeDto>>(emp);
            return EmplyeeDTo;
            #endregion

            #region GetALL with IEnumerable

            //var empdto = employee_Repository.GetALLIEnumerable().Where(e => e.IsDeleted == false)
            //      .Select(e => new EmployeeDto
            //      {
            //          Id = e.Id,
            //          Name = e.Name,
            //          Age = e.Age,
            //          Salary = e.Salary,
            //      });

            //return empdto;

            //IEnumerable return all data then filter in memory
            //Filtering in BBL (Services layer) its not good idea as can any one change in the query that sent to database
            //Filtering should be done inside the Repository (DAL Layer)

            #endregion

            #region GetALL with IQueryable

            //var empdto = employee_Repository.GetALLIQueryable().Where(e => e.IsDeleted == false)
            //      .Select(e => new EmployeeDto
            //      {
            //          Id = e.Id,
            //          Name = e.Name,
            //          Age = e.Age,
            //          Salary = e.Salary,
            //      });

            //return empdto;

            // IQueryable filter in database first then return data to memory 
            // cant use IQueryable alone in services as it communicate with database directly 
            // so must use it with repository as a DAL between services and database then use IEnumerable in services 
            // and then converted to IEnumerable when returning data to the Service Layer
            #endregion

            #region  GetAll Employees Using Repository (IQueryable and IEnumerable)
            //var emp = employee_Repository.GetAll(e => new EmployeeDto()
            ////Dynamic Filtering =>Can Change the Data (Selector) That return to the Memory
            //{

            //    Id = e.Id,
            //    Name = e.Name,
            //    Salary = e.Salary,
            //    Age = e.Age
            //});
            //return emp;
            // IQueryable used in repository then use IEnumerable in Services 
            // IQueryable is used inside the Repository to let EF Core build the SQL query
            //SELECT[e].[Id], [e].[Name], [e].[Salary], [e].[Age]
            //FROM[Employees] AS[e]
            //WHERE[e].[IsDeleted] = CAST(0 AS bit)


            #endregion
        }



        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var emp = unitOfWork.Employee_Repository.GetById(id);
            if (emp == null) return null;
            #region Old way
          //  var empDto = new EmployeeDetailsDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Address = emp.Address,
            //    Salary = emp.Salary,
            //    IsActive = emp.IsActive,
            //    Email = emp.Email,
            //    PhoneNumber = emp.PhoneNumber,
            //    Gender = emp.Gender.ToString(),
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    CreatedBy = emp.Created_By,
            //    CreatedOn = emp.Created_On,
            //    HiringDate = DateOnly.FromDateTime(emp.HiringDate),
            //    LastModifiedBy = emp.Last_Modified_By,
            //    LastModifiedOn = emp.Last_Mdifed_On
            //};
            //return empDto; 
            #endregion
            var empDto= _mapper.Map<EmployeeDetailsDto>(emp);
            return empDto;
        }


        public int AddEmployee(CreateEmployeeDto dto)
        {
            var empDto = _mapper.Map<Employee>(dto);
            if (dto.ImageName is not null)
            {
                string ?imgname = _attachment.Updload(dto.ImageName, "Image");
                empDto.ImageName = imgname;
            }
            unitOfWork.Employee_Repository.Add(empDto);
            return unitOfWork.SaveChanges();

        }

        public int UpdateEmployee(UpdateEmployeeDto dto)
        {
            var empDto = _mapper.Map<Employee>(dto);
            unitOfWork.Employee_Repository.Update(empDto);
            return unitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
           var emp=unitOfWork.Employee_Repository.GetById(id);
            if (emp == null) return false;
            else
            {
                emp.IsDeleted = true;

                unitOfWork.Employee_Repository.Update(emp);
                return unitOfWork.SaveChanges() > 0 ? true : false;


            }
          
        }

 
    }
}
