using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reporting_Assistant_New.ViewModel;
using Reporting_Assistant_New.Identity;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Runtime.Remoting.Contexts;
using Reporting_Assistant_New.Models;

namespace Reporting_Assistant_New.Controllers
{
    public class AccountController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        CommonDataController cdc = new CommonDataController();
        // GET: /Account/RegistrationPage
        [ActionName("RegistrationPage")]//action name and the view name should be same to execute the exact view on the browser.
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Registration data)
        {
               
           
                var checkdupli = db.Users.Where(m => m.Email == data.Email || m.PhoneNumber == data.Mobile || m.Email == data.Email && m.PhoneNumber == data.Mobile).ToList();
                if (checkdupli.Count == 0)
                {
                    if (ModelState.IsValid)
                    {
                        
                        var appdbcontext = new ApplicationDbContext();
                        var userstore = new ApplicationUserStore(appdbcontext);
                        var userManager = new ApplicationManagercs(userstore);
                        var passharsh = Crypto.HashPassword(data.Password);//encrypts password
                        var user = new ApplicationUser() { Email = data.Email, UserName = data.Username, PasswordHash = passharsh, Address = data.Address, city = data.City, PhoneNumber = data.Mobile, Birthday = data.DateOfBirth, country = data.Country };
                        IdentityResult result = userManager.Create(user);
                        var UserAuth = userManager.Find(data.Username, data.Password);
                        if (result != null && UserAuth != null)
                       {
                        //Role Creation
                        //TempData["ID"] = UserId[0].Id;
                        //TempData.Peek("ID");
                        //Session["UserId"] = UserId;
                        Session["strUserId"] = UserAuth.Id;
                        Session["UserName"] = UserAuth.UserName;
                        
                        //Session["UName"] = UserDetails.UserId;
                       // HttpCookie ApiToken = new HttpCookie("ApiToken", UserAuth.ApiToken);
                        HttpCookie UserId = new HttpCookie("UserId", user.Id);
                        Response.Cookies.Add(UserId);
                        UserId.Expires = DateTime.Now.AddMinutes(60);
                        HttpCookie username = new HttpCookie("UserName", user.UserName);
                        Response.Cookies.Add(username);
                        username.Expires = DateTime.Now.AddMinutes(60);
                        //Login

                        this.loginUser(userManager, user);//By this code we will use three line of code under loginuser method.
                        return RedirectToAction("Index", "Home");
                    }
                    
                
                    else
                    {
                        ModelState.AddModelError("My error", "Invalid data");
                        return View();
                    }
                }
                    else
                    {
                        ModelState.AddModelError("My error", "Invalid data");
                        return View();
                    }

                   
                }
                    

                else 
                {
                    ModelState.AddModelError("My error", "Data Already Exist");
                    return View();
                }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [OverrideExceptionFilters]//this code is to override exception filter which besically stops to execute the exception filter when any exception occured. This can be applied to any actio method to prevent that method to override any filter.
        public ActionResult Login(LoginViewModel lvm)
        {

            //applicationDbContext db = new applicationDbContext();
            //var passharsh=Crypto.HashPassword(lvm.password);
            //var data = db.Users.Where(m=> m.UserName==lvm.username && m.PasswordHash==passharsh).ToList();
            ApplicationDbContext db=new ApplicationDbContext();
            var userstore = new ApplicationUserStore(db);
            var usermanager = new ApplicationManagercs(userstore);
            var user = usermanager.Find(lvm.username, lvm.password);
            
            if(user!=null)
            {
                Session["UserId"] = user.Id;
                Session["UserName"] = user.UserName;

                HttpCookie UserId = new HttpCookie("UserId", user.Id);
                Response.Cookies.Add(UserId);
                UserId.Expires = DateTime.Now.AddMinutes(30);
                HttpCookie username = new HttpCookie("UserName", user.UserName);
                Response.Cookies.Add(username);
                username.Expires = DateTime.Now.AddMinutes(30);
                this.loginUser(usermanager, user);//By this code we will use three line of code under loginuser method.

                if(usermanager.IsInRole(user.Id,"Admin"))
                {
                    return RedirectToAction("Home", "AdminHome", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");   
                }
                
            }
            else
            {
                ModelState.AddModelError("myerror","Invalid Username or password");
                return View();
            }
        }
        
        public ActionResult Logout()
        {
            var authmanager = HttpContext.GetOwinContext().Authentication;
            authmanager.SignOut();
            return RedirectToAction("Index","home");
        }
       
        
        
        [NonAction] //As this action method is being created for re usability of these three lines of code which is needed to login to the account after the sucessfull registration and the login. Hence it needed to be a non action method otherwise it can user by the url also by ay client.
        public void loginUser(ApplicationManagercs usermanager, ApplicationUser user)
        {
            var authmanager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = usermanager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authmanager.SignIn(new AuthenticationProperties(), userIdentity);
        }




        public ActionResult Myprofile()
        {
            var appdbcontext = new ApplicationDbContext();
            var userstore = new ApplicationUserStore(appdbcontext);
            var usermanager = new ApplicationManagercs(userstore);
            ApplicationUser appuser = usermanager.FindById(User.Identity.GetUserId());
            return View(appuser);
        }

        public ActionResult changepassword(string ids)
        {
            HttpCookie userid = Request.Cookies["UserId"];
            if (userid != null)
            {
                var Userid = userid.Value;
                changepassword data = new changepassword();
                var username = db.Users.Where(m => m.Id == Userid).Select(m=> m.UserName).FirstOrDefault();
                data.Username =username;
                return View(data);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }
        [HttpPost]
        public ActionResult changepassword(changepassword chngpss)
        {
            String UserId = Convert.ToString(Session["UserId"]);
            //string oldpss= Crypto.HashPassword(chngpss.oldpass);
            string newps = Crypto.HashPassword(chngpss.newpass);
            var userstore = new ApplicationUserStore(db);
            var usermanager = new ApplicationManagercs(userstore);
            var user = usermanager.Find(chngpss.Username, chngpss.oldpass);
            var data = db.Users.Where(m => m.Id == UserId).ToList();
            //string DBpassword = base64Decode(data[0].PasswordHash);
            if(data!=null)
            {
                if(user!=null)
                {
                    data[0].PasswordHash = newps;
                    db.SaveChanges();
                    TempData["Message"] = cdc.Success("Password Updated Successfully");
                    return RedirectToAction("Myprofile");
                }
                TempData["Message"] = cdc.Warning("Wrong Password");
                return View();
                
            }
            TempData["Message"] = cdc.Danger("No Data Available");
            return RedirectToAction("Myprofile");
        }
        [ChildActionOnly]
        public ActionResult DisplayUserDetails(ApplicationUser user)
        {
            return PartialView("SingleProfilePartialView", user);
        }

        //[ChildActionOnly]
        //public static string base64Decode(string sData) //Decode    
        //{
        //    try
        //    {
        //        var encoder = new System.Text.UTF8Encoding();
        //        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        //        byte[] todecodeByte = Convert.FromBase64String(sData);
        //        int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
        //        char[] decodedChar = new char[charCount];
        //        utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
        //        string result = new String(decodedChar);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error in base64Decode" + ex.Message);
        //    }
        //}
       
	}
}