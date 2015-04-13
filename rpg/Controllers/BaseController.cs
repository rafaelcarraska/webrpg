using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Filtros;
using rpg.Models;

namespace rpg.Controllers
{
    [Autorizacaofilter]
    public class BaseController : Controller
    {
       
        public bool verifica_acesso(string tela, string acao)
        {
            IList<Permisao> _permisoes = SessionView.PermisoesSession;
            List<Permisao> filtro = _permisoes.Where(y => y.descricao == tela && y.permisao == acao).ToList();
            if (filtro.Count > 0)
            {
                return true;
            }
            return false;
        }

        public string data_hora()
        {
            CultureInfo cultura = new CultureInfo("pt-BR");
            return DateTime.Now.ToString("D", cultura);
        }
        public string bemvindo()
        {
            Usuario _usuario = new Usuario();
            _usuario = (Usuario)SessionView.UsuarioSession;

            return "Olá, " + _usuario.Nome;
        }

        public string montaMenu()
        {
            Usuario _usuario = new Usuario();
            _usuario = (Usuario)SessionView.UsuarioSession;
            ModuloDao _ModuloDao = new ModuloDao();
            IList<Modulo> modulos = _ModuloDao.Listar_modulo_acesso(_usuario.Cod_Usuario);
            List<Modulo> filtro = modulos.Where(y => y.cod_modulo_pai == 0).ToList();
            string html = "<ul id='ulMenu' class='menubar'>\n";
            int cont = 0;

            foreach (Modulo _modulos in filtro)
            {
                html += "<li class='submenu'>";
                html += "<a href='javascript:void(0);'>" + _modulos.descricao + "</a>";

                if (_modulos.filho)
                {
                    html = achaFilhos(_modulos.cod_modulo, html, modulos);
                }
                html += "</li>\n";
                cont++;
            }
            html += "</ul>\n";

            return html;
        }

        public string achaFilhos(int codigoModulo, string html, IList<Modulo> modulos)
        {
            html += "<ul  class='menu'>\n";
            List<Modulo> filho = modulos.Where(y => y.cod_modulo_pai == codigoModulo).ToList();
            foreach (Modulo _modulosfilho in filho)
            {
                html += "<li class='submenu'>";

                if (string.IsNullOrEmpty(_modulosfilho.descricao))
                {
                    html += "<a href='javascript:void(0);'>" + _modulosfilho.descricao + "</a>";
                }
                else
                {
                    html += "<a href=\"" + _modulosfilho.route + "\">" + _modulosfilho.descricao + "</a>";
                }

                if (_modulosfilho.filho)
                {
                    achaFilhos(_modulosfilho.cod_modulo, html, modulos);
                }
                html += "</li>\n";
            }

            html += "</ul>\n";

            return html;

        }
    }
}