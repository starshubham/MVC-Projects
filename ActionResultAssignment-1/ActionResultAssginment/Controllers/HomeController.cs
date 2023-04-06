using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ActionResultAssginment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index(string arg)
        {
            //In case of 'sample', return pdf file
            if (arg == "sample")
            {
                return File("~/Sample.pdf", "application/pdf");
            }

            //In case of 'gotoabout', return redirect to About action method
            else if (arg == "gotoabout")
            {
                return RedirectToAction("About");
            }

            //In case of 'login', return 'Login' view
            else if (arg == "login")
            {
                return View("Login");
            }

            //Otherwise, default message
            else
            {
                return Content("You entered:  " + arg);
            }
        }

        // GET: Home/About
        public ActionResult About()
        {
            return Content("About content here");
        }
    }
}

