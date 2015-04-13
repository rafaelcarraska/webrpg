using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rpg.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Menu
        public ActionResult Index()
        {      

            ViewBag.bemvindo = bemvindo();
            ViewBag.data_hora = data_hora();
            ViewBag.menu = montaMenu();
            return View();
        }

        [Route("logout", Name = "logout")]
        public ActionResult logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");            
        }
    }
}