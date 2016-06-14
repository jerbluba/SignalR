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
    public partial class register : System.Web.UI.Page
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        private static string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private static SqlDataReader reader;
        private static OleDbConnection oledbConn;
        private static OleDbDataAdapter oleda;
        private static OleDbCommand oleCmd;
        private static SqlConnection dataConnection;
        private static SqlCommand mySqlCmd;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Form["nameBox"] != null)
            {
                Session["name"] = Request.Params["nameBox"].ToString();
            }

            if(Request.Form["idBox"]!=null){
                Session["id"] = Request.Params["idBox"].ToString();
                idBox.Text = Session["id"].ToString();
            }
            if (context.Session["id"] != null)
            {
                idBox.Text  = Session["id"].ToString();
            }

            if (Request.Form["newIdBox"] != null)
            {
                Session["newId"] = Request.Params["newIdBox"].ToString();
            }
            if (Request.Form["pwBox"] != null)
            {
                Session["pw"] = Request.Params["pwBox"].ToString();
               pwBox.Text= Session["pw"].ToString();
            }
            if (context.Session["pw"] != null)
            {
                pwBox.Text = Session["pw"].ToString();
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
             if (context.Session["confirm"]!= null)
            {
                cBox.Text = Session["confirm"].ToString();
            }

             foreach (string key in Session.Keys)
             {

                // Response.Write(key + " - " + Session[key].ToString() + "<br />");

             }

             plugin.easy(this.Page);
            if (!Session["confirm"].Equals("false"))
            {
                if (context.Session["pw"] != null&&context.Session["name"] != null&&context.Session["id"] != null&&context.Session["icon"] != null) addUser(Session["name"].ToString(), Session["id"].ToString(), Session["pw"].ToString(), Session["icon"].ToString());
                Response.Write("<div id='selector' class='hide'></div>");
                Response.Write("<div id='newIddiv' class='space'><input id='newId' type='text' name='newId' maxlength='8' value='" + Session["id"].ToString() + "' /><label id='noid' class='hide'>請輸入名稱</label></div>");
                Response.Write("<div id='pwdiv' class='space'><input id='pw' type='text' name='pw' maxlength='8' value='" + Session["pw"].ToString() + "' /><label id='nopwc' class='hide'>請輸入密碼</label><label id='wrongpwc' class='hide'>密碼錯誤</label></div>");
                
                Response.Write("<div id='picdiv' class='space'><div id='icon' ></div></div>");
                Response.Write("<div id='nextdiv' class='space'><input id='next' name='next' type='button' /></div>");
                Response.Write("<div id='backdiv' class='space'><input id='back' name='back' type='button'/></div>");

                Response.Write("<img src='pic/confirm.JPG?v="+gridViewSet.version+"' alt='' id='mappic' usemap='#Map' />");
                Response.Write("<map name='map' id='map'>");
                Response.Write("<area id='newIdspace' class='input' alt='' title=''  shape='rect' coords='297,265,672,331' />");
                Response.Write("<area id='pwspace' class='input' alt='' title=''  shape='rect' coords='275,388,662,456' />");
                Response.Write("<area id='nextspace' class='input' alt='' title=''  shape='rect' coords='511,532,669,720' />");
                Response.Write("<area id='backspace' class='input' alt='' title=''  shape='rect' coords='278,635,455,744' />");
                Response.Write("<area id='picspace' class='input' alt='' title=''  shape='rect' coords='844,181,987,297' />");
                Response.Write("</map>");
             //   mappic.ImageUrl = "pic/confirm.jpg";
               
            }
            else {
                Response.Write("<div id='selector' class='hide'></div>");
                Response.Write("<div id='namediv' class='space'><input id='name' type='text' name='name' maxlength='8'/><label id='noname' class='hide'>請輸入名稱</label></div>");
                Response.Write("<div id='iddiv' class='space'><input id='id' type='text' name='id' maxlength='8'/><label id='noid' class='hide'>請輸入帳號</label><label id='reid' class='hide'>帳號已有人使用</label></div>      ");
                Response.Write("<div id='pwdiv' class='space'><input id='pw' type='password' name='pw' maxlength='8'/><label id='nopw' class='hide'>請輸入密碼</label></div>");
                Response.Write("<div id='pwcdiv' class='space'><input id='pwc' type='password' name='pwc' maxlength='8'/><label id='nopwc' class='hide'>請輸入密碼</label><label id='wrongpwc' class='hide'>密碼錯誤</label></div>");
                Response.Write("<div id='surediv' class='space'><input id='sure' name='sure' type='button' /></div>");
              
    
                Response.Write("<div id='picdiv' class='space'><div id='icon' ></div></div>");
                Response.Write("<img src='pic/register.JPG?v="+gridViewSet.version+"' alt='' id='mappic' usemap='#Map' />");
                Response.Write("<map name='map' id='map'>");

                Response.Write("<area  id='namespace' class='input' alt='' title=''  shape='rect' coords='345,126,557,192' />");
                Response.Write("<area id='idspace' class='input' alt='' title=''  shape='rect' coords='607,129,851,185' />");
                Response.Write("<area id='pwspace' class='input' alt='' title=''  shape='rect' coords='333,305,528,364' />");
                Response.Write("<area id='pwcspace' class='input' alt='' title=''  shape='rect' coords='601,303,852,363' />");
                Response.Write("<area id='surespace' class='input' alt='' title=''  shape='rect' coords='947,489,1041,768' />");
                Response.Write("<area id='picspace' class='input' alt='' title=''  shape='rect' coords='107,278,241,408' />");

                Response.Write("</map>");
             //   mappic.ImageUrl = "pic/register.jpg";
            
            
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]//
        public static String checkID(string id)
        {

            String answer = "";
            try
            {
            
                {
                    mySqlCmd = new SqlCommand("SELECT * FROM users where CONVERT(NVARCHAR(MAX), id) = N'" + id + "'", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while(reader.Read()){
                            answer = "false";
                        }
                        
                    }
                    else
                    {
                        answer = "true";
                    }
                }
            }
            finally
            {
                if(mySqlCmd!=null)
                    mySqlCmd.Dispose();
            }
            return answer;
        }

        public String getNo()
        {

            String answer = "";
            try
            {
                
                {
                    mySqlCmd = new SqlCommand("SELECT * FROM users where CONVERT(NVARCHAR(MAX), id) = N'" + Session["id"].ToString() + "'", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                   if (reader.HasRows)
                    {
                        while(reader.Read()){
                            answer = reader.GetInt32(0) + "";
                        }
                        
                    }
                    else
                    {
                        answer = "0";
                    }
                }
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }
            return answer;
        }

        public void addUser(string name,string id,string pw,string icon)
        {
            if(checkID(id).Equals("true")){
                try
                {
                    {
                        mySqlCmd = new SqlCommand("INSERT INTO users (name,id,pw,icon,score) VALUES(N'" + name + "',N'" + id + "',N'" + pw + "',N'" + icon + "',0)", new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        mySqlCmd.ExecuteNonQuery();
                        mySqlCmd.Connection.Close();
                    }
                }
                finally
                {
                    if (mySqlCmd != null)
                        mySqlCmd.Dispose();
                }
            }

           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    mySqlCmd = new SqlCommand("UPDATE  users set id=N'" + Session["newId"].ToString() + "',pw=N'" + Session["pw"].ToString() + "',icon='" + Session["icon"].ToString() + "' where no='" + getNo() + "'", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    mySqlCmd.ExecuteNonQuery();
                    mySqlCmd.Connection.Close();
                }
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }

            Session["id"] = Session["newId"];
            Session["pw"] = Session["pw"];
            Session["icon"] = iconBox.Text;
            Session["confirm"] = cBox.Text;
            Response.Redirect("Default.aspx"); 
        }
        protected void login(object sender, EventArgs e)
        {
            Session["id"] = idBox.Text;
            Session["pw"] = pwBox.Text;
            Session["icon"] = Session["iconBox"];
            Session["confirm"] = cBox.Text;
            Response.Redirect("Default.aspx"); 
        }
    }

}