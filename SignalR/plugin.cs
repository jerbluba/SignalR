using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalR
{
    public class plugin
    {
            private static int version = 4;
            public static void AddCss(string path, Page page)
            {
                Literal cssFile = new Literal() { Text = @"<link href=""" + page.ResolveUrl(path+"?v="+version) + @""" type=""text/css"" rel=""stylesheet"" />" };
                page.Header.Controls.AddAt(0,cssFile);
                
            }
            public static void AddScript(string path, Page page)
            {
                Literal jsFile = new Literal() { Text = @"<script src=""" + page.ResolveUrl(path + "?v=" + version) + @""" type=""text/javascript""></script>" };
                page.Header.Controls.AddAt(0, jsFile);
            }
            public static void defaultSet(Page page) { 

                AddScript("Scripts/gridView.js", page);
                AddScript("Scripts/fit.js", page);
                AddScript("Scripts/jquery-1.6.4.js", page);
                AddCss("StyleSheet/map.css", page);
                AddCss("StyleSheet/reset.css", page);
            
            }
            public static void easy(Page page)
            {

                AddScript("Scripts/fit.js", page);
                AddScript("Scripts/jquery-1.6.4.js", page);
                AddCss("StyleSheet/map.css", page);
                AddCss("StyleSheet/reset.css", page);

            }

            public static void practice(Page page)
            {

                AddScript("Scripts/fit.js", page);
                AddScript("Scripts/bpmf_functions3.js", page);
                AddScript("Scripts/bpmf_data.js", page);
                AddScript("Scripts/jquery-1.6.4.js", page);
                AddCss("StyleSheet/map.css", page);
                AddCss("StyleSheet/reset.css", page);


            }


         public static void chat(Page page)
            {

                AddScript("Scripts/fit.js", page);
                AddScript("Scripts/bpmf_functions3.js", page);
                AddScript("Scripts/bpmf_data.js", page);
             
                AddScript("Scripts/jquery.easing.min.js", page);
                AddScript("Scripts/jQueryRotate.2.2.js", page);
                AddScript("/signalr/hubs", page);
              AddScript("Scripts/jquery.signalR-1.1.1.min.js", page);
                AddScript("Scripts/jquery-1.6.4.js", page);
                AddCss("StyleSheet/map.css", page);
             AddCss("StyleSheet/chat.css", page);
                AddCss("StyleSheet/reset.css", page);


      

            }
    }
}