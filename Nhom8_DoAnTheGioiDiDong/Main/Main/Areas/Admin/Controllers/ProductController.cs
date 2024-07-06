using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        dbTGDDDataContext db = new dbTGDDDataContext();
        // GET: Admin/Product
        public ActionResult showProduct()
        {
            List<Product> lpd = db.Products.ToList();
            return View(lpd);
        }
        [HttpPost]
        public ActionResult createProduct(Product pd)
        {
            ViewBag.pdcg = new SelectList(db.Products.ToList(), "Id", "Name");
            ViewBag.pdbr = new SelectList(db.Brands.ToList(), "Id", "Name");
            if (ModelState.IsValid)
            {
                ViewBag.pdcg = new SelectList(db.Products.ToList(), "Id", "Name");
                ViewBag.pdbr = new SelectList(db.Brands.ToList(), "Id", "Name");
                db.Products.InsertOnSubmit(pd);
                db.SubmitChanges();
                return RedirectToAction("showProduct", "Product");
            }
            return View();
        }
        public ActionResult createProduct()
        {
            ViewBag.pdcg = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.pdbr = new SelectList(db.Brands.ToList(), "Id", "Name");
            return View();
        }
        public ActionResult deleteProduct(int id)
        {
            Product pd = db.Products.Single(s => s.Id == id);
            db.Products.DeleteOnSubmit(pd);
            db.SubmitChanges();
            List<Product> lPd = db.Products.ToList();
            return View("showProduct", lPd);
        }
        public ActionResult deleteShow(int id)
        {
            Product pd = db.Products.Single(s => s.Id == id);
            Session["info"] = pd;
            return View();
        }

        [HttpPost]
        public ActionResult editShow(ado_Product pd)
        {
            ViewBag.pdcg = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.pdbr = new SelectList(db.Brands.ToList(), "Id", "Name");
            Product dbpd = db.Products.Single(s => s.Id == pd.id);
            if (pd.name != null)
            {
                dbpd.Name = pd.name;
            }
            if (pd.decription != null)
            {
                dbpd.Description = pd.decription;
            }
            if(pd.categoryId != 0)
            {
                dbpd.categoryId = pd.categoryId;
            }
            if (pd.brandId != 0)
            {
                dbpd.brandId = pd.brandId;
            }
            db.SubmitChanges();
            return RedirectToAction("showProduct", "Product");
        }
        public ActionResult editShow()
        {
            ViewBag.pdcg = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.pdbr = new SelectList(db.Brands.ToList(), "Id", "Name");
            return View();
        }
        public ActionResult searchForName(string search)
        {
            List<Product> lPd = db.Products.Where(t => t.Name.Contains(search)).ToList();
            return View("showProduct", lPd);
        }
        public ActionResult detailProduct(int id)
        {
            Product pd = db.Products.Single(t => t.Id == id);
            Session["info"] = pd;
            return View(pd);
        }
    }
}