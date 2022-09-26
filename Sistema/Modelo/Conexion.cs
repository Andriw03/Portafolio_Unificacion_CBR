using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Modelo
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
                return false;
            }
        }
        
    }
}
