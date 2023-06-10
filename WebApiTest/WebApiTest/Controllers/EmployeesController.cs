using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [EnableCors(origins: "https://localhost:44397",headers:"*",methods:"*")]
    public class EmployeesController : ApiController
    {
        public List<Employee> Get()
        {
            CompanyDbContext db = new CompanyDbContext();
            List<Employee> emps = db.Employees.ToList();
            return emps;
        }
    }
}
