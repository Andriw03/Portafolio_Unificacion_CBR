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

                if (txtRutCliente.Text != string.Empty)
                {
                    txtPrimerNom.Text = cli.primer_nombre;
                    txtSegundoNom.Text = cli.segundo_nombre;
                    txtPrimerApe.Text = cli.primer_apellido;
                    txtSegundoApe.Text = cli.segundo_apellido;
                    txtCorreo.Text = cli.correo_electronico;
                    txtTel.Text = cli.telefono.ToString();

                    //cli.primer_nombre = txtPrimerNom.Text;
                    //cli.segundo_nombre = txtSegundoNom.Text;
                    //cli.primer_apellido = txtPrimerApe.Text;
                    //cli.segundo_apellido = txtSegundoApe.Text;
                    //cli.correo_electronico = txtCorreo.Text;
                    //cli.telefono = Int32.Parse(txtTel.Text);

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

        public static string FormatearRut(string rut)
        {
            string rutFormateado = string.Empty;

            if (rut.Length == 0)
            {
                rutFormateado = "";
            }
            else
            {
                string rutTemporal;
                string dv;
                Int64 rutNumerico;

                rut = rut.Replace("-", "").Replace(".", "");

                if (rut.Length == 1)
                {
                    rutFormateado = rut;
                }
                else
                {
                    rutTemporal = rut.Substring(0, rut.Length - 1);
                    dv = rut.Substring(rut.Length - 1, 1);

                    //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                    if (!Int64.TryParse(rutTemporal, out rutNumerico))
                    {
                        rutNumerico = 0;
                    }

                    //este comando es el que formatea con los separadores de miles
                    rutFormateado = rutNumerico.ToString("N0");

                    if (rutFormateado.Equals("0"))
                    {
                        rutFormateado = string.Empty;
                    }
                    else
                    {
                        //si no hubo problemas con el formateo agrego el DV a la salida
                        rutFormateado += "-" + dv;

                        //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                        rutFormateado = rutFormateado.Replace(",", ".");
                    }
                }
            }

            return rutFormateado;
        }

        private void txtRutCliente_KeyUp(object sender, KeyEventArgs e)
        {
            txtRutCliente.Text = FormatearRut(txtRutCliente.Text);
            txtRutCliente.SelectionStart = txtRutCliente.Text.Length;
            txtRutCliente.SelectionLength = 0;
        }
    }
}
