using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.Models
{
    [Table("Products",Schema = "dbo")]
    public class Product
    {
        [Key]
        [Display(Name ="Product ID")]
        public long ProductID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        public Nullable<decimal> Price { get; set; }

        [Column("DateOfPurchase", TypeName ="datetime")]
        [Display(Name = "Date of Purchase")]
        public Nullable<System.DateTime> DOP { get; set; }

        [Display(Name = "Availability Status")]
        public string AvailabilityStatus { get; set; }

        [Display(Name = "Category ID")]
        public Nullable<long> CategoryID { get; set; }

        [Display(Name = "Brand ID")]
        public Nullable<long> BrandID { get; set; }

        [Display(Name = "Active")]
        public Nullable<bool> Active { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        public Nullable<decimal> Quantity { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}