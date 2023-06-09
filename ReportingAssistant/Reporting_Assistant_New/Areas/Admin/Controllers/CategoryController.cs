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
    public class CategoryController : Controller
    {
        CommonDataController cdc = new CommonDataController();
        ReportingDbContext db = new ReportingDbContext();
        ApplicationDbContext userDb = new ApplicationDbContext();
        // GET: /Admin/Category/
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category data)
        {
            if(ModelState.IsValid)
            {
                var savedata = db.Category.Create();
                savedata.CategoryName = data.CategoryName;
                db.Category.Add(savedata);
                db.SaveChanges();
                TempData["Message"] = cdc.Success("Details Created Successfully");
                return RedirectToAction("ViewAll");
            }
            else
            {
                TempData["Message"] = cdc.Success("Invalid Data!");
                return RedirectToAction("ViewAll");
            }

           
        }
        public ActionResult Edit(long ids)
        {
            var data = db.Category.Where(m => m.CategoryId == ids).FirstOrDefault();
            if(data!=null)
            {
                Category obj = new Category();
                obj.CategoryId = data.CategoryId;
                obj.CategoryName = data.CategoryName;
                return View(obj);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Category data)
        {
            if(ModelState.IsValid)
            {
                var details = db.Category.Where(m => m.CategoryId == data.CategoryId).FirstOrDefault();
                details.CategoryName = data.CategoryName;
                db.SaveChanges();
                TempData["Message"] = cdc.Success("Details Updated Successfully");
                return RedirectToAction("ViewAll");
            }
            else
            {
                TempData["Message"] = cdc.Success("Invalid Data");
                return RedirectToAction("ViewAll");
            }
            
        }

        public ActionResult ViewAll()
        {
            List<Category> list = new List<Category>();
            var data = db.Category.ToList();
            if(data!=null)
            {
                foreach(var item in data)
                {
                    Category obj = new Category();
                    obj.CategoryId = item.CategoryId;
                    obj.CategoryName = item.CategoryName;
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
       
        [ChildActionOnly]
        public string Msgbar(string errMsg, int correct)
        {
            if (correct > 0)
            {
                errMsg = correct.ToString() + "  record(s) Deleted Successfully!";
                TempData["Message"] = cdc.Success(errMsg);
            }
            //else if (correct == 0 && incorret > 0)
            //{
            //    errMsg = incorret.ToString() + "  record(s) failed to Delete due to existence of records!";
            //    TempData["Message"] = cdc.Danger(errMsg);
            //}
            //else if (correct > 0 && incorret > 0)
            //{
            //    errMsg = correct.ToString() + "record(s) Deleted Successfully and  " + incorret.ToString() + "  record(s) failed to Delete due to existence of records!";
            //    TempData["Message"] = cdc.Warning(errMsg);
            //}
            return errMsg;
        }
	}
}