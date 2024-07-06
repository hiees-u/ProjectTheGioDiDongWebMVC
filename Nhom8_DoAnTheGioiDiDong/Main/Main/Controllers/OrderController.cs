using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [HttpPost]
        public ActionResult Index(string[] checkboxValues)
        {
            ConnectCart objC = new ConnectCart();
            ConnectDetailCart objDC = new ConnectDetailCart();
            List<ado_CartDetail> ls = objDC.getDataByArrIdOp(objC.getIdCart(Session["user"].ToString()), checkboxValues);
            Session["ls"] = ls;
            return Json(new { newUrl = Url.Action("ConfirmOrder", "Order") });
        }

        [HttpPost]
        public ActionResult ConfirmOrder(FormCollection f)
        {
            if(ModelState.IsValid)
            {

            }    
            return View();
        }

        public ActionResult ConfirmOrder()
        {
            List<ado_CartDetail> ls = Session["ls"] as List<ado_CartDetail>;
            return View(ls);
        }

        public ActionResult frmUserOrderPartial()
        {
            ConnectOrder objOrder = new ConnectOrder();
            ConnectUserInfo objUserIf = new ConnectUserInfo();
            int id = (int)objOrder.getIdUserByUserName(Session["user"].ToString());
            ViewBag.userif = objUserIf.getUserInfoByIdUser(id);
            List<ado_Order> ls = objOrder.getOrdersByIdUser(Session["user"].ToString());
            return View(ls);
        }    


        

    }
}