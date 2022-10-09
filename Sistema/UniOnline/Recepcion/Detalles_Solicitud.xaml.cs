using Controlador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UniOnline.Recepcion
{
    /// <summary>
    /// Lógica de interacción para Detalles_Solicitud.xaml
    /// </summary>
    public partial class Detalles_Solicitud : Window
    {
        public Detalles_Solicitud()
        {
            InitializeComponent();
            
        }
        
        Recepcionista recep = new Recepcionista();
        public Recepcionista ObtenerUsuarioRec
        {
            get
            {
                return this.recep;
            }
            set
            {
                this.recep = value;
                this.recep = value;


                lblNombreCompleto.Content = "Nombre Completo: " + recep.primer_nombre + " " + recep.segundo_nombre + " " + recep.primer_apellido + " " + recep.segundo_apellido + " ";
                lblFechaSoli.Content = "Fecha Solicitud: " + recep.fecha_solicitud;
                lblNombreT.Content = "Nombre del Trámite: " + recep.nombre_tramite;
                lblEstado.Content = "Estado Solicitud: " + recep.estado;
                lblIdSoli.Content = "Número Solicitud: " + recep.id_soli;
                lblNseg.Content = "Número Seguimiento: " + recep.numero_seguimiento;
                lblValorT.Content = "Valor Pagado : " + recep.valor_tramite;



            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}
