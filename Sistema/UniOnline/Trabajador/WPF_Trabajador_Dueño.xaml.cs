using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (txtRutDuenno.Text != string.Empty && txtPrimerNombre.Text != string.Empty && txtPrimerApellido.Text != string.Empty && txtSegundoApellido.Text != string.Empty && txtCorreo != null && txtTelefono != null )
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

                        string email = txtCorreo.Text;
                        if (due.CorreoValido(email) == true)
                        {
                            AdvertenciaCor.Text = string.Empty;
                            due.CorreoElectronico = txtCorreo.Text;
                            string texto = txtTelefono.Text;
                            if (texto.Length > 8)
                            {
                                AdvertenciaTel.Text = string.Empty;
                                due.Telefono = txtTelefono.Text;
                                MessageBox.Show(due.Insertar(due), "Mensaje:");
                            }
                            else
                            {
                                AdvertenciaTel.Text = "Teléfono debe tener al menos 9 dígitos.";
                            }
                        }
                        else
                        {
                            AdvertenciaCor.Text = "El formato del correo es invalido.";
                        }
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
        private void txtRutDuenno_KeyUp(object sender, KeyEventArgs e)
        {
            txtRutDuenno.Text = FormatearRut(txtRutDuenno.Text);
            txtRutDuenno.SelectionStart = txtRutDuenno.Text.Length;
            txtRutDuenno.SelectionLength = 0;
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

    }
}
