using Demo.PL.ViewModels.DepartmentVewModel;
using Demo_BLL.DTOs;
using Demo_BLL.DTOs.DepartmentDto;
using Demo_BLL.Services.Interfaces;
using Demo_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartment_Service _department_Service;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        public DepartmentController(IDepartment_Service department_Service, ILogger<HomeController> Logger, IWebHostEnvironment environment)
        {
            _department_Service = department_Service;
            _logger = Logger;
            _environment = environment;
        }

        #region Index
        [HttpGet]
        public IActionResult Index(string? DepartmenSearchName)
        {
            var department = _department_Service.GetAllDepartmnets(DepartmenSearchName);
            return View(department);
        }
        #endregion

        #region Create
        #region Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        } 
        #endregion

        #region Post
        [HttpPost]
        public IActionResult Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _department_Service.AddDepartment(new Add_Department_Dto()
                    {
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        Description = viewModel.Description,
                        DataOfCreation = viewModel.DateOfCreation

                    });
                    if (res > 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add department.");
                        return View(viewModel);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        //Development
                        ModelState.AddModelError("", ex.Message);
                        return View(viewModel);
                    }
                    else
                    {
                        //Deployment
                        return View(viewModel);
                    }
                }
            }
            else
            {
                return View(viewModel);
            }

        }
        #endregion
        #endregion


        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var dept = _department_Service.GetDepartmentById(id.Value);
            if (dept is null) return NotFound();

            return View(dept);
        }


        #endregion


        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var dept = _department_Service.GetDepartmentById(id.Value);
            if (dept is null) return NotFound();

            var deptForUpdate = new DepartmentViewModel()
            {
                Id = id.Value,
                Name = dept.Name,
                Code = dept.Code,
                Description = dept.Description,
                DateOfCreation = dept.DateOfCreation
            };
            return View(deptForUpdate);
        }
        #endregion
        #region Post
        [HttpPost]
        public IActionResult Edit(DepartmentViewModel viewModel, [FromRoute] int? id)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var deptForUpdate = new Update_Department_Dto()
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Code = viewModel.Code,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };
                var res = _department_Service.UpdateDepartment(deptForUpdate);
                if (res > 0) return RedirectToAction(nameof(Index));
                else
                {
                    return View(deptForUpdate);
                }

            }
            catch (Exception e)
            {
                return View(viewModel);
            }
        }
        #endregion
        #endregion

        #region Delete
        //#region Get
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var dept = _department_Service.GetDepartmentById(id.Value);
        //    if (dept is null) return NotFound();
        //    return View(dept);
        //}
        //#endregion

        #region Post
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool is_deleted = _department_Service.DeleteDepartment(id);
                if (is_deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError("", "Failed to delete department.");

                    return RedirectToAction(nameof(Delete), new { id = id });
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
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
        #endregion
    }
}
