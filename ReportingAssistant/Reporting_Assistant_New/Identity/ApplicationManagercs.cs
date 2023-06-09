using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Reporting_Assistant_New.Identity
{
    public class ApplicationManagercs : UserManager<ApplicationUser>
    {
          public ApplicationManagercs(IUserStore<ApplicationUser> store): base(store)
            {

            }
    }
}