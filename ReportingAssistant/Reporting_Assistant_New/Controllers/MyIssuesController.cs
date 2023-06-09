using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reporting_Assistant_New.ViewModel;
using Reporting_Assistant_New.Models;
using Reporting_Assistant_New.Identity;
using System.Collections;
using System.IO;

namespace Reporting_Assistant_New.Controllers
{
    public class MyIssuesController : Controller
    {
        ReportingDbContext db = new ReportingDbContext();
        ApplicationDbContext userDb = new ApplicationDbContext();
        CommonDataController cdc = new CommonDataController();
        // GET: /Admin/MyIssues/
        public ActionResult Issues()
        {
            dropdown();
            String UserId = Convert.ToString(Session["UserId"]);
            if(UserId==null)
            {
                return RedirectToAction("Account", "Logout");
            }
            else
            {
                List<Task> List = new List<Task>();
                List<TaskDone> tdoneList = new List<TaskDone>();
                List<FinalComments> fcmntlist = new List<FinalComments>();
                TaskDone alldata = new TaskDone();
                Task Lstdata = new Task();
                DateTime fromdate = Convert.ToDateTime(System.DateTime.Now.AddDays(-1).ToString("dd/MMM/yyyy"));
                DateTime todate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MMM/yyyy") + " 23:59");
                if (UserId != null)
                {
                    //var data = (from t in db.Task
                    //join user in userDb.Users on t.AdminUserId equals user.Id where(t.UserId==UserId && user.Id==UserId)
                    //select new { t.Description, t.Screen, user.UserName }).ToList();
                    //var data = db.Task.Join(userDb.Users, p => p.AdminUserId == UserId, pc => pc.Id == UserId, (p, pc) => new { p.Screen, p.Description, pc.UserName }).ToList(); ;
                    var data = db.Task.Where(m => m.UserId == UserId && m.DOT >= fromdate && m.DOT <= todate).ToList();
                    foreach (var item in data)
                    {
                        var admin_name = userDb.Users.Where(m => m.Id == item.AdminUserId).Select(m => m.UserName).FirstOrDefault();
                        var obj = new Task();
                        obj.Screen = item.Screen;
                        obj.Description = item.Description;
                        obj.UserId = admin_name;
                        obj.DOT = item.DOT;
                        List.Add(obj);
                    }
                    var tdone = db.TaskDone.Where(m => m.UserId == UserId && m.DOTD >= fromdate && m.DOTD <= todate).ToList();
                    foreach (var item in tdone)
                    {
                        var tddata = new TaskDone();
                        tddata.Screen = item.Screen;
                        tddata.Projects = item.Projects;
                        tddata.ProjectId = item.ProjectId;
                        tddata.ImageAttached = item.ImageAttached;
                        tddata.DOTD = item.DOTD;
                        tddata.Description = item.Description;
                        tdoneList.Add(tddata);

                    }
                    var fcomments = db.FinalComment.Where(m => m.UserId == UserId && m.DOFC >= fromdate && m.DOFC <= todate).ToList();
                    foreach (var item in fcomments)
                    {
                        var admin_name = userDb.Users.Where(m => m.Id == item.AdminUserId).Select(m => m.UserName).FirstOrDefault();
                        var fcdata = new FinalComments();
                        fcdata.Screen = item.Screen;
                        fcdata.Projects = item.Projects;
                        fcdata.ProjectId = item.ProjectId;
                        fcdata.ImageAttached = item.ImageAttached;
                        fcdata.AdminUserId = admin_name;
                        fcdata.DOFC = item.DOFC;
                        fcdata.Description = item.Description;
                        fcmntlist.Add(fcdata);

                    }
                    //string AdminuserId = data[0].AdminUserId.ToString();
                    //foreach (var item in TaskList)
                    //{
                    //    var UserName = userDb.Users.Where(m => m.Id == item.AdminUserId.ToString()).ToList();
                    //    AdminNamelist.Add(UserName);
                    //    ViewBag.AdminNameList = AdminNamelist;
                    //}

                    alldata.tlist = List.ToList();
                    alldata.tdlist = tdoneList.ToList();
                    alldata.fclist = fcmntlist.ToList();
                }
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
        }

        [HttpPost]
        public ActionResult Insertd_data(TaskDone tddata, HttpPostedFileBase BaseImg)
        {
           if(ModelState.IsValid) 
           {
                
              string userid = Convert.ToString(Session["UserId"]);

                try
                {
                    if (tddata.ProjectId.ToString() != "" && userid != "")
                    {
                        string path = uploadImage(BaseImg);
                        var data = db.TaskDone.Create();
                        try
                        {
                            data.ProjectId = tddata.ProjectId;
                            data.Screen = tddata.Screen;
                            data.UserId = userid;
                            data.Description = tddata.Description;
                            data.DOTD = DateTime.Now;
                            data.ImageAttached = path;
                            db.TaskDone.Add(data);
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
        
    }
}