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

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_ConsultarTramite.xaml
    /// </summary>
    public partial class WPF_ConsultarTramite : Window
    {
        public WPF_ConsultarTramite()
        {
            InitializeComponent();
            llenarTabla();
        }

        private void llenarTabla()
        {
            Recepcionista recep = new Recepcionista();
            DataTable tabla = recep.MostrarTodasSolicitud(txtRut.Text);
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

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text != string.Empty || txtNumeroSeguimiento.Text != string.Empty )
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
                catch(Exception ex)
                {
                    MessageBox.Show("Solicitud sin registro: "+ ex.Message);
                }  
            }
            else
            {
                MessageBox.Show("Debe llenar al menos un valor", "Advertencia");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
            try
            {
                WPF_VisorSolicitud solicitud = new WPF_VisorSolicitud();
                solicitud.ObtenerSeguimiento = dataView[3].ToString();
                this.Hide();
                solicitud.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
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

        private void txtRut_KeyUp(object sender, KeyEventArgs e)
        {
            txtRut.Text = FormatearRut(txtRut.Text);
            txtRut.SelectionStart = txtRut.Text.Length;
            txtRut.SelectionLength = 0;
        }

        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
