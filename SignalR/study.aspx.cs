using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class study : System.Web.UI.Page
    {

        System.Web.HttpContext context = System.Web.HttpContext.Current;
        private static string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private static SqlDataReader reader;
        private static SqlConnection dataConnection;
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
            if (context.Session["id"] != null)
            {
                plugin.easy(this.Page);

                Response.Write("<div id='practicediv' class='space'><input id='practice' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='knowledgediv' class='space'><input id='knowledge' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='defaultdiv' class='space'><input id='default' type='button' onclick='enter(this.id)' /></div>");

                Response.Write("<img src='pic/study.JPG' alt='' usemap='#Map' id='mappic' />");
                Response.Write("<map name='map' id='map'>");
                Response.Write("<area id='practicespace' class='input'  alt='' title=''  shape='rect' coords='800,249,980,466' />");
                Response.Write("<area id='knowledgespace' class='input' alt='' title='' href='#' shape='rect' coords='352,469,527,644' />");
                Response.Write("<area id='defaultspace' alt='' title='' class='input' shape='rect' coords='140,408,298,627' />");
                Response.Write("</map>");



            }
            else {
                Response.Redirect("Default.aspx");
            
            }
        }


        protected void enter(object sender, EventArgs e)
        {
            Session["id"] = idBox.Text;
            Session["pw"] = pwBox.Text;
            Session["confirm"] = cBox.Text;
            if (Session["confirm"].ToString().Equals(""))
            {
                Session["confirm"] = "false";
            }
            Session["icon"] = iconBox.Text;
            Response.Redirect(Request.Params["enterBox"] + ".aspx");
        }

    }
}