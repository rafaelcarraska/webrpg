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

        public IList<int> Vantagens_Desvantagens { get; set; }

        public List<string> Bonus_Atributo { get; set; }

        public String Idiomas { get; set; }

        public IList<string> Pericias { get; set; }

        public decimal Lv_PontosPericias { get; set; }

        public decimal Lv_PontosVantagens { get; set; }

        public int Custo { get; set; }

        public decimal Lv_pontosAtributo { get; set; }

        public int Deslocamento { get; set; }

        public int Bonus_Hp { get; set; }

        public int Bonus_Mp { get; set; }

        public int Bonus_CA { get; set; }

        public bool Monstro { get; set; }

        public bool Ativo { get; set; }
    }
}