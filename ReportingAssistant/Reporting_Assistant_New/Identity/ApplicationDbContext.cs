using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Reporting_Assistant_New.Identity;

namespace Reporting_Assistant_New.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
    }
}