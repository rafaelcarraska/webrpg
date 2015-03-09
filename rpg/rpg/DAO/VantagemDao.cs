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
    public class VantagemDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Vantagem> Listar_Vantagens_dt()
        {
            _conn = new Conexao();
            List<Vantagem> list_Vantagens = new List<Vantagem>();

            DataTable dt_Vantagem = _conn.dataTable("select cod_vantagem, descricao, ativo from Vantagens order by descricao", "VANTAGEM");
            foreach (DataRow row in dt_Vantagem.Rows)
            {
                list_Vantagens.Add(new Vantagem
                {
                    Cod_Vantagem = Convert.ToInt32(row["cod_vantagem"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Ativo = Convert.ToBoolean(row["ativo"].ToString())
                });
            }

            return list_Vantagens;
        }

        public List<Vantagem> Listar_Vantagens_dt_cb()
        {
            _conn = new Conexao();
            List<Vantagem> list_Vantagens = new List<Vantagem>();

            DataTable dt_Vantagem = _conn.dataTable("select cod_vantagem, descricao from Vantagens where ativo = 1 order by descricao", "VANTAGEM");
            foreach (DataRow row in dt_Vantagem.Rows)
            {
                list_Vantagens.Add(new Vantagem
                {
                    Cod_Vantagem = Convert.ToInt32(row["cod_vantagem"].ToString()),
                    Descricao = row["descricao"].ToString()
                });
            }

            return list_Vantagens;
        }

        public Vantagem Listar_Vantagem(int Cod_Vantagem)
        {
            _conn = new Conexao();
            Vantagem _Vantagem = new Vantagem();

            DataTable dt_vantagem = _conn.dataTable("select * from Vantagens where cod_vantagem = " + Cod_Vantagem + "", "VANTAGEM");
            if (dt_vantagem.Rows.Count > 0)
            {
                _Vantagem.Cod_Vantagem = Convert.ToInt32(dt_vantagem.Rows[0]["Cod_Vantagem"].ToString());
                _Vantagem.Descricao = dt_vantagem.Rows[0]["Descricao"].ToString();
                _Vantagem.Custo = Convert.ToInt32(dt_vantagem.Rows[0]["Custo"].ToString());
                _Vantagem.Campanha = Convert.ToInt32(dt_vantagem.Rows[0]["Campanha"].ToString());
                _Vantagem.Bonus_Atributo = new List<string>(dt_vantagem.Rows[0]["Bonus_Atributo"].ToString().Split(';'));
                _Vantagem.Pre_Vantagens = new List<int>(Array.ConvertAll(dt_vantagem.Rows[0]["Pre_Vantagens"].ToString().Split('_'), int.Parse));
                _Vantagem.Pre_Requisitos = dt_vantagem.Rows[0]["Pre_Requisitos"].ToString();
                _Vantagem.Caracteristicas = dt_vantagem.Rows[0]["Caracteristicas"].ToString();
                _Vantagem.Campanha = Convert.ToInt32(dt_vantagem.Rows[0]["Campanha"].ToString());
                _Vantagem.Ativo = Convert.ToBoolean(dt_vantagem.Rows[0]["Ativo"].ToString());
            }

            return _Vantagem;
        }
    }
}
