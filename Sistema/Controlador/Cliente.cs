using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Controlador
{
    public class Cliente : Conexion
    {
        public string rut_usuario { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }
        public int telefono { get; set; }

        public Cliente()
        {
            this.Init();
        }

        private void Init()
        {
            rut_usuario = string.Empty;
            primer_nombre = string.Empty;
            segundo_nombre = string.Empty;
            primer_apellido = string.Empty;
            segundo_apellido = string.Empty;
            correo_electronico = string.Empty;
            telefono = 0;
        }

        public bool ExisteCliente(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + id + "' ", conex);
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

        public Cliente BuscarCliente(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + id + "' and T_USUARIO_id_tipoU = 5;");
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    rut_usuario = rd["rut_usuario"].ToString();
                    primer_nombre = rd["primer_nombre"].ToString();
                    segundo_nombre = rd["segundo_nombre"].ToString();
                    primer_apellido = rd["primer_apellido"].ToString();
                    segundo_apellido = rd["segundo_apellido"].ToString();
                    correo_electronico = rd["correo_electronico"].ToString();
                    telefono = int.Parse(rd["telefono"].ToString());
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return this;
        }

    }


}
