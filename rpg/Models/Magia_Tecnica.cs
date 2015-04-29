using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Magia_Tecnica
    {
        public int Cod_Magic_Tecnica {get; set;}
        public String Descricao {get; set;}
        public int Custo {get; set;}
        public String Tipo {get; set;}
        public String Elemento {get; set;}
        public int Nvl { get; set; }
        public int Pre_Requisito_Lv {get; set;}
        public String Bonus_Mosificador {get; set;}
        public int X_ao_Dia {get; set;}
        public String Dano {get; set;}
        public int Consumo {get; set;}
        public decimal Alcance {get; set;}
        public String Duraçao {get; set;}
        public String Tempo_de_preparo {get; set;}
        public String Caracteristicas {get; set;}
        public int Campanha {get; set;}
        public bool Ativo {get; set;}
        					 									

    }
}