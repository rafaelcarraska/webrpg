using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Perfil
    {
        public int Cod_Perfil { get; set; }

        [Required]
        public String Descricao { get; set; }

        public bool Ativo { get; set; }
    }
}