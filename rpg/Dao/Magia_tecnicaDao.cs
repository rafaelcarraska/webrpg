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
    public class Magia_TecnicaDao
    {
        Conexao _conn;
        LogDao _LogDao;
        public List<Magia_Tecnica> Listar_Magia_Tecnicas_grid()
        {
            _conn = new Conexao();
            List<Magia_Tecnica> list_Magia_Tecnica = new List<Magia_Tecnica>();

            DataTable dt_Magia_Tecnica = _conn.dataTable("select Cod_Magic_Tecnica, descricao, Tipo, Nvl from Magic_Tecnicas order by descricao", "RACA");
            foreach (DataRow row in dt_Magia_Tecnica.Rows)
            {
                list_Magia_Tecnica.Add(new Magia_Tecnica
                {
                    Cod_Magic_Tecnica = Convert.ToInt32(row["Cod_Magic_Tecnica"].ToString()),
                    Descricao = row["descricao"].ToString(),
                    Tipo = row["Tipo"].ToString(),
                    Nvl = Convert.ToInt32(row["Nvl"].ToString())
                });
            }

            return list_Magia_Tecnica;
        }

    }
}
