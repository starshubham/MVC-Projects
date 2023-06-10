using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public int Salary { get; set; }
    }

    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(): base("data source=DESKTOP-01MAY05\\SQLEXPRESS; user id=ssa;password=Provana@123; " +
            "initial catalog=CompanyCors")
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}