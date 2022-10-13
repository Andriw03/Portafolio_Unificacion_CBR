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
    public partial class WPF_Director : Window
    {
        Conexion con = new Conexion();
        public WPF_Director()
        {
            InitializeComponent();
            Conectar();

        }
        //Conexión BD
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

        private void btnRegistrarUser_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxRUT.Text != string.Empty && txtBoxContra.Text != string.Empty && txtBoxNombre.Text != string.Empty && txtBoxSNombre.Text != string.Empty && txtBoxApellido.Text != string.Empty && txtBoxSApellido.Text != string.Empty && txtBoxCorreo.Text != string.Empty && txtBoxTelefono.Text != string.Empty)
            {
                try
                {
                    Hashing hash = new Hashing();
                    Usuario usu = new Usuario();
                    if (!usu.ExisteUsuario(txtBoxRUT.Text))
                    {
                        usu.rut_usuario = txtBoxRUT.Text;
                        usu.contrasenna = hash.ToSHA256(txtBoxContra.Text);
                        usu.primer_nombre = txtBoxNombre.Text;
                        usu.segundo_nombre = txtBoxSNombre.Text;
                        usu.primer_apellido = txtBoxApellido.Text;
                        usu.segundo_apellido = txtBoxSApellido.Text;
                        usu.correo_electronico = txtBoxCorreo.Text;
                        usu.telefono = txtBoxTelefono.Text;
                        usu.id_cbr = 1;
                        if (rdBtn_Recepcionista.IsChecked == true)
                        {
                            usu.id_tipoU = 3;
                            MessageBox.Show(usu.Insertar(usu), "Mensaje");
                        }
                        else if (rdBtn_Trabajador.IsChecked == true)
                        {
                            usu.id_tipoU = 4;
                            MessageBox.Show(usu.Insertar(usu), "Mensaje");
                        }
                        else if (rdBtn_Moderador.IsChecked == true)
                        {
                            usu.id_tipoU = 6;
                            MessageBox.Show(usu.Insertar(usu), "Mensaje");
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error El Usuario ya se encuentra registrado");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al ingresar el Usuario:" + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Error, debe ingresar los campos porfavor");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxSApellido_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxSNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {

        }

        private void rdBtn_Recepcionista_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rdBtn_Recepcionista_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
