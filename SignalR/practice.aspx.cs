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
    public partial class practice : System.Web.UI.Page
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        private string[][] question = { new String[] { "白", "日", "依", "山", "盡" } };
        private string[][][] answer = { new String[][] { new String[] { "non", "ㄅ", "non", "ㄞ", "ˊ", }, new String[] { "non", "ㄖ", "non", "non", "ˋ", }, new String[] { "non", "non", "ㄧ", "non", "non", }, new String[] { "non", "ㄕ", "non", "ㄢ", "non", }, new String[] { "non", "ㄐ", "ㄧ", "ㄣ", "ˋ", } } };
        
        private SqlCommand mySqlCmd;
        private string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private SqlDataReader reader;
        string questionname = "";
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
                plugin.practice(this.Page);

                if (SQLChecker.checkID(Session["id"].ToString(), Session["pw"].ToString()))
                {
                    
                    loadQuestion();


                    gridViewSet.setGridView(GridView1, question, answer);

                    try
                    {

                        Response.Write("<div id='exitdiv' class='space'><input id='exit' type='button' onclick='exit()' /></div>");
                        Response.Write("<div id='backdiv' class='space'><input id='back' type='button' onclick='enter()' /></div>");
                        Response.Write("<div id='nextdiv' class='space'><input id='next' type='button' onclick='enter()' /></div>");
                        Response.Write("<div id='namediv' class='space'><input id='name' type='button' value='" + Session["id"].ToString() + "' onclick='edit(this.id)' /></div>");
                        Response.Write("<div id='showdiv' class='space'><div id='show' type='button' ></div></div>");
                        Response.Write("<div id='questiondiv' class='space'><div id='question' type='button' >"+questionname+"</div></div>");
                        Response.Write("<div id='keyboarddiv' class='space'><div id='keyboard'></div></div>");
                        
                        Response.Write("<div id='picdiv' class='space'><div id='icon'></div></div>");
                        

                        Response.Write("<img src='pic/practice.JPG' alt='' usemap='#Map' id='mappic' />");

                        Response.Write("<map name='map' id='map'>");

                        Response.Write("<area id='exitspace' class='input' alt='' title=''  shape='rect' coords='774,660,937,760' />");
                        Response.Write("<area id='backspace' class='input' alt='' title=''  shape='rect' coords='117,95,361,168' />");
                        Response.Write("<area id='nextspace' class='input' alt='' title=''  shape='rect' coords='666,136,922,200' />");
                        Response.Write("<area id='picspace' class='input' alt='' title=''  shape='rect' coords='16,181,141,285' />");
                        Response.Write("<area id='namespace' class='input' alt='' title=''  shape='rect' coords='4,316,196,396' />");
                        Response.Write("<area id='showspace' class='div' alt='' title=''  shape='rect' coords='273,227,918,533' />");
                        Response.Write("<area id='questionspace' class='div' alt='' title=''  shape='rect' coords='5,557,224,621' />");
                        Response.Write("<area id='keyboardspace' alt='' title=''  shape='rect' coords='245,559,766,763' />");

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
            else
            {
                Response.Redirect("Default.aspx");

            }

        }


        protected void loadQuestion()
        {
            try
            {

                mySqlCmd = new SqlCommand("SELECT top 1 name FROM question order by NEWID() ", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        questionname = reader.GetString(0);
                        question = SQLChecker.getQuestion(questionname);
                    }
                }
                wordHandle wh=new wordHandle();
                wh.init();
                answer = new string[question.Length][][];
                for (int i = 0; i < answer.Length; i++)
                {
                    answer[i] = new string[question[i].Length][];

                    for (int j = 0; j < question[i].Length; j++)
                    {

                        answer[i][j] = wh.transfer(question[i][j]);

                    }
                }

                mySqlCmd.Connection.Close();
            }
            catch (Exception ex3)
            {
                Response.Write(ex3.Message);
                Response.Write(ex3.Data);
                Response.Write(ex3.StackTrace);
                Response.Write(ex3.Source);

            }
            finally
            {
                if (mySqlCmd != null)
                {
                    mySqlCmd.Dispose();

                }
                
                
              
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



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static String tryAnswer(string str,string id,string className,string name )
        {

           return SQLChecker.tryAnswer(str,id,className,name)+"";
            
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static String showAnswer(string name,  int x,int y)
        {
            switch (y % 2)
            {
                case 0:
                    y -= 4;
                    break;
                case 1:
                    y -= 5;
                    break;
            }
            y /= 6;
            return SQLChecker.showAnswer(name,x,y);

        }

    }
}