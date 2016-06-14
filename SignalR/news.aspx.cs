using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class news : System.Web.UI.Page
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        private static string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private static SqlDataReader reader;
        private static SqlCommand mySqlCmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["idBox"] != null)
            {
                Session["id"] = Request.Params["idBox"].ToString();
                idBox.Text = Session["id"].ToString();

            }
            if (context.Session["id"] != null)
            {
                idBox.Text = Session["id"].ToString();
            }


            if (Request.Form["pwBox"] != null)
            {
                Session["pw"] = Request.Params["pwBox"].ToString();
                pwBox.Text = Session["pw"].ToString();
            }
            if (context.Session["pw"] != null)
            {
                pwBox.Text = Session["pw"].ToString();
            }

            if (Request.Form["welBox"] != null)
            {
                Session["wel"] = Request.Params["welBox"].ToString();
                welBox.Text = Session["wel"].ToString();
            }

            if (context.Session["wel"] != null)
            {
                welBox.Text = Session["wel"].ToString();
            }

            if (Request.Form["iconBox"] != null)
            {
                Session["icon"] = Request.Params["iconBox"].ToString();
                iconBox.Text = Session["icon"].ToString();
            }

            if (context.Session["icon"] != null)
            {
                iconBox.Text = Session["icon"].ToString();
            }

            if (Request.Form["cBox"] != null)
            {
                Session["confirm"] = Request.Params["cBox"].ToString();
                cBox.Text = Session["confirm"].ToString();
            }


            if (context.Session["confirm"] != null)
            {
                cBox.Text = Session["confirm"].ToString();
            }


            if (Request.Form["cBox"] == null && context.Session["confirm"] == null)
            {
                Session["confirm"] = "false";
                //    Response.Write("noCon");
            }

            plugin.defaultSet(this.Page);
         
                if (!Page.IsPostBack)
                {
                    mygDB();
                }

                Response.Write("<div id='backdiv' class='space'><input id='back' type='button' onclick='exit()' /></div>");
                Response.Write("<div id='showdiv' class='space'><div id='show'></div></div>");
                


                Response.Write("<img src='pic/public.JPG?v=" + gridViewSet.version + "' alt='' usemap='#Map' id='mappic' />");
                Response.Write("<map name='map' id='map'>");

                Response.Write("<area id='showspace' class='input' alt='' title=''  shape='rect' coords='168,303,796,646' />");
                Response.Write("<area id='backspace' class='input' alt='' title=''  shape='rect' coords='833,334,985,530' />");
                Response.Write("</map>");

            
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            mygDB();
        }
        protected void mygDB()
        {
            try
            {
                mySqlCmd = new SqlCommand("SELECT time,name FROM announce order by time desc", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();


                DataTable dt = new DataTable();
                dt.Load(reader);

                GridView1.DataSource = dt.AsDataView();
                GridView1.DataBind();
                mySqlCmd.Connection.Close();
            }
            catch (Exception e)
            {
                Response.Write("QQ");
            }
            finally
            {


            }


        }
        protected void exit(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}