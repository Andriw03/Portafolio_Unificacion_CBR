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
    /// Lógica de interacción para WPF_Solicitudes.xaml
    /// </summary>
    public partial class WPF_Solicitudes : Window
    {
        public WPF_Solicitudes()
        {
            InitializeComponent();
            Conectar();
            LlenarCombo();
        }

        private void LlenarCombo()
        {
            cmbTipoTraS.Items.Add("-------");
            cmbTramiteS.Items.Add("-------");
            List<string> registro = con.Llenado("T_TRAMITE", "nombre_tipoT");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbTipoTraS.Items.Add(registro[i]);
                }
                cmbTipoTraS.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error Conexion T_TRAMITE", "Advertencia");
            }
        }

        Conexion con = new Conexion();
        private void Conectar()
        {
            if (con.Conectar())
            {

            }
            else
            {
                MessageBox.Show("Error de Conexión.");
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://127.0.0.1:8000/registrarse");
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBuscarUsu_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = new Usuario();
            usu = usu.BuscarUsuarioCliente(txtBuscarUsu.Text);
            try
            {
                if (usu.primer_nombre != string.Empty)
                {
                    txtNombre.Text = usu.primer_nombre + " " + usu.primer_apellido + " " + usu.segundo_apellido;
                    txtCorreo.Text = usu.correo_electronico;
                    txtTelefono.Text = usu.telefono;
                    lblCliente.Content = usu.id_usuario;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Cliente no existe, ¿Desea registrar uno nuevo?", "Advertencia", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            System.Diagnostics.Process.Start("http://127.0.0.1:8000/registrarse");
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar cliente: " + ex.ToString());
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

        private void txtBuscarUsu_KeyUp(object sender, KeyEventArgs e)
        {
            txtBuscarUsu.Text = FormatearRut(txtBuscarUsu.Text);
            txtBuscarUsu.SelectionStart = txtBuscarUsu.Text.Length;
            txtBuscarUsu.SelectionLength = 0;
        }

        private void btnBuscarFoja_Click(object sender, RoutedEventArgs e)
        {
            Propiedad prop = new Propiedad();
            try
            {
                DataTable tabla = new DataTable();
                tabla = prop.MostrarPropiedad(Int32.Parse(txtFoja.Text.ToString()));
                if(tabla != null)
                {
                    txtDireccion.Text = tabla.Rows[0]["direccion_prop"].ToString();
                    txtTipoP.Text = tabla.Rows[0]["nombre_tipoP"].ToString();
                    txtDueño.Text = tabla.Rows[0]["nombre_duenno"].ToString();
                    txtCorreoDuenno.Text = tabla.Rows[0]["correo_electronico"].ToString();
                    txtTelefonoDuenno.Text = tabla.Rows[0]["telefono"].ToString();
                    lblProp.Content = tabla.Rows[0]["id_propiedad"].ToString();
                }
                else
                {
                    MessageBox.Show("No existe esa propiedad." , "Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No existe esa propiedad.", "Advertencia");
            }
        }

        private void cmbTipoTraS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbTramiteS.Items.Clear();
                cmbTramiteS.Items.Add("-------");
                txtValorTra.Text = string.Empty;
                int TramiteID = Convert.ToInt32(cmbTipoTraS.SelectedIndex);
                List<string> registro = con.LlenadoWhereInt("TRAMITE", "nombre_tramite", "T_TRAMITE_id_tipoT", TramiteID);
                if (registro != null)
                {
                    for (int i = 0; i < registro.Count(); i++)
                    {
                        cmbTramiteS.Items.Add(registro[i]);
                    }
                    cmbTramiteS.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Error Conexion Trámite.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Conexion Trámite." + ex.Message, "Error");
            }
        }

        private void cmbTramiteS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtValorTra.Text = string.Empty;
                string TramiteID = cmbTramiteS.SelectedItem.ToString();
                List<string> registro = con.LlenadoWhereString("TRAMITE", "valor_tramite", "nombre_tramite", TramiteID);
                if (registro != null)
                {
                    for (int i = 0; i < registro.Count(); i++)
                    {
                        txtValorTra.Text = registro[i];
                    }
                }
                else
                {
                    MessageBox.Show("Error Conexion Trámite.", "Error");
                }
                List<string> reg = con.LlenadoWhereString("TRAMITE", "id_tramite", "nombre_tramite", TramiteID);
                if (reg != null)
                {
                    for (int i = 0; i < reg.Count(); i++)
                    {
                        lblTra.Content = reg[i];
                    }
                }
                else
                {
                    MessageBox.Show("Error Conexion Trámite.", "Error");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error Conexion Trámite." + ex.Message, "Error");
            }
        }

        private void btnSolicitud_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != string.Empty & txtDireccion.Text != string.Empty & txtValorTra.Text != string.Empty)
            {
                try
                {
                    Cliente cli = new Cliente();
                    if (cli.AgregarSolicitud(Int32.Parse(lblCliente.Content.ToString()), Int32.Parse(lblProp.Content.ToString()), Int32.Parse(lblTra.Content.ToString())))
                    {

                        MessageBox.Show("Solicitud ingresada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo realizar la solicitud.", "Advertencia");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar la solicitud. " + ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe buscar el cliente, la propiedad y el trámite que desea realizar.", "Advertencia");
            }

        }
    }
}
