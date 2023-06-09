using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reporting_Assistant_New.Areas.ViewModels;
using System.ComponentModel.DataAnnotations;
using Reporting_Assistant_New.Identity;
using System.Web.Mvc;

namespace Reporting_Assistant_New.ViewModel
{
    public class TaskDone
    {
        [Key]
        public long TaskDoneId { get; set; }

        //[Range(2, 50, ErrorMessage = "Please Enter Screen Between 2 to 50 only")]
        public string Screen { get; set; }

       // [Range(2, 10000, ErrorMessage = "Please Enter Description Between 2 to 10000 only")]
        public string Description { get; set; }

        public string adminid { get; set; }
        public string UserId { get; set; }

        [Column("DateOfTaskDone")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DOTD { get; set; }
        public string ImageAttached { get; set; }

        public virtual long ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        public List<Task> tlist { get; set; }
        public List<TaskDone> tdlist { get; set; }
        public List<FinalComments> fclist { get; set; }
        
    }
}