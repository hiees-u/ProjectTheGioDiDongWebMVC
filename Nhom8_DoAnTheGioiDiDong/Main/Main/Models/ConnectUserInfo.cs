using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Main.Models;

namespace Main.Models
{
    public class ConnectUserInfo
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

        public ado_UserInfo getUserInfoByIdUser(int id)
        {
            ado_UserInfo userInfo = new ado_UserInfo();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                string query = "select Top 1 * from infoUser where userId = " + id;

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.CommandType = CommandType.Text;

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    userInfo.userId = Convert.ToInt32(rdr.GetValue(0).ToString());
                    userInfo.accountName = rdr.GetValue(1).ToString();
                    userInfo.Address = rdr.GetValue(2).ToString();
                    userInfo.Gender = Convert.ToInt32( Boolean.Parse(rdr.GetValue(3).ToString()) );
                    userInfo.Phone  = rdr.GetValue(4).ToString();
                    userInfo.Email = rdr.GetValue(5).ToString();
                }

                con.Close();
            }

            return userInfo;
        }
    }
}