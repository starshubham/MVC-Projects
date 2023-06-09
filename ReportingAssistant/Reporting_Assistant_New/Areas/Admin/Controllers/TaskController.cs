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
    public class TaskController : Controller
    {
        //
        // GET: /Administrator/UserDetails/

        CommonDataController cdc = new CommonDataController();
        ApplicationDbContext db = new ApplicationDbContext();
        ReportingDbContext rdb = new ReportingDbContext();
        //[MyActionFilter]
        //[MyResultFilter]
        //[MyExceptionFilter]

        public ActionResult CreateTaskDetails()
        {
            dropdown();
            return View();
        }
        [HttpPost]
        public ActionResult CreateTaskDetails(Task data, HttpPostedFileBase ImageAttached)
        {
            if (ModelState.IsValid)
            {

                var db = new ReportingDbContext();
                dropdown();
                HttpCookie AdminID = Request.Cookies["UserId"];

                if (AdminID == null)
                {
                    return RedirectToAction("Logout", "AdminHome");
                }
                else
                {


                    var Admin = AdminID.Value;
                    string path = uploadImage(ImageAttached);
                    if (path.Equals("-1"))
                    {
                        Response.Write("<script>alert('Some error occured')</script>");
                    }
                    else
                    {
                        Task databaseData = db.Task.Create();
                        databaseData.AdminUserId = Admin;
                        databaseData.Description = data.Description;
                        databaseData.ProjectId = data.ProjectId;
                        databaseData.Screen = data.Screen;
                        databaseData.TaskId = data.TaskId;
                        databaseData.UserId = data.UserId;
                        databaseData.DOT = Convert.ToDateTime(data.DOT);
                        databaseData.ImageAttached = path;
                        db.Task.Add(databaseData);
                        db.SaveChanges();
                        TempData["Message"] = cdc.Success("Details Created Successfully");
                    }
                }
                return RedirectToAction("ViewTaskDetails", "Task", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("My error", "Invalid data");
                return View();
            }
        }

       
      


        public ActionResult TaskDetailsEdit(long ids)
        {
            var appdbcontext = new ReportingDbContext();

            dropdown();
            var data = appdbcontext.Task.Where(m => m.TaskId == ids).ToList();
            var userid=data[0].UserId;
            var adminid=data[0].AdminUserId;
            var usename = db.Users.Where(m => m.Id == userid).Select(m => m.UserName).FirstOrDefault();
            var Adminname = db.Users.Where(m => m.Id == adminid ).Select(m => m.UserName).FirstOrDefault();
            Task Data = new Task();
            Data.TaskId = data[0].TaskId;
            Data.Screen = data[0].Screen;
            Data.Description = data[0].Description;
            Data.ProjectId = data[0].ProjectId;
            Data.UserId = usename;
            Data.AdminUserId = Adminname;
            Data.DOT = Convert.ToDateTime(data[0].DOT);
            Data.ImageAttached = data[0].ImageAttached;
            return View(Data);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaskDetailsEdit(Task data, HttpPostedFileBase ImageAttached)
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
                    Task databaseData = db.Task.Where(m => m.TaskId == data.TaskId).FirstOrDefault();
                    databaseData.AdminUserId = data.AdminUserId;
                    databaseData.Description = data.Description;
                    databaseData.ProjectId = data.ProjectId;
                    databaseData.Screen = data.Screen;
                    databaseData.TaskId = data.TaskId;
                    databaseData.UserId = data.UserId;
                    databaseData.DOT = Convert.ToDateTime(data.DOT);
                    databaseData.ImageAttached = path;
                    db.SaveChanges();
                    TempData["Message"] = cdc.Success("Details Updated Successfully");
                }

                return RedirectToAction("ViewTaskDetails", "Task", new { area = "Admin" });
            }
            else
            {
                ModelState.AddModelError("My error", "Invalid data");
                return View();
            }
        }

        public ActionResult ViewTaskDetails()
        {

            List<Task> listdata = new List<Task>();
            ReportingDbContext db = new ReportingDbContext();
            ApplicationDbContext userdb= new ApplicationDbContext();
            var data = db.Task.ToList();
            foreach (var item in data)
            {
                var AdminName = userdb.Users.Where(m => m.Id == item.AdminUserId).ToList();
                var UserName = userdb.Users.Where(m => m.Id == item.UserId).ToList();
                var Taskdata = new Task();
                Taskdata.TaskId = item.TaskId;
                Taskdata.Screen = item.Screen;
                Taskdata.Description = item.Description;
                Taskdata.ProjectId = item.ProjectId;
                Taskdata.username = UserName[0].UserName;
                Taskdata.DOT = Convert.ToDateTime(item.DOT);
                Taskdata.ImageAttached = item.ImageAttached;
                Taskdata.AdminName = AdminName[0].UserName;

                listdata.Add(Taskdata);
            }
            return View(listdata);
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

        //public string saveImage(string BaseImg)
        //{
        //    string filename = Guid.NewGuid().ToString() + ".jpg";
        //    System.Drawing.Image ImgFile = cdc.Base64ToImage(BaseImg);
        //    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/UploadedFile/"), filename);
        //    ImgFile.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return filename;
        //}
        public void dropdown()
        {
            List<SelectListItem> ProjectList = new List<SelectListItem>();
            var pr_data = rdb.Project.OrderBy(a => a.ProjectName).Select(m => new { m.ProjectId, m.ProjectName }).ToList();
            if (pr_data.Count > 0)
            {

                foreach (var Value in pr_data)
                {
                    ProjectList.Add(new SelectListItem { Text = Value.ProjectName, Value = Value.ProjectId.ToString() });


                }
                ViewBag.pr_data = ProjectList;
            }
            List<SelectListItem> UserList = new List<SelectListItem>();
            var user_data = db.Users.OrderBy(a => a.UserName).Select(m => new { m.Id, m.UserName }).ToList();
            if (user_data.Count > 0)
            {

                foreach (var Value in user_data)
                {
                    UserList.Add(new SelectListItem { Text = Value.UserName, Value = Value.Id.ToString() });


                }
                ViewBag.user_data = UserList;
            }
        }
    }
}