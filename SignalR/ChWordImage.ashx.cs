using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace SignalR
{
    /// <summary>
    /// ChWordImage 的摘要描述
    /// </summary>
    public class ChWordImage : IHttpHandler
    {
        static string[] PhonFonts = new string[] 
    {
      "王漢宗中楷體注音", "王漢宗中楷體破音一",
      "王漢宗中楷體破音二", "王漢宗中楷體破音三"
    };
        public void ProcessRequest(HttpContext context)
        {
            //忽略參數檢查
            int w = int.Parse(context.Request["w"] ?? "256");
            int h = int.Parse(context.Request["h"] ?? "64");
            float fs = float.Parse(context.Request["fs"] ?? "20");
            Color bc = ColorTranslator.FromHtml("0x" + (context.Request["bc"] ?? "dddddd"));
            Color fc = ColorTranslator.FromHtml("0x" + (context.Request["fc"] ?? "000000"));
            string txt = context.Request["t"] ?? "黑暗執行緒";
            //允許不同的字指定破音字，如四個字第三個字要用破音字一 af=0010
            string af = context.Request["af"] ?? new string('0', txt.Length);

            //建立畫布
            Bitmap bmp = new Bitmap(w, h);
            //取得字數，測量並計算寬度，決定置中用的位移
            Graphics g = Graphics.FromImage(bmp);
            //塗上背景色
            Brush p = new SolidBrush(bc);
            g.FillRectangle(p, 0, 0, bmp.Width, bmp.Height);
            //使用"王漢宗中楷體"
            Font fnt = new Font(PhonFonts[0], fs);
            var sz = g.MeasureString(txt, fnt);
            //設定文字反鋸齒
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            //取得每個字的寬度
            float widthPerChar = sz.Width / txt.Length;
            //計算置中用的位移     
            float offsetX = (bmp.Width - sz.Width) / 2;
            float offsetY = (bmp.Height - sz.Height) / 2;
            
            //考量破音字要換字型，每個字元可用不同字型
            //用迴圈一次畫一個字元
            for (int i = 0; i < txt.Length; i++)
            {
                //查第i個字元的破音字指定
                int fntIdx = (byte)af[i] - 0x30;
                //以前景色寫上文件
                string answer=txt[i].ToString();
             Font myFont = new Font(PhonFonts[fntIdx], fs);
                if (myFont.Name != PhonFonts[fntIdx] ) 
                {
                    answer = "錯";
                } 

                g.DrawString(
                   answer ,
                     myFont,
                    new SolidBrush(fc),
                    new PointF(
                        offsetX + widthPerChar * i,
                        offsetY));
              
            }
            if (context.Request["m"] == "save")
            {
                //將結果存為檔案
                bmp.Save(context.Server.MapPath(
                    string.Format("{0}", context.Request["f"])));
            }
            else
            {
                //將結果以PNG格式傳回
                context.Response.ContentType = "image/png";
                bmp.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}