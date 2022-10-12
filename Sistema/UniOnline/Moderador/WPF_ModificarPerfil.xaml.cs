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
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_ModificarPerfil.xaml
    /// </summary>
    public partial class WPF_ModificarPerfil : Window
    {

        DataTable dt = new DataTable();
        public WPF_ModificarPerfil()
        {
            InitializeComponent();
        }

        Conexion con = new Conexion();
        private void Conectar()
        {
            if (con.Conectar())
            {
                MessageBox.Show("Vas bien mi rey");
            }
            else
            {
                MessageBox.Show("Error Conexión");
            }
        }


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente();
                cli.BuscarCliente(txtRutCliente.Text);

                if (txtRutCliente.Text != cli.rut_usuario)
                {
                    txtPrimerNom.Text = cli.primer_nombre;
                    txtSegundoNom.Text = cli.segundo_nombre;
                    txtPrimerApe.Text = cli.primer_apellido;
                    txtSegundoApe.Text = cli.segundo_apellido;
                    txtCorreo.Text = cli.correo_electronico;
                    txtTel.Text = cli.telefono.ToString();

                }
                else
                {
                    MessageBox.Show("Ingrese rut valido");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente();
                Duenno due = new Duenno();
                string mail = txtCorreo.Text;
                if (!due.CorreoValido(mail) == true)
                {
                    MessageBox.Show("El formato correo es invalido");
                }
                else
                {
                    cli.correo_electronico = txtCorreo.Text;
                    string texto = txtTel.Text;
                    if(texto.Length <=8)
                    {
                        MessageBox.Show("El telefono debe tener al menos 9 digitos");
                    }
                    else
                    {
                        cli.ModificarCliente(txtRutCliente.Text, txtPrimerNom.Text, txtSegundoNom.Text, txtPrimerApe.Text, txtSegundoApe.Text, txtCorreo.Text, Int32.Parse(txtTel.Text));
                        MessageBox.Show("Cliente modificado exitosamente");
                        this.Close();
                    }
                }
                
                


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
    }
}
