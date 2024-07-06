using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Main.Models
{
    public class ConnectOrder
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        //  tạo 1 order của user id
        public void AddOrder(ado_Order order)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "insert into Orders (userId, createAt, Phone, Address, Name) values(" + order.userId + ",'" + order.createAt + "','" + order.Phone + "',N'" + order.Address + "', N'" + order.Name + "')";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                cmd.ExecuteNonQuery();

                con.Close();
            }
        }

        // trả về danh sách các order của user id
        public List<ado_Order> getOrdersByIdUser(string userName)
        {
            int idUser = getIdUserByUserName(userName);

            List<ado_Order> ls = new List<ado_Order>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select * from Orders where userId = " + idUser;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ado_Order o = new ado_Order();

                    o.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    o.userId = Convert.ToInt32(rdr.GetValue(1).ToString());
                    o.createAt = DateTime.Parse(rdr.GetValue(2).ToString());
                    o.State = Convert.ToInt32(rdr.GetValue(3).ToString());
                    string deliveryDate = rdr.GetValue(4).ToString();
                    if(deliveryDate != "")
                        o.deliveryDate = DateTime.Parse(deliveryDate);
                    o.Phone = rdr.GetValue(5).ToString();
                    o.Address = rdr.GetValue(6).ToString(); 
                    o.Name = rdr.GetValue(7).ToString();
                    o.SumPrice = (float)Double.Parse(rdr.GetValue(8).ToString());

                    ls.Add(o);
                }

                con.Close();
            }   
            return ls;
        }

        public int getIdUserByUserName(string userName)
        {
            int id = 0;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select Top 1 Id from Users where Username = '" + userName + "'";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    id = Convert.ToInt32(rdr.GetValue(0).ToString());
                }

                con.Close();
            }

            return id;
        }

    }
}