using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyGame.admin
{
    /// <summary>
    /// Summary description for Handler
    /// </summary>
    public class Handler : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                int chunk = context.Request["chunk"] != null ? int.Parse(context.Request["chunk"]) : 0;
                string fileName = context.Request["name"] != null ? context.Request["name"] : string.Empty;

                HttpPostedFile fileUpload = context.Request.Files[0];

                var uploadPath = context.Server.MapPath("~/img/temp/");
                using (var fs = new System.IO.FileStream(Path.Combine(uploadPath, fileName), chunk == 0 ? System.IO.FileMode.Create : System.IO.FileMode.Append))
                {
                    var buffer = new byte[fileUpload.InputStream.Length];
                    fileUpload.InputStream.Read(buffer, 0, buffer.Length);

                    fs.Write(buffer, 0, buffer.Length);
                }

                // Hvis filen er færdig
                if (fileUpload.ContentLength < 512000)
                {
                    string fileExtension = Path.GetExtension(fileName);

                    if (context.Request.QueryString["id"] != null && context.Request.QueryString["type"] != null)
                    {
                        if (Helper.IsQueryStringInt("id"))
                        {
                            DataClassesDataContext db = new DataClassesDataContext();

                            int id = 0;
                            string type = context.Request.QueryString["type"].ToString();
                            string ResizeSettings = "";

                            switch(context.Request.QueryString["type"])
                            {
                                case "race":
                                    race_picture NewRacePicture = new race_picture();
                                    NewRacePicture.name = fileName;
                                    NewRacePicture.img_type = fileExtension;
                                    NewRacePicture.race_id = Convert.ToInt32(context.Request.QueryString["id"]);

                                    db.race_pictures.InsertOnSubmit(NewRacePicture);
                                    db.SubmitChanges();

                                    id = NewRacePicture.id;
                                    ResizeSettings = "width=200;height=200;mode=crop";
                                break;
                                case "item":
                                item_picture NewItemPicture = new item_picture();
                                NewItemPicture.name = fileName;
                                NewItemPicture.img_type = fileExtension;
                                NewItemPicture.slot_type_id = Convert.ToInt32(context.Request.QueryString["id"]);

                                db.item_pictures.InsertOnSubmit(NewItemPicture);
                                db.SubmitChanges();

                                id = NewItemPicture.id;
                                ResizeSettings = "width=100;height=100;mode=crop";
                                break;
                            }

                            string oldFilename = uploadPath + fileName;
                            string newFilename = uploadPath + id + fileExtension;
                            string newFilenameShort = id + fileExtension;

                            string Folder = context.Server.MapPath("~/img/" + type + "/");

                            System.IO.File.Move(oldFilename, newFilename);

                            ImageResizer.ImageBuilder.Current.Build(
                                                        newFilename,
                                                        Folder + newFilenameShort,
                                                        new ImageResizer.ResizeSettings(ResizeSettings)
                                                        );

                            System.IO.File.Delete(newFilename);
                            
                        }
                    }
                }
              
                context.Response.ContentType = "text/plain";
                context.Response.Write("Hello World");
            }                                
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}