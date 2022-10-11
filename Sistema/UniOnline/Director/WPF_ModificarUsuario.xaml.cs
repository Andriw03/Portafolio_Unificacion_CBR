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
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Security.Cryptography;


namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_ModificarUsuario.xaml
    /// </summary>
    public partial class WPF_ModificarUsuario : Window
    {
        public WPF_ModificarUsuario()
        {
            InitializeComponent();
        }

        private void rdBtn_Recepcionista_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txtBoxRUT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxSNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnBuscarRUT_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxRUT.Text != string.Empty)
            {
                Hashing hash = new Hashing();
                Usuario usu = new Usuario();
                usu = usu.BuscarUsuario(txtBoxRUT.Text);
                if (usu != null)
                {
                    txtBoxNombre.Text = usu.primer_nombre;
                    txtBoxSNombre.Text = usu.segundo_nombre;
                    txtBoxApellido.Text = usu.primer_apellido;
                    txtBoxSApellido.Text = usu.segundo_apellido;
                    txtBoxTelefono.Text = usu.telefono;
                    txtBoxCorreo.Text = usu.correo_electronico;
                    txtBoxContra.Text =usu.contrasenna;
                    if (usu.id_tipoU == 4)
                    {
                        rdBtn_Trabajador.IsChecked = true;
                    }
                    else if (usu.id_tipoU == 3)
                    {
                        rdBtn_Recepcionista.IsChecked = true;
                    }
                    else if (usu.id_tipoU == 6)
                    {
                        rdBtn_Moderador.IsChecked = true;
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no existe", "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe digitar un Rut para usar esta función", "Error");
            }


        }

        private void txtBoxApellido_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxSApellido_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxContra_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            {
                if (txtBoxRUT.Text != string.Empty && txtBoxContra.Text != string.Empty && txtBoxNombre.Text != string.Empty && txtBoxSNombre.Text != string.Empty && txtBoxApellido.Text != string.Empty && txtBoxSApellido.Text != string.Empty && txtBoxCorreo.Text != string.Empty && txtBoxTelefono.Text != string.Empty)
                {
                    Usuario usu = new Usuario();
                    int idTUser = 0;
                    if ((bool)rdBtn_Recepcionista.IsChecked)
                    {
                        idTUser = 3;
                    }
                    else if ((bool)rdBtn_Moderador.IsChecked)
                    {
                        idTUser = 6;
                    }
                    else if ((bool)rdBtn_Trabajador.IsChecked)
                    {
                        idTUser = 4;
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un rol");
                    }
                    Hashing hash = new Hashing();
                    if (usu.ModificarUsuario(txtBoxRUT.Text, txtBoxNombre.Text, txtBoxSNombre.Text, txtBoxApellido.Text, txtBoxSApellido.Text, txtBoxTelefono.Text, txtBoxCorreo.Text, hash.ToSHA512(txtBoxContra.Text), idTUser))

                    {

                        MessageBox.Show("Usuario Modificado con Éxito");

                    }
                    else
                    {
                        MessageBox.Show("Error en el Update", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Rellene todos los campos", "Advertencia");
                }
            }
        }
    }
}