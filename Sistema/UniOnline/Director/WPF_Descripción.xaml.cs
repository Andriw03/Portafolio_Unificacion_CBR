using Controlador;
using System;
using System.Collections.Generic;
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

namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_Descripción.xaml
    /// </summary>
    public partial class Descripción : Window
    {
        public Descripción()
        {
            InitializeComponent();
        }

        string usu = string.Empty;
        public string ObtenerFormulario
        {
            get
            {
                return this.usu;
            }
            set
            {
                this.usu = value;
                this.usu = value;
                LlenadoDetalle(usu);

                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
        private void LlenadoDetalle(string idFor)
        {
            try
            {
                Usuario usua = new Usuario();
                List<string> formulario = usua.Descripcion(idFor);
                txtRut.Text = formulario[0];
                txtNombre.Text = formulario[1];
                txtAsunto.Text = formulario[2];
                txtDetalle.Text = formulario[3];
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al cargar detalle: " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WPF_ModificarUsuario modUsus = new WPF_ModificarUsuario();
            modUsus.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usua = new Usuario();
                if (usua.AprobarSolicitud(usu))
                {
                    MessageBox.Show("Solicitud Aprobada");
                }
                else 
                {
                    MessageBox.Show("Error al aprobar solicitud");
                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalle: " + ex.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usua = new Usuario();
                if (usua.RechazarSolicitud(usu))
                {
                    MessageBox.Show("Solicitud Rechazada");
                }
                else
                {
                    MessageBox.Show("Error al rechazar solicitud");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalle: " + ex.Message);
            }
        }
    }
}
