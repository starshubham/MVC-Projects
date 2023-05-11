using CustomValidationAssignmentSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomValidationAssignmentSolution.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Registration
        public ActionResult Registration()
        {
            return View();
        }

        // POST: Home/Registration
        [HttpPost]
        public ActionResult Registration(User u)
        {
            if (ModelState.IsValid)
            {
                return Content("Valid");
            }
            else
            {
                IEnumerable<string> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList().Select(error => error.ErrorMessage);  //convert list of error objects into a List<string>
                return Content(string.Join(". ", allErrors));
            }
        }
    }
}
