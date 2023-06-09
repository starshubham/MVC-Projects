using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Reporting_Assistant_New.Areas.ViewModels
{
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }

       // [RegularExpression(@"^[a-zA-Z""0-9]*$", ErrorMessage = "Use letters only please")]
        public string CategoryName { get; set; }


    }
}