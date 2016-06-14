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
    public partial class ladder : System.Web.UI.Page
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


            if (context.Session["id"] != null && context.Session["pw"] != null)
            {


                    try
                    {


                        plugin.easy(this.Page);

                        string[][] table=new string[10][];


                        mySqlCmd = new SqlCommand("SELECT top 10 id,score FROM users order by score desc" , new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        reader = mySqlCmd.ExecuteReader();
                        int i = 0;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                table[i]=new string[]{""+(i+1),reader.GetString(0),""+reader.GetInt32(1)};
                                ++i;
                            }

                        }

                        while(i<10){
                            table[i] = new string[] { "--", "--", "--", };
                            ++i;
                        }
                        gridViewSet.setGridView(GridView1, table, new string[] { "名次", "帳號", "分數", });



                        // TODO 加入骰子大小 加入表格

                        Response.Write("<div id='backdiv' class='space'><input id='back' name='back' type='button' onclick='enter()' /></div></div>");
                        Response.Write("<div id='ladderdiv' class='space'><div id='ladder'><table id='laddertable'></table></div></div>");

                        Response.Write("<img src='pic/ladder.JPG' alt='' usemap='#Map' id='mappic' />");
                        Response.Write("<map name='map' id='map'>");
                        Response.Write("<area id='ladderspace' class='div' alt='' title=''  shape='rect' coords='232,95,797,482' />");
                        Response.Write("<area id='backspace' class='input' alt='' title=''  shape='rect' coords='818,499,1052,766' />");

                        Response.Write("</map>");





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
            Session["wel"] = welBox.Text;

            Response.Redirect(Request.Params["enterBox"] + ".aspx");
        }
    }
}