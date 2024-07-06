using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Main.Models;

namespace Main.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            List<ado_CartDetail> ls= new List<ado_CartDetail>();
            if (Session["user"] != null)
            {
                ConnectCart obj1 = new ConnectCart();
                if(obj1.checkUserCart(Session["user"].ToString()))
                {
                    ConnectDetailCart obj2 = new ConnectDetailCart();
                    if (obj2.countDetailCartbyIdCart(obj1.getIdCart(Session["user"].ToString())) == 0)
                        ViewBag.Error = "Hiện Chưa Có Sản Phẩm";
                    else
                    {
                        ls = obj2.getAllDetailCartbyIdCart(obj1.getIdCart(Session["user"].ToString()));
                        ViewBag.Error = null;
                    }
                }
                else
                    ViewBag.Error = "Hiện Chưa Có Sản Phẩm";
            }
            else
                ViewBag.Error = "Vui Lòng Đặng Nhập";

            return View(ls);
        }

        public ActionResult CountCartPartial()
        {
            // lấy id cart của người dùng

            if(Session["user"] != null)
            {
                ConnectCart obj1 = new ConnectCart();
                ConnectDetailCart obj2 = new ConnectDetailCart();
                ViewBag.CountCart = obj2.countDetailCartbyIdCart(obj1.getIdCart(Session["user"].ToString()));
            }    

            return View();
        }

        public ActionResult AddCart(int idOP, string strURL)
        {
            if (Session["user"] != null)
            {
                ConnectCart objCart = new ConnectCart();
                ConnectDetailCart objdetailC = new ConnectDetailCart();

                //
                objCart.addCartUser(Session["user"].ToString());
                int idCart = objCart.getIdCart(Session["user"].ToString());
                if (idCart != 0)
                    objdetailC.addOptionProductInCartDetail(idCart, idOP);
            }
            return Redirect(strURL);
        }

        public ActionResult DeleteCart(int OptId)
        {
            if (Session["user"] != null)
            {
                ConnectCart objCart = new ConnectCart();
                ConnectDetailCart objDC = new ConnectDetailCart();

                objDC.DeleteOptProduct(objCart.getIdCart(Session["user"].ToString()), OptId);
            }
            return RedirectToAction("Index","Cart");
        }
        [HttpPost]
        public ActionResult UpdateCart(FormCollection f)
        {
            if (Session["user"] != null)
            {
                ConnectCart objCart = new ConnectCart();
                ConnectDetailCart objDC = new ConnectDetailCart();

                objDC.UpdateOptProduct(objCart.getIdCart(Session["user"].ToString()),Convert.ToInt32(f["Options"]), Convert.ToInt32( f["idOption"]), Convert.ToInt32(f["Sl"] ));
            }
            return RedirectToAction("Index", "Cart");
        }


    }
}