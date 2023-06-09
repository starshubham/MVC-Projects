using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reporting_Assistant_New.Models;
using Reporting_Assistant_New.ViewModel;
using Reporting_Assistant_New.Controllers;
using Reporting_Assistant_New.Identity;
using System.IO;

namespace Reporting_Assistant_New.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        ReportingDbContext db = new ReportingDbContext();
        ApplicationDbContext userDb = new ApplicationDbContext();
        CommonDataController cdc = new CommonDataController();
        // GET: /Home/
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Issues()
        {
            //String UserId = Convert.ToString(Session["UserId"]);

            
            List<Task> List = new List<Task>();
            List<TaskDone> tdoneList = new List<TaskDone>();
            List<FinalComments> fcmntlist = new List<FinalComments>();
            TaskDone alldata = new TaskDone();
            Task Lstdata = new Task();
            DateTime fromdate = Convert.ToDateTime(System.DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy"));
            DateTime todate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MMM/yyyy") + " 23:59");
            HttpCookie AdminID= Request.Cookies["UserId"];
            dropdown();
            if(AdminID==null)
            {
                return RedirectToAction("Logout", "AdminHome");
            }
            else
            {
                var Adminid = AdminID.Value;

                var User_Details = userDb.Users.ToList();
                foreach (var i in User_Details)
                {

                    var TAdata = db.Task.Where(m => m.DOT >= fromdate && m.DOT <= todate && m.UserId == i.Id).ToList();
                    if (TAdata != null)
                    {
                        foreach (var item in TAdata)
                        {


                            var obj = new Task();
                            obj.TaskId = item.TaskId;
                            obj.Screen = item.Screen;
                            obj.Description = item.Description;
                            obj.UserId = i.UserName;
                            obj.DOT = item.DOT;
                            obj.AdminUserId = item.AdminUserId;
                            List.Add(obj);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("My error", "No Records Found");
                        return View();
                    }

                    var tdone = db.TaskDone.Where(m => m.UserId == i.Id && m.DOTD >= fromdate && m.DOTD <= todate).ToList();
                    if (tdone != null)
                    {
                        foreach (var item2 in tdone)
                        {

                            var tddata = new TaskDone();
                            tddata.TaskDoneId = item2.TaskDoneId;
                            tddata.Screen = item2.Screen;
                            tddata.Projects = item2.Projects;
                            tddata.ProjectId = item2.ProjectId;
                            tddata.ImageAttached = item2.ImageAttached;
                            tddata.DOTD = item2.DOTD;
                            tddata.UserId = i.UserName;
                            tddata.Description = item2.Description;
                            tddata.adminid = item2.adminid;
                            tdoneList.Add(tddata);

                        }
                    }
                    else
                    {

                        ModelState.AddModelError("My error", "No Records Found");
                        return View();
                    }

                    var fcomments = db.FinalComment.Where(m => m.UserId == i.Id && m.DOFC >= fromdate && m.DOFC <= todate).ToList();
                    if (fcomments != null)
                    {
                        foreach (var item3 in fcomments)
                        {

                            var fcdata = new FinalComments();
                            fcdata.FinalCommentId = item3.FinalCommentId;
                            fcdata.Screen = item3.Screen;
                            fcdata.Projects = item3.Projects;
                            fcdata.ProjectId = item3.ProjectId;
                            fcdata.ImageAttached = item3.ImageAttached;
                            fcdata.UserId = i.UserName;
                            fcdata.AdminUserId = item3.AdminUserId;
                            fcdata.DOFC = item3.DOFC;
                            fcdata.Description = item3.Description;
                            fcmntlist.Add(fcdata);

                        }

                    }
                    else
                    {

                        ModelState.AddModelError("My error", "No Records Found");
                        return View();
                    }

                    //string AdminuserId = data[0].AdminUserId.ToString();
                    //foreach (var item in TaskList)
                    //{
                    //    var UserName = userDb.Users.Where(m => m.Id == item.AdminUserId.ToString()).ToList();
                    //    AdminNamelist.Add(UserName);
                    //    ViewBag.AdminNameList = AdminNamelist;
                    //}


                }
                alldata.adminid = Adminid.ToString();
                alldata.tlist = List.ToList();
                alldata.tdlist = tdoneList.ToList();
                alldata.fclist = fcmntlist.ToList();


                return View(alldata);
            }
            
	}
        public void dropdown()
        {
            List<SelectListItem> ProjectList = new List<SelectListItem>();
            var pr_data = db.Project.OrderBy(a => a.ProjectName).Select(m => new { m.ProjectId, m.ProjectName }).ToList();
            if (pr_data.Count > 0)
            {

                foreach (var Value in pr_data)
                {
                    ProjectList.Add(new SelectListItem { Text = Value.ProjectName, Value = Value.ProjectId.ToString() });


                }
                ViewBag.pr_data = ProjectList;
            }
            List<SelectListItem> UserList = new List<SelectListItem>();
            var user_data = userDb.Users.OrderBy(a => a.UserName).Select(m => new { m.Id, m.UserName }).ToList();
            if (user_data.Count > 0)
            {

                foreach (var Value in user_data)
                {
                    UserList.Add(new SelectListItem { Text = Value.UserName, Value = Value.Id.ToString() });


                }
                ViewBag.user_data = UserList;
            }
        }
        [HttpPost]
        public ActionResult Insertd_data(FinalComments fcdata, HttpPostedFileBase BaseImg)
        {
            if (ModelState.IsValid)
            {
                string Adminid = Convert.ToString(Session["UserId"]);
                try
                {
                    if (fcdata.ProjectId.ToString() != "" && fcdata.UserId != "")
                    {
                        string path = uploadImage(BaseImg);
                        var data = db.FinalComment.Create();
                        try
                        {
                            data.ProjectId = fcdata.ProjectId;
                            data.Screen = fcdata.Screen;
                            data.UserId = fcdata.UserId;
                            data.AdminUserId = Adminid;
                            data.Description = fcdata.Description;
                            data.DOFC = DateTime.Now;
                            data.ImageAttached = path;
                            db.FinalComment.Add(data);
                            db.SaveChanges();
                            TempData["Message"] = cdc.Success("The data saved successfully!");
                            return RedirectToAction("Issues");
                            //var data2 = new { status = "Success", message = "Data Added Successfully!!" };
                            //return Json(data2, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            //var data2 = new { status = "fail", message = "Please try after some time!!" };
                            //return Json(data2, JsonRequestBehavior.AllowGet);
                            TempData["Message"] = cdc.Success("Some Issue Appeared!");
                            return RedirectToAction("Issues");
                        }
                    }
                    else
                    {
                        TempData["Message"] = cdc.Success("Some Issue Appeared!");
                        return RedirectToAction("Issues");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = cdc.Success("Some Issue Appeared!");
                    return RedirectToAction("Issues");
                }
            }
            else
            {
                TempData["Message"] = cdc.Success("Some Issue Appeared!");
                return RedirectToAction("Issues");
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
        public ActionResult Logout()
        {
            var authmanager = HttpContext.GetOwinContext().Authentication;
            authmanager.SignOut();
            return RedirectToAction("Index", "Home");
        }
  }
}