using System.Web;
using Main.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Deployment.Internal;
using System;

namespace Main.Models
{
    public class ConnectCart
    {

        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        // trả về id cart của user
        public int getIdCart(string UserName)
        {
            ado_Cart cart = new ado_Cart();
            if (!checkUserCart(UserName))
            {
                cart.Id = 0;
                cart.UserName = "";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();

                    string query = "select * from Carts where Username = '" + UserName + "'";

                    SqlCommand cmd = new SqlCommand(query,con);

                    cmd.CommandType = CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while(rdr.Read())
                    {
                        cart.Id = Convert.ToInt32(rdr.GetValue(0).ToString());
                        cart.UserName = rdr.GetValue(1).ToString();
                    }    

                    con.Close();
                } 
            }    

            return cart.Id;
        }

        // kiểm tra người dùng có giỏ hàng chưa?? => true(đã có) && false(chưa có)
        public bool checkUserCart(string UserName)
        {
            int rs = 0;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select COUNT(*) from Carts where Username = '" + UserName + "'";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                rs = (int)cmd.ExecuteScalar();

                con.Close();
            }

            if (rs != 1)
                return false;
            return true;
        }

        // Thêm Cart mới cho user 
        public void addCartUser(string UserName)
        {
            if(!checkUserCart(UserName))
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();

                    string query = "insert into Carts (Username) values('"+ UserName +"')";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.CommandType = CommandType.Text;

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }    
        }

    }
}