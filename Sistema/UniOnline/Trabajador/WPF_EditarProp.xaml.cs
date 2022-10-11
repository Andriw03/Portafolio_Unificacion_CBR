using Controlador;
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

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_EditarProp.xaml
    /// </summary>
    public partial class WPF_EditarProp : Window
    {
        public WPF_EditarProp()
        {
            InitializeComponent();
            Conectar();
            LlenarCmbPropiedad();

        }
        int v_foja = 0;
        public int ObtenerFoja
        {
            get
            {
                return this.v_foja;
            }
            set
            {
                this.v_foja = value;
                this.v_foja = value;
                LlenadoPropiedad(v_foja);
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
        //Llenado de campos
        private void LlenadoPropiedad(int id)
        {
            Propiedad prop = new Propiedad();
            DataTable tabla = prop.MostrarPropiedad(v_foja);
            Duenno duenno = new Duenno();
            if (tabla != null)
            {
                try
                {
                    txtDueño.Text = tabla.Rows[0]["rut_duenno"].ToString();                    
                    duenno = duenno.BuscarDuenno(txtDueño.Text);
                    txtRutDueño.Text = duenno.RutDuenno;
                    txtNombreDueño.Text = duenno.PrimerNombre + ' ' + duenno.PrimerApellido;
                    txtFoja.Text = tabla.Rows[0]["foja"].ToString();
                    //txtNumero.Text = tabla.Rows[0]["rut_duenno"].ToString();
                    //txtAnno.Text = ;
                    cmbTipoProp.SelectedItem = tabla.Rows[0]["nombre_tipoP"].ToString();
                    txtRazonSocial.Text = tabla.Rows[0]["razon_social"].ToString();
                    txtRutEmpresa.Text = tabla.Rows[0]["rut_empresa"].ToString();
                    txtDescripcion.Text = tabla.Rows[0]["descripcion"].ToString();
                    txtDireccion.Text = tabla.Rows[0]["direccion_prop"].ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Tabla sin registro", "Error");
            }
        }
        //Método para llenar el combobox tipo de propiedad
        private void LlenarCmbPropiedad()
        {
            cmbTipoProp.Items.Clear();
            cmbTipoProp.Items.Add("-------");
            List<string> registro = con.Llenado("TIPO_PROPIEDAD", "nombre_tipoP");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbTipoProp.Items.Add(registro[i]);
                }
                cmbTipoProp.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion TIPO_PROPIEDAD");
            }
        }
        //Botón para volver a la vista principal de Trabajador
        private void btnVolverProp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Boton que busca el dueño en base su rut
        private void btnBuscarDueño_Click(object sender, RoutedEventArgs e)
        {
            if (txtDueño.Text != string.Empty)
            {
                Duenno duenno = new Duenno();
                duenno = duenno.BuscarDuenno(txtDueño.Text);
                if (duenno != null)
                {
                    txtRutDueño.Text = duenno.RutDuenno;
                    txtNombreDueño.Text = duenno.PrimerNombre + ' ' + duenno.PrimerApellido;
                }
                else
                {
                    MessageBox.Show("Dueño vacío", "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe digitar un Rut para usar esta función", "Error");
            }
        }

        private void btnAgregarDuenno_Click(object sender, RoutedEventArgs e)
        {
            WPF_Trabajador_Dueño tdueño = new WPF_Trabajador_Dueño();
            tdueño.ShowDialog();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRutDueño.Text != string.Empty && txtFoja.Text != string.Empty && txtDescripcion.Text != string.Empty && txtDireccion.Text != string.Empty && cmbTipoProp.SelectedItem != null)
            {
                Propiedad prop = new Propiedad();
                if (prop.ModificarProp(txtDescripcion.Text,cmbTipoProp.SelectedIndex, txtRutDueño.Text, Int32.Parse(txtFoja.Text)))
                {
                    MessageBox.Show("Propiedad Modificada con Éxito");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error en el Update","Error");
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos","Advertencia");
            }
        }
        private void cmbTipoProp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoProp.SelectedItem.ToString() == "Comercial")
            {
                txtRazonSocial.IsEnabled = true;
                txtRutEmpresa.IsEnabled = true;
            }
            else
            {
                txtRazonSocial.IsEnabled = false;
                txtRutEmpresa.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Propiedad prop = new Propiedad();
            if (prop.EliminarProp(Int32.Parse(txtFoja.Text)))
            {
                MessageBox.Show("Propiedad Eliminada con Éxito");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en el Eliminar", "Error");
            }
        }

        private void btnModificarDuenno_Click(object sender, RoutedEventArgs e)
        {
            Duenno due = new Duenno();
            WPF_EditarDueño edue = new WPF_EditarDueño();
            edue.ObtenerDuenno = due.BuscarDuenno(txtDueño.Text);
            edue.ShowDialog();
        }
    }
}
