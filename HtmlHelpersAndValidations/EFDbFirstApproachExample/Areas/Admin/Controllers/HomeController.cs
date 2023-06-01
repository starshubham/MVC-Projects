﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFDbFirstApproachExample.Filters;

namespace EFDbFirstApproachExample.Areas.Admin.Controllers
{
    [AdminAuthorizaton]
    public class HomeController : Controller
    {
        // GET: Admin/Home/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}