using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopify.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shopify.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly ShopifyContext db;

        public ProductsController(ShopifyContext shopifydb)
        {
            db = shopifydb;
        }

        public ActionResult Product(Product obj)
        {
            LoadCategories();
            return View(obj);
        }

        public ActionResult List()
        {
            var productList = db.Product.ToList();
            return View(productList);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product model)
        {
            Product productDb = new Product();


            if (ModelState.IsValid)
            {
                productDb.ProductId = model.ProductId;
                productDb.ProductName = model.ProductName;
                productDb.CategoryId = model.CategoryId;

                if(model.ProductId != 0)
                {
                    db.Entry(productDb).State = EntityState.Modified;
                    await db.SaveChangesAsync();

                    return RedirectToAction("List");
                    
                }
                else
                {
                    db.Product.Add(productDb);
                    await db.SaveChangesAsync();

                    return RedirectToAction("List");
                }
            }
            ModelState.Clear();
            return View("Product");
        }

        public ActionResult Delete(int id)
        {
            var delete = db.Product.Where(x => x.ProductId == id).First();

            db.Product.Remove(delete);
            db.SaveChanges();

            var list = db.Product.ToList();

            return View("List", list);
        }

        public ActionResult AllProductList()
        {
            List<Product> productdetails = db.Product.ToList();
            List<Category> categorydetails = db.Category.ToList();

            var combinedetails = from c in categorydetails
                                 join p in productdetails
                                 on c.CategoryId equals p.CategoryId
                                 into table1
                                 from p in table1.DefaultIfEmpty()
                                 select new ProductDetails
                                 {
                                     categoryDetails = c,
                                     productDetails = p
                                 };


            return View(combinedetails);
        }

        private void LoadCategories()
        {
            List<Category> categoryList = new List<Category>();
            categoryList = db.Category.ToList();

            ViewBag.CategoryList = categoryList;
        }

    }
}
