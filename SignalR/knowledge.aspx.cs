using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class knowledge : System.Web.UI.Page
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        private static string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private static SqlDataReader reader;
        private static SqlCommand mySqlCmd;
        private string name = "";
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

                if (context.Session["id"] != null && context.Session["pw"] != null)
            {

                plugin.easy(this.Page);
                if (SQLChecker.checkID(Session["id"].ToString(), Session["pw"].ToString()))
                {

                    try
                    {
                        Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");
                        Response.Write("<div id='backdiv' class='space'><input id='back' type='button' onclick='enter()' /></div>");
                        Response.Write("<div id='nextdiv' class='space'><input id='next' type='button' onclick='enter()' /></div>");
                        Response.Write("<div id='idiomdiv' class='space'><input id='idiom' type='button' onclick='edit(this.id)' /></div>");
                        Response.Write("<div id='poetrydiv' class='space'><input id='poetry' type='button' onclick='edit(this.id)' /></div>");
                        Response.Write("<div id='showdiv' class='space'><div id='show' type='button' ></div></div>");
                        Response.Write("<div id='show2div' class='space'><div id='show2' type='button' ></div></div>");


                        
                        Response.Write("<img src='pic/knowledge.JPG' alt='' usemap='#Map' id='mappic' />");
                        
                        Response.Write("<map name='map' id='map'>");

                        Response.Write("<area id='exitspace' class='input' alt='' title=''  shape='rect' coords='722,595,985,718' />");
                        Response.Write("<area id='backspace' class='input' alt='' title=''  shape='rect' coords='594,3,828,80' />");
                        Response.Write("<area id='nextspace' class='input' alt='' title=''  shape='rect' coords='837,6,1056,86' />");
                        Response.Write("<area id='idiomspace' class='input' alt='' title=''  shape='rect' coords='49,528,224,594' />");
                        Response.Write("<area id='poetryspace' class='input' alt='' title=''  shape='rect' coords='50,614,228,678' />");
                        Response.Write("<area id='showspace' class='div' alt='' title=''  shape='rect' coords='212,150,601,447' />");
                        Response.Write("<area id='show2space' class='div' alt='' title=''  shape='rect' coords='614,152,1029,459' />");
                        Response.Write("</map>");
                        
                        switch (typeBox.Text)
                        {
                            default:
                                GridView1.CssClass = "hide";

                                GridView2.CssClass = "hide";
                                break;

                            case "idiom":
                                GridView1.CssClass = "show";

                                GridView2.CssClass = "show2";
                                name = SQLChecker.randomName("成語");

                                gridViewSet.setGridView(GridView1, SQLChecker.getQuestion(name));
                                gridViewSet.setGridView(GridView2, SQLChecker.getExplain(name));
                                break;

                            case "poetry":
                                GridView1.CssClass = "show";
                                GridView2.CssClass = "show2";
                                name = SQLChecker.randomName("詩詞");

                                gridViewSet.setGridView(GridView1, SQLChecker.getQuestion(name));

                                 gridViewSet.setGridView(GridView2, SQLChecker.getExplain(name));
                                 /*  * */
                                break;
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.StackTrace);
                        Response.Write(ex.HelpLink);
                        Response.Write(ex.Message);
                        Response.Write(ex.Data);
                    }
                }
                else
                {

                    Response.Redirect("Default.aspx");

                }
            }
            else
            {
                Response.Redirect("Default.aspx");

            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("study.aspx");
        }
    }
}