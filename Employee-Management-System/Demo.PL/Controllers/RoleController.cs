using System.Buffers;
using Demo.PL.ViewModels.RoleViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager,IWebHostEnvironment env) : Controller
    {
        public IActionResult Index(string? searchValue)
        {
            var RoleQuery = _roleManager.Roles.AsQueryable();//Filtering in database 
            if (!string.IsNullOrEmpty(searchValue))
            {
                RoleQuery = RoleQuery.Where(r => r.Name!.Contains(searchValue));
            }
            var Roles = RoleQuery.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name= r.Name
            }).ToList();

            return View(Roles);
            
        }
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var roleVm = new RoleViewModel()
            {
                /*Id = id,*/
                Name = role.Name,
            };

            return View(roleVm);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleViewModel);
            }
            var role = new IdentityRole()
            {
                Name = roleViewModel.Name,
            };
            var result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(roleViewModel);
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(string? id ,RoleViewModel roleViewModel)
        {
            string msg="";
            if (!ModelState.IsValid)
            {
                return View(roleViewModel);
            }
            if (id !=roleViewModel.Id) return BadRequest();
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role is null) return BadRequest();
                ;
                role.Name = roleViewModel.Name;
                var updateRole = _roleManager.UpdateAsync(role).Result;
                if (updateRole.Succeeded)
                {
                    return  RedirectToAction("Index");
                }
                else
                {
                    msg = "Role Can not Be Updated";
                }
            }
            catch(Exception ex )
            {
                msg = env.IsDevelopment() ? ex.Message : "Role Can not be Updated , Error Happen Here ! ";
            }
            ModelState.AddModelError("", msg);
            return View(roleViewModel);
        }


        [HttpPost]
        public IActionResult Delete(string id)
        {
            var user = _roleManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            string msg="";
            try
            {
                var result = _roleManager.DeleteAsync(user).Result;
                if (result.Succeeded) RedirectToAction(nameof(Index));
                else
                {
                    msg = "the user can not be deletd";
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An Error Happen When Edit the User ");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

    

