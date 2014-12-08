using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyGame.ajax
{
    public partial class adminraceimage : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(LoginHandler.UserRights.Contains("admin"))
            {
                if(Request.QueryString["id"] != null)
                {
                    if (Helper.IsQueryStringInt("id"))
                    {
                        DataClassesDataContext db = new DataClassesDataContext();

                        race_picture Picture = (from p in db.race_pictures
                                                where p.id.Equals(Request.QueryString["id"])
                                                select p).FirstOrDefault();
                        if (Picture != null)
                        {
                            Literal_Title.Text = "Edit " + Picture.name;
                            RaceImage.Src = "../img/race/" + Picture.id + Picture.img_type;
                            RaceImage.Alt = Picture.name;
                            TextBox_RaceImageName.Text = Picture.name;
                            HiddenField_RaceImageId.Value = Picture.id.ToString();
                        }
                    }
                }
            }
        }
        [WebMethod]
        public static void SaveFunction(object id, object name)
        {
             DataClassesDataContext db = new DataClassesDataContext();
             race_picture Picture = (from p in db.race_pictures
                                      where p.id.Equals(Convert.ToInt32(id))
                                      select p).FirstOrDefault();
            if (Picture != null)
            {
                Picture.name = name.ToString();
                db.SubmitChanges();
            }
        }
        [WebMethod]
        public static void DeleteFunction(object id, object name)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            race_picture Picture = (from p in db.race_pictures
                                    where p.id.Equals(Convert.ToInt32(id))
                                    select p).FirstOrDefault();
            if (Picture != null)
            {
                string FileName = HttpContext.Current.Server.MapPath("~/img/race/" + Picture.id + Picture.img_type);

                if (File.Exists(FileName))
                {
                    File.Delete(FileName);
                }
                MsgHandler.InsertMsg(3, "Image " + Picture.name + " deleted!");
                db.race_pictures.DeleteOnSubmit(Picture);
                db.SubmitChanges();
                
            }
        }
    }
}