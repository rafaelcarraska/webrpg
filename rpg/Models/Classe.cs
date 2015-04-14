using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Classe
    {
        public int Cod_Classe { get; set; }

        public String Descricao { get; set; }

        public String Descricao_Detalhada { get; set; }

        public int Campanha { get; set; } //admin pode cria classe global (campanha 0) mestre tem q escolher a campanha (dele)

        public IList<int> Vantagens_Desvantagens { get; set; }

        public IList<int> Pericias { get; set; }

        public int Custo { get; set; }

        public bool Ativo { get; set; }
    }
}