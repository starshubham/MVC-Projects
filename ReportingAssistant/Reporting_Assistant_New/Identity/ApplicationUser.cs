using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Reporting_Assistant_New.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string UserImg { get; set; }
        public List<ApplicationUser> userList { get; set; }


        internal bool IsInRole(string p)
        {
            throw new NotImplementedException();
        }
    }
}