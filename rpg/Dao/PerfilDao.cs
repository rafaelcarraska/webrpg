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
using rpg.Dao;

namespace rpg.Dao
{
    public class PerfilDao
    {
        Conexao _conn;

        public List<Perfil> Listar_Perfil_dt(bool ativo)
        {
            _conn = new Conexao();
            List<Perfil> list_Perfil = new List<Perfil>();

            DataTable dt_Atributo = _conn.dataTable("select * from Perfil where ativo = '"+ ativo +"' order by descricao", "PERFIL");
            foreach (DataRow row in dt_Atributo.Rows)
            {
                list_Perfil.Add(new Perfil
                {
                    Cod_Perfil = Convert.ToInt32(row["Cod_Perfil"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Ativo = Convert.ToBoolean(row["ativo"].ToString())
                });
            }

            return list_Perfil;
        }
    }
}
