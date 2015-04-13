using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace rpg.Dao
{
    public class Conexao
    {
        private SqlConnection _conn;

        public Conexao()
        {
            if (_conn == null)
            {
                _conn = new SqlConnection("Data Source=SQL5014.Smarterasp.net;Initial Catalog=DB_9BC14A_systemrpg;User ID=DB_9BC14A_systemrpg_admin;Password=Managers2b;");
            }
        }

        public Conexao(string strConn)
        {
            if (_conn == null)
            {
                _conn = new SqlConnection(strConn);
            }
        }

        public void open()
        {
            if (_conn.State == ConnectionState.Closed)
            {
                _conn.Open();
                using (SqlCommand cmd = new SqlCommand("SET DATEFORMAT dmy", _conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void close()
        {
            if (_conn.State == ConnectionState.Open)
                _conn.Close();
        }

        public virtual void execute(string sql)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            this.close();
        }

        public virtual int executeReturnRows(string sql)
        {
            int rows = 0;
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                rows = cmd.ExecuteNonQuery();
            }
            this.close();
            return rows;
        }

        public virtual object scalar(string sql)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                object result = null;
                result = cmd.ExecuteScalar();
                this.close();
                return result;
            }

        }

        public virtual DataTable dataTable(string sql, string nome)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable tb = new DataTable(nome);
                    //adapter.SelectCommand.Transaction = _transacao;
                    adapter.Fill(tb);
                    //_transacao.Commit();
                    this.close();
                    return tb;
                }
            }

        }

        public virtual DataSet dataSet(string sql)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        //adapter.SelectCommand.Transaction = _transacao;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(ds);
                        //_transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        System.Web.HttpContext.Current.Response.Write(ex.Message);
                        //_transacao.Rollback();
                    }
                    finally
                    {
                        this.close();
                    }
                    return ds;
                }
            }
        }

        public virtual void fill(string sql, ref DataTable tb)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    try
                    {
                        //adapter.SelectCommand.Transaction = _transacao;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(tb);
                        //_transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        System.Web.HttpContext.Current.Response.Write(ex.Message);
                        //_transacao.Rollback();
                    }
                    finally
                    {
                        this.close();
                    }
                }
            }
        }

        public virtual DataSet dataSet(string sql, string tabela)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        //adapter.SelectCommand.Transaction = _transacao;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(ds, tabela);
                        //_transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        System.Web.HttpContext.Current.Response.Write(ex.Message);
                        //_transacao.Rollback();
                    }
                    finally
                    {
                        this.close();
                    }
                    return ds;
                }
            }
        }

        public virtual List<Hashtable> reader(string sql)
        {
            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                cmd.CommandTimeout = 0;
                List<Hashtable> arr = new List<Hashtable>();
                try
                {
                    //cmd.Transaction = _transacao;
                    SqlDataReader leitor = cmd.ExecuteReader();
                    int colunas = leitor.FieldCount;

                    while (leitor.Read())
                    {
                        Hashtable ht = new Hashtable();
                        for (int i = 0; i < colunas; i++)
                        {
                            ht.Add(leitor.GetName(i), leitor[leitor.GetName(i)]);
                        }
                        arr.Add(ht);
                    }
                    leitor.Close();
                    //_transacao.Commit();
                }
                catch (Exception ex)
                {
                    System.Web.HttpContext.Current.Response.Write(ex.Message);
                    //_transacao.Rollback();
                }
                finally
                {
                    this.close();
                }
                return arr;
            }
        }
        public virtual SqlDataReader reader_list(string sql)
        {

            this.open();
            using (SqlCommand cmd = new SqlCommand(sql, _conn))
            {
                SqlDataReader rds = cmd.ExecuteReader();
                this.close();
                return rds;
            }
        }

    }

}