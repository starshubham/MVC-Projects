﻿using System;
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
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        [Column("DateOfPurchase", TypeName ="datetime")]
        public Nullable<System.DateTime> DOP { get; set; }
        public string AvailabilityStatus { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public Nullable<long> BrandID { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Photo { get; set; }

        public Nullable<decimal> Quantity { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}