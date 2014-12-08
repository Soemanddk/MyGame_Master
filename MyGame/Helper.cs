using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyGame
{
    public static class Helper
    {
        public static bool ClassActiveBool(string page)
        {
            if (HttpContext.Current.Request.RawUrl.Contains(page))
            {
                return true;
            }

            return false;
        }
        public static string ClassActiveString(string page, string placement)
        {
            if (HttpContext.Current.Request.RawUrl.Contains(page))
            {
                string RequestReturn = "";
                switch (placement)
                {
                    case "inside":
                        RequestReturn = "active";
                        break;
                    case "outside":
                        RequestReturn = "class='active'";
                        break;
                }

                return RequestReturn;
                
            }

            return "";
        }
        public static bool IsQueryStringInt(string QueryString)
        {
            bool YesOrNo = true;

            if (HttpContext.Current.Request.QueryString[QueryString] != null)
            {
                try
                {
                    int Test = Convert.ToInt32(HttpContext.Current.Request.QueryString[QueryString]);
                }
                catch
                {
                    YesOrNo = false;
                }
            }
            else
            {
                YesOrNo = false;
            }

            return YesOrNo;
        }
        public static string Glyphicon(string Glyph, string Method)
        {
            string Glyphicon = "";
            string Start = "<span class='glyphicon glyphicon-";
            string End = "'></span>";

            switch (Method)
            {
                case "cut":
                    Glyphicon = Glyph.Replace(Start, "").Replace(End, "");
                    break;
                case "asample":
                    Glyphicon = Start + Glyph + End;
                    break;
            }
            return Glyphicon;
        }
        public static string BreadCrumb(string PageUrl, string PageName, string Action)
        {
            string li_1st = "<li><a href='" + PageUrl + "'>" + PageName + "</a></li>";
            string li_2nd = "<li class='active'>" + Action + "</li>";

            return li_1st + li_2nd;
        }
    }
}