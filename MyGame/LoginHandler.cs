using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace MyGame
{
    /// <summary>
    /// Class that handle login details
    /// </summary>
    public static class LoginHandler
    {
        public static bool HasAccess
        {
            get
            {
                bool Response = false;

                foreach (link alink in LoginHandler.AllLinks)
                {
                    if (HttpContext.Current.Request.RawUrl.Contains(alink.urlpage))
                    {
                        if (MyGame.LoginHandler.UserRights.Contains(alink.right.codename))
                        {
                            Response = true;
                        }
                    }
                }

                return Response;
            }
        }
        public static List<link> AllLinks
        {
            get
            {
                DataClassesDataContext db = new DataClassesDataContext();

                List<link> PrivateAllLinks = new List<link>();

                if(HttpContext.Current.Session["allLinks"] == null)
                {
                    PrivateAllLinks = (from l in db.links
                                       select l).ToList<link>();
                    HttpContext.Current.Session["allLinks"] = PrivateAllLinks;
                }
                else
                {
                    PrivateAllLinks = HttpContext.Current.Session["allLinks"] as List<link>;
                }

                return PrivateAllLinks;
            }
        }

        public static List<string> UserRights
        {
            get
            {
                DataClassesDataContext db = new DataClassesDataContext();

                List<string> PrivateUserRights = new List<string>();

                if (HttpContext.Current.Session["userRights"] == null)
                {

                    PrivateUserRights = (from aright1 in db.role_rights
                                         join aright2 in db.rights on aright1.rights_id equals aright2.id
                                         where aright1.role_id.Equals(LoginHandler.ActualUser.role_id)
                                         select aright2.codename).ToList();
                    HttpContext.Current.Session["userRights"] = PrivateUserRights;
                }
                else
                {
                    PrivateUserRights = HttpContext.Current.Session["userRights"] as List<string>;
                }

                return PrivateUserRights;
            }
        }
        /// <summary>
        /// Denne metodes formål, er at returnere den bruger som er logget på. 
        /// Læg mærke til at "Getteren" er udvidet med noget kode, og at der ikke er en "Setter"
        /// </summary>
        public static user ActualUser
        {
            get
            {
                // før en bruger kan returneres, er det nødvendigt at tjekke om der overhovedet findes en bruger i sessionen
                if (HttpContext.Current.Session["user"] == null)
                {
                    user auser = new user();
                    auser.name = "Guest";
                    auser.role_id = 1;
                    auser.id = 0;
                    // er sessionen tom, oprettes en standard Gæst.
                    HttpContext.Current.Session["user"] = auser;
                }
                // her returneres brugeren direkte fra sessionen, som et Bruger objekt
                return HttpContext.Current.Session["user"] as user;
            }
        }

        /// <summary>
        /// Denne metode sørger for at logge en bruger på, hvis der findes en bruger med det login og det kodeord som sendes med til metoden
        /// </summary>
        /// <param name="login">Brugerens login</param>
        /// <param name="password">Brugerens kodeord</param>
        /// <returns>true hvis det lykkes at logge på, ellers returneres false.</returns>
        public static bool Login(string email, string password)
        {
            DataClassesDataContext db = new DataClassesDataContext();

            user auser = (from u in db.users
                          where u.email.Equals(email) && u.password.Equals(password)
                          select u).FirstOrDefault();

            if (auser != null)
            {
                HttpContext.Current.Session["user"] = auser;
                HttpContext.Current.Session.Remove("userRights");
                HttpContext.Current.Session.Remove("allLinks");
            }

            // efter at have prøvet at logge ind, returneres true hvis RolleID er forskellig fra Gæsterollen 1
            // hvis logind fejler, vil rolleId fortsat være Gæsterollen 1
            return (ActualUser.role_id != 1);
        }

        public static void LogOut()
        {
            if (HttpContext.Current.Session["user"] != null)
            {
                HttpContext.Current.Session.Remove("user");
                HttpContext.Current.Session.Remove("userRights");
                HttpContext.Current.Session.Remove("allLinks");
            }
        }

        public static bool IsGuest()
        {
            return (ActualUser.role_id == 1);
        }
    }
}