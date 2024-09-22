using Data.Models;
using hebamohsen_MVC_P03.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hebamohsen_MVC_P03.Controllers
{
    [Authorize(Roles="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager ,ILogger<RoleController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleModel)
        {
            var role = new IdentityRole
            {
                Name = roleModel.Name
            };
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role); 
                
                if(result.Succeeded)
               
                    return RedirectToAction("Index");

                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);
               
                
            }
            return View(roleModel);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            
                var roleUpdateViewModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return View(viewName, roleUpdateViewModel);
            
          
        }

        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, RoleViewModel roleModel)
        {
            if (id != roleModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();

                    user.Name = roleModel.Name;
                    user.NormalizedName = roleModel.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully");
                        return RedirectToAction("Index");
                    }

                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return View(roleModel);

        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();

                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var item in result.Errors)
                    _logger.LogError(item.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            ViewBag.RoleId = roleId;

            var users = await _userManager.Users.ToListAsync();

            var usersRole = new List<UserInRoleViewModel>();

            foreach(var user in users)
            {
                var userRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userRole.IsSelected = true;
                else
                    userRole.IsSelected = false;

                usersRole.Add(userRole);
            }
            return View(usersRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            if(ModelState.IsValid)
            {
                foreach(var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if (appUser is null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser,role.Name))
                            await _userManager.AddToRoleAsync(appUser,role.Name);
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction("Update",new {id = roleId});
            }

            return View(users);
        }


    }
}
