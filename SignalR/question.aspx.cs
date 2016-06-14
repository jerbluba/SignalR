using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class question : System.Web.UI.Page
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

            if (Request.Form["typeBox"] != null)
            {
                Session["type"] = Request.Params["typeBox"].ToString();
                typeBox.Text = Session["type"].ToString();
            }

            if (context.Session["type"] != null)
            {
                typeBox.Text = Session["type"].ToString();
            }


            plugin.easy(this.Page);
            if (context.Session["id"] == null)
            {

                Response.Redirect("Default.aspx");


            }
            else if (context.Session["id"].ToString().Equals("admin") || SQLChecker.admin(context.Session["id"].ToString(), context.Session["pw"].ToString()))
            {

                switch(typeBox.Text){
                    case "add":
                        GridView1.CssClass = "hide";
                        GridView2.CssClass = "hide";
                        for (int i = 1; i < 5; i++)
                        {
                            Response.Write("<div id='t" + i + "div' class='space'><input type='text' id='t" + i + "' maxLength='7' class='t' value='' /></div>");
                            Response.Write("<div id='spnAlt" + i + "div' class='space'><span>破音字選擇：</span><span id='spnAlt" + i + "'></span></div>");

                            Response.Write("<div style='padding: 5px;' id='preview" + i + "div' class='space'><img id='preview" + i + "' /></div>");
                        }
                        Response.Write("<div id='nextdiv' class='space'><input id='next' type='button' /></div>");
                        Response.Write("<div id='backdiv' class='space'><input id='back' type='button' onclick='enter(this.id)' /></div>");
                        Response.Write("<div id='setypediv' class='space'><input id='setype' type='text' /></div>");
                        Response.Write("<div id='namediv' class='space'><input id='name' type='text' /></div>");
                        Response.Write("<div id='explaindiv' class='space'><textarea id='explain' type='text'></textarea></div>");

                        Response.Write("<img src='pic/add.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");
                        Response.Write("<map name='map' id='map'>");
                        Response.Write("<area id='backspace' class='input'  alt='' title=''  shape='rect' coords='41,683,185,739' />");
                        Response.Write("<area id='nextspace' class='input'  alt='' title=''  shape='rect' coords='38,629,184,672' />");
                        Response.Write("<area id='setypespace' class='input'  alt='' title=''  shape='rect' coords='224,653,480,751' />");
                        Response.Write("<area id='namespace' class='input'  alt='' title=''  shape='rect' coords='530,653,790,751' />");
                        Response.Write("<area id='explainspace' class='textarea'  alt='' title=''  shape='rect' coords='752,95,1041,452' />");

                        for (int i = 1; i < 5; i++) {
                            Response.Write("<area id='t" + i + "space' class='input'  alt='' title='' shape='rect' coords='26," + (93 + (i - 1) * 87) + ",365," + (93 +60+ (i - 1) * 87 ) + "' />");
                            Response.Write("<area id='spnAlt" + i + "space'  alt='' title='' href='register.aspx' shape='rect' coords='26," + (93 + 60 + (i - 1) * 87) + ",365," + (93 + 60+27 + (i - 1) * 87) + "' />");
                            
                            Response.Write("<area id='preview" + i + "space'  alt='' title='' href='register.aspx' shape='rect' coords='382," + (92 + (i - 1) * 90) + ",733," + (92 + (i ) * 90) + "' />");
                        
                        }

                        
                        Response.Write("</map>");

                        break;

                    case "edit":


                        GridView2.CssClass = "show2";
                        GridView1.CssClass = "hide";
                        GridView2.PageSize = 10;

                        for (int i = 1; i < 5; i++)
                        {
                     //       Response.Write("<div id='t" + i + "div' class='space'><input type='text' id='t" + i + "' maxLength='7' class='t' value='' /></div>");
                       //     Response.Write("<div id='spnAlt" + i + "div' class='space'><span>破音字選擇：</span><span id='spnAlt" + i + "'></span></div>");

                         //   Response.Write("<div style='padding: 5px;' id='preview" + i + "div' class='space'><img id='preview" + i + "' /></div>");
                        }
                        Response.Write("<div id='backdiv' class='space'><input id='back' type='button' onclick='enter(this.id)' /></div>");
                        Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");
                     //   Response.Write("<div id='updiv' class='space'><input id='up' type='button' onclick='up()' /></div>");
                        Response.Write("<div id='show2div' class='space'><div id='show2'></div></div>");
                     //   Response.Write("<div id='setypediv' class='space'><input id='setype' type='text' /></div>");
                    //    Response.Write("<div id='namediv' class='space'><input id='name' type='text' /></div>");
                       
                        Response.Write("<img src='pic/edit.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");
                        Response.Write("<map name='map' id='map'>");

                        Response.Write("<area id='backspace' class='input'  alt='' title=''  shape='rect' coords='86,192,204,272' />");
                        Response.Write("<area id='exitspace' class='input'  alt='' title=''  shape='rect' coords='926,472,1029,519' />");
                //        Response.Write("<area id='upspace' class='input'  alt='' title=''  shape='rect' coords='829,190,917,271' />");
                        Response.Write("<area id='show2space' class='input'  alt='' title=''  shape='rect' coords='245,4,800,434' />");
                //        Response.Write("<area id='setypespace' class='input'  alt='' title=''  shape='rect' coords='516,486,696,544' />");
                //        Response.Write("<area id='namespace' class='input'  alt='' title=''  shape='rect' coords='748,487,912,543' />");
                        
                        for (int i = 1; i < 5; i++) {
                     //       Response.Write("<area id='t" + i + "space' class='input'  alt='' title='' shape='rect' coords='245," + (4 + (i - 1) * 58) + ",800," + (4 +50+ (i - 1) * 58 ) + "' />");
                   //         Response.Write("<area id='spnAlt" + i + "space'  alt='' title='' href='register.aspx' shape='rect' coords='245," + (4 + 50 + (i - 1) * 58) + ",800," + (4 + 50+8 + (i - 1) * 58) + "' />");
                            
                   //         Response.Write("<area id='preview" + i + "space'  alt='' title='' href='register.aspx' shape='rect' coords='246," + (248 + (i - 1) * 47) + ",805," + (248 + (i ) * 47) + "' />");
                        
                        }

                        
                        Response.Write("</map>");


                        break;

                    case "delete":
                        GridView1.CssClass = "show";
                        GridView2.CssClass = "hide";

                        GridView1.PageSize = 10;
                        Response.Write("<div id='showdiv' class='space'><div id='show'></div></div>");
                        Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");
                        
                        Response.Write("<img src='pic/delete.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");
                        
                        Response.Write("<map name='map' id='map'>");
                        Response.Write("<area id='exitspace' class='input'  alt='' title=''  shape='rect' coords='489,604,698,697' />");
                        Response.Write("<area id='showspace' class='div'  alt='' title=''  shape='rect' coords='254,262,808,548' />");
                        Response.Write("</map>");

//                        SqlDataSource1.DeleteCommand=


                        break;
                    default:
                        GridView1.CssClass = "hide";
                        GridView2.CssClass = "hide";
                Response.Write("<div id='adddiv' class='space'><input id='add' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='editdiv' class='space'><input id='edit' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='deletediv' class='space'><input id='delete' type='button' onclick='enter(this.id)' /></div>");
                Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");

                Response.Write("<img src='pic/questions.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");
                Response.Write("<map name='map' id='map'>");
                Response.Write("<area id='addspace' class='input'  alt='' title=''  shape='rect' coords='503,611,690,681' />");
                Response.Write("<area id='editspace' class='input'  alt='' title=''  shape='rect' coords='774,614,955,696' />");
                Response.Write("<area id='deletespace' class='input'  alt='' title='' shape='rect' coords='273,590,437,668' />");
                Response.Write("<area id='exitspace'  class='input' alt='' title='' href='register.aspx' shape='rect' coords='893,470,1035,515' />");
                Response.Write("</map>");
                        break;
                
                }

                
            }
            else
            {
                Session["id"] = idBox.Text;
                Session["pw"] = pwBox.Text;
                Session["confirm"] = cBox.Text;
                if (Session["confirm"].ToString().Equals(""))
                {
                    Session["confirm"] = "false";
                }
                Session["iconBox"] = iconBox.Text;
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
        }
        protected void exit(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        { 
            string information="";
            if (Request.Form["nameBox"] != null)
            {
                 information += "N'" + Request.Params["nameBox"].ToString() + "'";
            }

            if (Request.Form["setypeBox"] != null)
            {
                information += ",N'" + Request.Params["setypeBox"].ToString() + "'";
            }
            if (Request.Form["explainBox"] != null)
            {
                information += ",N'" + Request.Params["explainBox"].ToString() + "'";
            }
            if (SQLChecker.checkInformation(Request.Form["nameBox"].ToString(),information)) { 
            for (int j = 1; j < 5;j++ )
            {
                if (Request.Form["t" + j + "Box"] != null && Request.Form["s" + j + "Box"] != null)
                {
                    string before = "";
                    string after = "";
                   
                    if (Request.Form["nameBox"] != null)
                    {
                        before+="name";
                        after+="N'"+Request.Params["nameBox"].ToString()+"'";
                        
                    }
                    
                     if (Request.Form["setypeBox"] != null)
                    {
                        before+=",id";
                        after+=",N'"+Request.Params["setypeBox"].ToString()+"'";
                       
                    }
                    
                    for (int i = 0; i < Request.Params["t" + j + "Box"].ToString().Length; i++)
                    {
                        before += ",w" + (i + 1);
                        after += ",N'" + Request.Params["t" + j + "Box"].ToString().Substring(i, 1) + Request.Params["s" + j + "Box"].ToString().Substring(i, 1) + "'";
                    }

                    
                    { 
                       
                        SQLChecker.insertQuestion(before,after);
                    
                    }

                    
                }

            }
            
            
            }
            
          
          }
        protected void Button4_Click(object sender, EventArgs e)
        {   string[] information=dataBox.Text.Split(new string[]{"data"},StringSplitOptions.None);
            string before = "";
            string after = "";
            if (Request.Form["nameBox"] != null)
            {
                 before += "name=N'" + Request.Params["nameBox"].ToString() + "'";
            }
            if (Request.Form["setypeBox"] != null)
            {
                before += ",id=N'" + Request.Params["setypeBox"].ToString() + "'";

            }
            after+="no="+information[0];
            SQLChecker.update("information",before,after);
            
            for (int j = 1; j < information.Length;j++ )
            {
                if (Request.Form["t" + j + "Box"] != null && Request.Form["s" + j + "Box"] != null)
                { 
                    if (Request.Form["nameBox"] != null)
                    {
                        before=("name="+"N'"+Request.Params["nameBox"].ToString()+"'");
                        
                    }
                    
                     if (Request.Form["setypeBox"] != null)
                    {
                        before += ",id=N'" + Request.Params["setypeBox"].ToString() + "'";
                       
                    }
                    
                    for (int i = 0; i < Request.Params["t" + j + "Box"].ToString().Length; i++)
                    {
                        before += ",w" + (i + 1)+"=N'" + Request.Params["t" + j + "Box"].ToString().Substring(i, 1) + Request.Params["s" + j + "Box"].ToString().Substring(i, 1) + "'";
                    }

                    after = "no=" + information[j];
                    SQLChecker.update("question", before, after);
                    

                    
                }

            }
            
            
            



            
            
           
        }

        protected void SqlDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            try
            {

                /*

                    mySqlCmd = new SqlCommand("SELECT name FROM question q,information i", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    mySqlCmd.ExecuteNonQuery();
                    mySqlCmd.Connection.Close();
               */
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }
        }

        protected void GridView1_PageIndexChanged(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] getData(string name) {
            string[] answer = SQLChecker.question(name);

            return answer;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow) {
                for (int t = 0; t < e.Row.Cells.Count;t++ )
                {
                    foreach (Control c in e.Row.Cells[t].Controls)
                    {
                        if (c is LinkButton)
                        {
                            LinkButton lbDel = c as LinkButton;
                            lbDel.OnClientClick = @"if (confirm('Are you sure?') == false) return false;";
                        }
                    }


                }
                
            
            }
        }
    }
}