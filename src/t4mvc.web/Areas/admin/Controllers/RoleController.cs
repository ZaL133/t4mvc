using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Data;
using t4mvc.core;
using t4mvc.web.Areas.admin.Models;

namespace t4mvc.web.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<t4mvcRole> roleManager;
        private readonly UserManager<t4mvcUser> userManager;

        public RoleController(RoleManager<t4mvcRole> roleManager, UserManager<t4mvcUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(t4mvcRole role)
        {
            var existingRole = await roleManager.FindByNameAsync(role.Name);
            if (existingRole == null)
            {
                await roleManager.CreateAsync(new t4mvcRole { Name = role.Name });
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Details(Guid? id)
        {
            return View(id.Value);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(UserToRole addUsertoRole)
        {
            var user    = await userManager.FindByIdAsync(addUsertoRole.UserId.ToString());
            var role    = await roleManager.FindByIdAsync(addUsertoRole.RoleId.ToString());
            var result  = await userManager.AddToRoleAsync(user, role.Name);

            return RedirectToAction("Details", new { id = role.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(UserToRole usertoRole)
        {
            var user    = await userManager.FindByIdAsync(usertoRole.UserId.ToString());
            var role    = await roleManager.FindByIdAsync(usertoRole.RoleId.ToString());
            var result  = await userManager.RemoveFromRoleAsync(user, role.Name);

            return RedirectToAction("Details", new { id = role.Id });
        }
    }
}
