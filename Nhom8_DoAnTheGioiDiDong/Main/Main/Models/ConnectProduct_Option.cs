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
    public class ConnectProduct_Option
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        // lấy tất cả sản phẩm
        public List<ado_Product_Option> getAllData()
        {
            List<ado_Product_Option> products = new List<ado_Product_Option>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from Products", con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ado_Product_Option item = new ado_Product_Option();

                    item.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    item.name = rdr.GetValue(1).ToString();
                    item.decription = rdr.GetValue(2).ToString();
                    item.categoryId = Convert.ToInt32(rdr.GetValue(3).ToString());
                    item.bardId = Convert.ToInt32(rdr.GetValue(4).ToString());
                   
                    ConnectOption obj = new ConnectOption();
                    obj.instOpsInPro(item); 

                    products.Add(item);
                }

                con.Close();
            }

            return products;
        }

        public List<ado_Product_Option> GetProductOptionsCategory(int id)
        {
            List<ado_Product_Option> products = new List<ado_Product_Option>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from Products where categoryId =" + id, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ado_Product_Option item = new ado_Product_Option();

                    item.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    item.name = rdr.GetValue(1).ToString();
                    item.decription = rdr.GetValue(2).ToString();
                    item.categoryId = Convert.ToInt32(rdr.GetValue(3).ToString());
                    item.bardId = Convert.ToInt32(rdr.GetValue(4).ToString());

                    ConnectOption obj = new ConnectOption();
                    obj.instOpsInPro(item);

                    products.Add(item);
                }

                con.Close();
            }

            return products;
        }

        public ado_Product_Option GetProductDetail(int id)
        {
            ado_Product_Option product = new ado_Product_Option();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from Products where Id =" + id, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    product.id = Convert.ToInt32(rdr.GetValue(0).ToString());
                    product.name = rdr.GetValue(1).ToString();
                    product.decription = rdr.GetValue(2).ToString();
                    product.categoryId = Convert.ToInt32(rdr.GetValue(3).ToString());
                    product.bardId = Convert.ToInt32(rdr.GetValue(4).ToString());

                    ConnectOption obj = new ConnectOption();
                    obj.instOpsInPro(product);
                }

                con.Close();
            }

            return product;
        }
    }
}