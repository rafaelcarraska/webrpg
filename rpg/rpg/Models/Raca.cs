using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Raca
    {
        public int Cod_Raca { get; set; }

        public String Descricao_Detalhada { get; set; }

        public String Descricao { get; set; }

        public int Campanha { get; set; } //admin pode cria raca global (campanha 0) mestre tem q escolher a campanha (dele)

        public String Vantagens_Desvantagens { get; set; }

        public String Idiomas { get; set; }

        public String Pericias { get; set; }

        public List<string> Lv_PontosPericias { get; set; }

        public List<string> Lv_PontosVantagens { get; set; }

        public int Custo { get; set; }

        public List<string> Ponto_Atributo { get; set; }

        public int Deslocamento { get; set; }

        public bool Monstro { get; set; }

        public bool Ativo { get; set; }
    }
}