using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        DataClassesDataContext db = new DataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MsgHandler.Exsist)
            {
                Msg thisMessage = MsgHandler.Get();

                Panel_Msg.Visible = true;
                Panel_Msg.CssClass = thisMessage.Css;
                Literal_MsgTitle.Text = thisMessage.Title;
                Literal_MsgMsg.Text = thisMessage.Message;
            }

            Literal_UserName.Text = LoginHandler.ActualUser.email;

            int menu_id = 0;

            if (Session["Control_Panel"] != null)
            {
                switch (Session["Control_Panel"].ToString())
                {
                    case "Settings":
                        menu_id = 5;
                        LinkButton_Control_Settings.CssClass += " active";
                        break;
                    case "Gameplay":
                        menu_id = 6;
                        LinkButton_Control_Gameplay.CssClass += " active";
                        break;
                }
            }
            else
            {
                Session["Control_Panel"] = "Settings";
                Response.Redirect(Request.RawUrl);
            }

            var NavUlLinks = (from t1 in db.menu_links
                              join t2 in db.links on t1.link_id equals t2.id
                              join t3 in db.rights on t2.rights_id equals t3.id
                              join t4 in db.glyphicons on t2.glyph_id equals t4.id
                              where t1.menu_id.Equals(menu_id)
                              select new { CodeName = t3.codename, UrlPage = t2.urlpage, GlyphTag = t4.tag, LinkName = t2.name, BadgetTable = t2.badgets_table }).ToList();
            foreach (var link in NavUlLinks)
            {
                if (MyGame.LoginHandler.UserRights.Contains(link.CodeName))
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    if (MyGame.Helper.ClassActiveBool(link.UrlPage))
                    {
                        li.Attributes.Add("class", "active");
                    }
                    menu.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", link.UrlPage);
                    anchor.InnerHtml = link.GlyphTag + " " + link.LinkName;
                    if (link.BadgetTable != "None")
                    {       
                        anchor.InnerHtml += " <span class='badge pull-right'>" + MyGame.SqlConn.Count_Rows(link.BadgetTable) + "</span>";
                    }
                    li.Controls.Add(anchor);
                }
            }
        }

        protected void LinkButton_Control_Settings_Click(object sender, EventArgs e)
        {
            Session["Control_Panel"] = "Settings";
            Response.Redirect(Request.RawUrl);
        }

        protected void LinkButton_Control_Gameplay_Click(object sender, EventArgs e)
        {
            Session["Control_Panel"] = "Gameplay";
            Response.Redirect(Request.RawUrl);
        }

        protected void LinkButton_SignOut_Click(object sender, EventArgs e)
        {
            LoginHandler.LogOut();
            Response.Redirect(Request.RawUrl);
        }
    }
}