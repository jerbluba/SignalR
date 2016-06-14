using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class Default : System.Web.UI.Page
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
            if(context.Session["id"]!=null){
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

            if (Request.Form["edit"] != null)
            {
                Session["edit"] = Request.Params["edit"].ToString();
                edit.Text = Session["edit"].ToString();
            }
            else {
                edit.Text = "";
            }

            plugin.defaultSet(this.Page);

            if (context.Session["id"] == null)
            {
                GridView1.Visible = !true;
                
                Response.Write("<div id='main'>");

                Response.Write("<div id='iddiv' class='space'><input id='id' type='text' maxlength='8'/><label id='noid' class='hide'>請輸入帳號</label></div>");
                Response.Write("<div id='pwdiv' class='space'><input id='pw' type='password' maxlength='8'/><label id='nopw' class='hide'>請輸入密碼</label><label id='wrong' class='hide'>帳密錯誤</label></div>");
                Response.Write("<div id='logindiv' class='space'><input id='login' type='button' /></div>");
                Response.Write("<div id='registerdiv' class='space'><input id='register' type='button' onclick='enter(this.id)' /></div>");

                Response.Write("<img src='pic/login.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");
                Response.Write("<map name='map' id='map'>");
                Response.Write("<area id='idspace' class='input' alt='' title=''  shape='rect' coords='457,105,824,203' />");
                Response.Write("<area id='pwspace' class='input'  alt='' title=''  shape='rect' coords='458,225,826,307' />");
                Response.Write("<area id='loginspace' class='input' alt='' title='' href='' shape='rect' coords='49,631,334,760' />");
                Response.Write("<area id='registerspace' class='input' alt='' title='' href='' shape='rect' coords='836,611,1013,757' />");
                Response.Write("</map>");
                Response.Write("</div>");

              

            }
            else if (context.Session["id"].ToString().Equals("admin") || SQLChecker.admin(context.Session["id"].ToString(), context.Session["pw"].ToString()))
            {
                GridView1.Visible = true;
                Response.Write("<div id='main'>");
                Response.Write("<div id='announcediv' class='space'><input id='announce' type='button' onclick='edit(this.id)' /></div>");
                Response.Write("<div id='usersdiv' class='space'><input id='users' type='button' onclick='edit(this.id)' /></div>");
                Response.Write("<div id='questiondiv' class='space'><input id='question' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");
                Response.Write("<div id='showdiv' class='space'><div id='show'></div></div>");
                Response.Write("<img src='pic/admin.JPG?v=" + gridViewSet.version + "' alt='' usemap='#Map' id='mappic' />");
                Response.Write("<map name='map' id='map'>");
                Response.Write("<area id='announcespace' class='input'  alt='' title=''  shape='rect' coords='20,357,135,417' />");
                Response.Write("<area id='usersspace' class='input'  alt='' title=''  shape='rect' coords='59,474,163,559' />");
                Response.Write("<area id='questionspace' class='input'  alt='' title=''  shape='rect' coords='116,391,220,465' />");
                Response.Write("<area id='exitspace'  class='input' alt='' title=''  shape='rect' coords='191,285,318,376' />");
                Response.Write("<area id='showspace'  class='input' alt='' title=''  shape='rect' coords='337,90,1039,664' />");


                Response.Write("</map>");
                Response.Write("</div>");
            }
            else
            {
                GridView1.Visible = !true;
  
                Response.Write("<div id='main'>");



                Response.Write("<div id='newsdiv' class='space'><input id='news' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='registerdiv' class='space'><input id='register' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='chatdiv' class='space'><input id='chat' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='ladderdiv' class='space'><input id='ladder' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='studydiv' class='space'><input id='study' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='boarddiv' class='space'><input id='board' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");


                Response.Write("<img src='pic/main.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");
                Response.Write("<map name='map' id='map'>");
                Response.Write("<area id='newsspace' class='input' alt='' title=''  shape='rect' coords='701,179,835,277' />");
                Response.Write("<area id='registerspace' class='input' alt='' title=''  shape='rect' coords='362,557,630,739' />");
                Response.Write("<area id='chatspace' class='input' alt='' title='' href='' shape='rect' coords='13,510,296,771' />");
                Response.Write("<area id='ladderspace' class='input' alt='' title=''  shape='rect' coords='299,121,507,207' />");
                Response.Write("<area id='studyspace' class='input' alt='' title=''  shape='rect' coords='16,213,275,470' />");
                Response.Write("<area id='boardspace' class='input' alt='' title=''  shape='rect' coords='419,254,697,418' />");
                Response.Write("<area id='exitspace' class='input' alt='' title=''  shape='rect' coords='813,597,994,660' />");
                Response.Write("</map>");
                Response.Write("</div>");
            
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
            Session["icon"] =iconBox.Text;

            Session["type"] = "type";
            Response.Redirect(Request.Params["enterBox"]+".aspx");
        }

        protected void exit(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            
            switch(table.Text){
                case "users":
                    GridView1.DataSourceID = SqlDataSource1.ID;
                    DetailsView1.DataSourceID = SqlDataSource1.ID;
                    GridView1.RowHeaderColumn = "編輯使用者";
                    break;
                case "announce":
                    GridView1.DataSourceID = SqlDataSource2.ID;
                    DetailsView1.DataSourceID = SqlDataSource2.ID;
                    GridView1.RowHeaderColumn = "編輯公告";
                    break;
            }
            GridView1.CssClass = "show";
            DetailsView1.CssClass = "hide";
//            GridView1.EmptyDataTemplate.InstantiateIn(DetailsView1);

        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "myInsert")
            {
                //GridView1.DataSourceID = null;
                //==註解： DataSourceID屬性如果是「空」的，就會引發錯誤，找不到資料！
                //==            此時便會展開 GridView的「EmptyData」樣版了！
                DetailsView1.CssClass = "show";
                GridView1.CssClass = "hide";
            }
        }


        protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            GridView1.DataSourceID = "SqlDataSource1";

            //== 完成新增一筆資料後，重新讓 GridView作 DataBinding，展示資料庫裡面的全部最新資料。
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static String checkID(string id,string pw)
        {

            String answer = "";
            try
            {

                {
                    mySqlCmd = new SqlCommand("SELECT id,pw FROM users where CONVERT(NVARCHAR(MAX), id) = N'" + id + "' and CONVERT(NVARCHAR(MAX), pw) = N'"+pw+"'", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    if  (reader.HasRows)
                    {
                        if (reader.Read()) {
                            answer = "true"; 
                       }
                        
                    }
                    else
                    {
                        answer = "false";
                    }
                }
            }
            finally
            {
                if (mySqlCmd != null)
                {
                    mySqlCmd.Connection.Close();
                    mySqlCmd.Dispose();

                }
            }
            return answer;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            
                    foreach (Control c in e.Row.Cells[0].Controls)
                    {
                        if (c is LinkButton )
                        {
                            LinkButton lbDel = c as LinkButton;
                            
                            if(lbDel.Text.Equals("刪除")){
                                lbDel.OnClientClick = @"if (confirm('確定刪除?') == false) return false;";
                            
                            }else if(lbDel.Text.Equals("更新")){
                              
                            }
                            else if (lbDel.Text.Equals("編輯"))
                            {
                               
                            }
                            
                        }
                    }


                


            }
        }

       

        
    }
}