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
using Controlador;

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_Trabajador_Dueño.xaml
    /// </summary>
    public partial class WPF_Trabajador_Dueño : Window
    {
        public WPF_Trabajador_Dueño()
        {
            InitializeComponent();
            Conectar();
        }

        Conexion con = new Conexion();
        private void Conectar()
        {
            if (con.Conectar())
            {

            }
            else
            {
                MessageBox.Show("Error Conexión");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRutDuenno.Text != string.Empty && txtPrimerNombre.Text != string.Empty && txtSegundoNombre.Text != string.Empty && txtPrimerApellido.Text != string.Empty && txtSegundoApellido.Text != string.Empty && txtCorreo != null && txtTelefono != null )
            {
                try
                {
                    Duenno due = new Duenno();
                    if (!due.ExisteDuenno(txtRutDuenno.Text))
                    {
                        due.RutDuenno = txtRutDuenno.Text;
                        due.PrimerNombre = txtPrimerNombre.Text;
                        due.SegundoNombre = txtSegundoNombre.Text;
                        due.PrimerApellido = txtPrimerApellido.Text;
                        due.SegundoApellido = txtSegundoApellido.Text;
                        due.CorreoElectronico = txtCorreo.Text;
                        due.Telefono = txtTelefono.Text;
                        MessageBox.Show(due.Insertar(due),"Mensaje:");
                        limpiarDuenno();
                    }
                    else
                    {
                        MessageBox.Show("Error, El Dueño ya se encuentra registrado.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al ingresar el Dueño:" + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Error, Debe ingresar los campos por favor.");
            }
        }
        private void limpiarDuenno()
        {
            txtRutDuenno.Text = string.Empty;
            txtPrimerNombre.Text = string.Empty;
            txtSegundoNombre.Text = string.Empty;
            txtPrimerApellido.Text = string.Empty;
            txtSegundoApellido.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            txtTelefono.Text = string.Empty;
        }
    }
}
