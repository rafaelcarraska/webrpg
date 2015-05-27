using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rpg.Dao;
using rpg.Filtros;
using rpg.Models;

namespace rpg.Controllers
{
    public class HomeController : BaseController
    {
    
        public ActionResult Index()
        {
            ViewBag.pagina = "Inicio";
            return RedirectToAction("Home", "Home");
        }

        [Route("Home", Name = "Home")]
        public ActionResult Home()
        {
            ViewBag.pagina = "Inicio";
            CampanhaDao _CampanhaDao = new CampanhaDao();
            ViewBag.cbmestre = _CampanhaDao.Listar_Campanhas_cb_mestre();
            ViewBag.cbjogador = _CampanhaDao.Listar_Campanhas_cb();
            return View();
        }

        [Route("Mestre/{id}", Name = "Mestre")]
        public ActionResult Mestre(int id)
        {
            return View();
        }

        [Route("Personagem/{id}", Name = "Personagem")]
        public ActionResult Personagem(int id)
        {
            ViewBag.idjpg = id+".jpg";

            return View();
        }

        [Route("Ficha/{id}", Name = "Ficha")]
        public ActionResult Ficha(int id)
        {
            ViewBag.idjpg = id + ".jpg";
            ViewBag.hp = 20;

            //cabecalho da ficha
            @ViewBag.nomeJogador = "Rodrigo";
            @ViewBag.nomePersonagem = "Turnak";
            @ViewBag.raca = "Orc";
            @ViewBag.lvl = "4";
            @ViewBag.classe = "Guerreiro";
            @ViewBag.pontosHeroicos = "2";
            @ViewBag.proximo = "100";

            //Atributos
            @ViewBag.attr1 = "10";
            @ViewBag.attr2 = "10";
            @ViewBag.attr3 = "10";
            @ViewBag.attr4 = "10";
            @ViewBag.attr5 = "10";

            //Status
            @ViewBag.hp = "200";
            @ViewBag.mp = "40";
            @ViewBag.hpAtual = "100";
            @ViewBag.mpAtual = "20";

            //CA
            int caArmadura = 1;
            int caEscudo = 2;
            int caRaca = 1;
            int caItem = 0;
            int caTotal = caArmadura + caEscudo + caRaca + caItem;

            @ViewBag.caArmadura = caArmadura.ToString();
            @ViewBag.caEscudo = caEscudo.ToString();
            @ViewBag.caRaca = caRaca.ToString();
            @ViewBag.caItem = caItem.ToString();
            @ViewBag.caTotal = caTotal.ToString();

            //Vantagens&Destavangens
            List<String> vantagem = new List<String> ();
            vantagem.Add("força +1");
            vantagem.Add("ambidestro");
            vantagem.Add("sangue que cura");
            vantagem.Add("Leal");
            ViewBag.vantagem = vantagem;

            List<String> desvantagem = new List<String>();
            desvantagem.Add("Protegido +2");
            desvantagem.Add(" ");
            desvantagem.Add(" ");
            desvantagem.Add(" ");
            ViewBag.desvantagem = desvantagem;
            









            return View();
        }

        [Route("Editar_Personagem/{id}", Name = "Editar_Personagem")]
        public ActionResult Editar_Personagem(int id)
        {
            ViewBag.pagina = "Editar_Personagem";
            ViewBag.id = id;
            return View();
        }
    }
}