using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Reporting_Assistant_New.Areas.ViewModels;
namespace Reporting_Assistant_New.ViewModel
{
    public class FinalComments
    {
        [Key]
        public long FinalCommentId { get; set; }

        [Range(2, 50, ErrorMessage = "Please Enter Screen Between 2 to 50 only")]
        public string Screen { get; set; }

        [Range(2, 10000, ErrorMessage = "Please Enter Description Between 2 to 10000 only")]
        public string Description { get; set; }

        public string AdminUserId { get; set; }
        public string UserId { get; set; }

        [Column("DateOfFinalComment")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DOFC { get; set; }
        public string ImageAttached { get; set; }

        public virtual long ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
    }
}