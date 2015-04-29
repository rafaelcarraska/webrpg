using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;

namespace rpg.Controllers
{
    public class ShopController : BaseController
    {
        [Route("Shop", Name = "Shop")] 
        public ActionResult Index()
        {
            if (!verifica_acesso("Shop", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.pagina = "Shop";
            ShopDao _ShopDao = new ShopDao();
            IList<Shop> _Shop = _ShopDao.Listar_Shop_dt();
            return View(_Shop);
        }
    }
}