using Microsoft.AspNetCore.Identity;
using ProfileWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileWeb.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole<int>(Enums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole<int>(Enums.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole<int>(Enums.Roles.Teacher.ToString()));
        }
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Firstname_kz = "admin",
                Lastname_kz = "admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Teacher.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    
                }

            }
        }
    }
}
