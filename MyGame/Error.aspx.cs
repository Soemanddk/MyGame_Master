using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["type"] != null)
            {
                Literal_Error.Visible = true;

                switch (Request.QueryString["type"])
                {
                    case "401":
                        Literal_Error.Text = "Error 401";
                        break;
                    case "403":
                        Literal_Error.Text = "Error 403";
                        break;
                    case "404":
                        Literal_Error.Text = "Error 404";
                        break;
                    case "500":
                        Literal_Error.Text = "Error 500";
                        break;
                    default:
                        Literal_Error.Text = "Unknown Error!";
                        break;
                }
            }
        }
    }
}