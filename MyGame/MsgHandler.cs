using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame
{
    public static class MsgHandler
    {
        public static string SessionNavn = "msg";


        public static bool Exsist
        {
            get
            {
                if (HttpContext.Current.Session[SessionNavn] != null)
                {
                    return true;
                }

                return false;
            }
        }
        /// <summary>
        /// Insert new postback message
        /// </summary>
        /// <param name="MsgType">1:Succes, 2:Error, 3:Warning, 4:Notice</param>
        /// <param name="message">Message to return</param>
        public static void InsertMsg(int MsgType, string message)
        {
            string title = "";
            string css = "";

            switch (MsgType)
            {
                case 1:
                    title = "Success";
                    css = "alert alert-success alert-dismissible";
                    break;
                case 2:
                    title = "Error";
                    css = "alert alert-danger alert-dismissible";
                    break;
                case 3:
                    title = "Warning";
                    css = "alert alert-warning alert-dismissible";
                    break;
                case 4:
                    title = "Notice";
                    css = "alert alert-info alert-dismissible";
                    break;
            }

            HttpContext.Current.Session[SessionNavn] = new Msg(title, message, css);
        }

        public static void Delete()
        {
            HttpContext.Current.Session.Remove(SessionNavn);
        }

        public static Msg Get()
        {
            Msg Message = HttpContext.Current.Session[SessionNavn] as Msg;
            MsgHandler.Delete();
            return Message;
        }
    }
}