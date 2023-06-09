using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reporting_Assistant_New.Areas.ViewModels;

namespace Reporting_Assistant_New.ViewModel
{
    public class Task
    {
        [Key]
        public long TaskId { get; set; }

        
        public string Screen { get; set; }

        
        public string Description { get; set; }

        public string AdminUserId { get; set; }

        public string UserId { get; set; }

        [Column("DateOfTask")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DOT { get; set; }
        public string ImageAttached { get; set; }

        public virtual long ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project Projects { get; set; }
        public List<Task> TaskList { get; set; }
        public string username { get; set; }
        public string AdminName { get; set; }
            
    }
}