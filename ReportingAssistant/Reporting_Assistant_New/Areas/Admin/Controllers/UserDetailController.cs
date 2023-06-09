﻿using System;
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
    public class UserDetailController : Controller
    {
        //
        // GET: /Administrator/UserDetails/

        CommonDataController cdc = new CommonDataController();
        //[MyActionFilter]
        //[MyResultFilter]
        //[MyExceptionFilter]
        public ActionResult UserDetails()
        {
            List<uduser> listdata = new List<uduser>();
            ApplicationDbContext db = new ApplicationDbContext();
            var data = db.Users.ToList();
            foreach (var item in data)
            {
                var userdata = new uduser();
                userdata.userid = item.Id;
                userdata.Username = item.UserName;
                userdata.Mobile = item.PhoneNumber;
                userdata.DateOfBirth = item.Birthday;
                userdata.Address = item.Address;
                userdata.Email = item.Email;
                userdata.BaseImg = item.UserImg;
                listdata.Add(userdata);
            }
            return View(listdata);
        }



        public ActionResult UserDetailsEdit(string ids)
        {
            var appdbcontext = new ApplicationDbContext();
            var userstore = new ApplicationUserStore(appdbcontext);
            var userManager = new ApplicationManagercs(userstore);

            var data = appdbcontext.Users.Where(m => m.Id == ids).ToList();

            uduser model = new uduser();
            model.userid = data[0].Id;
            model.Username = data[0].UserName;
            model.Address = data[0].Address;
            model.BaseImg = data[0].UserImg;
            model.City = data[0].city;
            model.Email = data[0].Email;
            model.Country = data[0].country;
            model.DateOfBirth = data[0].Birthday;
            model.Mobile = data[0].PhoneNumber;
            return View(model);

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserDetailsEdit(uduser data, HttpPostedFileBase BaseImg)
        {
            if (ModelState.IsValid)
            {

                var db = new ApplicationDbContext();
                var userstore = new ApplicationUserStore(db);
                var userManager = new ApplicationManagercs(userstore);

                string path = uploadImage(BaseImg);
                if(path!=null)
                {
                    ApplicationUser databaseData = db.Users.Where(m => m.Id == data.userid).FirstOrDefault();
                    databaseData.Email = data.Email;
                    databaseData.UserName = data.Username;
                    databaseData.PhoneNumber = data.Mobile;
                    databaseData.Address = data.Address;
                    databaseData.city = data.City;
                    databaseData.country = data.Country;
                    databaseData.Birthday = data.DateOfBirth;
                    databaseData.UserImg = path;
                    db.SaveChanges();
                    TempData["Message"] = cdc.Success("Details Updated Successfully");
                    return RedirectToAction("UserDetails");
                }
                else
                {
                    TempData["Message"] = cdc.Warning("Please Select a Image");
                    return View();
                }
                
            }
            else
            {
                ModelState.AddModelError("My error", "Invalid data");
                return View();
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

        //public string saveImage(string BaseImg)
        //{
        //    string filename = Guid.NewGuid().ToString() + ".jpg";
        //    System.Drawing.Image ImgFile = cdc.Base64ToImage(BaseImg);
        //    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/UploadedFile/"), filename);
        //    ImgFile.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return filename;
        //}
    }
}