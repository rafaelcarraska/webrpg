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
    public class AtributoDao
    {
        Conexao _conn;
        LogDao _LogDao;

        public List<Atributos> Listar_Atributos_dt()
        {
            _conn = new Conexao();
            List<Atributos> list_Atributo = new List<Atributos>();

            DataTable dt_Atributo = _conn.dataTable("select * from Atributos order by descricao", "ATRIBUTOS");
            foreach (DataRow row in dt_Atributo.Rows)
            {
                list_Atributo.Add(new Atributos
                {
                    Cod_Atributo = Convert.ToInt32(row["cod_atributo"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Ativo = Convert.ToBoolean(row["ativo"].ToString())
                });
            }

            return list_Atributo;
        }
        public List<Atributos> Listar_Atributos_ativo()
        {
            _conn = new Conexao();
            List<Atributos> list_Atributo = new List<Atributos>();

            DataTable dt_Atributo = _conn.dataTable("select * from Atributos where ativo = 1 order by descricao", "ATRIBUTOS");
            foreach (DataRow row in dt_Atributo.Rows)
            {
                list_Atributo.Add(new Atributos
                {
                    Cod_Atributo = Convert.ToInt32(row["cod_atributo"].ToString()),
                    Descricao = row["descricao"].ToString()
                });
            }

            return list_Atributo;
        }
        public bool verifica_descricao (int cod_atributo, string descricao)
        {
            Conexao _conn = new Conexao();
            int count = Convert.ToInt32(_conn.scalar("select count(*) from Atributos where descricao = '"+descricao.Replace("'","''")+"' and cod_atributo <> "+ cod_atributo +""));
            if (count > 0)
	        {
		        return false;
	        }
            return true;
        }

        public string Insert(string descricao, bool ativo)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strInsert = "insert into atributos (descricao, ativo) values('" + descricao.Replace("'", "''") + "', '"+ativo+"')";
                _conn.execute(strInsert);                
                _LogDao.insert("Atributos", "add", "");
                msg = "Atributo '" + descricao + "', Criado.";
            }
            catch (Exception)
            {
                msg = "Erro ao adicionar um Atributo ('"+ descricao +"')";
            }
            return msg;
        }

        public string update(int cod_atributo, string descricao, bool ativo)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strupdate = "update atributos set descricao = '" + descricao.Replace("'", "''") + "', ativo = '"+ativo+"' where cod_atributo = " + cod_atributo + "";
                _conn.execute(strupdate);
                _LogDao.insert("Atributos", "alt", "id " + cod_atributo);
                msg = "Atributo '" + descricao + "', Alterado.";

            }
            catch (Exception)
            {
                msg = "Erro ao adicionar um Atributo ('" + descricao + "')";
            }
            return msg;
        }

        public string delete(int cod_atributo)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strdelete = "delete from atributos where cod_atributo = " + cod_atributo + "";
                _conn.execute(strdelete);
                _LogDao.insert("Atributos", "del", "id " + cod_atributo);
            }
            catch (Exception)
            {
                msg = "Erro ao deletar o Atributo ";
            }
            return msg;
        }
    }
}
