using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SignalR
{
    public class SQLChecker
    {
        private static string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private static SqlDataReader reader;
        private static SqlCommand mySqlCmd;

        public static bool checkID(string id, string pw)
        {

            bool answer = false;
            try
            {

                {
                    mySqlCmd = new SqlCommand("SELECT id,pw FROM users where CONVERT(NVARCHAR(MAX), id) = N'" + id + "' and CONVERT(NVARCHAR(MAX), pw) = N'" + pw + "'", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            answer = true;
                        }

                    }
                    else
                    {
                        answer = false;
                    }
                    mySqlCmd.Connection.Close();
                }
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }
            return answer;
        }

        public static int getScore(int no) {
            int answer = 0;

            try
            {

                {
                    mySqlCmd = new SqlCommand("SELECT score FROM playing where no=" + no, new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            answer = reader.GetInt32(0);
                        }

                    }
                    mySqlCmd.Connection.Close();
                }
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }


            return answer;
        
        }

        public static void comparer(int score,int no){
            try
            {

                {
                    mySqlCmd = new SqlCommand("SELECT score FROM users where no=" + no, new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            if (score > reader.GetInt32(0)) {
                                mySqlCmd.Connection.Close();
                                mySqlCmd = new SqlCommand("UPDATE users SET score=" + score+" where no="+no, new SqlConnection(connString));
                                mySqlCmd.CommandType = CommandType.Text;
                                mySqlCmd.Connection.Open();
                                mySqlCmd.ExecuteNonQuery();
                            
                            
                            }           
                        }

                    }
                    mySqlCmd.Connection.Close();
                }
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }
        
        }

        public static int getNoByPlay(int no)
        {
            int answer = -1;
            try
            {
                    mySqlCmd = new SqlCommand("SELECT id FROM playing where no=" + no, new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    int no2 = 0;
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            answer= reader.GetInt32(0);
                        }

                    }
                    mySqlCmd.Connection.Close();
            }
            finally
            {
                if (mySqlCmd != null)
                    mySqlCmd.Dispose();
            }
            return answer;

        }

        public static void insertQuestion(string before,string after) {



                try
                {
                    {
                        mySqlCmd = new SqlCommand("INSERT INTO question (" + before + ") VALUES(" + after + ")", new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        mySqlCmd.ExecuteNonQuery();
                       

                      
                    }
                }
                finally
                {

                    if (mySqlCmd != null)
                    {
                        if (mySqlCmd.Connection != null)
                        {
                            mySqlCmd.Connection.Close();
                        }
                        mySqlCmd.Dispose();
                    }

                }

            
            
        
        }

        public static Boolean checkInformation(string name, string information)
        {
            try
            {
                
                    mySqlCmd = new SqlCommand("SELECT * FROM information where CONVERT(NVARCHAR(MAX), name)=N'" + name + "'", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    reader = mySqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                      

                        return false;

                    }
                    else {

                        mySqlCmd.Connection.Close();
                        mySqlCmd = new SqlCommand("INSERT INTO information (name,id,answer) VALUES(" + information + ")", new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        mySqlCmd.ExecuteNonQuery();
                        return true;
                    }

                
            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }
        
        }

        public static string randomName(string id) {
            string answer ="";
            try
            {

                mySqlCmd = new SqlCommand("SELECT top 1 name FROM question where CONVERT(NVARCHAR(MAX), id) = N'" + id + "' order by NEWID()", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {

                        answer = reader.GetString(0);
                    }

                }
            }
            catch (Exception e) {

                answer = e.Message;
            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }

            return answer;
        
        }

        public static string[][] getQuestion(string name) {
            string[][] answer=new string[4][];
            try
            {

                mySqlCmd = new SqlCommand("SELECT w1,w2,w3,w4,w5,w6,w7 FROM question where CONVERT(NVARCHAR(MAX), name)=N'" + name + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read()) {

                        answer[i] = new string[] { "", "", "", "", "", "", "", };
                       for(int j=0;j<answer[i].Length;j++){

                           if (!reader.IsDBNull(j)) {
                               answer[i][j] = reader.GetString(j).Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "");
                           }
                       
                       }
                       ++i;
                    }

                }
                while(i<4){
                    answer[i] = new string[] { "", "", "", "", "", "", "", };
                    ++i;
                
                }

            }
            catch (Exception e)
            {

                answer = new string[][] { new string[] { e.Message } };
            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }

            return answer;
        }

        public static string[][] getExplain(string name)
        {
            string[][] answer = new string[1][];
            try
            {

                mySqlCmd = new SqlCommand("SELECT answer FROM information where CONVERT(NVARCHAR(MAX), name)=N'" + name + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
             
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        answer[0]=new string[]{reader.GetString(0)};
                    }
                }
            }
            catch (Exception e)
            {

                answer = new string[][] { new string[] { e.Message } };
            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }

            return answer;
        }

        public static Boolean checkOrder(int id) {
            Boolean answer = false;
            try
            {

                mySqlCmd = new SqlCommand("SELECT A1.orders your,A2.orders op FROM playing A1,playing A2  where A1.no=" +id + " and A2.no="+codingChatHub.opID(id)+" and A1.still<4 and A2.still<4", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        if (reader.GetInt32(0) > reader.GetInt32(1)) answer = true;
                    }
                }
            }
            catch (Exception e)
            {

                
            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }

            return answer;
        
        }

        public static Boolean admin(string id,string pw) {
            Boolean answer = false;
            try
            {

                mySqlCmd = new SqlCommand("SELECT room FROM users where CONVERT(NVARCHAR(MAX), id) = N'" + id + "' and CONVERT(NVARCHAR(MAX), pw) = N'" + pw + "' and CONVERT(NVARCHAR(MAX), room) = N'admin'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        answer = true;
                    }
                }
            }
            catch (Exception e)
            {


            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }

            return answer;
        
        }


        public static void update(string table, string before, string after) {
            try
            {

                mySqlCmd = new SqlCommand("UPDATE "+table+" SET "+before+" WHERE "+after, new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                mySqlCmd.ExecuteNonQuery();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                  
                    }
                }
            }
            catch (Exception e)
            {


            }
            finally
            {

                if (mySqlCmd != null)
                {
                    if (mySqlCmd.Connection != null)
                    {
                        mySqlCmd.Connection.Close();
                    }
                    mySqlCmd.Dispose();
                }

            }
        
        }




        public static Boolean tryAnswer(string str, string id, string className, string name)//帶出某一格的注音
        {
            Boolean forp = false;
            try
            {
                wordHandle wh = new wordHandle();
                wh.init();
                string[] rowArray = id.Replace("GridView1_", "").Split(new string[] { "plus" }, StringSplitOptions.None);
                string y = rowArray[1].Split(new string[] { "_" }, StringSplitOptions.None)[0];
                int l = Int32.Parse(rowArray[0]) / 4;
                int w = Int32.Parse(y) / 6;
                mySqlCmd = new SqlCommand("SELECT w" + (w + 1) + " FROM question where CONVERT(NVARCHAR(MAX), name) = N'" + name + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                int ny = 0;//行數
                bool right = false;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (l == ny)
                        {
                            string[] answer =
                            wh.transfer(reader.GetString(0));
                            switch (className)
                            {
                                case "first":
                                    if (str.Equals(answer[0]))
                                    {
                                        right = true;

                                    }

                                    break;
                                case "second":
                                    if (str.Equals(answer[1]))
                                    {
                                        right = true;

                                    }

                                    break;
                                case "third":
                                    if (str.Equals(answer[2]))
                                    {
                                        right = true;

                                    }

                                    break;
                                case "forth":
                                    if (str.Equals(answer[3]))
                                    {
                                        right = true;

                                    }

                                    break;
                                case "fifth":
                                    if (str.Equals(answer[4]))
                                    {
                                        right = true;

                                    }

                                    break;

                            }

                        }
                        ++ny;


                    }
                }

                mySqlCmd.Connection.Close();
                forp = right;

            }
            catch (Exception e)
            {
            }
            finally
            {
                if (mySqlCmd.Connection != null)
                    mySqlCmd.Connection.Close();
            }

            return forp;
        }

        public static string showAnswer(string name,  int x, int y)//抓出某一格的國字
        {
            string answer = "";
            try
            {
                mySqlCmd = new SqlCommand("SELECT * FROM question where CONVERT(NVARCHAR(MAX), name) = N'" + name + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int temp = 0;
                    while (reader.Read())
                    {
                        if (temp == x)
                        {
                            
                            answer = reader.GetString(y + 3).Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "");
                         
                        }
                        ++temp;
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (mySqlCmd.Connection != null)
                    mySqlCmd.Connection.Close();
            }
            return answer;
        }

        public static string[] question(string name)//抓出某題的全部製作成
        {
            string[] answer = new string[8];
            try
            {
                mySqlCmd = new SqlCommand("SELECT w1,w2,w3,w4,w5,w6,w7 FROM question where CONVERT(NVARCHAR(MAX), name) = N'" + name + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int temp = 0;
                    while (reader.Read())
                    {
                        string temp2="";
                        string temp3 = "";

                        for (int i = 0; i < 7;i++ ) {
                            temp2+=reader.GetString(i);
                            temp3 += reader.GetString(i).Substring(1,1);
                        }

                        answer[temp] = temp2.Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "");
                        answer[temp+4] = temp3;

                        ++temp;
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (mySqlCmd.Connection != null)
                    mySqlCmd.Connection.Close();
            }
            return answer;
        }
      
      

    }
}