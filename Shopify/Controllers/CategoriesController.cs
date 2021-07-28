using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopify.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ShopifyContext db;

        public CategoriesController(ShopifyContext shopifyDb)
        {
            db = shopifyDb;
        }

        // GET: Categories
        public ActionResult Index()
        {
            var categoryList = db.Category.ToList();
            return View(categoryList);
        }

        public ActionResult Category(Category obj)
        {
            return View(obj);
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            Category categoryDb = new Category();

            if (ModelState.IsValid)
            {
                categoryDb.CategoryId = model.CategoryId;
                categoryDb.CategoryName = model.CategoryName;

                if(model.CategoryId == 0)
                {
                    db.Category.Add(categoryDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Entry(categoryDb).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            ModelState.Clear();
            return View("Category");
        }

        public ActionResult Delete(int id)
        {
            var delete = db.Category.Where(x => x.CategoryId == id).First();

            db.Category.Remove(delete);
            db.SaveChanges();

            var list = db.Category.ToList();

            return View("Index", list);
        }

    }
}
