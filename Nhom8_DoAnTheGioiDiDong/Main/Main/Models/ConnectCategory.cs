using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Main.Models
{
    public class ConnectCategory
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public List<ado_Category> getAllData()
        {
            List<ado_Category> categories = new List<ado_Category>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "select * from Categorys";
                SqlCommand cmd = new SqlCommand(query,con);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    ado_Category cate = new ado_Category();
                    cate.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    cate.name = rdr.GetValue(1).ToString();
                    cate.img = rdr.GetValue(2).ToString();
                    categories.Add(cate);
                }    
                con.Close();
            }
            return categories;
        }

        public ado_Category getCategoryId(int id)
        {
            ado_Category category = new ado_Category();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "select * from Categorys where Id = "+ id;
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    category.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    category.name = rdr.GetValue(1).ToString();
                    category.img = rdr.GetValue(2).ToString();
                }
                con.Close();
            }
            return category;
        }



    }
}