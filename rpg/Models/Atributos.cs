using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Atributos
    {
        public int Cod_Atributo { get; set; }

        [Required]
        public String Descricao { get; set; }

        public bool Ativo { get; set; }
    }
}