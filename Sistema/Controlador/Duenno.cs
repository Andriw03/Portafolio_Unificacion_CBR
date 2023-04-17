using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Windows;

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

        public String Formatear(String rut)
        {
            int cont = 0;
            String format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                format = "-" + rut.Substring(rut.Length - 1);
                for (int i = rut.Length - 2; i >= 0; i--)
                {

                    format = rut.Substring(i, 1) + format;

                    cont++;
                    if (cont == 3 && i != 0)
                    {
                        format = "." + format;
                        cont = 0;
                    }
                }
                return format;
            }
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
                MessageBox.Show(ex.Message);
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
                string idDuenno = "";
                cmd = new MySqlCommand("SELECT id_duenno FROM UNIONLINE.DUENNO_PROP where rut_duenno = '" + due.RutDuenno + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idDuenno = rd["id_duenno"].ToString();
                }
                rd.Close();
                salida = idDuenno;
            }
            catch (Exception ex)
            {
                salida = "Error al agregar el Dueño: " + ex.Message;
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
                MessageBox.Show(ex.Message);
                return true;

            }
        }

        public bool CorreoValido(string correo)
        {
            Regex correoregex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);

            return correoregex.IsMatch(correo);
        }

        public bool ModificarDuenno(string rutDuenno, string PNombreDuenno, string SNombreDuenno, string PApellidoDuenno, string SApellidoDuenno, string CorreoDuenno, string TelefonoDuenno)
        {
            try
            {
                Conectar();
                int idDuenno = 0;
                cmd = new MySqlCommand("SELECT id_duenno FROM UNIONLINE.DUENNO_PROP where rut_duenno = '" + rutDuenno + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idDuenno = Int32.Parse(rd["id_duenno"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`DUENNO_PROP` SET `primer_nombre` = '" + PNombreDuenno + "', `segundo_nombre` = '" + SNombreDuenno + "', `primer_apellido` = '" + PApellidoDuenno + "', `segundo_apellido` = '" + SApellidoDuenno + "', `correo_electronico` = '" + CorreoDuenno + "', `telefono` = '" + TelefonoDuenno + "' WHERE `id_duenno` = '" + idDuenno + "'; ", conex);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool EliminarDuenno(string rutDuen)
        {
            Conectar();
            try
            {
                int idDuenno = 0;
                cmd = new MySqlCommand("SELECT id_duenno FROM UNIONLINE.DUENNO_PROP where rut_duenno = '" + rutDuen + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idDuenno = Int32.Parse(rd["id_duenno"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("DELETE FROM `UNIONLINE`.`DUENNO_PROP` WHERE id_duenno = " + idDuenno + ";", conex);
                cmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
