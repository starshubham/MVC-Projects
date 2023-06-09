using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Reporting_Assistant_New.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
[assembly: OwinStartup(typeof(Reporting_Assistant_New.Startup))]

namespace Reporting_Assistant_New
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            this.CreateRolesandUser();
        }
        public void CreateRolesandUser()
        {
            var ManageRole= new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var db = new ApplicationDbContext();
            var userAppUserStore = new ApplicationUserStore(db);
            var userManager = new ApplicationManagercs(userAppUserStore);
            if (!ManageRole.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                ManageRole.Create(role);//AspNetRole table.
            }

            //Behind code is to create new admin user in database if data is not exist in database.
            
            if (userManager.FindByName("harsha")== null)
            {
                var user = new ApplicationUser();
                user.UserName = "Harsha";
                user.Email = "harsha.test.com";
                string password = "harsha123";
                    var chckUser = userManager.Create(user, password);//AspNetUsers Table
                    if (chckUser.Succeeded)
                    {
                        userManager.AddToRole(user.Id, "Admin");
                        userManager.AddToRole(user.Id, "User");
                    }
                
            }

            if (userManager.FindByName("Shylender") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Shylender";
                user.Email = "shylender.test.com";
                string password = "shylender123";
                var chckUser = userManager.Create(user, password);
                if (chckUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (userManager.FindByName("Harika") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Harika";
                user.Email = "harika.test.com";
                string password = "harika123";
                var chckUser = userManager.Create(user, password);
                if (chckUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }





            if (!ManageRole.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                ManageRole.Create(role);
            }

            //Behind code is to create new Manager user in database if data is not exist in database.

            //if (userManager.FindByName("harsha") == null)
            //{
            //    var user = new ApplicationUser();
            //    user.UserName = "Harsha";
            //    user.Email = "harsha.test.com";
            //    string password = "harsha123";
            //    var chckUser = userManager.Create(user, password);
            //    if (chckUser.Succeeded)
            //    {
            //        userManager.AddToRole(user.Id, "Manager");
            //    }
            //}
            if (userManager.FindByName("Nuzhath") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Nuzhath";
                user.Email = "nuzhath.test.com";
                string password = "nuzhath123";
                var chckUser = userManager.Create(user, password);//AspNetUsers Table
                if (chckUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                }
            }
            if (userManager.FindByName("Nazareen") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "Nazareen";
                user.Email = "nazareen.test.com";
                string password = "nazareenharsha123";
                var chckUser = userManager.Create(user, password);//AspNetUsers Table
                if (chckUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                }
            }
           
            //Creating Customer Role in database

            //if (!ManageRole.RoleExists("User"))
            //{
            //    var role = new IdentityRole();
            //    role.Name = "User";
            //    ManageRole.Create(role);
            //}

            ////Behind code is to create new Customer user in database if data is not exist in database.

            //if (userManager.FindByName("User") == null)
            //{
            //    var user = new ApplicationUser();
            //    user.UserName = "User";
            //    user.Email = "User.task.com";
            //    string password = "user123";
            //    var chckUser = userManager.Create(user, password);
            //    if (chckUser.Succeeded)
            //    {
            //        userManager.AddToRole(user.Id, "User");
            //    }
            //}
        }
    }
}
