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
    public partial class createSQL : System.Web.UI.Page
    {
        private static data no = new data("no", new bool[] { true, true, true, true, true, true, true, true, }, "INTEGER IDENTITY(1,1) PRIMARY KEY", "序號", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data name = new data("name", new bool[] { true, true, true, !true, true, true, true, true, }, "NTEXT NOT NULL", "名稱", new String[] { "", "INTEGER NOT NULL", "", "", "", "", "NTEXT NOT NULL", "", });
        private static data id = new data("id", new bool[] { true, true, true, true, !true, true, true, true, }, "NTEXT NOT NULL", "帳號", new String[] { "", "INTEGER NOT NULL", "VARCHAR(50) NOT NULL", "", "", "", "INTEGER NOT NULL", "", });
        private static data pw = new data("pw", new bool[] { !true, true, true, !true, !true, true, true, !true, }, "NTEXT NOT NULL", "密碼", new String[] { "", "INTEGER NOT NULL", "", "", "", "", "VARCHAR(50) NOT NULL", "", });
        private static data icon = new data("icon", new bool[] { !true, !true, true, !true, !true, true, !true, !true, }, "INTEGER NOT NULL", "頭像", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data score = new data("score", new bool[] { !true, true, true, !true, !true, true, true, !true, }, "INTEGER NOT NULL", "最佳分數", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data ip = new data("ip", new bool[] { !true, true, !true, true, !true, !true, true, !true, }, "NTEXT NOT NULL", "位置", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data time = new data("time", new bool[] { !true, true, !true, true, true, true, true, !true, }, "DATETIME NOT NULL", "時間", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data show = new data("show", new bool[] { !true, !true, !true, true, !true, !true, !true, !true, }, "NTEXT NOT NULL", "顯示", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data leader = new data("leader", new bool[] { !true, true, !true, !true, !true, true, true, !true, }, "NTEXT NOT NULL", "管理員", new String[] { "", "INTEGER NOT NULL", "", "", "", "", "INTEGER NOT NULL", "", });
        private static data room = new data("room", new bool[] { !true, true, true, !true, !true, !true, true, !true, }, "NTEXT", "社團", new String[] { "", "INTEGER NOT NULL", "", "", "", "", "INTEGER NOT NULL", "", });
        private static data w1 = new data("w1", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字1", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data w2 = new data("w2", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字2", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data w3 = new data("w3", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字3", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data w4 = new data("w4", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字4", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data w5 = new data("w5", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字5", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data w6 = new data("w6", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字6", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data w7 = new data("w7", new bool[] { true, !true, !true, !true, !true, !true, !true, !true, }, "NTEXT", "字7", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data still = new data("still", new bool[] { !true, true, !true, !true, !true, !true, true , !true,}, "INTEGER NOT NULL", "進行", new String[] { "", "", "", "", "", "", "", "", });
        private static data orders = new data("orders", new bool[] { !true, true, !true, !true, !true, !true, true, !true, }, "INTEGER NOT NULL", "順序", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data unique = new data("CONSTRAINT player_Id", new bool[] { !true, !true, true, !true, !true, !true, !true, !true, }, "UNIQUE (no, id )", "唯一", new String[] { "", "", "", "", "", "", "", "", "", });
        private static data answer = new data("answer", new bool[] { !true, !true, !true, !true, !true, !true, !true, true, }, "NVARCHAR(MAX) NOT NULL", "註解", new String[] { "", "", "", "", "", "", "", "", "", });
        
        private String[] table = new String[] { "question", "roomgame", "users", "board", "announce", "room","playing" ,"information"};
        private data[] columns = new data[] { no, name, id, pw, icon, score, ip, time, show, leader, room, w1, w2, w3, w4, w5, w6, w7,still,orders,unique,answer};
        
        private static string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private static SqlDataReader reader;
        private static OleDbConnection oledbConn;
        private static OleDbDataAdapter oleda;
        private static OleDbCommand oleCmd;
        private static SqlConnection dataConnection;
        private static SqlCommand mySqlCmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            createDatabase();
        }

        private void createDatabase()
        {
            int debug = 0;
            int end = table.Length;
            
            try
            {

                for (int i = debug; i <end; i++)
                {
                    using (SqlCommand cmd = new SqlCommand(createTable(i), new SqlConnection(connString)))
                    {
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        Response.Write("成功:"+createTable(i));
                        Response.Write("<br>");
                    }
                }

            }
            catch (Exception e)
            {
                for (int i = debug; i < table.Length; i++)
                {
                    Response.Write(createTable(i));
                    Response.Write("<br>");
                }
                throw;
            }
            finally
            {
                /*
                 Response.Clear();
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.End();

             
                 */



            }
        }


        private String createTable(int i)
        {
            String answer = "CREATE TABLE " + table[i] + "(";//+data.KEY_ID.getDataName();
            for (int j = 0; j < columns.Length; j++)
            {
                if (columns[j].getTable()[i])
                {
                    answer += (columns[j].getDataName() + " " + columns[j].getDataType(i) + ",");
                }
            }
            return answer = (answer += ")").Replace(",)", ")");
        }

       

        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public sealed class data : Attribute
        {

            private String dataName;
            private String dataType;
            private String keyWord;
            private String[] myName = new String[7];
            private bool[] table = new bool[7];

            public data(string dataName, bool[] tables, string dataType, string keyWord, string[] filter)
            {
                this.dataName = dataName;
                this.dataType = dataType;
                this.keyWord = keyWord;
                myName = filter;
                table = tables;
            }

            public String[] getMyName()
            {
                return myName;
            }
            public String getKeyWord()
            {
                return keyWord;
            }
            public String getDataName()
            {
                return dataName;
            }

            public bool[] getTable()
            {
                return table;
            }


            public String getDataType() {
               
                return dataType;
            }
            public String getDataType(int table)
            {
                if (myName[table].Equals(""))
                {
                    return dataType;
                }
                else {

                    return myName[table];
                }
                
            }
            public String getEasyDataType()
            {
                if (this.dataType.Contains("INTEGER"))
                {
                    return "integer";
                }
                else if (this.dataType.Contains("TEXT") || this.dataType.Contains("BOOL"))
                {
                    return "ntext";
                }
                else if (this.dataType.Contains("DATETIME"))
                {
                    return "datetime";
                }
                else
                {
                    return "";
                }

            }


        }
    }
}