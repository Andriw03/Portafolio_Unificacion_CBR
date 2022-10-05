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
    public partial class WPF_Trabajador_Tra : Window
    {
        public WPF_Trabajador_Tra()
        {
            InitializeComponent();
            Conectar();
            LimpiarTra();
            LlenarCmbTramite();
            LlenarCmbListar();
        }

        private void LimpiarTra()
        {
            txtNomTra.Text = string.Empty;
            txtValorTra.Text = string.Empty;
            cmbTipoTra.SelectedIndex = 0;
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

        private void LlenarCmbTramite()
        {
            cmbTipoTra.Items.Clear();
            cmbTipoTra.Items.Add("-------");
            List<string> registro = con.Llenado("T_TRAMITE", "nombre_tipoT");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbTipoTra.Items.Add(registro[i]);
                }
                cmbTipoTra.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion T_TRAMITE");
            }
        }

        private void btnVolverTra_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbTipoTra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomTra.Text != string.Empty && txtValorTra.Text != string.Empty && cmbTipoTra.SelectedItem != null)
            {
                try
                {
                    Tramite Tra = new Tramite();
                    if (!Tra.ExisteTramite(txtNomTra.Text))
                    {
                        Tra.NombreTra = txtNomTra.Text;
                        Tra.ValorTra = txtValorTra.Text;
                        Tra.TipoTramite = Int32.Parse(cmbTipoTra.SelectedIndex.ToString());
                        MessageBox.Show(Tra.InsertarTramite(Tra), "Mensaje:");
                        LimpiarTra();
                    }
                    else
                    {
                        MessageBox.Show("Error, El Trámite ya se encuentra registrado.");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al ingresar el Trámite:" + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Error, Debe ingresar los campos por favor.");
            }
        }

        private void cmbListarTra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBuscarTra_Click(object sender, RoutedEventArgs e)
        {
            if (cmbListarTra.SelectedIndex != 0)
            {
                Tramite tra = new Tramite();
                if (tra.ExisteTipo(cmbListarTra.SelectedIndex.ToString()))
                {
                    DataTable tabla = tra.MostrarTipoTra(cmbListarTra.SelectedIndex);
                    if (tabla != null)
                    {
                        try
                        {
                            dtgTramite.ItemsSource = tabla.DefaultView;
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
                else
                {
                    MessageBox.Show("Propiedad no encontrada", "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos para buscar", "Advertencia");
            }
        }

        private void btnListarVol_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LlenarCmbListar()
        {
            cmbListarTra.Items.Clear();
            cmbListarTra.Items.Add("-------");
            List<string> registro = con.Llenado("T_TRAMITE", "nombre_tipoT");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbListarTra.Items.Add(registro[i]);
                }
                cmbListarTra.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion T_TRAMITE");
            }
        }
    }

}
