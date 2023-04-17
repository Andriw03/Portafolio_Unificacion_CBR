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
using Controlador;

namespace UniOnline.Recepcion
{
    /// <summary>
    /// Lógica de interacción para WPF_Recepcion.xaml
    /// </summary>
    public partial class WPF_Recepcion : Window
    {
        
        public WPF_Recepcion()
        {
            InitializeComponent();
            Conectar();
        }
        Recepcionista rec = new Recepcionista();
        Usuario us = new Usuario();

        public Usuario ObtenerUsuario
        {
            get
            {
                return this.us;
            }
            set
            {
                this.us = value;
                this.us = value;

                lblBienvenidoRec.Content = us.primer_nombre + " " + us.primer_apellido + " ";

            }
        }
       
        //Conexión BD
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

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text != string.Empty || txtNumeroSeguimiento.Text != string.Empty)
            {
                try
                {
                    Recepcionista recep = new Recepcionista();
                    if (txtRut.Text != string.Empty)
                    {
                        DataTable tabla = recep.MostrarSolicitud(txtRut.Text);
                        if (tabla != null)
                        {
                            dg_listartramite.ItemsSource = tabla.DefaultView;
                            dg_listartramite.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Tabla sin registro", "Error");
                        }

                    }
                    else if (txtNumeroSeguimiento.Text != string.Empty)
                    {
                        

                        DataTable tabla = recep.MostrarSolicitud(txtNumeroSeguimiento.Text);
                        
                        if (tabla != null)
                        {
                            dg_listartramite.ItemsSource = tabla.DefaultView;
                            dg_listartramite.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Tabla sin registro", "Error");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Solicitud sin registro: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe llenar al menos un valor", "Advertencia");
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {
            WPF_MainRecepcion mrec = new WPF_MainRecepcion();
            mrec.ObtenerUsuario = us;
            this.Close();
            mrec.ShowDialog();
            
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            this.Hide();
            ventana.ShowDialog();
            this.Close();

        }

        private void btnDetalles_Click(object sender, RoutedEventArgs e)
        {
           
                Detalles_Solicitud dSoli = new Detalles_Solicitud();
                Recepcionista recep = new Recepcionista();
                dSoli.ObtenerUsuarioRec = recep.ObtenerDatos(txtRut.Text, txtNumeroSeguimiento.Text);
                dSoli.ShowDialog();
           
        }

        private void btnPerfil_Click(object sender, RoutedEventArgs e)
        {
            WPF_PerfilUsuario perfilUsuario = new WPF_PerfilUsuario();
            perfilUsuario.ObtenerUsuario = us;
            perfilUsuario.ShowDialog();

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

        private void txtRut_KeyUp(object sender, KeyEventArgs e)
        {
            txtRut.Text = FormatearRut(txtRut.Text);
            txtRut.SelectionStart = txtRut.Text.Length;
            txtRut.SelectionLength = 0;
        }

        private void txtNumeroSeguimiento_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}


