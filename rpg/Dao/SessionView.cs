using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using rpg.Models;

namespace rpg.Dao
{
    public class SessionView
    {
        private static HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        public static Usuario UsuarioSession
        {
            get
            {
                if (HttpContext.Current.Session["usuariologado"] == null)
                {
                    return null;
                }
                return (Usuario)HttpContext.Current.Session["usuariologado"];
            }

            set
            {
                HttpContext.Current.Session["usuariologado"] = value;
            }
        }

        public static IList<Permisao> PermisoesSession
        {
            get
            {
                if (HttpContext.Current.Session["permisoes"] == null)
                {
                    return null;
                }
                return (IList<Permisao>)HttpContext.Current.Session["permisoes"];
            }

            set
            {
                HttpContext.Current.Session["permisoes"] = value;
            }
        }

    }
}