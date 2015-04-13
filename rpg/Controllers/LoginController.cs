using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Models;
using rpg.Dao;

namespace rpg.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            UsuarioDao _UsuarioDao = new UsuarioDao();
            //IList<Usuario> _usuario = _UsuarioDao.Listar_Empresa_pai(0);
            //ViewBag.empresa = _usuario;
            //ViewBag.empresaerr = new Usuario();
            return View();
        }

        public ActionResult Autentica(String login, String senha)
        {
            UsuarioDao _UsuarioDao = new UsuarioDao();
            Usuario _usuario = _UsuarioDao.busca(login, senha);
            if (_usuario != null)
            {
                SessionView.UsuarioSession = _usuario;
                ModuloDao _ModuloDao = new ModuloDao();
                IList<Permisao> _permisoes = _ModuloDao.Listar_permisao(_usuario.Cod_Perfil);
                SessionView.PermisoesSession = _permisoes;
                //Session["empresa"] = _UsuarioDao.Empresa(_usuario.Cod_Empresa_Pai);
                return RedirectToAction("Home", "Home");
            }
            else
            {
                TempData["notice"] = "erro";
                return RedirectToAction("Index");
            }

        }
    }
}