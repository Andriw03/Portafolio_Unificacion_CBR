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
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string numero_seguimiento { get; set; }
        public string estado { get; set; }

        public string fecha_solicitud { get; set; }
        public string fecha_cierre { get; set; }
        public int id_soli { get; set; }

        public string nombre_tramite { get; set; }
        public string valor_tramite { get; set; }

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
            segundo_nombre = string.Empty;
            segundo_apellido = string.Empty;

            numero_seguimiento = string.Empty;
            estado = string.Empty;
            fecha_solicitud = string.Empty;
            fecha_cierre = string.Empty;
            id_soli = 0;
            nombre_tramite = string.Empty;
            valor_tramite = string.Empty;

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

        public DataTable MostrarTodasSolicitud(string rut)
        {
            Conectar();
            DataTable tabla = new DataTable();


            try
            {
                cmd = new MySqlCommand("SELECT rut_usuario, primer_nombre, primer_apellido, numero_seguimiento, SOLICITUD.estado FROM UNIONLINE.USUARIO join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario inner join UNIONLINE.CAR_COMPRA on UNIONLINE.SOLICITUD.id_soli = UNIONLINE.CAR_COMPRA.SOLICITUD_id_soli where SOLICITUD.estado = 'En Proceso';", conex);
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

        public DataTable MostrarSolicitud(string rut)
        {
            Conectar();
            DataTable tabla = new DataTable();

           
            try
            {
                cmd = new MySqlCommand("SELECT rut_usuario, primer_nombre, primer_apellido, numero_seguimiento, SOLICITUD.estado FROM UNIONLINE.USUARIO join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario inner join UNIONLINE.CAR_COMPRA on UNIONLINE.SOLICITUD.id_soli = UNIONLINE.CAR_COMPRA.SOLICITUD_id_soli where CAR_COMPRA.estado = 1 and rut_usuario = '" + rut + "' or numero_seguimiento = '" + rut + "';", conex);
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

        public Recepcionista ObtenerDatos(string rut, string nseg)
        {
            Recepcionista recep = new Recepcionista();


            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO inner join UNIONLINE.SOLICITUD join UNIONLINE.TRAMITE on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario where rut_usuario = '" + rut + "' or numero_seguimiento = '" + nseg + "'", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    //hacer cosulta de los datos de detalle solicitud.

                    recep.primer_nombre = rd["primer_nombre"].ToString();
                    recep.segundo_nombre = rd["segundo_nombre"].ToString();
                    recep.primer_apellido = rd["primer_apellido"].ToString();
                    recep.segundo_apellido = rd["segundo_apellido"].ToString();
                    recep.segundo_apellido = rd["segundo_apellido"].ToString();
                    recep.fecha_solicitud = rd["fecha_solicitud"].ToString();
                    recep.nombre_tramite = rd["nombre_tramite"].ToString();
                    recep.estado = rd["estado"].ToString();
                    recep.id_soli = Int32.Parse(rd["id_soli"].ToString());
                    recep.numero_seguimiento = rd["numero_seguimiento"].ToString();
                    recep.valor_tramite = rd["valor_tramite"].ToString();



                }
                rd.Close();
                return recep;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }






    }





}
