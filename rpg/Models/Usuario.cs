using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Usuario
    {
        public int Cod_Usuario { get; set; }

        public String Nome { get; set; }
        
        public int Cod_Perfil { get; set; }

        public String Login { get; set; }

        public String Email { get; set; }

        public bool Ativo { get; set; }
    }
}