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
    public partial class WPF_Trabajador_Tra : Window
    {
        public WPF_Trabajador_Tra()
        {
            InitializeComponent();
            Conectar();
            LimpiarTra();
            LlenarCmbTramite();
        }

        private void LimpiarTra()
        {
            txtNomTra.Text = string.Empty;
            txtValorTra.Text = string.Empty;
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
                        Tra.TipoTramite = Int32.Parse(cmbTipoTra.SelectedItem.ToString());
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
    }

}
