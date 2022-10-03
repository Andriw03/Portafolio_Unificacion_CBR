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
        public string Insertar(Propiedad prop)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`PROPIEDAD` (`descripcion`, `DIRECCION_id_direccion`, `TIPO_PROPIEDAD_id_tipoP`, `CLAS_PROP_id_clas`, `DUENNO_PROP_id_duenno`) VALUES ('"+ prop.Descripcion +"', "+ prop.Direccion +", "+ prop.TipoPropiedad +", "+ prop.ClasPropiedad +", "+prop.Duenno+");", conex);
                cmd.ExecuteNonQuery();
                salida = "Propiedad agregada correctamente";
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
                cmd = new MySqlCommand("SELECT foja, nombre_tipoP, concat(nombre_calle,' ', numero_casa ,', ', nombre_comuna, ', ', nombre_region) as direccion_prop, rut_duenno, concat(primer_nombre, ' ', primer_apellido) as nombre_duenno , correo_electronico, telefono, descripcion, razon_social, rut_empresa FROM UNIONLINE.PROPIEDAD join UNIONLINE.DIRECCION on UNIONLINE.PROPIEDAD.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas join UNIONLINE.DUENNO_PROP on UNIONLINE.PROPIEDAD.DUENNO_PROP_id_duenno = UNIONLINE.DUENNO_PROP.id_duenno join UNIONLINE.TIPO_PROPIEDAD on UNIONLINE.PROPIEDAD.TIPO_PROPIEDAD_id_tipoP = UNIONLINE.TIPO_PROPIEDAD.id_tipoP join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia= UNIONLINE.PROVINCIA.id_provincia join UNIONLINE.REGION on UNIONLINE.PROVINCIA.REGION_id_region = UNIONLINE.REGION.id_region where foja = " + foja + " limit 1;", conex);
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
    }
}
