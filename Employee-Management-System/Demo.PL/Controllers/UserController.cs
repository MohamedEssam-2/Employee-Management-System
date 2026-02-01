using Demo.PL.ViewModels.IdentityViewModels;
using Demo_DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManager) : Controller
    {
        //Get all users
        [HttpGet]
        public IActionResult Index(string searchValue)
        {
            var USerQuery = _userManager.Users.AsQueryable();//Filtering in database 
            if (!string.IsNullOrEmpty(searchValue))
            {
                USerQuery = USerQuery.Where(u => u.Email!.Contains(searchValue));
            }
            var users = USerQuery.Select(u => new ViewModels.IdentityViewModels.UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName!,
                LastName = u.LastName!,
                Email = u.Email!,
            }).ToList();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;
                user.Roles = roles;
            }


            return View(users);
        }

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userVM = new UserViewModel()
            {
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result

            };
            return View(userVM);
        }

        [HttpGet]
      
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userVM);
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid) return View(userViewModel);
            if (userViewModel.Id is null) return BadRequest();
            try
            {
                var user = _userManager.FindByIdAsync(userViewModel.Id).Result;
                if (user is null) return NotFound();
                user.Email = userViewModel.Email;
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An Error Happen When Edit the User ");

            }
            return View(userViewModel);

        }
        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            string msg;
            try
            {
                var result = _userManager.DeleteAsync(user).Result;
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
//Test