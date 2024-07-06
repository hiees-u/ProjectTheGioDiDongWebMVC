using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Main.Models
{
    public class ConnectOption
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public void instOpsInPro(ado_Product_Option pro)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                pro.Options = new List<ado_Option>();

                SqlCommand cmd = new SqlCommand("select * from Options where productId = " + pro.id, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ado_Option op = new ado_Option();

                    op.id = Convert.ToInt32(rdr.GetValue(0).ToString());

                    op.name = rdr.GetValue(1).ToString();              

                    op.price = Convert.ToDecimal(rdr.GetValue(2).ToString());

                    op.img = rdr.GetValue(3).ToString();

                    op.ProductId = Convert.ToInt32(rdr.GetValue(4).ToString());

                    pro.Options.Add(op);
                }

                con.Close();
            }
        }

        //trả về Option => { idOption, name, giá , ảnh, idProduct } theo idOption truyền vào
        public ado_Option getDataById(int ID)
        {
            ado_Option op = new ado_Option();

            using(SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select * from Options where Id = " + ID;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {

                    op.id = Convert.ToInt32(rdr.GetValue(0).ToString());

                    op.name = rdr.GetValue(1).ToString();              

                    op.price = Convert.ToDecimal(rdr.GetValue(2).ToString());

                    op.img = rdr.GetValue(3).ToString();

                    op.ProductId = Convert.ToInt32(rdr.GetValue(4).ToString());

                }    

                con.Close();
            }    

            return op;
        }

        public List<ado_Option> getDataByIdProduct(int idPro)
        {
            List<ado_Option> ls = new List<ado_Option>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select * from Options where productId = " + idPro;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    ado_Option op = new ado_Option();

                    op.id = Convert.ToInt32(rdr.GetValue(0).ToString());

                    op.name = rdr.GetValue(1).ToString();

                    op.price = Convert.ToDecimal(rdr.GetValue(2).ToString());

                    op.img = rdr.GetValue(3).ToString();

                    op.ProductId = Convert.ToInt32(rdr.GetValue(4).ToString());

                    ls.Add(op);
                }    

                con.Close();
            }
            
            return ls;
        }



    }
}