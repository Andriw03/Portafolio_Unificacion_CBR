using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Controlador
{
    public class Duenno : Conexion
    {
        public int duennoId { get; set; }
        public string RutDuenno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        public Duenno()
        {
            this.Init();
        }
        private void Init()
        {
            duennoId = 0;
            RutDuenno = string.Empty;
            PrimerNombre = string.Empty;
            SegundoNombre = string.Empty;
            PrimerApellido = string.Empty;
            SegundoApellido = string.Empty;
            CorreoElectronico = string.Empty;
            Telefono = string.Empty;
        }
        public Duenno BuscarDuenno(string rut)
        {
            try
            {

                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.DUENNO_PROP where rut_duenno = '" + rut + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    duennoId = Int32.Parse(rd["id_duenno"].ToString());
                    RutDuenno = rd["rut_duenno"].ToString();
                    PrimerNombre = rd["primer_nombre"].ToString();
                    SegundoNombre = rd["segundo_nombre"].ToString();
                    PrimerApellido = rd["primer_apellido"].ToString();
                    SegundoApellido = rd["segundo_apellido"].ToString();
                    CorreoElectronico = rd["correo_electronico"].ToString();
                    Telefono = rd["telefono"].ToString();
                }
                rd.Close();
                return this;


            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public string Insertar(Duenno due)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("insert into DUENNO_PROP (`rut_duenno`,`primer_nombre`,`segundo_nombre`,`primer_apellido`,`segundo_apellido`,`correo_electronico`,`telefono`) values ('" + due.RutDuenno + "','" + due.PrimerNombre + "','" + due.SegundoNombre + "','" + due.PrimerApellido + "','" + due.SegundoApellido + "','" + due.CorreoElectronico + "'," + due.Telefono + ")", conex);
                cmd.ExecuteNonQuery();
                salida = "Dueño agregado correctamente";
            }
            catch (Exception ex)
            {
                salida = "Error al agregar el Dueño: " + ex.ToString();
            }
            return salida;
        }

        public bool ExisteDuenno(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM DUENNO_PROP where rut_duenno = '" + id + "' ", conex);
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

    }
}
