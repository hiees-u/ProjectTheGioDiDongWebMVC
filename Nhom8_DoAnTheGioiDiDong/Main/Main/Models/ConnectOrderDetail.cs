using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Main.Models
{
    public class ConnectOrderDetail
    {
        string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

    }
}