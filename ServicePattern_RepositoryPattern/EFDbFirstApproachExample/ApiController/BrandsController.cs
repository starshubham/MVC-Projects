﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Company.DomainModels;
using Company.DataLayer;

namespace EFDbFirstApproachExample
{
    public class BrandsController : ApiController
    {
        public List<Brand> Get()
        {
            CompanyDbContext db = new CompanyDbContext();
            List<Brand> brands = db.Brands.ToList();
            return brands;
        }
    }
}
