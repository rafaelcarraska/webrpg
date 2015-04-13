using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Campanha
    {
        public int Cod_Campanha { get; set; }

        public int Cod_Mestre { get; set; }

        public int Cod_Ficha { get; set; }
        
        public String Descricao_Detalhada { get; set; }

        public String Descricao { get; set; }

        public int penalidade_peso { get; set; }

        public List<int> requisito_classe { get; set; }

        public bool Ativo { get; set; }

        public bool utilizar_Global { get; set; }
    }
}