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
    public partial class WPF_EditarDueño : Window
    {
        public WPF_EditarDueño()
        {
            InitializeComponent();
        }

        Duenno duenno = new Duenno();
        public Duenno ObtenerDuenno
        {
            get
            {
                return this.duenno;
            }
            set
            {
                this.duenno = value;
                this.duenno = value;
                txtRutDuenno.Text = duenno.RutDuenno;
                txtPrimerNombre.Text = duenno.PrimerNombre;
                txtPrimerApellido.Text = duenno.PrimerApellido;
                txtSegundoNombre.Text = duenno.SegundoNombre;
                txtSegundoApellido.Text = duenno.SegundoApellido;
                txtCorreo.Text = duenno.CorreoElectronico;
                txtTelefono.Text = duenno.Telefono;

            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtPrimerNombre.Text != string.Empty && txtPrimerApellido.Text != string.Empty && txtSegundoNombre.Text != string.Empty && txtSegundoApellido.Text != string.Empty && txtCorreo.Text != string.Empty && txtTelefono.Text != string.Empty)
            {
                try
                {
                    Duenno duen = new Duenno();
                    string email = txtCorreo.Text;
                    if (!duen.CorreoValido(email) == true)
                    {
                        AdvertenciaCor.Text = "El formato del correo es invalido.";
                    }
                    else
                    {
                        AdvertenciaCor.Text = string.Empty;
                        duen.CorreoElectronico = txtCorreo.Text;
                        string texto = txtTelefono.Text;
                        if (texto.Length <= 8)
                        {
                            AdvertenciaTel.Text = "Teléfono debe tener al menos 9 dígitos.";
                        }
                        else
                        {
                            AdvertenciaTel.Text = string.Empty;
                            if (duen.ModificarDuenno(txtRutDuenno.Text, txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, txtCorreo.Text, txtTelefono.Text))
                            {
                                MessageBox.Show("Dueño Modificado con Éxito");
                            }
                            else
                            {
                                MessageBox.Show("Error al modificar el dueño.", "Error");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error al modificar el Dueño:" + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos", "Advertencia");
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

        private void txtRutDuenno_KeyUp(object sender, KeyEventArgs e)
        {
            txtRutDuenno.Text = FormatearRut(txtRutDuenno.Text);
            txtRutDuenno.SelectionStart = txtRutDuenno.Text.Length;
            txtRutDuenno.SelectionLength = 0;
        }
    }
}
