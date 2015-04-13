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
    public class ClasseDao
    {
        Conexao _conn;
        LogDao _LogDao;

        public List<Classe> Listar_Classes_dt()
        {
            _conn = new Conexao();
            List<Classe> list_Classe = new List<Classe>();

            DataTable dt_Classes = _conn.dataTable("select Cod_Classe, Descricao, Custo from classes order by descricao", "CLASSE");
            foreach (DataRow row in dt_Classes.Rows)
            {
                list_Classe.Add(new Classe
                {
                    Cod_Classe = Convert.ToInt32(row["Cod_Classe"].ToString()),
                    Descricao = row["Descricao"].ToString(),
                    Custo = Convert.ToInt32(row["Custo"].ToString())
                });
            }

            return list_Classe;
        }

        public Classe Listar_Classe(int cod_classe)
        {
            _conn = new Conexao();
            Classe _Classe = new Classe();

            DataTable dt_classe = _conn.dataTable("select * from classes where cod_classe = " + cod_classe + "", "CLASSE");
            if (dt_classe.Rows.Count > 0)
            {
                _Classe.Cod_Classe = Convert.ToInt32(dt_classe.Rows[0]["Cod_Classe"].ToString());
                _Classe.Descricao_Detalhada = dt_classe.Rows[0]["Descricao_Detalhada"].ToString();
                _Classe.Descricao = dt_classe.Rows[0]["Descricao"].ToString();
                _Classe.Campanha = Convert.ToInt32(dt_classe.Rows[0]["Campanha"].ToString());
                if (!string.IsNullOrEmpty(dt_classe.Rows[0]["Vantagens_Desvantagens"].ToString()))
                {
                    _Classe.Vantagens_Desvantagens = new List<int>(Array.ConvertAll(dt_classe.Rows[0]["Vantagens_Desvantagens"].ToString().Split('_'), int.Parse));
                }                
                _Classe.Pericias = dt_classe.Rows[0]["Pericias"].ToString();
                _Classe.Custo = Convert.ToInt32(dt_classe.Rows[0]["Custo"].ToString());
                _Classe.Ativo = Convert.ToBoolean(dt_classe.Rows[0]["Ativo"].ToString());
            }

            return _Classe;
        }

    }
}
