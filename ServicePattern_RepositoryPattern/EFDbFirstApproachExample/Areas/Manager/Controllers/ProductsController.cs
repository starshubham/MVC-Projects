﻿using Company.DataLayer;
using Company.DomainModels;
using EFDbFirstApproachExample.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFDbFirstApproachExample.Areas.Manager.Controllers
{
    [ManagerAuthorization]
    public class ProductsController : Controller
    {
        CompanyDbContext db = new CompanyDbContext();
        // GET: Manager/Products
        public ActionResult Index(string search = "", string SortColumn = "ProductName", string IconClass = "fa-sort-asc", int PageNo = 1)
        {
            //List<Product> products = db.Products.Where(temp => temp.CategoryID == 1 && temp.Price >= 50000).ToList();
            ViewBag.search = search;
            List<Product> products = db.Products.Where(temp => temp.ProductName.Contains(search)).ToList();

            //Sorting
            ViewBag.SortColumn = SortColumn;
            ViewBag.IconClass = IconClass;
            if (ViewBag.SortColumn == "ProductID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductID).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductID).ToList();
            }
            else if (ViewBag.SortColumn == "ProductName")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.ProductName).ToList();
                else
                    products = products.OrderByDescending(temp => temp.ProductName).ToList();
            }
            else if (ViewBag.SortColumn == "Price")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Price).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Price).ToList();
            }
            else if (ViewBag.SortColumn == "DateOfPurchase")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.DOP).ToList();
                else
                    products = products.OrderByDescending(temp => temp.DOP).ToList();
            }
            else if (ViewBag.SortColumn == "AvailabilityStatus")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.AvailabilityStatus).ToList();
                else
                    products = products.OrderByDescending(temp => temp.AvailabilityStatus).ToList();
            }
            else if (ViewBag.SortColumn == "CategoryID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.CategoryID).ToList();
                else
                    products = products.OrderByDescending(temp => temp.CategoryID).ToList();
            }
            else if (ViewBag.SortColumn == "BrandID")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.BrandID).ToList();
                else
                    products = products.OrderByDescending(temp => temp.BrandID).ToList();
            }
            else if (ViewBag.SortColumn == "Active")
            {
                if (ViewBag.IconClass == "fa-sort-asc")
                    products = products.OrderBy(temp => temp.Active).ToList();
                else
                    products = products.OrderByDescending(temp => temp.Active).ToList();
            }

            /* Paging */
            int NoOfRecordsPerPage = 5;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordsPerPage)));
            int NoOfRecordsToSkip = (PageNo - 1) * NoOfRecordsPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPages = NoOfPages;
            products = products.Skip(NoOfRecordsToSkip).Take(NoOfRecordsPerPage).ToList();
            return View(products);
        }

        public ActionResult Details(long id)
        {
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,Price,DOP,AvailabilityStatus,CategoryID,BrandID,Active,Photo")] Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count >= 1)
                    {
                        var file = Request.Files[0];
                        var imgBytes = new Byte[file.ContentLength];
                        file.InputStream.Read(imgBytes, 0, file.ContentLength);
                        var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                        p.Photo = base64String;
                    }
                    db.Products.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Categories = db.Categories.ToList();
                    ViewBag.Brands = db.Brands.ToList();
                    return View();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {

                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }

        public ActionResult Edit(long id)
        {
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product p)
        {
            if (ModelState.IsValid)
            {
                Product existingProduct = db.Products.Where(temp => temp.ProductID == p.ProductID).FirstOrDefault();
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    existingProduct.Photo = base64String;
                }
                existingProduct.ProductName = p.ProductName;
                existingProduct.Price = p.Price;
                existingProduct.DOP = p.DOP;
                existingProduct.CategoryID = p.CategoryID;
                existingProduct.BrandID = p.BrandID;
                existingProduct.AvailabilityStatus = p.AvailabilityStatus;
                existingProduct.Active = p.Active;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Products");
        }
    }
}