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
    public class ProjectController : Controller
    {
        CommonDataController cdc = new CommonDataController();
        ReportingDbContext db = new ReportingDbContext();
        ApplicationDbContext userDb = new ApplicationDbContext();
        // GET: /Admin/Category/
        public ActionResult Create()
        {
            dropdown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Project data, HttpPostedFileBase BaseImg)
        {
            dropdown();
            if (ModelState.IsValid)
            {
                string path = cdc.uploadImage(BaseImg);
                if (path.Equals("-1"))
                {
                    Response.Write("<script>alert('Some error occured')</script>");
                    return View();
                }
                else
                {
                    var prdata = db.Project.Create();
                    prdata.ProjectName = data.ProjectName;
                    prdata.DOS = data.DOS.Value.Date;
                    prdata.Availability = data.Availability;
                    prdata.CategoryId = data.CategoryId;
                    prdata.image = path;
                    db.Project.Add(data);
                    db.SaveChanges();
                    TempData["Message"] = cdc.Success("Details Created Successfully");
                    return RedirectToAction("ViewAll");
                }
            }
            else
            {
                TempData["Message"] = cdc.Success("Invalid Data!");
                return View();
            }


        }
        public ActionResult Edit(long ids)
        {
            dropdown();
            var data = db.Project.Where(m => m.ProjectId == ids).FirstOrDefault();
            if (data != null)
            {
                Project obj = new Project();
                obj.ProjectId = data.ProjectId;
                obj.ProjectName = data.ProjectName;
                obj.DOS = data.DOS;
                ViewBag.date = data.DOS.Value.Date;
                obj.Availability = data.Availability;
                obj.CategoryId = data.CategoryId;
                obj.image = data.image;
                return View(obj);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Project data, HttpPostedFileBase BaseImg)
        {
            dropdown();
            if (ModelState.IsValid)
            {
                string path = uploadImage(BaseImg);
                if(path!=null)
                {
                    var details = db.Project.Where(m => m.ProjectId == data.ProjectId).FirstOrDefault();
                    details.ProjectName = data.ProjectName;
                    details.CategoryId = data.CategoryId;
                    details.DOS = data.DOS.Value.Date;
                    
                    details.Availability = data.Availability;
                    details.image = path;
                    db.SaveChanges();
                    TempData["Message"] = cdc.Success("Details Updated Successfully");
                    return RedirectToAction("ViewAll");
                }
                else
                {
                    TempData["Message"] = cdc.Warning("Please Select a Image");
                    return View();
                }
            }
            else
            {
                TempData["Message"] = cdc.Success("Invalid Data");
                return View();
            }

        }

        public ActionResult ViewAll(string search)
        {
            if (search == null)
            {
                List<Project> list = new List<Project>();
                var prodata = db.Project.ToList();
                if (prodata != null)
                {
                    foreach (var item in prodata)
                    {
                        Project obj = new Project();
                        obj.ProjectId = item.ProjectId;
                        obj.ProjectName = item.ProjectName;
                        obj.DOS = item.DOS;
                        obj.image = item.image;
                        obj.CategoryId = item.CategoryId;
                        obj.Availability = item.Availability;
                        list.Add(obj);
                    }
                    return View(list);
                }
                else
                {
                    ModelState.AddModelError("My error", "No Records Found");
                    return View();

                }

            }
            else if (search != null)
            {
               
                    var List = db.Project.Where(e => e.ProjectName.Contains(search)).ToList();
                    try
                    {
                        if (List.Count > 0)
                        {
                            //var data = new { List, status = "success", message = "" };
                            //return Json(data, JsonRequestBehavior.AllowGet);
                            return View(List);
                        }
                        else
                        {

                            ModelState.AddModelError("My error", "No Records Found");
                            return View();
                        }

                    }
                    catch (Exception ex)
                    {
                        
                            ModelState.AddModelError("My error", "Due to some problem unable to fetch data");
                            return View();
                    }
                    
            }
            else
            {
                ModelState.AddModelError("My error", "Kindly refresh the page to date data");
                return View();
            }

        }
       

        [ChildActionOnly]
        public void dropdown()
        {
            List<SelectListItem> CategoryList = new List<SelectListItem>();
            var ct_data = db.Category.OrderBy(a => a.CategoryName).Select(m => new { m.CategoryId, m.CategoryName }).ToList();
            if (ct_data.Count > 0)
            {

                foreach (var Value in ct_data)
                {
                    CategoryList.Add(new SelectListItem { Text = Value.CategoryName.ToString(), Value = Value.CategoryId.ToString() });


                }
                ViewBag.list = CategoryList;
            }
        }
        public string uploadImage(HttpPostedFileBase BaseImg)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (BaseImg != null && BaseImg.ContentLength > 0)
            {
                string extension = Path.GetExtension(BaseImg.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".gif"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/UploadImage"), random + Path.GetFileName(BaseImg.FileName));
                        BaseImg.SaveAs(path);
                        path = "~/Content/UploadImage/" + random + Path.GetFileName(BaseImg.FileName);
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
                path = "-1";
                TempData["Message"] = cdc.Success("Please Select a Image");
            }

            return path;
        }

        [HttpPost]
        public ActionResult searchdata(string search)
        {
            {

                try
                {
                    if (search != "")
                    {
                        var List = db.Project.Where(e => e.ProjectName.Contains(search)).ToList();
                        try
                        {
                            if (List.Count > 0)
                            {
                                //var data = new { List, status = "success", message = "" };
                                //return Json(data, JsonRequestBehavior.AllowGet);
                                return RedirectToAction("ViewAll",List); 
                            }
                            else
                            {

                                var data = new { search, status = "fail", message = "No Record Found" };
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (Exception ex)
                        {
                            var data = new { status = "exception", message = "Due to some problem unable to fetch data" };
                            return RedirectToAction("ViewAll", List);
                        }
                    }
                    else
                    {
                        var status = "fail";
                        var message = "All fields are required !";
                        var data = new { status = status, message = message };
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    var data = new { status = "Exception", message = ex.Message };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }


            }
        }

	}
}