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
    /// Lógica de interacción para WPF_EditarTra.xaml
    /// </summary>
    public partial class WPF_EditarTra : Window
    {
        public WPF_EditarTra()
        {
            InitializeComponent();
            Conectar();
            LlenadoEstado();
            LLenarTipo();
            LlenarCmbDoc();
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

        private void LLenarTipo()
        {
            cmbTipoTram.Items.Clear();
            cmbTipoTram.Items.Add("-------");
            List<string> registro = con.Llenado("T_TRAMITE", "nombre_tipoT");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbTipoTram.Items.Add(registro[i]);
                }
                cmbTipoTram.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error Conexion T_TRAMITE.", "Advertencia");
            }
        }

        private void LlenadoEstado()
        {
            cmbEstado.Items.Add("Vigente");
            cmbEstado.Items.Add("No vigente");
        }
        private void LlenarCmbDoc()
        {
            cmbDoc.Items.Add("No aplica.");
            cmbDoc.Items.Add("Copia de cédula de identidad.");
            cmbDoc.Items.Add("Copia de cédula de identidad y Escritura de propiedad.");
        }

        int idTra = 0;
        public int ObtenerTra
        {
            get
            {
                return this.idTra;
            }
            set
            {
                this.idTra = value;
                this.idTra = value;
                Tramite tram = new Tramite();

                tram = tram.BuscarTramite(idTra);

                txtNomTram.Text = tram.NombreTra;
                txtValorTra.Text = tram.ValorTra;
                txtDescTram.Text = tram.Descripcion;
                cmbEstado.SelectedItem = tram.Estado;
                cmbDoc.SelectedItem = tram.Doc;
                cmbTipoTram.SelectedIndex = tram.TipoTramite;

            }
        }

        private void btnVolverTra_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEliminarTra_Click(object sender, RoutedEventArgs e)
        {
            Tramite tra = new Tramite();
            MessageBoxResult result = MessageBox.Show("Seguro que desea modificar esta propiedad", "Modificar Propiedad", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (tra.EliminarTra(idTra))
                    {
                        MessageBox.Show("Trámite Eliminado con Éxito.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al Eliminar", "Error");
                    }
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void btnModificarTra_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomTram.Text != string.Empty && txtValorTra.Text != string.Empty && cmbEstado.SelectedItem != null && cmbTipoTram.SelectedItem != null && txtDescTram.Text != string.Empty)
            {
                try
                {
                    Tramite Tra = new Tramite();
                    Tra.NombreTra = txtNomTram.Text;
                    if (txtValorTra.Text.Length >= 5)
                    {
                        Tra.ValorTra = txtValorTra.Text;
                        if (cmbEstado.SelectedIndex == 0)
                        {
                            Tra.Estado = "Vigente";
                            Tra.TipoTramite = Int32.Parse(cmbTipoTram.SelectedIndex.ToString());
                            Tra.Descripcion = txtDescTram.Text;
                            Tra.Doc = cmbDoc.SelectedItem.ToString();
                            if (Tra.ModificarTra(idTra, Tra.NombreTra, Tra.ValorTra, Tra.Estado, Tra.Descripcion, Tra.TipoTramite, Tra.Doc))
                            {
                                MessageBox.Show("Trámite modificado.");
                            }
                            else
                            {
                                MessageBox.Show("Error al modificar.");
                            }
                        }
                        else if (cmbEstado.SelectedIndex == 1)
                        {
                            Tra.Estado = "No vigente";
                            Tra.TipoTramite = Int32.Parse(cmbTipoTram.SelectedIndex.ToString());
                            Tra.Descripcion = txtDescTram.Text;
                            Tra.Doc = cmbDoc.SelectedItem.ToString();
                            if (Tra.ModificarTra(idTra, Tra.NombreTra, Tra.ValorTra, Tra.Estado, Tra.Descripcion, Tra.TipoTramite, Tra.Doc))
                            {
                                MessageBox.Show("Trámite modificado.");
                            }
                            else
                            {
                                MessageBox.Show("Error al modificar.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("El Trámite ya se encuentra registrado.", "Advertencia");
                        }
                        lblValor.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        lblValor.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al ingresar el Trámite:" + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Debe ingresar los campos por favor.", "Advertencia");
            }
        }

        private void txtValorTra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
