using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.admin
{
    public partial class Default : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (MyGame.LoginHandler.HasAccess)
            {
                CountUsers();
                CountCharacters();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }           
        }

        private void CountCharacters()
        {
            int Good = db.charecters.Where(c => c.race.alignment_id.Equals(1)).Count();
            Literal_CountGood.Text = Good.ToString();
            int Evil = db.charecters.Where(c => c.race.alignment_id.Equals(2)).Count();
            Literal_CountEvil.Text = Evil.ToString();
            int Human = db.charecters.Where(c => c.race_id.Equals(4)).Count();
            Literal_CountHumans.Text = Human.ToString();
            int Elf = db.charecters.Where(c => c.race_id.Equals(3)).Count();
            Literal_CountElfs.Text = Elf.ToString();
            int Dwarf = db.charecters.Where(c => c.race_id.Equals(2)).Count();
            Literal_CountDwarfs.Text = Dwarf.ToString();
            int Orc = db.charecters.Where(c => c.race_id.Equals(5)).Count();
            Literal_CountOrcs.Text = Orc.ToString();
            int Troll = db.charecters.Where(c => c.race_id.Equals(6)).Count();
            Literal_CountTrolls.Text = Troll.ToString();
            int Ogre = db.charecters.Where(c => c.race_id.Equals(7)).Count();
            Literal_CountHalfOgre.Text = Ogre.ToString();
            int CountTotal = db.charecters.Count();
            Literal_CountCharacters.Text = CountTotal.ToString();
        }
        public void CountUsers()
        {
            string ListGroups = "";
            int CountTotal = 0;
            List<role> Roles = db.roles.ToList();

            foreach(role Role in Roles)
            {
                if (Role.name != "Guest")
                {
                    string ListGroup = "<li class='list-group-item'><ul><li>";
                    ListGroup +=  Role.name + "</li>";
                    int RoleCount = (from u in db.users
                                     where u.role_id.Equals(Role.id)
                                     select u.id).Count();

                    CountTotal += RoleCount;
                    ListGroup += "<li>" + RoleCount + "</li></ul></li>";
                    ListGroups += ListGroup;
                }
            }

            string ListGroupTotalCount = "<li class='list-group-item list-group-item-success'><ul><li>Total</li><li>" + CountTotal + "</li></ul></li>";
            Literal_CountRoles.Text = ListGroups + ListGroupTotalCount;
        }   
    }
    
}