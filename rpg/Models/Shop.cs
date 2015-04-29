using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Shop
    {
        public int Cod_Shop { get; set; }
        public string Descricao { get; set; }
        public int Cod_Mestre { get; set; }
        public string Itens { get; set; }
        public decimal Valor_Min { get; set; }
        public decimal Valor_Max { get; set; }
        public bool Magico { get; set; }
        public int Raridade { get; set; }
        public bool Ativo { get; set; }
        public int N_Max { get; set; }
    }
}