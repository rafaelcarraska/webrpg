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
    public class CampanhaDao
    {
        Conexao _conn;
        LogDao _LogDao;

        public List<Campanha> Listar_Campanhas_dt()
        {
            _conn = new Conexao();
            List<Campanha> list_Campanhas = new List<Campanha>();

            DataTable dt_Campanhas = _conn.dataTable("select * from campanhas order by descricao", "CAMPANHA");
            foreach (DataRow row in dt_Campanhas.Rows)
            {
                list_Campanhas.Add(new Campanha
                {
                    Cod_Campanha = Convert.ToInt32(row["Cod_Campanha"].ToString()),
                    Cod_Mestre = Convert.ToInt32(row["Cod_Mestre"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Descricao_Detalhada = row["Descricao_Detalhada"].ToString(),
                    utilizar_Global = Convert.ToBoolean(row["utilizar_Global"].ToString()),
                    Cod_Ficha = Convert.ToInt32(row["Cod_Ficha"].ToString()),
                    Ativo = Convert.ToBoolean(row["ativo"].ToString())
                });
            }

            return list_Campanhas;
        }

        public List<Campanha> Listar_Campanhas_cb()
        {
            _conn = new Conexao();
            List<Campanha> list_Campanhas = new List<Campanha>();

            DataTable dt_Campanhas = _conn.dataTable("select Cod_Campanha, Descricao  from campanhas where ativo = 1 order by descricao", "CAMPANHA");
            foreach (DataRow row in dt_Campanhas.Rows)
            {
                list_Campanhas.Add(new Campanha
                {
                    Cod_Campanha = Convert.ToInt32(row["Cod_Campanha"].ToString()),
                    Descricao = row["descricao"].ToString()
                });
            }

            return list_Campanhas;
        }

        public List<Campanha> Listar_Campanhas_cb_mestre()
        {
            _conn = new Conexao();
            List<Campanha> list_Campanhas = new List<Campanha>();

            DataTable dt_Campanhas = _conn.dataTable("select Cod_Campanha, Descricao  from campanhas where ativo = 1 and cod_mestre = " + SessionView.UsuarioSession.Cod_Usuario + " order by descricao", "CAMPANHA");
            foreach (DataRow row in dt_Campanhas.Rows)
            {
                list_Campanhas.Add(new Campanha
                {
                    Cod_Campanha = Convert.ToInt32(row["Cod_Campanha"].ToString()),
                    Descricao = row["descricao"].ToString()
                });
            }

            return list_Campanhas;
        }

    }
}
