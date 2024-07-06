using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["user"] = "admin1";
            return View();
        }

        public ActionResult CategoryPartial()
        {
            ConnectCategory obj = new ConnectCategory();
            List<ado_Category> ls = obj.getAllData();
            return View(ls);
        }

        public ActionResult ProductCategory(int id)
        {
            ConnectProduct_Option obj = new ConnectProduct_Option();
            ConnectCategory obj1 = new ConnectCategory();
            ViewBag.Banner = obj1.getCategoryId(id).img;
            List<ado_Product_Option> ls = obj.GetProductOptionsCategory(id);
            return View(ls);
        }

        

    }
}