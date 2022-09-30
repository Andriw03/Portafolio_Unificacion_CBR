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

        public bool ExisteRut(string id)
        {

            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT rut_usuario FROM UNIONLINE.USUARIO where rut_usuario = " + id + ";", conex);
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


        public DataTable MostrarSolicitud(string rut)
        {
            Conectar();
            DataTable tabla = new DataTable();
            tabla.Columns.Add(new DataColumn("Rut"));
            tabla.Columns.Add(new DataColumn("Nombre"));
            tabla.Columns.Add(new DataColumn("Apellido"));
            tabla.Columns.Add(new DataColumn("N Seguimiento"));
            tabla.Columns.Add(new DataColumn("Estado"));
            try
            {
                cmd = new MySqlCommand("SELECT rut_usuario, primer_nombre, primer_apellido, numero_seguimiento, estado FROM UNIONLINE.USUARIO join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario where rut_usuario or numero_seguimiento = '" + rut + "';", conex);
                rd = cmd.ExecuteReader();

              

                while (rd.Read())
                {
                    DataRow row;
                    row = tabla.NewRow();
                    row[0] = rd["rut_usuario"].ToString();
                    row[1] = rd["primer_nombre"].ToString();
                    row[2] = rd["primer_apellido"].ToString();
                    row[3] = rd["numero_seguimiento"].ToString();
                    row[4] = rd["estado"].ToString();
                    tabla.Rows.Add(row);
                }
                rd.Close();
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
