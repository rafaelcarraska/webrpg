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
    public class UsuarioDao
    {
        public List<Usuario> Listar_Usuarios()
        {
            Conexao _conn = new Conexao();
            List<Usuario> list_usuarios = new List<Usuario>();

            DataTable dt_usuarios = _conn.dataTable("select * from usuario ", "USUARIOS");
            foreach (DataRow row in dt_usuarios.Rows)
            {
                list_usuarios.Add(new Usuario
                {
                    Cod_Usuario = Convert.ToInt32(row["cod_usuario"].ToString()),
                    Nome = row["nome"].ToString(),
                    Cod_Perfil = Convert.ToInt32(row["Cod_Perfil"].ToString()),
                    Login = row["login"].ToString(),
                    Email = row["Email"].ToString(),
                    Ativo = Convert.ToBoolean(row["Ativo"].ToString())
                    
                });
            }
            
            return list_usuarios;
        }



        public Usuario Listar_Usuarios_dt(int cod_usuario)
        {
            Conexao _conn = new Conexao();

            DataTable dt_usuarios = _conn.dataTable("select * from usuario where cod_usuario = " + cod_usuario + " ", "USUARIOS");
            Usuario usuario = new Usuario();
            if (dt_usuarios.Rows.Count > 0 )
            {
                usuario.Login = dt_usuarios.Rows[0]["login"].ToString();
                usuario.Nome = dt_usuarios.Rows[0]["nome"].ToString();
                usuario.Cod_Usuario = Convert.ToInt32(dt_usuarios.Rows[0]["cod_usuario"].ToString());
            }
            return usuario;
        }

        //public List<Usuario> Listar_Empresa_pai(int Cod_Empresa_pai)
        //{
        //    Conexao _conn = new Conexao();
        //    List<Usuario> list_usuarios = new List<Usuario>();

        //    DataTable dt_usuarios = _conn.dataTable("select cod_empresa, NOME_RAZAO_SOCIAL from cad_empresas  where cod_empresa_pai = " + Cod_Empresa_pai + " ", "USUARIOS");
        //    foreach (DataRow row in dt_usuarios.Rows)
        //    {
        //        list_usuarios.Add(new Usuario
        //        {
        //            Cod_Empresa = Convert.ToInt32(row["cod_empresa"].ToString()),
        //            Nome_Razao_Social = row["Nome_Razao_Social"].ToString()
        //        });
        //    }

        //    return list_usuarios;
        //}

        public Usuario busca(String login, String senha)
        {

            Conexao _conn = new Conexao();

            senha = getMD5Hash(senha);
            DataTable dt_usuarios = _conn.dataTable("SELECT cod_usuario, login, nome, cod_perfil FROM usuario where ativo = 1 and login = '" + login.Replace("'", "''") + "' and senha = '" + senha.Replace("'", "''") + "' ", "USUARIOS");
            Usuario usuario = new Usuario();
            if (dt_usuarios.Rows.Count > 0)
            {
                usuario.Login = dt_usuarios.Rows[0]["login"].ToString();
                usuario.Nome = dt_usuarios.Rows[0]["nome"].ToString();
                usuario.Cod_Usuario = Convert.ToInt32(dt_usuarios.Rows[0]["cod_usuario"].ToString());
                usuario.Cod_Perfil = Convert.ToInt32(dt_usuarios.Rows[0]["cod_perfil"].ToString());
            }
            else
            {
                usuario = null;
            }
            return usuario;
        }

        //public string Empresa(int Cod_Empresa)
        //{
        //    Conexao _conn = new Conexao();

        //    return _conn.scalar("select Nome_Razao_Social from CAD_EMPRESAS where Cod_Empresa = " + Cod_Empresa + " ").ToString();  
        //}

        public string getMD5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }

    }
}
