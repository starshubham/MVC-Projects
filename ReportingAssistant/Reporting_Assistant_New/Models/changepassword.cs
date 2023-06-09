using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reporting_Assistant_New.Models
{
    public class changepassword
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Old Password is required")]
        public string oldpass { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        public string newpass { get; set; }
        [Compare("newpass", ErrorMessage = "Password and confirm password should be same")]
        public string confirmpass { get; set; }
    }
}