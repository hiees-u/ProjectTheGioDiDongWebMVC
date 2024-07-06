using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        dbTGDDDataContext db = new dbTGDDDataContext();
        public ActionResult showCategory()
        {
            ConnectCategory obj = new ConnectCategory();
            List<Category> lcg = db.Categories.ToList();
            return View(lcg);
        }
        [HttpPost]
        public ActionResult createCategory(Category cg)
        {
            if (ModelState.IsValid)
            {
                db.Categories.InsertOnSubmit(cg);
                db.SubmitChanges();
                return RedirectToAction("showCategory", "Category");
            }
            return View();
        }
        public ActionResult createCategory()
        {
            return View();
        }
        public ActionResult deleteCategory(int id)
        {
            Category cg = db.Categories.Single(s => s.Id == id);
            db.Categories.DeleteOnSubmit(cg);
            db.SubmitChanges();
            List<Category> lCg = db.Categories.ToList();
            return View("showCategory", lCg);
        }
        public ActionResult deleteShow(int id)
        {
            Category cg = db.Categories.Single(s => s.Id == id);
            Session["info"] = cg;
            return View();
        }

        [HttpPost]
        public ActionResult editShow(ado_Category cg)
        {
            Category dbcg = db.Categories.Single(s => s.Id == cg.id);
            if(cg.name != null)
            {
                dbcg.Name = cg.name;
            }
            if(cg.img != null)
            {
                dbcg.Img = cg.img;
            }
            db.SubmitChanges();
            return RedirectToAction("showCategory", "Category");
        }
        public ActionResult editShow()
        {

            return View();
        }
        public ActionResult searchForName(string search)
        {
            List<Category> lCg = db.Categories.Where(t => t.Name.Contains(search)).ToList();
            return View("showCategory", lCg);
        }
        public ActionResult detailCategory(int id)
        {
            Category cg = db.Categories.Single(e => e.Id == id);
            Session["info"] = cg;
            List<Product> lpd = db.Products.Where(t => t.categoryId == id).ToList();
            return View(lpd);
        }
    }
}