using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using MySqlConnector;
using MySql.Data;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;

namespace Controlador
{
    public class Usuario : Conexion
    {
        public int id_usuario { get; set; }
        public string rut_usuario { get; set; }
        public string contrasenna { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public int id_cbr { get; set; }
        public int id_tipoU { get; set; }

        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            id_usuario = 0;
            rut_usuario = string.Empty;
            contrasenna = string.Empty;
            primer_nombre = string.Empty;
            segundo_nombre = string.Empty;
            primer_apellido = string.Empty;
            segundo_apellido = string.Empty;
            correo_electronico = string.Empty;
            telefono = string.Empty;
            id_cbr = 1;
            id_tipoU = 0;
        }

        public string Insertar(Usuario usu)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("insert into USUARIO (`rut_usuario`,`contrasenna`,`primer_nombre`,`segundo_nombre`,`primer_apellido`,`segundo_apellido`,`correo_electronico`,`telefono`,`CBR_id_cbr`,`T_USUARIO_id_tipoU`) VALUES ('" + usu.rut_usuario + "','" + usu.contrasenna + "','" + usu.primer_nombre + "','" + usu.segundo_nombre + "','" + usu.primer_apellido + "','" + usu.segundo_apellido + "','" + usu.correo_electronico + "','" + usu.telefono + "'," + usu.id_cbr + "," + usu.id_tipoU + ")", conex);
                cmd.ExecuteNonQuery();
                salida = "Usuario agregado correctamente.";
            }
            catch (Exception ex)
            {
                salida = "Error al agregar el Cliente: " + ex.ToString();
            }
            return salida;
        }
        public bool ExisteUsuario(string id)
        {

            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM USUARIO where rut_usuario = '" + id + "' ", conex);
                rd = cmd.ExecuteReader();
                bool e = rd.Read();
                rd.Close();
                return e;

            }
            catch (Exception ex)
            {
                return true;

            }
        }
        public Usuario LoginUsuario( string rut)
        {
            Usuario usuario = new Usuario();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT id_usuario, rut_usuario, contrasenna, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, correo_electronico, telefono, CBR_id_cbr, T_USUARIO_id_tipoU FROM UNIONLINE.USUARIO where rut_usuario = '"+ rut +"';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    usuario.id_usuario = Int32.Parse(rd["id_usuario"].ToString());
                    usuario.rut_usuario = rd["rut_usuario"].ToString();
                    usuario.contrasenna = rd["contrasenna"].ToString();
                    usuario.primer_nombre = rd["primer_nombre"].ToString();
                    usuario.segundo_nombre = rd["segundo_nombre"].ToString();
                    usuario.primer_apellido = rd["primer_apellido"].ToString();
                    usuario.segundo_apellido = rd["segundo_apellido"].ToString();
                    usuario.correo_electronico = rd["correo_electronico"].ToString();
                    usuario.telefono = rd["telefono"].ToString();
                    usuario.id_cbr = Int32.Parse(rd["CBR_id_cbr"].ToString());
                    usuario.id_tipoU = Int32.Parse(rd["T_USUARIO_id_tipoU"].ToString());
                }
                rd.Close();
                return usuario;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }



    }



}