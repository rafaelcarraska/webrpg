using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Pericia
    {
        public int Cod_Pericia { get; set; }

        public String Descricao { get; set; }

        public String Descricao_atributo { get; set; }

        public int Cod_Atributo { get; set; }

        public int penalidade_peso { get; set; }

        public List<int> requisito_classe { get; set; }

        public bool Treinada { get; set; }

        public String Caracteristicas { get; set; }

        public int Campanha { get; set; }

        public bool Ativo { get; set; }
    }
}