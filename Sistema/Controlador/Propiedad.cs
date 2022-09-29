using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool ExistePropiedad(string id)
        {

            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.PROPIEDAD INNER JOIN UNIONLINE.CLAS_PROP ON PROPIEDAD.CLAS_PROP_id_clas = CLAS_PROP.id_clas where CLAS_PROP.foja = "+id+";", conex);
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
