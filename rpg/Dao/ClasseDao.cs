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
                _Classe.Pericias = new List<string>(dt_classe.Rows[0]["Pericias"].ToString().Split(';'));
                _Classe.Custo = Convert.ToInt32(dt_classe.Rows[0]["Custo"].ToString());
                _Classe.Ativo = Convert.ToBoolean(dt_classe.Rows[0]["Ativo"].ToString());
            }

            return _Classe;
        }

        public List<Classe> Listar_Classes_dt_cb()
        {
            _conn = new Conexao();
            List<Classe> list_Classe = new List<Classe>();

            DataTable dt_Classe = _conn.dataTable("select Cod_Classe, descricao from Classes where ativo = 1 order by descricao", "VANTAGEM");
            foreach (DataRow row in dt_Classe.Rows)
            {
                list_Classe.Add(new Classe
                {
                    Cod_Classe = Convert.ToInt32(row["Cod_Classe"].ToString()),
                    Descricao = row["descricao"].ToString()
                });
            }

            return list_Classe;
        }

        public string Insert(Classe classe)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strInsert = "insert into classes (Descricao, Custo, Pericias, Vantagens_Desvantagens, Descricao_Detalhada, Campanha, Ativo) "
                    + " values('" + classe.Descricao.Replace("'", "''") + "', " + classe.Custo + ", '" + string.Join<string>(";", classe.Pericias).Replace("'", "''") + "', '"
                    + string.Join<int>("_", classe.Vantagens_Desvantagens).Replace("'", "''") + "', '" + classe.Descricao_Detalhada.Replace("'", "''") + "', "
                    + classe.Campanha + ", '" + classe.Ativo.ToString() + "')";
                _conn.execute(strInsert);
                _LogDao.insert("Classe", "add", "");
            }
            catch (Exception)
            {
                msg = "Erro ao adicionar a Classe ('" + classe.Descricao + "')";
            }
            return msg;
        }

        public string update(Classe classe)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strupdate = "update classes set Descricao = '" + classe.Descricao.Replace("'", "''") + "', Custo = " + classe.Custo
                    + ", Pericias = '" + string.Join<string>(";", classe.Pericias).Replace("'", "''") + "', Vantagens_Desvantagens = '" + string.Join<int>("_", classe.Vantagens_Desvantagens).Replace("'", "''")
                    + "', Descricao_Detalhada = '" + classe.Descricao_Detalhada.Replace("'", "''") + "', Campanha = " + classe.Campanha + ", Ativo = '" + classe.Ativo.ToString() + "' where Cod_Classe = " + classe.Cod_Classe + " ";
                _conn.execute(strupdate);
                _LogDao.insert("Vantagem", "up", "cod_classe = " + classe.Cod_Classe.ToString());
            }
            catch (Exception)
            {
                msg = "Erro ao atualizar a Classe ('" + classe.Descricao + "')";
            }
            return msg;
        }

        public bool verificar_descricao(string descricao, int cod_classe)
        {
            try
            {
                _conn = new Conexao();

                string strselect = "select count(cod_classe) from classes where descricao = '" + descricao.Replace("'", "''") + "' and cod_classe <> " + cod_classe + "";
                if (Convert.ToInt32(_conn.scalar(strselect)) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return true;
            }
        }

        public string delete(int cod_classe)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strdelete = "delete from classes where cod_classe = " + cod_classe + "";
                _conn.execute(strdelete);
                _LogDao.insert("Classe", "del", "id " + cod_classe);
            }
            catch (Exception)
            {
                msg = "Erro ao deletar a Classe ";
            }
            return msg;
        }

    }
}
