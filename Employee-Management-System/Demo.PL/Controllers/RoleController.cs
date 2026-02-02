using System.Buffers;
using Demo.PL.ViewModels.IdentityViewModels;
using Demo.PL.ViewModels.RoleViewModel;
using Demo_DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize(Roles ="Super Admin")]
    public class RoleController(RoleManager<IdentityRole> _roleManager,IWebHostEnvironment env,UserManager<ApplicationUser>_userManager) : Controller
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
        public  IActionResult Edit(string? id )
        {
            if (id is null)
                return BadRequest();
            var role =  _roleManager.FindByIdAsync(id).Result;
            if (role is null)
                return NotFound();
            var users =  _userManager.Users.ToListAsync().Result;
            return View(new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Users = users.Select(user => new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(roleViewModel);

            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();

                // Update role name
                role.Name = roleViewModel.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return View(roleViewModel);
                }

                // Update users in role
                foreach (var userRole in roleViewModel.Users)
                {
                    var user = await _userManager.FindByIdAsync(userRole.UserId);
                    if (user == null) continue;

                    bool isInRole = await _userManager.IsInRoleAsync(user, role.Name);

                    if (userRole.IsSelected && !isInRole)
                        await _userManager.AddToRoleAsync(user, role.Name);

                    else if (!userRole.IsSelected && isInRole)
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    env.IsDevelopment() ? ex.Message : "Role cannot be updated");

                return View(roleViewModel);
            }
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

    

//