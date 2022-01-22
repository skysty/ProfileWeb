using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProfileWeb.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public RoleManagerController(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName.Trim()));
            }
            return RedirectToAction("Index");
        }
    }
}
