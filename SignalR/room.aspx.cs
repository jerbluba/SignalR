using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class room : System.Web.UI.Page
    {
        string x=SQLChecker.randomName("詩詞");
        
        protected void Page_Load(object sender, EventArgs e)
        {

            plugin.easy(this.Page);
            Random x = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < 50;i++ )
            {
                Response.Write("</br>"+x.Next(2));
                SQLChecker.comparer(666, SQLChecker.getNoByPlay(45));
            }
        }
    }
}