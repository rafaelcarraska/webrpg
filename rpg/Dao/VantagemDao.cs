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
                if (string.IsNullOrEmpty(dt_vantagem.Rows[0]["Pre_Vantagens"].ToString()))
                {
                    _Vantagem.Pre_Vantagens = new List<int>();
                }
                else
                {
                    _Vantagem.Pre_Vantagens = new List<int>(Array.ConvertAll(dt_vantagem.Rows[0]["Pre_Vantagens"].ToString().Split('_'), int.Parse));
                }                
                _Vantagem.Pre_Requisitos = dt_vantagem.Rows[0]["Pre_Requisitos"].ToString();
                _Vantagem.Caracteristicas = dt_vantagem.Rows[0]["Caracteristicas"].ToString();
                _Vantagem.Campanha = Convert.ToInt32(dt_vantagem.Rows[0]["Campanha"].ToString());
                _Vantagem.Ativo = Convert.ToBoolean(dt_vantagem.Rows[0]["Ativo"].ToString());
            }

            return _Vantagem;
        }

        public string Insert(Vantagem vantagem)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strInsert = "insert into vantagens (Descricao, Custo, Bonus_Atributo, Pre_Vantagens, Pre_Requisitos, Caracteristicas, Campanha, Ativo) "
                    +" values('" + vantagem.Descricao.Replace("'", "''") + "', " + vantagem.Custo + ", '" + string.Join<string>(";", vantagem.Bonus_Atributo).Replace("'", "''") + "', '"
                    + string.Join<int>("_", vantagem.Pre_Vantagens).Replace("'", "''") + "', '" + vantagem.Pre_Requisitos.Replace("'", "''") + "', '" + vantagem.Caracteristicas.Replace("'", "''") + "', " 
                    + vantagem.Campanha + ", '" + vantagem.Ativo.ToString() + "')";
                _conn.execute(strInsert);
                _LogDao.insert("Vantagem", "add", "");
                //msg = "Vantagem '" + vantagem.Descricao + "', Criada.";
            }
            catch (Exception)
            {
                msg = "Erro ao adicionar a Vantagem ('" + vantagem.Descricao + "')";
            }
            return msg;
        }

        public string update(Vantagem vantagem)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strupdate = "update vantagens set Descricao = '" + vantagem.Descricao.Replace("'", "''") + "', Custo = " + vantagem.Custo 
                    + ", Bonus_Atributo = '" + string.Join<string>(";", vantagem.Bonus_Atributo).Replace("'", "''") + "', Pre_Vantagens = '"+ string.Join<int>("_", vantagem.Pre_Vantagens).Replace("'", "''") 
                    + "', Pre_Requisitos = '" + vantagem.Pre_Requisitos.Replace("'", "''") + "', Caracteristicas = '" + vantagem.Caracteristicas.Replace("'", "''") + "', Campanha = "+ vantagem.Campanha + ", Ativo = '" + vantagem.Ativo.ToString() + "' where cod_vantagem = "+vantagem.Cod_Vantagem+" ";
                _conn.execute(strupdate);
                _LogDao.insert("Vantagem", "up", "cod_vantagem = "+vantagem.Cod_Vantagem.ToString());
                //msg = "Vantagem '" + vantagem.Descricao + "', Criada.";
            }
            catch (Exception)
            {
                msg = "Erro ao atualizar a Vantagem ('" + vantagem.Descricao + "')";
            }
            return msg;
        }

        public bool verificar_descricao(string descricao, int cod_vantagem)
        {
            try
            {
                _conn = new Conexao();

                string strselect = "select count(cod_vantagem) from vantagens where descricao = '" + descricao.Replace("'", "''") + "' and cod_vantagem <> " + cod_vantagem + "";
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

        public string delete(int cod_vantagem)
        {
            string msg = "";
            try
            {
                _conn = new Conexao();
                _LogDao = new LogDao();

                string strdelete = "delete from Vantagens where cod_vantagem = " + cod_vantagem + "";
                _conn.execute(strdelete);
                _LogDao.insert("Vantagem", "del", "id " + cod_vantagem);
            }
            catch (Exception)
            {
                msg = "Erro ao deletar o Atributo ";
            }
            return msg;
        }
    }
}
