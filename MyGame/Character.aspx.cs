using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace MyGame
{
    public partial class Character : System.Web.UI.Page
    {
        DataClassesDataContext db = new DataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!LoginHandler.HasAccess)
            //{
            //    Response.Redirect("~/Default.aspx");
            //}

            FillShowCharacter();
        }

        private void FillShowCharacter()
        {
            if (!IsPostBack)
            {
                //udskriver text fra db udenom repeater
                //tjekker den user som er logget ind's ID

                List<charecter> Characters = (from p in db.charecters
                                              where p.user_id.Equals(LoginHandler.ActualUser.id)
                                              select p).ToList();
                int MaxAllowedCharacters = 2;

                HtmlGenericControl divOverAll = new HtmlGenericControl("div");
                divOverAll.Attributes.Add("class", "col-lg-6 col-md-6 col-sm-6 col-xs-6");
                Panel_Characters.Controls.Add(divOverAll);

                #region character
                foreach (charecter character in Characters)
                {                  
                    HtmlGenericControl divRow = new HtmlGenericControl("div");
                    divRow.Attributes.Add("class","row carousel-row");
                    divOverAll.Controls.Add(divRow);
                       
                    HtmlGenericControl divColumn = new HtmlGenericControl("div");
                    divColumn.Attributes.Add("class","col-lg-12 col-md-12 col-sm-12 col-xs-12 slide-row");
                    divRow.Controls.Add(divColumn);

                    HtmlGenericControl Image = new HtmlGenericControl("img");
                    Image.Attributes.Add("class", "image-character");
                    Image.Attributes.Add("src", "img/race/" + character.race_picture_id + character.race_picture.img_type);
                    Image.Attributes.Add("alt","Image");
                    divColumn.Controls.Add(Image);

                    HtmlGenericControl divContent = new HtmlGenericControl("div");
                    divContent.Attributes.Add("class","slide-content");
                    divColumn.Controls.Add(divContent);

                    HtmlGenericControl p = new HtmlGenericControl("p");
                    p.Attributes.Add("class", "margin-zero letterspace");
                    p.InnerHtml = "<b><u>Character name:</u></b>" + "<br />" + " - " + character.name;
                    divContent.Controls.Add(p);

                    HtmlGenericControl p1 = new HtmlGenericControl("p");
                    p1.Attributes.Add("class", "margin-zero letterspace");
                    p1.InnerHtml = "<b><u>Race:</u></b>" + "<br />" +  " - " + character.race.name;
                    divContent.Controls.Add(p1);

                    HtmlGenericControl p2 = new HtmlGenericControl("p");
                    p2.Attributes.Add("class", "margin-zero letterspace");
                    p2.InnerHtml = "<b><u>Alignment:</u></b>" + "<br />" + " - " + character.race.alignment.name;
                    divContent.Controls.Add(p2);

                    HtmlGenericControl divFooter = new HtmlGenericControl("div");
                    divFooter.Attributes.Add("class","slide-footer");
                    divColumn.Controls.Add(divFooter);

                    HtmlGenericControl spanPull = new HtmlGenericControl("div");
                    spanPull.Attributes.Add("class","pull-right");
                    divFooter.Controls.Add(spanPull);

                    HtmlGenericControl aLink1 = new HtmlGenericControl("a");
                    aLink1.Attributes.Add("class", "btn btn-sm btn-danger push-right-14 letterspace");
                    aLink1.InnerHtml = "<span class='glyphicon glyphicon-trash' ></span> Delete";
                    aLink1.Attributes.Add("href","#");
                    aLink1.Attributes.Add("onclick","return confirm('Are you sure you want to delete your character?')");
                    spanPull.Controls.Add(aLink1);

                    HtmlGenericControl aLink2 = new HtmlGenericControl("a");
                    aLink2.Attributes.Add("class", "btn btn-sm btn-success push-right-6 letterspace");
                    aLink2.InnerHtml = "<span class='glyphicon glyphicon-play' ></span> Play";
                    aLink2.Attributes.Add("href","#");
                    spanPull.Controls.Add(aLink2);

                    //Tæller på om man har nået max oprettede charactere som i dette tilfælde er (2) Så derfor minusser vi.
                    MaxAllowedCharacters--;
                }
                #endregion

                #region MaxAllowedCharacters
                //udregner på hvor mange characters man har inden for grænsen som er 2
                for (int i = MaxAllowedCharacters; i > 0; i--)
                {
                    HtmlGenericControl divRow = new HtmlGenericControl("div");
                    divRow.Attributes.Add("class", "row carousel-row");
                    divOverAll.Controls.Add(divRow);

                    HtmlGenericControl divColumn = new HtmlGenericControl("div");
                    divColumn.Attributes.Add("class", "col-lg-12 col-md-12 col-sm-12 col-xs-12 slide-row");
                    divRow.Controls.Add(divColumn);

                    HtmlGenericControl h1 = new HtmlGenericControl("h1");
                    h1.Attributes.Add("class", "text-center push-top-50");
                    h1.InnerText = "Create a new Character";
                    divColumn.Controls.Add(h1);

                    HtmlGenericControl divFooter = new HtmlGenericControl("div");
                    divFooter.Attributes.Add("class", "slide-footer");
                    divColumn.Controls.Add(divFooter);

                    HtmlGenericControl spanPull = new HtmlGenericControl("div");
                    spanPull.Attributes.Add("class", "pull-right");
                    divFooter.Controls.Add(spanPull);

                    HtmlGenericControl aLink3 = new HtmlGenericControl("a");
                    aLink3.Attributes.Add("class", "btn btn-sm btn-primary push-right-6 letterspace");
                    aLink3.InnerHtml = "<span class='glyphicon glyphicon-cog' ></span> Create Character";
                    aLink3.Attributes.Add("href", "Create_Character.aspx");
                    spanPull.Controls.Add(aLink3);
                }
                #endregion
            }
        }
    }
}