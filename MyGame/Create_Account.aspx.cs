using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame
{
    public partial class Create_Account : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!LoginHandler.HasAccess)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void LinkButton_Reset_Click(object sender, EventArgs e)
        {
            TextBox_Email.Text = "";
            TextBox_Password.Text = "";
            TextBox_Name.Text = "";
        }

        protected void LinkButton_Create_Account_Click(object sender, EventArgs e)
        {
            user duplicateUser = (from u in db.users
                                  where u.email.Equals(TextBox_Email.Text)
                                  select u).FirstOrDefault();

            //hvis email ikke eksisterer i db kan den oprettes
            if (duplicateUser == null)
            {
                user newUser = new user();

                newUser.role_id = 2;
                newUser.email = TextBox_Email.Text;
                newUser.password = TextBox_Password.Text;
                newUser.name = TextBox_Name.Text;
                newUser.created = DateTime.Now;
                newUser.last_online = DateTime.Now;

                db.users.InsertOnSubmit(newUser);
                db.SubmitChanges();

                //sørger for user bliver logget ind efter de er blevet oprettet
                LoginHandler.Login(TextBox_Email.Text, TextBox_Password.Text);

                Response.Redirect("~/Default.aspx");
            }
            else
            {
                
                Literal_duplicateUser.Visible = true;
            }
        }
    }
}