using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame
{
    /// <summary>
    /// Message Class for MsgHandler Class
    /// </summary>
    public class Msg
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Css { get; set; }

        public Msg(string title, string message, string css)
        {
            this.Title = title;
            this.Message = message;
            this.Css = css;
        }
    }
}