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
        
    }
}
