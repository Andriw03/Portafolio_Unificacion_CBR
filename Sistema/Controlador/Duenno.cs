using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controlador
{
    class Duenno : Conexion
    {
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
            RutDuenno = string.Empty;
            PrimerNombre = string.Empty;
            SegundoNombre = string.Empty;
            PrimerApellido = string.Empty;
            SegundoApellido = string.Empty;
            CorreoElectronico = string.Empty;
            Telefono = string.Empty;
        }

        public bool BuscarDuenno(string rut)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM Cliente where RutCliente = '" + id + "' ", conex);
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
