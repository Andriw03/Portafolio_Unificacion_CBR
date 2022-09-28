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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
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
            telefono = string.Empty;
            correo_electronico = string.Empty;
        }

        public string Insertar(Usuario usu)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new SqlCommand("insert into USUARIO values ('"+ usu.id_usuario + "','"  + usu.rut_usuario +  "," + usu.contrasenna + "','" + usu.primer_nombre + "','" + usu.segundo_nombre + "','" + usu.primer_apellido + "','" + usu.segundo_apellido + "','" + usu.telefono + "','" + usu.correo_electronico+")", conex);
                cmd.ExecuteNonQuery();
                salida = "Cliente agregado correctamente";
            }
            catch (Exception ex)
            {

                salida = "Error al agregar el Cliente: " + ex.ToString();
            }
            return salida;
        }
    }
    
    

}
