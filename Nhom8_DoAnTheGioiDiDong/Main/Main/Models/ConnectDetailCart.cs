using System.Web;
using Main.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Deployment.Internal;
using System;
using System.Collections.Generic;

namespace Main.Models
{
    public class ConnectDetailCart
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        // trả về danh sách sản phẩm trong giỏ hàng của 1 người dùng
        // idOption, ImgOption, Tên SP, Tên Option, Giá Option, Số lượng 
        public List<ado_CartDetail> getAllDetailCartbyIdCart(int IdCart)
        {
            List<ado_CartDetail> lst = new List<ado_CartDetail>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select * from detailCart where cartId = " + IdCart;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ado_CartDetail item = new ado_CartDetail();

                    item.CartId = Convert.ToInt32(rdr.GetValue(0).ToString());
                    item.OptionId = Convert.ToInt32(rdr.GetValue(1).ToString());
                    item.Capacity = Convert.ToInt32(rdr.GetValue(2).ToString());

                    ConnectOption objOp = new ConnectOption();

                    ado_Option option = objOp.getDataById(item.OptionId);
                    item.OpName = option.name;
                    item.price = option.price;
                    item.img = option.img;
                    item.ProductId = option.ProductId;

                    ConnectProduct objPro = new ConnectProduct();
                    ado_Product product = objPro.getDataById(item.ProductId);
                    item.nameProduct = product.name;
                    item.decription = product.decription;
                    item.categoryId = product.categoryId;
                    item.bardId = product.brandId;
                    
                    lst.Add(item);
                }

                con.Close();
            }
            return lst;
        }

        // out put: số sản phẩm (khác nhau) trong giỏ hàng
        public int countDetailCartbyIdCart(int IdCart)
        {
            int count = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select count(*) from detailCart where cartId = " + IdCart;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                count = (int)cmd.ExecuteScalar();

                con.Close();
            }
            return count;
        }

        //Thêm sản phẩm(idOption, số lượng = 1) vào cartID
        public void addOptionProductInCartDetail(int cartID, int OptionID)
        {
            if (checkOptionInCartDetail(cartID, OptionID)) // kiểm tra OptionID có trong  cartID chưa
                updateCapacity(cartID, OptionID,1); //Update số lượng option trong cartDetail tăng lên 1
            else
            {   //Thêm sản phẩm(idOption, số lượng = 1) vào cartID
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();

                    string query = "insert into detailCart (cartId, productId, Capacity) values("+cartID+","+OptionID+",1)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }    

        }

        // kiểm tra option đó đã có trong giỏ hàng chưa
        // true => có || false => không
        public bool checkOptionInCartDetail(int CartId, int OptionId)
        {
            int rs = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select COUNT(*) from detailCart where cartId = "+ CartId +" and productId = " + OptionId;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                rs = (int)cmd.ExecuteScalar();

                con.Close();
            }
            if (rs == 0)
                return false;
            return true;
        }

        // Update số lượng option trong cartDetail tăng lên 1
        public void updateCapacity(int CartId, int OptionId, int i)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "update detailCart set Capacity = (select Capacity from detailCart where cartId = " +CartId+ " and productId = " +OptionId+ ") + "+i+" where cartId = " + CartId + " and productId = " + OptionId;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        // Delete OptionProduct in DetailCart with idCart && idOption
        public void DeleteOptProduct(int idCart, int idOpt)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "delete from detailCart where cartId = "+ idCart+" and productId = " + idOpt;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        //
        public void UpdateOptProduct(int idCart, int idOptNew, int idOptOld, int Sl)
        {
            if(idOptNew != idOptOld && checkOptionInCartDetail(idCart, idOptNew))
            {
                updateCapacity(idCart, idOptNew,Sl);
                DeleteOptProduct(idCart,idOptOld);
            }                
            else
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();

                    string query = "update detailCart set productId = "+idOptNew+", Capacity = "+Sl+" where cartId = "+idCart+" and productId = " + idOptOld;

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }    
        }


        // trả về dữ liệu khi có mảng id option
        public ado_CartDetail getDataByIdOp(int IdCart, int idOption)
        {
            ado_CartDetail item = new ado_CartDetail();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select Top 1 * from detailCart where cartId = " + IdCart + "and productId = " + idOption;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    item.CartId = Convert.ToInt32(rdr.GetValue(0).ToString());
                    item.OptionId = Convert.ToInt32(rdr.GetValue(1).ToString());
                    item.Capacity = Convert.ToInt32(rdr.GetValue(2).ToString());

                    ConnectOption objOp = new ConnectOption();

                    ado_Option option = objOp.getDataById(item.OptionId);
                    item.OpName = option.name;
                    item.price = option.price;
                    item.img = option.img;
                    item.ProductId = option.ProductId;

                    ConnectProduct objPro = new ConnectProduct();
                    ado_Product product = objPro.getDataById(item.ProductId);
                    item.nameProduct = product.name;
                    item.decription = product.decription;
                    item.categoryId = product.categoryId;
                    item.bardId = product.brandId;

                }

                con.Close();
            }
            return item;
        }

        public List<ado_CartDetail> getDataByArrIdOp(int IdCart, string[] arr)
        {
            if(arr == null)
                return null;
            List<ado_CartDetail> lst = new List<ado_CartDetail>();

            foreach(string item in arr) {
                ado_CartDetail temp = getDataByIdOp(IdCart, Convert.ToInt32(item));
                lst.Add(temp);
            }    
            return lst;
        }

    }
}