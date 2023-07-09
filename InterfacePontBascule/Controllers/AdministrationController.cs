using System.Runtime.InteropServices.JavaScript;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using InterfacePontBascule.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InterfacePontBascule.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // We just need to specify a unique role name to create a new role
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                // Saves the role in the underlying AspNetRoles table
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(string tt)
        {
            return View();
        }


        public async Task<IActionResult> ListUsers()
        {
            var users = _userManager.Users;


            var usersList = new List<UsersViewModel>();

            foreach (var user in users)
            {
                usersList.Add(new UsersViewModel
                {
                    IdentityUser = user,
                    IdentityRoles = await _userManager.GetRolesAsync(user),
                    IsEnabled = await _userManager.IsLockedOutAsync(user)

                });
            }
            return View(usersList);
        }

        
        public IActionResult EditUser(string id)
        {
            return View();
        } 
        
        public async Task<IActionResult> AddRoleToUser()
        {
         

            return Content("shit");
        }


        public async Task<IActionResult> LockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
           await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddYears(1));


            return RedirectToAction("ListUsers", "Administration");
        }



        public async Task<IActionResult> UnlockUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            //await _userManager.ResetAccessFailedCountAsync(user);
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now);

            return RedirectToAction("ListUsers", "Administration");
        }

    }
}
