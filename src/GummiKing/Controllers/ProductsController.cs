using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GummiKing.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GummiKing.Controllers
{
    public class ProductsController : Controller
    {
        private GummiContext db = new GummiContext();
        public IActionResult Index()
        {
            return View(db.Products.Include(products => products.Country).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisProduct = db.Products.Include(products => products.Country).FirstOrDefault(products => products.ProductId == id);

            return View(thisProduct);
        }

        public IActionResult Country()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Country(Country country)
        {
            db.Countries.Add(country);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(products => products.ProductId == id);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisProduct = db.Products.FirstOrDefault(products => products.ProductId == id);
            db.Products.Remove(thisProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
