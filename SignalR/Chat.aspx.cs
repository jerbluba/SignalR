using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public partial class Chat : System.Web.UI.Page
    {

        System.Web.HttpContext context = System.Web.HttpContext.Current;
        private string[][] question = { new String[] { "白", "日", "依", "山", "盡" } };
        private string[][][] answer = {new String[][]{new String[]{ "non", "ㄅ", "non", "ㄞ", "ˊ", },  new String[]{ "non", "ㄖ", "non", "non", "ˋ", }  , new String[]{ "non", "non", "ㄧ", "non", "non", }    ,  new String[]{ "non", "ㄕ", "non", "ㄢ", "non", }  ,  new String[]{ "non", "ㄐ", "ㄧ", "ㄣ", "ˋ", } }};

        private SqlConnection dataConnection;
        private SqlCommand mySqlCmd;
        private SqlCommand mySqlCmd2;
        private string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private SqlDataReader reader;
        private SqlDataReader reader2;
        int rows = 4;
        int column = 6;
        private Random x = new Random(Guid.NewGuid().GetHashCode());

        //  1.取得和設置當前目錄（即該進程從中啟動的目錄）的完全限定路徑。
        string str0 = System.Environment.CurrentDirectory;
        //結果: C:\xxx\xxx

        //2.取得啟動了應用程序的可執行文件的路徑，不包括可執行文件的名稱。
        string str1 = System.Windows.Forms.Application.StartupPath;
        //結果: C:\xxx\xxx

        //3.取得應用程序的當前工作目錄。
        string str2 = System.IO.Directory.GetCurrentDirectory();
        //結果: C:\xxx\xxx

        //4.取得當前 Thread 的當前應用程序域的基目錄，它由程序集衝突解決程序用來探測程序集。
        string str3 = System.AppDomain.CurrentDomain.BaseDirectory;
        //結果: C:\xxx\xxx\

        //5.取得和設置包含該應用程序的目錄的名稱。
        string str4 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        //結果: C:\xxx\xxx\

        //6.取得啟動了應用程序的可執行文件的路徑，包括可執行文件的名稱。
        string str5 = System.Windows.Forms.Application.ExecutablePath;
        //結果: C:\xxx\xxx\xxx.exe

        //7.取得當前執行的exe的文件名。
        string str6 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        //結果: C:\xxx\xxx\xxx.exe

        
        
        private string yourname = "";
        private int yourRank= 0;
        private int yourscore = 100;
        private int youricon = 0;
        private string   questionname = "";
        private string hello="Hello 你好啊";

        private wordHandle wh = new wordHandle();
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

            if (Request.Form["noBox"] != null)
            {
                Session["no"] = Request.Params["noBox"].ToString();
                noBox.Text = Session["no"].ToString();

            }
            if (context.Session["no"] != null)
            {
                noBox.Text = Session["no"].ToString();
            }

            if (Request.Form["stillBox"] != null)
            {
                Session["still"] = Request.Params["stillBox"].ToString();
                stillBox.Text = Session["still"].ToString();

            }
            if (context.Session["still"] != null)
            {
                stillBox.Text = Session["still"].ToString();
            }


            plugin.chat(this.Page);


            if (context.Session["id"] != null && context.Session["pw"] != null)
            {

                if (SQLChecker.checkID(Session["id"].ToString(), Session["pw"].ToString()))
                {

                    loadQuestion();
                    
                    gridViewSet.setGridView(GridView1,question,answer);

                    try {
                        Response.Write("<div id='answerdiv' class='space'><div id='answer'><table id='answertable' style='background-color:#fff;'></table></div></div>");

                        Response.Write("<div id='" + Session["no2"].ToString() + "picdiv' class='space'><div id='" + Session["no2"].ToString() + "icon' class='icon'></div></div>");
                        Response.Write("<div id='" + Session["no2"].ToString() + "datadiv' class='space'><label id='" + Session["no2"].ToString() + "data'></label></div>");
                        Response.Write("<div id='" + Session["no2"].ToString() + "scorediv' class='space'><input id='" + Session["no2"].ToString() + "score'  readonly></div>");
                        Response.Write("<div id='picdiv' class='space'><div id='icon'></div></div>");
                        Response.Write("<div id='datadiv' class='space'><label id='data'>玩者" + Session["no"].ToString() + yourname + "  等級:" + yourRank + "</label></div>");
                        Response.Write("<div id='scorediv' class='space'><input id='score'  readonly value=" + (yourscore==0?"載入中":yourscore+"") + "></div>");
                        Response.Write("<div id='questiondiv' class='space'><label id='question'>" + questionname + "</label></div>");
                        Response.Write("<div id='backdiv' class='space'><input id='back' name='back' type='button' /></div>");
                        
                        Response.Write("<div id='wheeldiv' class='space'><div class='ly-plate'><div class='rotate-bg'></div><div class='lottery-star'><img src='pic/rotate-static.gif' id='lotteryBtn'></div></div></div>");
                        Response.Write("<div id='eventdiv' class='space'><div id='event'>" + hello + "</div></div>");
                        Response.Write("<div id='keyboarddiv' class='space'><div id='keyboard'></div></div>");
                        Response.Write("<div id='exitdiv' class='space'><input id='exit'  type='button' /></div>");
                        
                        Response.Write("<img src='pic/game.JPG?v="+gridViewSet.version+"' alt='' usemap='#Map' id='mappic' />");

                        Response.Write("<map name='map' id='map'>");
                        Response.Write("<area id='wheelspace' alt='' title=''  shape='rect' coords='431,12,604,141' />");
                        Response.Write("<area id='eventspace' alt='' title='' class='div'  shape='rect' coords='312,162,708,249' />");
                        Response.Write("<area id='keyboardspace' alt='' title=''  shape='rect' coords='732,277,1013,504' />");

                        Response.Write(" <area id='backspace' class='input' alt='' title='' shape='rect' coords='752,624,909,696' />");
                      //  Response.Write(" <area id='checkspace' class='input' alt='' title='' shape='rect' coords='861,512,1064,620' />");

                        Response.Write("<area id='questionspace' alt='' title='' href='' shape='rect' coords='162,536,356,597' />");
                        Response.Write("<area id='picspace' alt='' title='' href='' shape='rect' coords='74,73,208,167' />");
                        Response.Write("<area id='dataspace' alt='' title='' shape='rect' coords='62,4,205,40' />");
                        Response.Write("<area id='scorespace' class='input' alt='' title='' href='' shape='rect' coords='77,15,211,60' />");
                        Response.Write("<area id='" + Session["no2"].ToString() + "picspace' alt='' title='' shape='rect' coords='800,87,940,162' />");
                        Response.Write("<area id='" + Session["no2"].ToString() + "dataspace' alt='' title='' shape='rect' coords='813,6,996,47' />");
                        Response.Write("<area id='" + Session["no2"].ToString() + "scorespace' class='input'  alt='' title=''  shape='rect' coords='808,34,945,77' />");
                        Response.Write("<area id='answerspace' class='div' alt='' title=''  shape='rect' coords='115,265,712,514' />");
                        Response.Write("<area id='exitspace' class='input' alt='' title=''  shape='rect' coords='752,624,909,696' />");

                        Response.Write("</map>");
                    
                    
                    
                    
                    
                    }catch(Exception ex){
                        Response.Write(ex.StackTrace);
                        Response.Write(ex.HelpLink);
                        Response.Write(ex.Message);
                        Response.Write(ex.Data);
                    }
                    
                  


                }
                else {

                    Response.Redirect("Default.aspx");
                 
                }    
            }
            else {
                Response.Redirect("Default.aspx");
               
            }
        }


        //登入playing load question create answers
        protected void loadQuestion()
        {
            try
            {
                {
                    //STEP1取得你的會員編號
                    mySqlCmd2 = new SqlCommand("SELECT no,name,icon,score FROM users where CONVERT(NVARCHAR(MAX), id) = N'" + Session["id"].ToString() + "'", new SqlConnection(connString));
                    mySqlCmd2.CommandType = CommandType.Text;
                    mySqlCmd2.Connection.Open();
                    reader2 = mySqlCmd2.ExecuteReader();
                    int yourno = 0;


                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            yourno = reader2.GetInt32(0);
                            yourname = reader2.GetString(1);
                            youricon = reader2.GetInt32(2);
                            yourRank = reader2.GetInt32(3);
                            iconBox.Text = youricon + "";
                        }
                    }
                    bigDebug("你的編號:" + yourno);
                    // 檢查你是否還有在進行的遊戲
                    mySqlCmd = new SqlCommand("SELECT * FROM playing A1,users A2 where A1.id =" + yourno + " and A1.still<4", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();


                    //STEP2讀取題目
                    if (reader.HasRows)
                    {
                        bigDebug("你有遊戲");

                        //                        mySqlCmd2 = new SqlCommand("SELECT A1.no no,A1.name name,A1.id id,A1.w1 w1,A1.w2 w2,A1.w3 w3,A1.w4 w4,A1.w5 w5,A1.w6 w6,A1.w7 w7  FROM question A1,playing A2 where A1.no=A2.name", new SqlConnection(connString));
                        //將題目的每一句都讀取出來

                        while (reader.Read())
                        {
                            yourscore = reader.GetInt32(4);
                            questionname = reader.GetString(1);
                            mySqlCmd2 = new SqlCommand("SELECT *  FROM question  where CONVERT(NVARCHAR(MAX), name) = N'" + questionname + "'", new SqlConnection(connString));
                        }

                    }
                    else
                    {
                        bigDebug("你沒有的遊戲");
                        //隨機選擇一個新的題目
                        mySqlCmd2 = new SqlCommand("SELECT top 1 name FROM question order by NEWID() ", new SqlConnection(connString));
                        mySqlCmd2.CommandType = CommandType.Text;
                        mySqlCmd2.Connection.Open();
                        reader2 = mySqlCmd2.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                questionname = reader2.GetString(0);
                            }
                        }

                        mySqlCmd2.Connection.Close();
                        //最後選出這個題目的所有句子
                        mySqlCmd2 = new SqlCommand("SELECT *  FROM question  where CONVERT(NVARCHAR(MAX), name) = N'" + questionname + "'", new SqlConnection(connString));

                        //STEP3創建遊戲
                        bigDebug("創建遊戲");
                        //因為不存在遊戲，在playing中新增一筆在遊戲的資料
                        dataConnection = new SqlConnection(connString);

                        //SQL INSERT 語法INSERT INTO  [table] ([欄位名1],[欄位名2],[欄位名3],....) VALUES (@值1, @值2, @值3,...)
                        string strMsgInsert = @"INSERT INTO playing 
                        ([name],[id],[pw],[score],[ip],[time],[leader],[room],[still],[orders]) 
                        VALUES(@value1, @value2, @value3,@value4, @value5, @value6, @value7, @value8, @value9, @value10) ";
                        mySqlCmd = new SqlCommand(strMsgInsert, dataConnection);
                        mySqlCmd.CommandType = CommandType.Text;
                        //連接資料庫
                        dataConnection.Open();
                        int value = 0;
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "" + questionname + "");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "" + yourno + "");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "0");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "100");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "0.0.0.0");
                        DateTime myDateTime = DateTime.Now;
                        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), sqlFormattedDate);
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "0");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "0");
                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "0");

                        mySqlCmd.Parameters.AddWithValue("@value" + (++value), "0" + (1 + x.Next(6)));
                        //執行SQL INSERT 語法
                        mySqlCmd.ExecuteNonQuery();
                        dataConnection.Close();
                        //關閉資料庫

                    }


                    //呼叫你所有未結束的遊戲  
                    dataConnection = new SqlConnection(connString);
                    string search = "select no,still,orders from playing where still<4 and id=" + yourno;
                    mySqlCmd = new SqlCommand(search, dataConnection);
                    mySqlCmd.CommandType = CommandType.Text;
                    dataConnection.Open();
                    SqlDataReader geter;
                    geter = mySqlCmd.ExecuteReader();
                    int no2 = 0;
                    if (geter.HasRows)
                    {
                        if (geter.Read())
                        {
                            no2 = geter.GetInt32(0);
                            if (no2 % 2 == 1)//奇數  直接以questionname當作name不需更新 
                            {
                                Session["no"] = no2;
                                Session["no2"] = (no2 + 1);
                                noBox.Text = no2.ToString();
                                Session["still"] = geter.GetInt32(1);
                                stillBox.Text = Session["still"].ToString();
                                dataConnection.Close();
                            }
                            else
                            {//偶數 抄奇數題 用跟奇數不同骰

                                Session["no"] = no2;
                                Session["no2"] = (no2 - 1);

                                noBox.Text = no2.ToString();
                                Session["still"] = geter.GetInt32(1);
                                stillBox.Text = Session["still"].ToString();

                                if (geter.GetInt32(1) == 0)
                                {// 遊戲尚未開始
                                    //更新骰子
                                    int yourOrder = geter.GetInt32(2);
                                    dataConnection.Close();

                                    dataConnection = new SqlConnection(connString);
                                    search = "select orders,name from playing where still<4 and no=" + (no2 - 1);
                                    mySqlCmd = new SqlCommand(search, dataConnection);
                                    mySqlCmd.CommandType = CommandType.Text;
                                    dataConnection.Open();
                                    geter = mySqlCmd.ExecuteReader();
                                    if (geter.HasRows)
                                    {
                                        if (geter.Read())
                                        {
                                            int opOrder = geter.GetInt32(0);
                                            while (yourOrder == opOrder)
                                            {
                                                yourOrder = (1 + x.Next(6));
                                            }
                                            int leader = 1;
                                            questionname = geter.GetString(1);
                                            dataConnection.Close();

                                            if (yourOrder < opOrder)
                                            {
                                                leader = 0;
                                                string update2 = "update playing set name=N'" + questionname + "' ,leader=1 where no = " + Session["no2"].ToString() + "";
                                                mySqlCmd = new SqlCommand(update2, dataConnection);
                                                dataConnection.ConnectionString = connString;
                                                dataConnection.Open();
                                                mySqlCmd.ExecuteNonQuery();
                                                dataConnection.Close();

                                            }
                                            string update = "update playing set name=N'" + questionname + "' ,orders=" + yourOrder + ",leader=" + leader + " where no = " + Session["no"].ToString() + "";
                                            mySqlCmd = new SqlCommand(update, dataConnection);
                                            dataConnection.ConnectionString = connString;
                                            dataConnection.Open();
                                            mySqlCmd.ExecuteNonQuery();

                                        }
                                    }
                                }
                                dataConnection.Close();
                            }
                        }
                    }
                    else
                    {

                        // throw exception 沒有你遊戲的錯誤
                    }


                    question = SQLChecker.getQuestion(questionname);

                    try
                    {
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


                    }
                    catch (Exception e)
                    {



                    }

                    mySqlCmd.Connection.Close();
                }




            }
            catch (OleDbException ex)
            {
                Response.Write(ex.Message);
                Response.Write(ex.ErrorCode);
                Response.Write(ex.Errors);
                Response.Write(ex.Data);
                Response.Write(ex.StackTrace);
                Response.Write(ex.Source);



            }
            catch (DataException ex2)
            {
                Response.Write(ex2.Message);
                Response.Write(ex2.Data);
                Response.Write(ex2.StackTrace);
                Response.Write(ex2.Source);



            }
            catch (System.InvalidOperationException ex3)
            {
                Response.Write(ex3.Message);
                Response.Write(ex3.Data);
                Response.Write(ex3.StackTrace);
                Response.Write(ex3.Source);

            }
            finally
            {

                mySqlCmd.Cancel();
                mySqlCmd2.Cancel();

                if (dataConnection != null)
                {
                    dataConnection.Close();
                    dataConnection.Dispose();
                }
                /*
                if(stmRdr!=null)  {
                stmRdr.Close();
            
                    }
                 * */
            }
        }
       
      
        public void bigDebug(string message) {
         //   Response .Write(message+"</br>");
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}