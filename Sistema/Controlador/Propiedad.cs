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
    public class Propiedad : Conexion
    {
        public string Descripcion { get; set; }
        public int Direccion { get; set; }
        public int TipoPropiedad { get; set; }
        public int ClasPropiedad { get; set; }
        public int Duenno { get; set; }

        public Propiedad()
        {
            this.Init();
        }
        private void Init()
        {
            Descripcion = string.Empty;
            Direccion = 0;
            TipoPropiedad = 0;
            ClasPropiedad = 0;
            Duenno = 0;
        }
        public string Insertar(Propiedad prop , int foja)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`PROPIEDAD` (`descripcion`, `DIRECCION_id_direccion`, `TIPO_PROPIEDAD_id_tipoP`, `CLAS_PROP_id_clas`, `DUENNO_PROP_id_duenno`) VALUES ('"+ prop.Descripcion +"', "+ prop.Direccion +", "+ prop.TipoPropiedad +", "+ prop.ClasPropiedad +", "+prop.Duenno+");", conex);
                cmd.ExecuteNonQuery();
                string idProp = ""; 
                cmd = new MySqlCommand("SELECT id_propiedad FROM UNIONLINE.PROPIEDAD as prop inner join UNIONLINE.CLAS_PROP as cprop on prop.CLAS_PROP_id_clas = cprop.id_clas where cprop.foja = " + foja + ";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idProp = rd["id_propiedad"].ToString();
                }
                rd.Close();
                salida = idProp;
            }
            catch (Exception ex)
            {
                salida = "Error al agregar la Propiedad: " + ex.ToString();
            }
            return salida;
        }

        public bool ExistePropiedad(int id)
        {

            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.CLAS_PROP where CLAS_PROP.foja = "+ id +" ;", conex);
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
        public DataTable MostrarPropiedad(int foja)
        {
            Conectar();
            DataTable tabla = new DataTable();
            try
            {
                cmd = new MySqlCommand("SELECT foja, nombre_tipoP, concat(nombre_calle,' ', numero_casa ,', ', nombre_comuna, ', ', nombre_region) as direccion_prop, rut_duenno, concat(primer_nombre, ' ', primer_apellido) as nombre_duenno , correo_electronico, telefono, descripcion, razon_social, rut_empresa, id_propiedad FROM UNIONLINE.PROPIEDAD join UNIONLINE.DIRECCION on UNIONLINE.PROPIEDAD.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas join UNIONLINE.DUENNO_PROP on UNIONLINE.PROPIEDAD.DUENNO_PROP_id_duenno = UNIONLINE.DUENNO_PROP.id_duenno join UNIONLINE.TIPO_PROPIEDAD on UNIONLINE.PROPIEDAD.TIPO_PROPIEDAD_id_tipoP = UNIONLINE.TIPO_PROPIEDAD.id_tipoP join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia= UNIONLINE.PROVINCIA.id_provincia join UNIONLINE.REGION on UNIONLINE.PROVINCIA.REGION_id_region = UNIONLINE.REGION.id_region where foja = " + foja + " limit 1;", conex);
                MySqlDataAdapter ap = new MySqlDataAdapter(cmd);
                ap.Fill(tabla);
                cmd.Dispose();
                ap.Dispose();
                return tabla;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }
        public bool ModificarProp(string descripcion, int tipoPropiedad, string rutDuenno, int foja, string rutEmpresa, string razonSocial)
        {
            try
            {
                Conectar();
                
                int idDuenno = 0;
                cmd = new MySqlCommand("SELECT id_duenno FROM UNIONLINE.DUENNO_PROP where rut_duenno = '"+rutDuenno+"';",conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idDuenno = Int32.Parse(rd["id_duenno"].ToString()); 
                }
                rd.Close();
                int idPropiedad = 0;
                int idClas = 0;
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.PROPIEDAD inner join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas where foja = "+ foja +";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idPropiedad = Int32.Parse(rd["id_propiedad"].ToString());
                    idClas = Int32.Parse(rd["id_clas"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`PROPIEDAD` SET `descripcion` = '"+ descripcion +"', `TIPO_PROPIEDAD_id_tipoP` = " + tipoPropiedad + ", `DUENNO_PROP_id_duenno` = " + idDuenno + " WHERE `id_propiedad` = "+ idPropiedad +";", conex);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`CLAS_PROP` SET `razon_social` = '"+ razonSocial +"', `rut_empresa` = '"+ rutEmpresa +"' WHERE `id_clas` = "+ idClas +";", conex);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool EliminarProp(int foja)
        {
            Conectar();
            try
            {
                int idFoja = 0;
                int idPropiedad = 0;
                int idDireccion = 0;
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.PROPIEDAD inner join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas where foja = "+ foja +";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idFoja = Int32.Parse(rd["CLAS_PROP_id_clas"].ToString());
                    idPropiedad = Int32.Parse(rd["id_propiedad"].ToString());
                    idDireccion = Int32.Parse(rd["DIRECCION_id_direccion"].ToString());

                }
                rd.Close();
                cmd = new MySqlCommand("DELETE FROM `UNIONLINE`.`PROPIEDAD` WHERE id_propiedad = "+ idPropiedad +"; ",conex);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("DELETE FROM `UNIONLINE`.`CLAS_PROP` WHERE id_clas = "+ idFoja +";", conex);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("DELETE FROM `UNIONLINE`.`DIRECCION` WHERE id_direccion = "+ idDireccion +";", conex);
                cmd.ExecuteNonQuery();
                
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
