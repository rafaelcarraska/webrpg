using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace rpg.Models
{
    public class Item
    {
        public int Cod_Item { get; set; }
        public String Descricao { get; set; }
        public decimal Valor_Min { get; set; }
        public decimal Valor_Max { get; set; }
        public bool Up { get; set; }
        public int Bonus_Atk_Corpo { get; set; }
        public int Bonus_Atk_Distanc { get; set; }
        public int Decisivo { get; set; }
        public int Critico { get; set; }
        public String Dano { get; set; }
        public int Resistencia { get; set; }
        public decimal Peso { get; set; }
        public int Ca { get; set; }
        public int Raridade { get; set; }
        public string Tipo { get; set; }
        public List<int> Pre_Requisito { get; set; }
        public string  Penalidade { get; set; }
        public bool Duas_Maos { get; set; }
        public bool Municao { get; set; }
        public String Descricao_Detalhada { get; set; }
        public int Campanha { get; set; }
        public bool Ativo { get; set; }
    }
}