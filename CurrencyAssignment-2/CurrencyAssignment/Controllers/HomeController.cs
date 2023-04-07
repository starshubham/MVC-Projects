using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyAssignment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        //The Index action method receives an optional parameter called 'amount' of 'int' type.
        public ActionResult Index(int? amount)
        {
            //Pass amount to the view, using ViewBag.
            ViewBag.amount = amount;

            //call the view
            return View();
        }
    }
}


