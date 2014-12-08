using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyGame
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        DataClassesDataContext db = new DataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {

            // Integer to tell wish menu to show
            int MenuId = 0; // 0 for nothing

            if (MyGame.LoginHandler.IsGuest())
            {
                MenuId = 2; // 2 for guest menu
            }
            else
            {
                MenuId = 3; // 3 for member menu
                menu_adventure.HRef = "Adventure.aspx";
                menu_character.HRef = "Character.aspx";
                menu_socials.HRef = "Socials.aspx";
                menu_town.HRef = "Town.aspx";
            }

            // Building 'Top Header Right Ul'
            Repeater_Panel_Header_Right_UL.DataSource = (from t1 in db.menu_links
                                                         join t2 in db.links on t1.link_id equals t2.id
                                                         join t4 in db.glyphicons on t2.glyph_id equals t4.id
                                                         where t1.menu_id.Equals(MenuId)
                                                         select t2).ToList();
            Repeater_Panel_Header_Right_UL.DataBind();

            // Building Primary Link for 'Top Header Right Ul'
            var PrimaryHeaderRightLink = (from t1 in db.menu_links
                                          join t2 in db.links on t1.link_id equals t2.id
                                          join t3 in db.rights on t2.rights_id equals t3.id
                                          join t4 in db.glyphicons on t2.glyph_id equals t4.id
                                          where t1.menu_id.Equals(MenuId) && t1.is_primary.Equals(1)
                                          select new { Tag = t4.tag, UrlPage = t2.urlpage, LinkName = t2.name }).ToList();
            if (PrimaryHeaderRightLink.Count > 0)
            {
                Literal_LinkButton_Primary.Text = PrimaryHeaderRightLink.FirstOrDefault().Tag;
                LinkButton_Primary.PostBackUrl = PrimaryHeaderRightLink.FirstOrDefault().UrlPage;
                LinkButton_Primary.CssClass = "btn btn-sm btn-default button-log-panel " + MyGame.Helper.ClassActiveString(PrimaryHeaderRightLink.FirstOrDefault().UrlPage, "inside");
                LinkButton_Primary.ToolTip = PrimaryHeaderRightLink.FirstOrDefault().LinkName;
            }
        }

        public static string inactive()
        {
            if (MyGame.LoginHandler.IsGuest())
            {
                return "inactive";
            }

            return string.Empty;
        }

    }
}