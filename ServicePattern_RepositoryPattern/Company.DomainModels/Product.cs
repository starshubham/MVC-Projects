using Company.DomainModels.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DomainModels
{
    [Table("Products", Schema = "dbo")]
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        public long ProductID { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name can't be blank")]
        public string ProductName { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Price can't be blank")]
        [Range(0, 1000000, ErrorMessage = "Price should be in between 0 and 1000000")]
        [DivisibleBy10(ErrorMessage = "Price should be in multiples of 10")]
        public Nullable<decimal> Price { get; set; }

        [Column("DateOfPurchase", TypeName = "datetime")]
        [Display(Name = "Date of Purchase")]
        [DisplayFormat(DataFormatString = "M/dd/yyyy", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOP { get; set; }

        [Display(Name = "Availability Status")]
        [Required(ErrorMessage = "Please select availability status")]
        public string AvailabilityStatus { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category can't be blank")]
        public long CategoryID { get; set; }

        [Display(Name = "Brand")]
        [Required(ErrorMessage = "Brand can't be blank")]
        public long BrandID { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}
