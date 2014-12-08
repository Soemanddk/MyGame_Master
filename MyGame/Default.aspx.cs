using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyGame.Charecters.CharacterSheet Test = new Charecters.CharacterSheet(1);

            string Text = "<div class='col-lg-2 col-md-2 col-sm-3 col-xs-3'>";
            Text += "<img class='img-responsive' src='" + Test.CharcterRacePicture + "' />";
            Text += "</div><div class='col-lg-10 col-md-10 col-sm-9 col-xs-9'>";
            Text += "<p>Name: " + Test.Character.name + "</p>";
            Text += "<p>Race: " + Test.Character.race.name + "</p>";
            Text += "<p>Alignment: " + Test.Character.race.alignment.name + "</p>";
            Text += "</div><div class='col-lg-12 col-md-12 col-sm-12 col-xs-12'>";
            Text += "<p>Strength: " + Test.Strength + "</p>";
            Text += "<p>Stamina: " + Test.Stamina + "</p>";
            Text += "<p>Agility: " + Test.Agility + "</p>";
            Text += "<p>Dexterity: " + Test.Dexterity + "</p>";
            Text += "<p>Intelligence: " + Test.Intelligence + "</p>";
            Text += "<p>Wisdom: " + Test.Wisdom + "</p>";
            Text += "<p>Melee atk dmg: " + Test.Melee_Attack_Damage + "</p>";
            Text += "<p>Melee multi atk: " + Test.Melee_Multi_Attack + "</p>";
            Text += "<p>Spell cast dmg: " + Test.Spell_Cast_Damage + "</p>";
            Text += "<p>Spell multi cast: " + Test.Spell_Multi_Attack + "</p>";
            Text += "<p>Crit chance: " + Test.Critical_Chance + "</p>";
            Text += "<p>Crit bonus: " + Test.Critical_Bonus + "</p>";
            Text += "<p>Potency: " + Test.Potency + "</p>";
            Text += "<p>Skill mod: " + Test.Skill_Modifier + "</p>";
            Text += "<p>Avoidance: " + Test.Avoidance + "</p>";
            Text += "<p>Resistance: " + Test.Resistance + "</p>";
            Text += "<p>Health: " + Test.Health + "</p>";
            Text += "<p>Power: " + Test.Power + "</p>";
            Text += "<p>energy: " + Test.Energy + "</p>";
            Text += "</div>";

            Literal_Test.Text = Text;
        }
    }
}