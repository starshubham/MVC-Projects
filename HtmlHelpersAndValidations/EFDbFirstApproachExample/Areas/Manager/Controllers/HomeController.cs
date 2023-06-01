﻿using EFDbFirstApproachExample.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFDbFirstApproachExample.Areas.Manager.Controllers
{
    [ManagerAuthorization]
    public class HomeController : Controller
    {
        // GET: Manager/Home/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}