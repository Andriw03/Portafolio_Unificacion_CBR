using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controlador;
using MessageBox = System.Windows.MessageBox;

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_VisorSolicitud.xaml
    /// </summary>
    public partial class WPF_VisorSolicitud : Window
    {

        public WPF_VisorSolicitud()
        {
            InitializeComponent();
        }
        string nSeguimiento = string.Empty;
        public string ObtenerSeguimiento
        {
            get
            {
                return this.nSeguimiento;
            }
            set
            {
                this.nSeguimiento = value;
                this.nSeguimiento = value;
                LlenadoEstado();
                LlenadoSolicitud(nSeguimiento);
            }
        }
        //Metodo que llena el combobox estado
        public void LlenadoEstado()
        {
            cmbEstado.Items.Add("Aprobado");
            cmbEstado.Items.Add("Rechazado");
            cmbEstado.Items.Add("En Proceso");
        }
        //Metodo que llena los datos de la solicitud
        public void LlenadoSolicitud(string numeroS)
        {
            Cliente cli = new Cliente();
            List<string> soli = new List<string>();
            soli = cli.Solicitud(numeroS);
            if (soli != null)
            {
                lbNumeroSO.Text = soli[0];
                lbFecha.Text = soli[2];
                cmbEstado.SelectedItem = soli[1];
                lbRutSolicitante.Text = soli[3];
                lbNombreSolicitante.Text = soli[4];
                lbFoja.Text = soli[6];
                lbDireccion.Text = soli[8];
                lbDescripcion.Text = soli[7];
                lbNombreTramite.Text = soli[5];
                lbNombreDuenno.Text = soli[9];
                txtComentario.Text = soli[11];
                List<string> doc = cli.Documento(Int32.Parse(soli[10]));
                if (doc.Count != 0)
                {
                    int cant = doc.Count;
                    btnVerDoc.Visibility = Visibility.Visible;
                    lbDocumento.Visibility = Visibility.Visible;
                    lbNombreDoc.Visibility = Visibility.Visible;
                    lbNombreDoc.Text = "Hay " + (cant/2) +" Documentos";
                }
                else
                {
                    btnVerDoc.Visibility = Visibility.Hidden;
                    lbDocumento.Visibility = Visibility.Hidden;
                    lbNombreDoc.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                MessageBox.Show("Solicitud Vacía");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVerDoc_Click(object sender, RoutedEventArgs e)
        {
            WPF_VistaDoc vDoc = new WPF_VistaDoc();
            vDoc.ShowDialog();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cli = new Cliente();
            if (cli.ModificarSolicitud(lbNumeroSO.Text, cmbEstado.SelectedItem.ToString(),txtComentario.Text))
            {
                MessageBox.Show("Solicitud Guardada con éxito");
            }
            else
            {
                MessageBox.Show("Error al guardar la solicitud","Error");
            }
        }
    }    
}
