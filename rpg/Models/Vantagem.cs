using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Vantagem
    {
        public int Cod_Vantagem { get; set; }

        public String Descricao { get; set; }

        public int Custo { get; set; }

        public List<string> Bonus_Atributo { get; set; }

        public List<int> Pre_Vantagens { get; set; }

        public int Pre_Requisitos { get; set; }

        public int Caracteristicas { get; set; }

        public int Campanha { get; set; }

        public bool Ativo { get; set; }
        
    }
}