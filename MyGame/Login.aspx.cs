using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "logout")
                {
                    MyGame.LoginHandler.LogOut();
                    Response.Redirect("Default.aspx?login=false");
                }
            }
        }

        protected void LinkButton_Login_Click(object sender, EventArgs e)
        {
            if(MyGame.LoginHandler.Login(TextBox_Email.Text, TextBox_Password.Text))
            {
                Response.Redirect("Default.aspx?login=true");
            }
            else
            {
                Literal_Error_Message.Visible = true;
                Literal_Error_Message.Text = "<label class='error_message'> - Wrong email or password! <span class='glyphicon glyphicon-remove'></span></label>";
                TextBox_Password.Text = "";
            }
        }

        protected void LinkButton_Reset_Click(object sender, EventArgs e)
        {
            TextBox_Email.Text = "";
            TextBox_Password.Text = "";
        }
    }
}