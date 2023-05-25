using System;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using EFDbFirstApproachExample.Identity;

[assembly: OwinStartup(typeof(EFDbFirstApproachExample.Startup))]

namespace EFDbFirstApproachExample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            this.CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

            // Create Admin Role
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // Create Admin User
            if (userManager.FindByName("admin") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string userPassword = "admin@123";
                var chkUser = userManager.Create(user, userPassword);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            // Create Manager Role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            // Create Manager User
            if (userManager.FindByName("manager") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "manager";
                user.Email = "manager@gmail.com";
                string userPassword = "manager@123";
                var chkUser = userManager.Create(user, userPassword);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Manager");
                }
            }

            // Create Customer Role
            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole();
                role.Name = "Customer";
                roleManager.Create(role);
            }

        }
    }
}
