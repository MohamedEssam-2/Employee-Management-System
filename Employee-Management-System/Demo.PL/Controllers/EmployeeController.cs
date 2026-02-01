using System.Net;
using Demo.PL.ViewModels.EmployeeViewModel;
using Demo_BLL.DTOs.DepartmentDto;
using Demo_BLL.DTOs.EmployeeDto;
using Demo_BLL.Services.Classes;
using Demo_BLL.Services.Interfaces;
using Demo_DAL.Models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.PL.Controllers
{
    public class EmployeeController(IEmployee_Services employee_Services, IWebHostEnvironment _enviroment , ILogger<EmployeeController> _logger
) : Controller
    {
        #region Index
        public IActionResult Index(string ? EmployeeSearchName)
        {

            #region ViewDate & ViewBag
            //ViewBag and ViewData to send data from controller to view , view to Layout , View to partial view 
            //viewdata and ViewBag is  a dictionary object that mean they overide each other and they called (ViewStorage)
            //viewbag is dynamic object
            //viewdata requires type casting but viewbag not require type casting
            //viewdata is used in older version but viewbag is used in newer version of c#
            //viewdata (safe) Required casting(Type Safety) and  viewbag (not safe)(Weekly Typed)
            //viewdata best in Performance 


            //ViewData["Message"] = "Hello From View Data";//retrun Object type
            //ViewData.Add("Titles", "Employee List Page");
            ViewBag.Message = "Hello From Employee List";

            #endregion
            var e = employee_Services.GetAllEmployee(EmployeeSearchName);
            return View(e);
        }
        #endregion

        #region Create
        #region Create Get
        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartment_Service department_Service*/)
        {
            //var departments = department_Service.GetAllDepartmnets();
            //ViewData["Departments"] = departments;
            return View();
        }
        #endregion

        #region Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Rejects requests without the correct token to enhance security
        //Automatically works with @Html.AntiForgeryToken() in Razor views => method that was in older versions of c# but right now it's implemented in framework
        //work only with the post method

        public IActionResult Create(EmployeeViewModel viewModel)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    var res = employee_Services.AddEmployee(new CreateEmployeeDto()
                    {
                        Name = viewModel.Name,
                        Age = viewModel.Age,
                        Address = viewModel.Address,
                        Salary = viewModel.Salary,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber,
                        IsActive = viewModel.IsActive,
                        Gender = viewModel.Gender,
                        EmployeeType = viewModel.EmployeeType,
                        HiringDate = viewModel.HiringDate,
                        DepartmentId = viewModel.DepartmentId,
                        ImageName = viewModel.ImageName,
                    });
                    string msg;
                    //TempData=>ADD Toast Message after Redicrect to Another Action
                    if (res > 0)
                    {
                        msg = "Employee Created Successfully";
                        TempData["MSG"] = msg;
                        return RedirectToAction(nameof(Index));
                       
                    }
                    else
                    {
                        msg = "Employee Cant Created Successfully";
                        TempData["MSG"] = msg;
                        ModelState.AddModelError("", "Employee cant be added");
                        return View(viewModel);
                    }
                    
                }
                catch (Exception ex)
                {
                    if (_enviroment.IsDevelopment())
                    {
                        ModelState.AddModelError("", ex.Message);
                        return View(viewModel);
                    }
                    else
                    {
                        //_logger.LogError(ex, "Failed to add employee");
                        ModelState.AddModelError("", "Employee can't be added");
                        return View(viewModel);
                    }
                }
            }
            else
            {
                return View();
            }

        }

        #endregion
        #endregion

        #region Details
        #region Details GEt
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var emp = employee_Services.GetEmployeeById(id.Value);
            if (emp is null) return NotFound();
            return View(emp);
        }
        #endregion
        #endregion

        #region New Edit With using Partail View
        #region Edit GEt
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var emp = employee_Services.GetEmployeeById(id.Value);
            if (emp is null) return NotFound();

            var viewModel = new EmployeeViewModel
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Address = emp.Address,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Salary = emp.Salary,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                EmployeeType = Enum.Parse<EmployeeType>(emp.EmployeeType),
                Gender = Enum.Parse<Gender>(emp.Gender),
                DepartmentId=emp.DepartmentId

            };

            return View(viewModel);
        }
        #endregion


        #region Edit Post 
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel model)//**DepartmentID
        {
            if (id is null) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            try
            {
             
                var dto = new UpdateEmployeeDto
                {
                    Id = id.Value,
                    Name = model.Name,
                    Age = model.Age,
                    Address = model.Address,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Salary = model.Salary,
                    HiringDate = model.HiringDate,
                    IsActive = model.IsActive,
                    EmployeeType = model.EmployeeType,
                    Gender = model.Gender
                };

                var res = employee_Services.UpdateEmployee(dto);

                if (res > 0)
                    return RedirectToAction(nameof(Index));

              
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }  
        #endregion
        #endregion



        #region Old Edit (Without using Partial View)

        //#region Edit
        //#region Edit Get
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var emp = employee_Services.GetEmployeeById(id.Value);
        //    if (emp is null) return NotFound();

        //    var dto = new UpdateEmployeeDto
        //    {
        //        Id = emp.Id,
        //        Name = emp.Name,
        //        Age = emp.Age,
        //        Address = emp.Address,
        //        Email = emp.Email,
        //        PhoneNumber = emp.PhoneNumber,
        //        Salary = emp.Salary,
        //        HiringDate = emp.HiringDate,
        //        IsActive = emp.IsActive,
        //        EmployeeType = Enum.Parse<EmployeeType>(emp.EmployeeType),
        //        Gender = Enum.Parse<Gender>(emp.Gender),

        //    };

        //    return View(dto);
        //}
        //#endregion
        //#region Edit Post
        //[HttpPost]
        //public IActionResult Edit([FromRoute] int? id, UpdateEmployeeDto dto)
        //{
        //    if (id is null) return BadRequest();
        //    if (!ModelState.IsValid) return View(dto);
        //    try
        //    {
        //        #region Old way 
        //        //using view models 

        //        //var deptForUpdate = new Update_Department_Dto(viewModel)
        //        //{
        //        //    Id = viewModel.Id,
        //        //    Name = viewModel.Name,
        //        //    Code = viewModel.Code,
        //        //    Description = viewModel.Description,
        //        //    DateOfCreation = viewModel.DateOfCreation
        //        //}; 
        //        #endregion
        //        var res = employee_Services.UpdateEmployee(dto);
        //        if (res > 0) return RedirectToAction(nameof(Index));
        //        else
        //        {
        //            return View(dto);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return View(dto);
        //    }
        //}

        //#endregion
        //#endregion 
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool is_deleted = employee_Services.DeleteEmployee(id);
                if (is_deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError("", "Failed to delete department.");

                    return RedirectToAction(nameof(Delete), new { id = id });
                }
            }
            catch (Exception ex)
            {
                if (_enviroment.IsDevelopment())
                {
                    //Development
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }
                else
                {
                    //Deployment
                    return View();
                }
            }
        }

        #endregion
    } 
}

