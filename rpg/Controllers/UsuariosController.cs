using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Models;

namespace rpg.Controllers
{
    public class UsuariosController : BaseController
    {
        public ActionResult Index()
        {
            if (!verifica_acesso("Usuários", "Visualizar"))
            {
                return RedirectToAction("Index", "Login");
            }
            PerfilDao _PerfilDao = new PerfilDao();
            ViewBag.perfil = _PerfilDao.Listar_Perfil_dt(true);
            UsuarioDao _UsuarioDao = new UsuarioDao();
            IList<Usuario> _Usuarios = _UsuarioDao.Listar_Usuarios();
            ViewBag.pagina = "Usuário";
            return View(_Usuarios);
        }
    }
}