using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductDetail(int id, int idOp)
        {
            ConnectProduct_Option obj = new ConnectProduct_Option();
            ado_Product_Option product = obj.GetProductDetail(id);
            ViewBag.idOp = idOp;
            return View(product);
        }

        public ActionResult OptionPartial(int IdPro, int selected)
        {
            ConnectOption objOP = new ConnectOption();
            List<ado_Option> ls = objOP.getDataByIdProduct(IdPro);
            ViewBag.selected = selected;   
            return View(ls);  
        }

    }
}