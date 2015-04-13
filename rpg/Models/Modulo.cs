using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Modulo
    {
        public int cod_modulo { get; set; }

        public int cod_modulo_pai { get; set; }

        public string descricao { get; set; }

        public string permisao { get; set; }

        public string route { get; set; }

        public int ordem { get; set; }

        public bool ativo { get; set; }

        public bool filho { get; set; }
    }
}