using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Controlador
{
    public class Recepcionista : Conexion
    {
        public int id_usuario { get; set; }
        public string rut_usuario { get; set; }
        public string primer_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string numero_seguimiento { get; set; }
        public string estado { get; set; }


        public Recepcionista()
        {
            this.Init();
        }

        private void Init()
        {
            id_usuario = 0;
            rut_usuario = string.Empty;
            primer_nombre = string.Empty;
            primer_apellido = string.Empty;

            numero_seguimiento = string.Empty;
            estado = string.Empty;

        }

        public bool ExisteID(int id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT id_usuario FROM UNIONLINE.USUARIO inner join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario where rut_usuario = " + id + ";", conex);
                rd = cmd.ExecuteReader();
                bool e = rd.Read();
                rd.Close();
                return e;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        public bool ExisteRut(string id)
        {
             

            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO inner join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario where rut_usuario or numero_seguimiento = '" + id + "' ;", conex);
                rd = cmd.ExecuteReader();
                bool e = rd.Read();
                rd.Close();
                return e;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public DataTable MostrarSolicitud(string rut, string nseg)
        {
            Conectar();
            DataTable tabla = new DataTable();

           
            try
            {
                cmd = new MySqlCommand("SELECT rut_usuario, primer_nombre, primer_apellido, numero_seguimiento, estado FROM UNIONLINE.USUARIO join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario where rut_usuario or numero_seguimiento = '" + rut + "'  '" + nseg + "';", conex);
                MySqlDataAdapter ap = new MySqlDataAdapter(cmd);
                ap.Fill(tabla);
                cmd.Dispose();
                ap.Dispose();
                return tabla;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }






    }





}
