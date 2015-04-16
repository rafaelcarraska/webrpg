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
using System.Web.SessionState;
using rpg.Dao;

namespace rpg.Dao
{
    public class LogDao 
    {
        public void insert(string tela, string acao, string obs)
        {
            Conexao _conn = new Conexao();
            Usuario _usuario = new Usuario();
            _usuario = SessionView.UsuarioSession;
            string strInsert = "insert into log_h (datet, cod_usuario, tela, acao, obs) values('" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', " + _usuario.Cod_Usuario + ", '" + tela.Replace("'", "''") + "', '" + acao.Replace("'", "''") + "', '" + obs.Replace("'", "''") + "')";
            _conn.execute(strInsert);
        }

        public void delete(int cod_log)
        {
            Conexao _conn = new Conexao();
            string strdelete = "delete from log_h where cod_log = " + cod_log + "";
            _conn.execute(strdelete);
        }
    }
}
