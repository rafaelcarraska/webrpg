using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using rpg.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace rpg.Dao
{
    public class FichaDao
    {
        public List<Ficha> Listar_Fichas()
        {
            Conexao _conn = new Conexao();
            List<Ficha> list_Fichas = new List<Ficha>();
            List<int> list_Atributos = new List<int>();
            AtributoDao _AtributoDao = new AtributoDao();

            DataTable dt_Fichas = _conn.dataTable("select * from fichas ", "FICHA");
            foreach (DataRow row in dt_Fichas.Rows)
            {
                list_Fichas.Add(new Ficha
                {
                    Cod_Ficha = Convert.ToInt32(row["Cod_Ficha"].ToString()),
                    Cod_Mestre = Convert.ToInt32(row["Cod_Mestre"].ToString()),
                    Descricao = row["Descricao"].ToString(),
                    Calc_HP = row["Calc_HP"].ToString(),
                    Calc_MP = row["Calc_MP"].ToString(),
                    CA = Convert.ToBoolean(row["CA"].ToString()),
                    Redutor_Nv_Vida = Convert.ToBoolean(row["Redutor_Nv_Vida"].ToString()),
                    Redutor_Peso = Convert.ToBoolean(row["Redutor_Peso"].ToString()),
                    Ativo = Convert.ToBoolean(row["Ativo"].ToString()),
                    Atributos = new List<int>( Array.ConvertAll(row["Atributos"].ToString().Split('_'), int.Parse) )
                });
                                        
                
            }

            return list_Fichas;
        }
    }
}
