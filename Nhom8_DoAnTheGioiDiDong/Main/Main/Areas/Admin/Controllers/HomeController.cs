using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Controllers/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}