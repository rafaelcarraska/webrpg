using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Common;
using rpg.Models;
using System.Data.SqlClient;
using System.Data;

namespace rpg.Dao
{
    public class ModuloDao
    {

        public List<Modulo> Listar_modulo_acesso(int cod_usuario)
        {
            Conexao _conn = new Conexao();
            List<Modulo> list_modulos = new List<Modulo>();

            DataTable dt_modulos = _conn.dataTable("select * from modulo where ativo = 'true' and cod_modulo in ( "
                                                    +" select cod_modulo from acesso where cod_perfil in ( "
                                                    + " SELECT cod_perfil FROM usuario where cod_usuario = " + cod_usuario + ")) order by ordem ", "MODULO");
            foreach (DataRow row in dt_modulos.Rows)
            {
                list_modulos.Add(new Modulo
                {
                    cod_modulo = Convert.ToInt32(row["cod_modulo"].ToString()),
                    cod_modulo_pai = Convert.ToInt32(row["cod_modulo_pai"].ToString()),
                    descricao = row["descricao"].ToString(),
                    permisao = row["permisao"].ToString(),
                    route = row["route"].ToString(),
                    ordem = Convert.ToInt32(row["ordem"].ToString()),
                    ativo = Convert.ToBoolean(row["ativo"].ToString()),
                    filho = Convert.ToBoolean(row["filho"].ToString())
                });
            }

            return list_modulos;
        }

        public List<Permisao> Listar_permisao(int cod_perfil)
        {
            Conexao _conn = new Conexao();
            List<Permisao> list_permisao = new List<Permisao>();

            DataTable dt_permisao = _conn.dataTable("select descricao, permisao from modulo as m,acesso as a where "
                            + " m.cod_modulo = a.cod_modulo and a.cod_perfil = "+ cod_perfil +" and m.ativo = 1 ", "MODULO");
            foreach (DataRow row in dt_permisao.Rows)
            {
                list_permisao.Add(new Permisao
                {
                    descricao = row["descricao"].ToString(),
                    permisao = row["permisao"].ToString()
                });
            }

            return list_permisao;
        }

        //public Usuario Listar_Usuarios_dt(int cod_usuario)
        //{
        //    Conexao _conn = new Conexao();

        //    DataTable dt_usuarios = _conn.dataTable("select * from usuarios where CodigoUsuario = "+ cod_usuario +" ", "USUARIOS");
        //    Usuario usuario = new Usuario();
        //    if (dt_usuarios.Rows.Count > 0 )
        //    {
        //        usuario.Login = dt_usuarios.Rows[0]["login"].ToString();
        //        usuario.Nome = dt_usuarios.Rows[0]["nome"].ToString();
        //        usuario.cod_usuario = Convert.ToInt32(dt_usuarios.Rows[0]["CodigoUsuario"].ToString());
        //    }
        //    return usuario;
        //}

        public List<Modulo> Listar_modulo()
        {
            Conexao _conn = new Conexao();
            List<Modulo> list_modulos = new List<Modulo>();

            DataTable dt_modulos = _conn.dataTable("select * from modulo where ativo = 'true' order by ordem ", "MODULO");
            foreach (DataRow row in dt_modulos.Rows)
            {
                list_modulos.Add(new Modulo
                {
                    cod_modulo = Convert.ToInt32(row["cod_modulo"].ToString()),
                    cod_modulo_pai = Convert.ToInt32(row["cod_modulo_pai"].ToString()),
                    descricao = row["descricao"].ToString(),
                    permisao = row["permisao"].ToString(),
                    route = row["route"].ToString(),
                    ordem = Convert.ToInt32(row["ordem"].ToString()),
                    ativo = Convert.ToBoolean(row["ativo"].ToString()),
                    filho = Convert.ToBoolean(row["filho"].ToString())
                });
            }

            return list_modulos;
        }
        
    }
}
