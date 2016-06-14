using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SignalR
{
    public class wordHandle
    {
        private string[] first = { "non", "˙" };
        private string[] second = { "non", "ㄅ", "ㄆ", "ㄇ", "ㄈ", "ㄉ", "ㄊ", "ㄋ", "ㄏ", "ㄍ", "ㄎ", "ㄑ", "ㄔ", "ㄘ", "ㄒ", "ㄕ", "ㄙ", "ㄌ", "ㄖ", "ㄐ", "ㄓ", "ㄗ", };
        private string[] third = { "non", "ㄧ", "ㄨ", "ㄩ", };
        private string[] forth = { "non", "ㄚ", "ㄞ", "ㄢ", "ㄦ", "ㄛ", "ㄟ", "ㄣ", "ㄜ", "ㄠ", "ㄤ", "ㄝ", "ㄡ", "ㄥ", };
        private string[] fifth = { "non", "ˊ", "ˇ", "ˋ", };
        private string uncode = "";
        private Dictionary<string, string> json = null;
        private StreamReader stmRdr = null;
        string str4 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        public void init() {
            try {
                stmRdr = new StreamReader(str4 + "Scripts/bpmf_data.js", System.Text.Encoding.Default);

                int g = 0;
                string line = stmRdr.ReadLine();
                while (line != null)
                {
                    if (g == 7)
                    {
                        uncode = line;
                    }
                    else if (g == 2)
                    {
                        json = JsonConvert.DeserializeObject<Dictionary<string, string>>(line);
                    } ++g;
                    line = stmRdr.ReadLine();
                }
            
            
            }catch(Exception e){
                
            }finally{
             if(stmRdr!=null)  {
                stmRdr.Close();
            
                    }
            }
        }


        public string[] transfer(string word)//國字轉注音
        {
            string[] answer = new string[] { "non", "non", "non", "non", "non", };

            if (word.Equals(""))
            {
                return answer;

            }

            string ans = word.Replace("0", "").Replace("1", "").Replace("2", "").Replace("3", "");
            int index = 0;
            if (word.Contains("1"))
            {
                index = 1;
            }
            else if (word.Contains("2"))
            {
                index = 2;
            }
            else if (word.Contains("3"))
            {
                index = 3;
            }
            int fir = uncode.IndexOf(ans);
            int seco = uncode.IndexOf(ans, fir + 1);
            int thi = uncode.IndexOf(ans, seco + 1);
            int fort = uncode.IndexOf(ans, thi + 1);
            int[] query = new int[] { fir, seco, thi, fort };
            for (int i = 1; i < query.Length; i++)
            {
                if (query[i] == -1)
                {
                    query[i] = query[i - 1];
                }
            }

            int length = 7;

            string[] temp = check(query[index], length);
            while (temp.Length > 1)
            {
                --length;
                temp = check(query[index], length);
            }

            for (int i = 0; i < first.Length; i++)
            {

                if (json.ContainsKey(first[i]))
                {
                    if (temp[0].Contains(json[first[i]]))
                    {
                        answer[0] = first[i];
                    }


                }


            }
            for (int i = 0; i < second.Length; i++)
            {
                if (json.ContainsKey(second[i]))
                {
                    if (temp[0].Contains(json[second[i]]))
                    {
                        answer[1] = second[i];
                    }

                }


            }
            for (int i = 0; i < third.Length; i++)
            {
                if (json.ContainsKey(third[i]))
                {
                    if (temp[0].Contains(json[third[i]]))
                    {
                        answer[2] = third[i];
                    }


                }


            }
            for (int i = 0; i < forth.Length; i++)
            {
                if (json.ContainsKey(forth[i]))
                {
                    if (temp[0].Contains(json[forth[i]]))
                    {
                        answer[3] = forth[i];
                    }

                }


            }
            for (int i = 0; i < fifth.Length; i++)
            {
                if (json.ContainsKey(fifth[i]))
                {
                    if (temp[0].Contains(json[fifth[i]]))
                    {
                        answer[4] = fifth[i];
                    }


                }


            }
            return answer;

        }
        public string[] check(int start, int length)
        {

            string[] test = uncode.Substring(start - length, length).Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);
            return test;
        }



    }
}