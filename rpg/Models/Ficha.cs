using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Ficha
    {
        public int Cod_Ficha { get; set; }

        [Required]
        public int Cod_Mestre { get; set; }

        [Required]
        public String Descricao { get; set; }

        public IList<int> Atributos { get; set; }

        [Required]
        public String Calc_HP { get; set; }

        [Required]
        public String Calc_MP { get; set; }

        public bool CA { get; set; }

        public bool Redutor_Nv_Vida { get; set; }

        public bool Redutor_Peso { get; set; }

        public bool Ativo { get; set; }
    }
}