using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Controlador
{
    public class Conexion
    {
        public MySqlConnection conex { get; set; }
        public MySqlCommand cmd { get; set; }
        public MySqlDataReader rd { get; set; }
        public MySqlDataAdapter adapter { get; set; }

        public Conexion()
        {
            this.Init();
        }

        private void Init()
        {
            conex = null;
            cmd = null;
            rd = null;
            adapter = null;
        }

        public bool Conectar()
        {
            try
            {
                conex = new MySqlConnection("server=unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com;user id=root;password=nohomo123;persistsecurityinfo=True;database=UNIONLINE");
                conex.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }


            //Método para Buscar en la base de datos una columna de una tabla
            public List<string> Llenado(string tab, string colum)
        {
            List<string> registro = new List<string>();
            try
            {
                cmd = new MySqlCommand("SELECT "+colum+" FROM UNIONLINE."+tab+";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd[colum].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //Método para Buscar en la base de datos una columna de una tabla con un where = INT
        public List<string> LlenadoWhereInt(string tab, string colum, string colum2, int filtro)
        {
            List<string> registro = new List<string>();
            try
            {
                cmd = new MySqlCommand("SELECT " + colum + " FROM UNIONLINE." + tab + " where "+colum2+" = "+filtro+";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd[colum].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                registro.Add("Error BD");
                registro.Add(ex.Message);
                return registro;
            }
        }
        //Método para Buscar en la base de datos una columna de una tabla con un where = String
        public List<string> LlenadoWhereString(string tab, string colum, string colum2, string filtro)
        {
            List<string> registro = new List<string>();
            try
            {
                cmd = new MySqlCommand("SELECT " + colum + " FROM UNIONLINE." + tab + " where " + colum2 + " = '" + filtro + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd[colum].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                registro.Add("Error BD");
                registro.Add(ex.Message);
                return registro;
            }
        }
        public int InsertDireccion(string nombreCalle, int numeroCasa, String comuna)
        {
            try
            {
                string idComuna = string.Empty;
                cmd = new MySqlCommand("SELECT id_comuna FROM UNIONLINE.COMUNA where nombre_comuna = '"+comuna+"';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idComuna = rd["id_comuna"].ToString();
                }
                rd.Close();
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`DIRECCION` (`nombre_calle`, `numero_casa`, `COMUNA_id_comuna`) VALUES ( '"+ nombreCalle + "',"+ numeroCasa + ", "+ Int32.Parse(idComuna) +");", conex);
                cmd.ExecuteNonQuery();
                string idDireccion = string.Empty;
                cmd = new MySqlCommand("SELECT id_direccion FROM UNIONLINE.DIRECCION order by id_direccion desc limit 1;", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idDireccion = rd["id_direccion"].ToString();
                }
                rd.Close();
                return Int32.Parse(idDireccion);
            }
            catch
            {
                return 0;
            }
        }
        public int InsertClasProp(int foja, int numero, string anno, string razonSocial, string rutEmpresa)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`CLAS_PROP`(`foja`, `numero`, `anno`, `razon_social`, `rut_empresa`) VALUES ("+ foja + ", " + numero + ", '"+ anno +"', '"+razonSocial+ "', '" + rutEmpresa + "');", conex);
                cmd.ExecuteNonQuery();
                string idClasProp = string.Empty;
                cmd = new MySqlCommand("SELECT id_clas FROM UNIONLINE.CLAS_PROP where foja = "+ foja +";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idClasProp = rd["id_clas"].ToString();
                }
                rd.Close();
                return Int32.Parse(idClasProp);
            }
            catch
            {
                return 0;
            }
        }
        
    }
}
