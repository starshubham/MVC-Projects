using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security;
using Reporting_Assistant_New.Identity;
using Reporting_Assistant_New.ViewModel;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;
using Reporting_Assistant_New.Controllers;
using Reporting_Assistant_New.Areas.ViewModels;
using Reporting_Assistant_New.Models;


namespace Reporting_Assistant_New.Areas.Admin.Controllers
{
    public class FinalCommentController : Controller
    {
        //
        // GET: /Admin/FinalComment/
        CommonDataController cdc = new CommonDataController();
        ApplicationDbContext db = new ApplicationDbContext();
        //[MyActionFilter]
        //[MyResultFilter]
        //[MyExceptionFilter]



        public ActionResult FinalCDetailsEdit(long ids)
        {
            var appdbcontext = new ReportingDbContext();

            dropdown();

            var data = appdbcontext.FinalComment.Where(m => m.FinalCommentId == ids).ToList();

            FinalComments Data = new FinalComments();
            var userid = data[0].UserId;
            var adminid = data[0].AdminUserId;
            //var usename = db.Users.Where(m => m.Id == userid).Select(m => m.UserName).FirstOrDefault();
            //var Adminname = db.Users.Where(m => m.Id == adminid).Select(m => m.UserName).FirstOrDefault();
            Data.FinalCommentId = data[0].FinalCommentId;
            Data.Screen = data[0].Screen;
            Data.Description = data[0].Description;
            Data.ProjectId = data[0].ProjectId;
            Data.UserId = data[0].UserId;
            Data.AdminUserId = data[0].AdminUserId;
            Data.DOFC = Convert.ToDateTime(data[0].DOFC);
            Data.ImageAttached = data[0].ImageAttached;
            return View(Data);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalCDetailsEdit(FinalComments data, HttpPostedFileBase ImageAttached)
        {
            if (ModelState.IsValid)
            {

                var db = new ReportingDbContext();

                dropdown();
                string path = uploadImage(ImageAttached);
                if (path.Equals("-1"))
                {
                    Response.Write("<script>alert('Some error occured')</script>");
                }
                else
                {
                    FinalComments databaseData = db.FinalComment.Where(m => m.FinalCommentId == data.FinalCommentId).FirstOrDefault();
                    databaseData.AdminUserId = data.AdminUserId;
                    databaseData.Description = data.Description;
                    databaseData.ProjectId = data.ProjectId;
                    databaseData.Screen = data.Screen;
                    databaseData.FinalCommentId = data.FinalCommentId;
                    databaseData.UserId = data.UserId;
                    databaseData.DOFC = Convert.ToDateTime(data.DOFC);
                    databaseData.ImageAttached = path;
                    db.SaveChanges();
                    TempData["Message"] = cdc.Success("Details Updated Successfully");
                }

                return RedirectToAction("Index", "Home", new { Areas = "Administrator" });
            }
            else
            {
                ModelState.AddModelError("My error", "Invalid data");
                return View();
            }
        }
        [NonAction]
        public string uploadImage(HttpPostedFileBase ImageAttached)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (ImageAttached != null && ImageAttached.ContentLength > 0)
            {
                string extension = Path.GetExtension(ImageAttached.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".gif"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/UploadImage"), random + Path.GetFileName(ImageAttached.FileName));
                        ImageAttached.SaveAs(path);
                        path = "~/Content/UploadImage/" + random + Path.GetFileName(ImageAttached.FileName);
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg, jpeg, pg and jif files are allowed')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please Select a file')</script>");
            }

            return path;
        }
        public void dropdown()
        {
            List<SelectListItem> UserList = new List<SelectListItem>();
            List<SelectListItem> AdminList = new List<SelectListItem>();
            var userstore = new ApplicationUserStore(db);
            ApplicationManagercs appman= new ApplicationManagercs(userstore);
            var user_data = db.Users.OrderBy(a => a.UserName).Select(m => new { m.Id, m.UserName }).ToList();
            
            if (user_data.Count > 0)
            {

                foreach (var Value in user_data)
                {
                    if(appman.IsInRole(Value.Id,"Admin"))
                    {
                    UserList.Add(new SelectListItem { Text = Value.UserName, Value = Value.Id.ToString() });
                    }
                    else
                    {
                        UserList.Add(new SelectListItem { Text = Value.UserName, Value = Value.Id.ToString() });
                    }

                }
                ViewBag.user_data = UserList;
                ViewBag.Admin_data = UserList;
            }
        }

	}
}