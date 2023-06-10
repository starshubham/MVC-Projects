using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Filters;
using Company.DomainModels;
using Company.DataLayer;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    //[AdminAuthorizaton]
    public class BrandsController : Controller
    {
        // GET: Admin/Brands/Index
        //public ActionResult Index()
        //{
        //    CompanyDbContext db = new CompanyDbContext();
        //    List<Brand> brands = db.Brands.ToList();
        //    return View(brands);
        //}

        // To send request to WebApi controller through AJAX
        public ActionResult Index()
        {
            return View();
        }
    }
}