using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows;
using System.Data;
using System.Windows.Forms;

namespace Controlador
{
    public class Consultas_seg : Conexion
    {
        public int id_usuario { get; set; }
        public string rut_usuario { get; set; }
        public string primer_nombre { get; set; }
        public string primer_apellido { get; set; }

        public string numero_seguimiento { get; set; }

        public string estado { get; set; }



        public Consultas_seg()
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
                cmd = new MySqlCommand("SELECT * FROM Usuario where rut_usuario = '" + id + "' ", conex);
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
        public Consultas_seg BuscarDatos(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO inner join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    rut_usuario = rd["rut_usuario"].ToString();
                    primer_nombre = rd["primer_nombre"].ToString();
                    primer_apellido = rd["primer_apellido"].ToString();
                    numero_seguimiento =rd["numero_seguimiento"].ToString();
                    estado = rd["estado"].ToString();


                }
                rd.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            return this;
        }
        public DataTable MostrarSoli(string tab, int id, int id1, int cod)
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
                if (cod == 1)
                {
                    cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO inner join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario" + tab + " = '" + id + "'", conex);
                }
                else
                {
                    cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO inner join UNIONLINE.SOLICITUD on UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario" + tab + " = " + id1 + "", conex);
                }

                rd = cmd.ExecuteReader();
                List<Consultas_seg> lis = new List<Consultas_seg>();
                while (rd.Read())
                {
                    Consultas_seg con = new Consultas_seg();
                    con.rut_usuario = rd["rut_usuario"].ToString();
                    con.primer_nombre = rd["primer_nombre"].ToString();
                    con.primer_apellido = rd["primer_apellido"].ToString();
                    con.numero_seguimiento = rd["numero_seguimiento"].ToString();
                    con.estado = rd["estado"].ToString();

                    rd.Close();

                    for (int i = 0; i < lis.Count(); i++)
                    {
                        DataRow row;
                        row = tabla.NewRow();
                        row[0] = lis[i].rut_usuario;
                        row[1] = lis[i].primer_nombre;
                        row[2] = lis[i].primer_apellido;
                        row[3] = lis[i].numero_seguimiento;
                        row[4] = lis[i].estado;


                    }

                    return tabla;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }


        }
    }
            
}
