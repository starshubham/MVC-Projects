using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reporting_Assistant_New.Areas.ViewModels
{
    public class Project
    {
        [Key]
        public long ProjectId { get; set; }

        //[RegularExpression(@"^[a-zA-Z]^[0-9][ ]+$", ErrorMessage = "Use letters only please")]
        public string ProjectName { get; set; }

        [Column("DateOfStart")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public Nullable<DateTime> DOS { get; set; }

        public bool Availability { get; set; }

        public virtual long CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Categorys { get; set; }

        //[Required(ErrorMessage="Image can not be empty")]
        public string image { get; set; } 

    }
}