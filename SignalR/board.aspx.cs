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
    public partial class board : System.Web.UI.Page
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


            if (Request.Form["typeBox"] != null)
            {
                Session["type"] = Request.Params["typeBox"].ToString();
                typeBox.Text = Session["type"].ToString();

            }
            if (context.Session["type"] != null)
            {
                typeBox.Text = Session["type"].ToString();
            }
            else {

                typeBox.Text = "review";
            }








            if (context.Session["id"] != null && context.Session["pw"] != null)
            {

                if (SQLChecker.checkID(Session["id"].ToString(), Session["pw"].ToString()))
                {

                    try
                    {
                        plugin.defaultSet(this.Page);
                        switch (typeBox.Text)
                        {
                            default:

                                if (!Page.IsPostBack) {
                                //    mygDB();
                                }/*
                                  GridView1.Columns[0].HeaderText= "留言編號";
                                GridView1.Columns[1].HeaderText = "留言者";
                                GridView1.Columns[2].HeaderText = "留言時間";
                                GridView1.Columns[3].HeaderText = "留言內容";
                                GridView1.Columns[4].HeaderText = "ip位置";
                                  */
                                GridView1.CssClass = "show";
                                Response.Write("<div id='backdiv' class='space'><input id='back' type='button' onclick='exchange(this.id)' /></div>");
                                Response.Write("<div id='adddiv' class='space'><input id='add' type='button' onclick='exchange(this.id)' /></div>");
                                Response.Write("<div id='showdiv' class='space'><div id='show'></div></div>");
                                Response.Write("<img src='pic/board.JPG' alt='' usemap='#Map' id='mappic' />");
                                Response.Write("<map name='map' id='map'>");
                                Response.Write("<area id='backspace' class='input' alt='' title=''  shape='rect' coords='111,323,288,499' />");
                                Response.Write("<area id='addspace' class='input' alt='' title=''  shape='rect' coords='806,560,991,677' />");
                                Response.Write("<area id='showspace' class='div' alt='' title=''  shape='rect' coords='594,190,966,475' />");
                                Response.Write("</map>");
                                break;

                            case "add":
                                GridView1.CssClass = "hide";
                                Response.Write("<div id='canceldiv' class='space'><input id='cancel' type='button' onclick='exchange(this.id)' /></div>");
                                Response.Write("<div id='savediv' class='space'><input id='save' type='button' onclick='exchange(this.id)' /></div>");
                                Response.Write("<div id='contentdiv' class='space'><textarea id='content'></textarea></div>");
                                Response.Write("<img src='pic/newmessage.JPG' alt='' usemap='#Map' id='mappic' />");
                                Response.Write("<map name='map' id='map'>");
                                Response.Write("<area id='savespace' class='input' alt='' title=''  shape='rect' coords='848,539,983,687' />");
                                Response.Write("<area id='cancelspace' class='input' alt='' title=''  shape='rect' coords='590,537,695,699' />");
                                Response.Write("<area id='contentspace' class='textarea' alt='' title=''  shape='rect' coords='594,190,966,475' />");
                                Response.Write("</map>");


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
            Session["type"] = typeBox.Text;


            if (Session["type"].ToString().Equals("back"))
            {
                Response.Redirect("default.aspx");
            }
            else if (Session["type"].ToString().Equals("save"))
            {
                try
                {
                    {
                        DateTime myDateTime = DateTime.Now;
                        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        string insert="INSERT INTO board (id,ip,time,show) VALUES(N'" + Session["id"].ToString() + "','" + Request.Params["ip"] + "','" + sqlFormattedDate + "',N'" + Request.Params["show"] + "')";
               //         Response.Write(insert);
                        mySqlCmd = new SqlCommand(insert, new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        mySqlCmd.ExecuteNonQuery();
                        mySqlCmd.Connection.Close();
                    }
                }
                catch {
                    throw;
                }
                finally
                {
                    Session["type"] = "good";
                    if (mySqlCmd != null)
                        mySqlCmd.Dispose();
                }


              //  Session["type"] = "cancel";
//                Response.Redirect("board.aspx");
            }
            
        }




        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
           // mygDB();
        }
        protected void mygDB()
        {
            try
            {
                mySqlCmd = new SqlCommand("SELECT no,id,time,show,ip FROM board order by time desc", new SqlConnection(connString));
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
    }
}