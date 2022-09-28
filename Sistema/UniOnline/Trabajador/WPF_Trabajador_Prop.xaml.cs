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

namespace UniOnline.Trabajador
{
    public partial class WPF_Trabajador_Prop : Window
    {
        public WPF_Trabajador_Prop()
        {
            InitializeComponent();
            Conectar();
            LlenarCmbPropiedad();
            LlenarCmbRegion();
        }
        //Conexión BD
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
        //Método para llenar el combobox tipo de propiedad
        private void LlenarCmbPropiedad()
        {
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
        //Método para llenar el combobox Región 
        private void LlenarCmbRegion()
        {
            cmbRegion.Items.Add("-------");
            cmbProvincia.Items.Add("-------");
            cmbComuna.Items.Add("-------");
            List<string> registro = con.Llenado("REGION", "nombre_region");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbRegion.Items.Add(registro[i]);
                }
                cmbRegion.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion REGION");
            }
        }
        //Método para llenar el combobox de Provincia
        private void cmbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbProvincia.Items.Clear();
            cmbProvincia.Items.Add("-------");
            cmbComuna.Items.Clear();
            cmbComuna.Items.Add("-------");
            int RegionId = Convert.ToInt32(cmbRegion.SelectedIndex);
            List<string> registro = con.LlenadoWhereInt("PROVINCIA", "nombre_provincia", "REGION_id_region", RegionId);
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbProvincia.Items.Add(registro[i]);
                }
                cmbProvincia.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion Provincia");
            }

        }
        //Método para llenar el combobox de Comuna
        private void cmbProvincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbComuna.Items.Clear();
            cmbComuna.Items.Add("-------");
            List<string> registro = con.LlenadoWhereString("COMUNA inner join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia = PROVINCIA.id_provincia", "nombre_comuna", "PROVINCIA.nombre_provincia", cmbProvincia.SelectedItem.ToString());
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbComuna.Items.Add(registro[i]);
                }
                cmbComuna.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion Comuna");
            }
        }

        //Botón para volver a la vista principal de Trabajador
        private void btnVolverProp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbTipoProp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbComuna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAgregarDuenno_Click(object sender, RoutedEventArgs e)
        {
            WPF_Trabajador_Dueño tdueño = new WPF_Trabajador_Dueño();
            tdueño.ShowDialog();
        }

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
    }
}
