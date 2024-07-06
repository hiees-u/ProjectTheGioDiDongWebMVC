using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Main.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Deployment.Internal;



namespace Main.Models
{
    public class ConnectProduct
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public ado_Product getDataById(int ID)
        {
            ado_Product pro = new ado_Product();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select * from Products where Id = " + ID;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    pro.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    pro.name = rdr.GetValue(1).ToString();
                    pro.decription = rdr.GetValue(2).ToString();
                    pro.categoryId = Convert.ToInt32(rdr.GetValue(3).ToString());
                    pro.brandId = Convert.ToInt32(rdr.GetValue(4).ToString());

                }

                con.Close();
            }

            return pro;
        }
    }
}