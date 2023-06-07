using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DomainModels
{
    public class Category
    {
        [Key]
        [Display(Name = "Category")]
        public long CategoryID { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
