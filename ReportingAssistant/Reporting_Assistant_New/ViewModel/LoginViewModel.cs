using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reporting_Assistant_New.ViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Username Is Required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string password { get; set; }
    }
}