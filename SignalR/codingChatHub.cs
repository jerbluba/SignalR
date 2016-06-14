using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Timers;

namespace SignalR
{
    public class codingChatHub : Hub
    {

        private SqlConnection dataConnection;
        private SqlCommand mySqlCmd;
        private string connString = ConfigurationManager.ConnectionStrings["gameMDF"].ConnectionString;
        private SqlDataReader reader;
        private Random x = new Random(Guid.NewGuid().GetHashCode());
        
        public static class UserHandler
        {
            public static Dictionary<string, string> ConnectedIds = new Dictionary<string, string>();
        }


        //Client. All給所有人 others給其他人 client(pw)給指定id的人 Caller給自己

        // 使用者連現時呼叫 name是playing中的no
        public void userConnected(string name)
        {
            try {
                // 進行編碼，防止 XSS 攻擊
                name = HttpUtility.HtmlEncode(name);
                string message = " 歡迎 ";
                string message2 = " 加入遊戲,等待對手中...</br>若不想等待，點擊任意處離開。 ";
                mySqlCmd = new SqlCommand("update playing SET pw='" + Context.ConnectionId + "' where no=" + name, new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                mySqlCmd.ExecuteNonQuery();
                mySqlCmd.Connection.Close();
              
                // 新增目前使用者至上線清單
                UserHandler.ConnectedIds.Remove(name);
                UserHandler.ConnectedIds.Add(Context.ConnectionId, name);//no key id value

                int id = Int32.Parse(name);
                int opID = (id + 1);//基數玩家
                //STEP 1 決定敵我id
                switch (id % 2)
                {
                    case 0://偶數玩家
                       opID-=2;
                        break;
                }

                bool online = true;
                //STEP 2 判斷敵人是否上線
                if (UserHandler.ConnectedIds.ContainsValue((opID).ToString()))//對手在線上
                {

                }
                else
                { //對手沒上線
                    online = false;
                }
                string testbug = "";
                foreach (string x in  UserHandler.ConnectedIds.Values )
                {
                    testbug += x+"</br>";

                }

                testbug += " online" + online;
            //    Clients.All.debug(testbug);


                //客戶端預設為點擊後訊息面板消失
               // Clients.Caller.touch(0);
                //STEP 3 檢查是否有對手
                mySqlCmd = new SqlCommand("SELECT * FROM playing where no = " + opID , new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();

                if (reader.HasRows)//有對手
                {
                    if (reader.Read())
                    {
                        //取得對手數值
                        
                        int opStill = reader.GetInt32(9);//遊戲狀態 0:等開始 1:轉輪盤 2:猜字 3:等待對手 4:遊戲結束
                        int opOrder = reader.GetInt32(10);//骰子大小(先後順序)
                        int opScore = reader.GetInt32(4);//得分
                        int opNo= reader.GetInt32(2);//玩家編號
                        string opPW = reader.GetString(3);//hub編號
                        
                        int opIcon = 0;//頭象
                        int opRank=0;//最高得分
                        string opName = "";//註冊名字
                        //撈出對手個資
                        mySqlCmd.Connection.Close();
                        mySqlCmd = new SqlCommand("SELECT * FROM users where no = " + opNo + "", new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        reader = mySqlCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.Read()) {
                                opIcon = reader.GetInt32(4);
                                opName += reader.GetString(1);
                                opRank=reader.GetInt32(5);
                            }
                        }
                        //為自己的client端帶入對手資料
                        Clients.Caller.setData(opID,"玩者"+opID+" "+opName+" 等級:"+opRank);
                        Clients.Caller.setIcon(opID,opIcon);
                        Clients.Caller.setScore(opID,opScore);
                        //STEP 4 遊戲狀況判斷
                        if (opStill <4)//遊戲未結束
                        {
                            mySqlCmd.Connection.Close();
                            //STEP 5 讀取自己資料
                            mySqlCmd = new SqlCommand("SELECT * FROM playing where no = '" + id + "' and still<4", new SqlConnection(connString));
                            mySqlCmd.CommandType = CommandType.Text;
                            mySqlCmd.Connection.Open();
                            reader = mySqlCmd.ExecuteReader();
                            if (reader.Read())
                            {   int yourLeader=reader.GetInt32(7);//此輪受益者
                                int yourRoom=reader.GetInt32(8);//此次猜題基本分數
                                int yourOrder = reader.GetInt32(10);
                                int yourStill = reader.GetInt32(9);
                                int yourScore = reader.GetInt32(4);
                                int yourNo = reader.GetInt32(2);
                                int yourIcon = 0;
                                int yourRank = 0;
                                string yourName = "";

                                mySqlCmd.Connection.Close();
                                mySqlCmd = new SqlCommand("SELECT * FROM users where no = " + yourNo + "", new SqlConnection(connString));
                                mySqlCmd.CommandType = CommandType.Text;
                                mySqlCmd.Connection.Open();
                                reader = mySqlCmd.ExecuteReader();

                                
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        yourIcon = reader.GetInt32(4);
                                        yourName += reader.GetString(1);
                                        yourRank = reader.GetInt32(5);
                                    }
                                }
                                //將自己的資料傳送給對手
                                Clients.Client(opPW).setData(name, yourName + " " + yourRank);
                                Clients.Client(opPW).setIcon(name, yourIcon);
                                Clients.Client(opPW).setScore(name, yourScore);

                                if (yourStill == 0)//你的遊戲沒開始case(0,x)
                                {
                                    message = "歡迎";
                                    message2 = " 進入遊戲。</br> 你的骰子大小為" + yourOrder + "</br> 對手的骰子大小為" + opOrder + (yourOrder > opOrder ? "你" : "對手") + "先手!!";
                                    Clients.Caller.dice(yourOrder, opOrder);//執行色子狀態
                                    Clients.Caller.touch(0);


                                    if (online)//對手在線上
                                    { //在線上 送觸發給對手
                                        Clients.Client(opPW).dice(opOrder, yourOrder);//傳送色子狀態
                                        string message3 = " 進入遊戲。</br> 你的骰子大小為" + opOrder + "</br> 對手的骰子大小為" + yourOrder + (yourOrder < opOrder ? "你" : "對手") + "先手!!";
                                        Clients.Client(opPW).message(message, message3);
                                        Clients.Client(opPW).touch(opStill);
                                    }


                                }
                                else//你的遊戲已經開始case(x,x)
                                {
                                    message = "已經重新連線 ";
                                    message2 = " </br>點擊任意處繼續。 ";

                                    switch(yourStill){
                                        case 1:
                                           message2 = " </br>輪到你轉盤，點擊任意處繼續。 ";
                                            break;
                                    
                                        case 2:
                                            message2 = " </br>輪到你猜字，得趕快!";
                                            break;
                                    
                                        case 3:
                                            message2 = " </br>現在是你對手的回合";
                                            break;
                                    }
                                    Clients.Caller.touch(yourStill);
                                     
                                    if (online)
                                    { //在線上 送觸發給對手
                                        string message3= " 你的對手已經重新連線。";
                                        Clients.Client(opPW + "").setEvent(message3);
                                        Clients.Client(opPW).sendTable();//命令對手寄出答題區目前狀況
                                    }
                                }
                               
                            } 
                            else
                            {//你的遊戲已經結束case(4,x)
                                gameType(opID, 4);
                                message = "很抱歉 ";
                                message2 = " 上一場遊戲中斷了...</br>點擊任意處離開。 ";
                                Clients.Caller.touch(4);
                                if (online)
                                { //在線上 送觸發給對手
                                    string message3="遊戲已經結束";
                                    Clients.Client(opPW + "").message(message, message3);
                                }
                            }
                        }
                        else
                        {//對手這場遊戲已經結束
                            gameType(id, 4);
                            message = "很抱歉 ";
                            message2 = " 上一場遊戲中斷了...</br>點擊任意處離開。 ";
                            Clients.Caller.touch(4);
                        }
                    }
                }
                Clients.Caller.message(message, message2);
            }catch(Exception e){
                Clients.All.setEvent(e.Message);
                Clients.All.setEvent(e.StackTrace);
            }
        }


        public void gameStart(int id) {
            if (SQLChecker.checkOrder(id))
            {

                gameType(id, 1);


            }
            else {
                gameType(id, 3);
            
            }
        
        }



        //轉盤相關事件
        public void setRotate(int id)
        { //產生輪盤參數
            string caller = "";
            string op = "";
            switch(getType(id)){
                case 3:
                    Clients.Caller.setEvent("還沒輪到你！");
                    break;
                case 2:
                    Clients.Caller.setEvent("你已經轉過了,趕快作答！");
                    break;
                case 1:

                    count = 15;
                int[] data = { 1, 2, 3 }; //命運 機會 分數
                int data2 = data[x.Next(data.Length )];//從上方三個隨機決定

                string text = "";

                int al = 0;//角度
                int score = 0;//此局分數
                int goodID=id;//受益者
                
                
                if (data2 == 1)
                {
                    int[] angle = { 338, 248 };
                    al = angle[x.Next(angle.Length )];
                    text = "fate";
                    caller+="玩者"+id+"骰到命運!";
                    op += "玩者" + id + "骰到命運!";
                }
                if (data2 == 2)
                {
                    int[] angle = {  68 ,158 };
                    al = angle[x.Next(angle.Length )];
                    text = "chance";
                    caller += "玩者" + id + "骰到機會!";
                    op += "玩者" + id + "骰到機會!";
                }
                if (data2 == 3)
                {
                    int[] angle = { 293,113, 23, 203 };
                    int[] scores = { 10, 50, 100, 500 };
                    var q = x.Next(angle.Length);
                    al = angle[q];
                    score = scores[q];
                    text = "+";
                    caller += "玩者" + id + "骰到"+score+"分!";
                    op += "玩者" + id + "骰到" + score + "分!";
                }
                switch(text){
                    case "chance":
                    case "fate":
                        int[] param=handleParams(text,50);
                        score = param[0];
                         caller += "...這次的分數將會是"+score;
                         op += "...這次的分數將會是" + score;
                        if (param[1] == -1) {
                            goodID = opID(id);
                            caller += "...哎呀，這次答對會是玩者" + goodID + "得分!";
                            op += "...哎呀，這次答對會是玩者" + goodID + "得分!";
                        
                        }
                        
                        if (param[1] == 2){
                            text ="hint";
                            caller += "...你獲得一個提示!" + getHint()[x.Next(getHint().Length)];
                            op += "...哎呀，你的對手獲得一個提示!";
                        
                        }
                        
                        break;
                }

                try
                {
                    mySqlCmd = new SqlCommand("update playing SET leader=" +goodID  + ",room=" + score + " where no=" + id + " and still<4", new SqlConnection(connString));
                    mySqlCmd.CommandType = CommandType.Text;
                    mySqlCmd.Connection.Open();
                    mySqlCmd.ExecuteNonQuery();
                    mySqlCmd.Connection.Close();
                }
                catch (Exception e)
                {
                    Clients.All.setEvent(e.Message);
                    Clients.All.setEvent(e.StackTrace);

                }
                finally {
                    if (mySqlCmd.Connection != null)
                        mySqlCmd.Connection.Close();
                }
                gameType(id, 2);
                gameType(opID(id), 3);

                Clients.Caller.rotateFunction(al,id);
                Clients.Client(getPW(opID(id))).rotateFunction(al,id);
                
                Clients.Client(getPW(opID(id))).setEvent(op);
                Clients.Caller.setEvent(caller);

                
                    break;
            }
        }


        public int[] handleParams(string type, int sc)//處理轉盤參數
        {
            int[] answer = new int[] {sc,0 };
            switch (type)
            {
                case "fate":
                    int fate = x.Next(3);
                    switch (fate)
                    {
                        case 0:
                            answer[1] = -1;
                            break;
                        case 1:
                            answer[0] = 0;
                            break;
                        case 2:
                            answer[0] = (0-answer[0]);
                            break;
                    }
                    break;
                case "chance":
                    int chance = x.Next(2);
                    switch (chance)
                    {
                        case 0:

                            /*
                            answer[1] = 2;// 遊戲提示
                            break;
                        case 1:
                             */
                            answer[0] *= 2;
                            break;
                        case 1:
                            answer[0] += 10;
                            break;

                    }
                    break;
            }
            return answer;

        }


        int count = 15;
        int tempID = 0;
        Timer timer = new Timer();
        public void startCount(int id) {
            tempID = id;
            
            timer.Interval = 1000;
            timer.Elapsed += x_Elapsed;
            timer.AutoReset = true;
            timer.Enabled=true;
        }

        void x_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (count > 0)
            {
                Clients.Client(getPW(tempID)).setEvent("玩者"+tempID+"還剩下" + count + "秒可作答");
                Clients.Client(getPW(opID(tempID))).setEvent("玩者" + tempID + "還剩下" + count + "秒可作答");
                --count;

            }
            else {
                
                timer.AutoReset = false;
                Clients.Client(getPW(tempID)).setEvent("時間到! 換玩者" + opID(tempID)+"轉輪盤!!");
                Clients.Client(getPW(opID(tempID))).setEvent("時間到! 換玩者" + opID(tempID) + "轉輪盤!!");
                Clients.Client(getPW(tempID)).hideKeyBoard();
                Clients.Client(getPW(opID(tempID))).hideKeyBoard();
                
                gameType(tempID, 3);
                gameType(opID(tempID), 1);
            }
            
        }


        public void keyBoard(int id) {
            if(getType(id)==2){
                Clients.Caller.showKeyBoard();
            }else{
                Clients.Caller.setEvent("你還不能作答");
            }
            
        }



     
        public void tryAnswer(string str, string id, string className, string name, int yourID)
        {//帶出某一格的注音


            bool right = SQLChecker.tryAnswer(str, id, className, name);

            if (right)
            {
                timer.AutoReset = false;

                Clients.Caller.setEvent("答對了");
                Clients.Client(getPW(opID(yourID))).setEvent("答對了");
                Clients.Caller.setAll(str, className);
                Clients.Client(getPW(opID(yourID))).setAll(str, className);
                gameType(yourID, 1);
                gameType(opID(yourID), 3);


            }
            else
            {
                timer.AutoReset = false;

                Clients.Caller.setEvent("答錯了");
                Clients.Client(getPW(opID(yourID))).setEvent("答錯了");
                Clients.Caller.setInput(id, "");
                gameType(yourID, 3);
                gameType(opID(yourID), 1);

            }
        }

        public void showAnswer(string name, string combine, int id, int x, int y)//抓出某一格的國字
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
            string answer = SQLChecker.showAnswer(name, x, y);
            if (combine.Contains(answer)&&!answer.Equals(""))
            {
                Clients.Caller.showAnswer(answer, x * 4, y * 6);
            }
                       
        }

        
        public void showAnswer(string str, string id, string className, string name, int index, int yourID)//把填入相同的空格，每填一格，受益者加一次分數
        {
            
                bool right = SQLChecker.tryAnswer(str,id,className,name);
                if (right)
                {
                    scoreManager(yourID);
                    Clients.Caller.setSingle(str, className, index);
                    Clients.Client(getPW(opID(yourID))).setSingle(str, className, index);
                }
        }





        public void scoreManager(int id)//處理分數
        {
            try
            {
                mySqlCmd = new SqlCommand("SELECT score,still,room,leader FROM playing where still<4 and no= '" + id + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                int score = 0;
                int goodID = 0;
                int room = 0;
                int still = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        score = reader.GetInt32(0);
                        still = reader.GetInt32(1);
                        room = reader.GetInt32(2);
                        goodID = reader.GetInt32(3);
                        if(still!=3){
                            score +=room;
                            if (score < 100) score = 100;
                            
                        }
                    }
                }
                mySqlCmd.Connection.Close();

                switch (goodID) { 
                    case 0:
                        goodID = id;
                        break;
                    case 1:
                        goodID = opID(id);
                        break;
                }

                mySqlCmd = new SqlCommand("update playing SET score=" +score + " where no=" + goodID + " and still<4", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                mySqlCmd.ExecuteNonQuery();
                mySqlCmd.Connection.Close();

                //更新該玩家最高分數
               
                
                mySqlCmd = new SqlCommand("SELECT score FROM playing where still<4 and no = " + goodID , new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();
                
                if (reader.HasRows)
                {
                    if(reader.Read())
                    {
                        score = reader.GetInt32(0);
                    
                    }
                }
                mySqlCmd.Connection.Close();

                
                Clients.Caller.setScore(goodID, score);
                Clients.Caller.setEvent("玩者" + id + "答對了,使" + goodID + "獲得" + room + "分，目前分數:" + score);

                Clients.Client(getPW(opID(id))).score(goodID, score);
                Clients.Client(getPW(opID(id))).setEvent("玩者" + id + "答對了,使" + goodID + "獲得" + room + "分，目前分數:" + score);
            }
            catch (Exception e)
            {
                Clients.All.setEvent(e.Message);
                Clients.All.setEvent(e.StackTrace);

            }
            finally
            {
                if (mySqlCmd.Connection != null)
                    mySqlCmd.Connection.Close();
            }

        }
        // 當使用者斷線時呼叫
        public override Task OnDisconnected()
        {
            // 當使用者離開時，移除在清單內的 ConnectionId
            //Clients.All.removeList(Context.ConnectionId);
            int id = Int32.Parse(UserHandler.ConnectedIds[Context.ConnectionId]);
            UserHandler.ConnectedIds.Remove(Context.ConnectionId);
            int bestGood = SQLChecker.getNoByPlay(id);
            SQLChecker.comparer(SQLChecker.getScore(id), bestGood);
            switch (id%2) { 
                case 0:
                    if (!UserHandler.ConnectedIds.ContainsValue((id-1).ToString()))
                    {
                        gameType(id,4);

                        gameType(id-1,4);
                    }
                    break;
                case 1:
                    if (!UserHandler.ConnectedIds.ContainsValue((id + 1).ToString()))
                    {
                        mySqlCmd = new SqlCommand("SELECT * FROM playing where still<4 and no = '" + (id+1) + "'", new SqlConnection(connString));
                        mySqlCmd.CommandType = CommandType.Text;
                        mySqlCmd.Connection.Open();
                        reader = mySqlCmd.ExecuteReader();
                        
                        if (reader.HasRows)
                        {
                           if (reader.Read())
                            {
                                gameType(id,4);

                                gameType(id+1,4);
                            }
                        }

                    }
                    break;
            
            
            }
            
            
            return base.OnDisconnected();
        }
       

        

       
        
        public void sendTable(int id,string message) {//傳送table
            Clients.Client(getPW(id)).setTable(message);
        
        }

        public string getPW(int id) {
            string answer = "";
            try
            {

                mySqlCmd = new SqlCommand("SELECT pw FROM playing where still<4 and no=" + id , new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        answer = reader.GetString(0);
                    }
                }
            }
            catch (Exception e)
            {
                Clients.All.setEvent(e.Message);
                Clients.All.setEvent(e.StackTrace);
            }
            finally
            {
                if (mySqlCmd.Connection != null)
                    mySqlCmd.Connection.Close();
            }
            return answer;
        }

        public int getType(int id)
        {
            int answer = 0;
            try
            {

                mySqlCmd = new SqlCommand("SELECT still FROM playing where still<4 and no='" + id + "'", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                reader = mySqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        answer = reader.GetInt32(0);
                    }
                }
            }
            catch (Exception e)
            {
                Clients.All.setEvent(e.Message);
                Clients.All.setEvent(e.StackTrace);
            }
            finally
            {
                if (mySqlCmd.Connection != null)
                    mySqlCmd.Connection.Close();
            }
            return answer;
        }

        public static int opID(int id) {
            if (id % 2 == 0) return id - 1;
            return id + 1;
        }
        public void setEvent(int id, string message)
        {//傳遊戲訊息給特定玩家
            Clients.Client(getPW(id)).setEvent(message);
        }

        private void gameType(int id, int gameType)
        {//設定某位玩家的遊戲狀態
            try
            {
                mySqlCmd = new SqlCommand("update playing SET still=" + gameType + " where no=" + id + " and still<4", new SqlConnection(connString));
                mySqlCmd.CommandType = CommandType.Text;
                mySqlCmd.Connection.Open();
                mySqlCmd.ExecuteNonQuery();
                mySqlCmd.Connection.Close();
            }
            catch (Exception e)
            {
                Clients.All.setEvent(e.Message);
                Clients.All.setEvent(e.StackTrace);

            }

        }

        public string[] getHint() {
            return new string[] { "提示A", "提示B", "提示C", "提示D", "提示E", "提示F", "提示G", "提示H", };
        }
    }
}