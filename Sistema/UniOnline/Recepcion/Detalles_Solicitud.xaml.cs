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
        
        Recepcionista rec = new Recepcionista();
        public Recepcionista ObtenerUsuarioRec
        {
            get
            {
                return this.rec;
            }
            set
            {
                this.rec = value;
                this.rec = value;
             

                lblNombreCompleto.Content = "Nombre Completo: " + rec.primer_nombre + " " + rec.segundo_nombre + " " + rec.primer_apellido + " " + rec.segundo_apellido + " ";
                lblFechaSoli.Content = "Fecha Solicitud: "+ rec.fecha_solicitud;
                lblFechaCierre.Content = "Fecha Cierre: " + rec.fecha_cierre;
                lblEstado.Content = "Estado Solicitud: " + rec.estado;
                lblIdSoli.Content = "Número Solicitud: " + rec.id_soli;
                lblNseg.Content = "Número Seguimiento: "+ rec.numero_seguimiento;

                

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}
