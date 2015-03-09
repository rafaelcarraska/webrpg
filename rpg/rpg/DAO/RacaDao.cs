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
    public class RacaDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Raca> Listar_Racas_grid()
        {
            _conn = new Conexao();
            List<Raca> list_Raca = new List<Raca>();

            DataTable dt_raca = _conn.dataTable("select Cod_Raca, descricao, Custo from racas order by descricao", "RACA");
            foreach (DataRow row in dt_raca.Rows)
            {
                list_Raca.Add(new Raca
                {
                    Cod_Raca = Convert.ToInt32(row["Cod_Raca"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Custo = Convert.ToInt32(row["Custo"].ToString())
                });
            }

            return list_Raca;
        }

        public Raca Listar_Raca(int cod_raca)
        {
            _conn = new Conexao();
            Raca _Raca = new Raca();

            DataTable dt_raca = _conn.dataTable("select * from racas where cod_raca = "+ cod_raca +"", "RACA");
            if (dt_raca.Rows.Count >0)
	        {
                _Raca.Cod_Raca = Convert.ToInt32(dt_raca.Rows[0]["Cod_Raca"].ToString());
                _Raca.Descricao_Detalhada = dt_raca.Rows[0]["Descricao_Detalhada"].ToString();
                _Raca.Descricao = dt_raca.Rows[0]["Descricao"].ToString();
                _Raca.Campanha = Convert.ToInt32(dt_raca.Rows[0]["Campanha"].ToString());
                _Raca.Vantagens_Desvantagens = dt_raca.Rows[0]["Vantagens_Desvantagens"].ToString();
                _Raca.Idiomas = dt_raca.Rows[0]["Idiomas"].ToString();
                _Raca.Pericias = dt_raca.Rows[0]["Pericias"].ToString();
                _Raca.Lv_PontosPericias = new List<string>(dt_raca.Rows[0]["Lv_PontosPericias"].ToString().Split(';'));
                _Raca.Lv_PontosVantagens = new List<string>(dt_raca.Rows[0]["Lv_PontosVantagens"].ToString().Split(';'));
                _Raca.Custo = Convert.ToInt32(dt_raca.Rows[0]["Custo"].ToString());
                _Raca.Ponto_Atributo = new List<string>(dt_raca.Rows[0]["Ponto_Atributo"].ToString().Split(';'));
                _Raca.Deslocamento = Convert.ToInt32(dt_raca.Rows[0]["Deslocamento"].ToString());
                _Raca.Monstro = Convert.ToBoolean(dt_raca.Rows[0]["Monstro"].ToString());
                _Raca.Ativo = Convert.ToBoolean(dt_raca.Rows[0]["Ativo"].ToString());
            }

            return _Raca;
        }

    }
}
