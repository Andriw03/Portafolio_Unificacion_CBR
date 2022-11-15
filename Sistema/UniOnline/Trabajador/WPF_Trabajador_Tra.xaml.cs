using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class WPF_Trabajador_Tra : Window
    {
        public WPF_Trabajador_Tra()
        {
            InitializeComponent();
            Conectar();
            LimpiarTra();
            LlenadoEstado();
            LlenadoEstadoListar();
            LlenarCmbTramite();
            LlenarCmbListar();
            LlenarCmbDoc();
        }

        private void LlenarCmbDoc()
        {
            cmbDoc.Items.Add("No aplica.");
            cmbDoc.Items.Add("Copia de cédula de identidad.");
            cmbDoc.Items.Add("Copia de cédula de identidad y Escritura de propiedad.");
        }

        private void LlenadoEstado()
        {
            cmbEstado.Items.Add("Vigente");
            cmbEstado.Items.Add("No vigente");
        }
        private void LlenadoEstadoListar()
        {
            cmbEstadoListar.Items.Add("Vigente");
            cmbEstadoListar.Items.Add("No vigente");
        }

        private void LimpiarTra()
        {
            txtNomTra.Text = string.Empty;
            txtValorTra.Text = string.Empty;
            cmbTipoTra.SelectedIndex = 0;
            txtDescTra.Text = string.Empty;
            cmbEstado.Text = "Seleccione un estado";
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
                MessageBox.Show("Error Conexion T_TRAMITE.", "Advertencia");
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
            if (txtNomTra.Text != string.Empty && txtValorTra.Text != string.Empty && cmbEstado.SelectedItem != null && cmbTipoTra.SelectedItem != null && txtDescTra.Text != string.Empty)
            {
                try
                {
                    Tramite Tra = new Tramite();
                    if (!Tra.ExisteTramite(txtNomTra.Text))
                    {
                        Tra.NombreTra = txtNomTra.Text;
                        if (txtValorTra.Text.Length >= 5)
                        {
                            Tra.ValorTra = txtValorTra.Text;
                            if (cmbEstado.SelectedIndex == 0)
                            {
                                Tra.Estado = "Vigente";
                                Tra.TipoTramite = Int32.Parse(cmbTipoTra.SelectedIndex.ToString());
                                Tra.Descripcion = txtDescTra.Text;
                                Tra.Doc = cmbDoc.SelectedItem.ToString();
                                MessageBox.Show(Tra.InsertarTramite(Tra), "Mensaje:");
                                LimpiarTra();
                            }
                            else if (cmbEstado.SelectedIndex == 1)
                            {
                                Tra.Estado = "No vigente";
                                Tra.TipoTramite = Int32.Parse(cmbTipoTra.SelectedIndex.ToString());
                                Tra.Descripcion = txtDescTra.Text;
                                Tra.Doc = cmbDoc.SelectedItem.ToString();
                                MessageBox.Show(Tra.InsertarTramite(Tra), "Mensaje:");
                                LimpiarTra();
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
                    else
                    {
                        MessageBox.Show("El Trámite ya se encuentra registrado.", "Advertencia");
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
                            cmbEstadoListar.Visibility = Visibility.Visible;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tabla sin registro", "Advertencia");
                    }
                }
                else
                {
                    MessageBox.Show("Propiedad no encontrada", "Advertencia");
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
                MessageBox.Show("Error Conexion T_TRAMITE", "Advertencia");
            }
        }

        private void cmbEstadoListar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbListarTra.SelectedIndex != 0 && cmbEstadoListar != null)
            {
                Tramite tra = new Tramite();
                if (tra.ExisteTipo(cmbListarTra.SelectedIndex.ToString()))
                {
                    if (cmbEstadoListar.SelectedIndex == 0)
                    {
                        string estado = "Vigente";
                        DataTable tabla = tra.MostrarEstado(cmbListarTra.SelectedIndex, estado);
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
                            MessageBox.Show("Tabla sin registro", "Advertencia");
                        }
                    }
                    else if (cmbEstadoListar.SelectedIndex == 1)
                    {
                        string estado = "No vigente";
                        DataTable tabla = tra.MostrarEstado(cmbListarTra.SelectedIndex, estado);
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
                            MessageBox.Show("Tabla sin registro", "Advertencia");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tabla sin registro", "Advertencia");
                    }
                }
                else
                {
                    MessageBox.Show("Tabla sin registro", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos para buscar", "Advertencia");
            }
        }

        private void btnCamEstado_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
            try
            {
                Tramite tra = new Tramite();
                int tramid = Int32.Parse(dataView[0].ToString());
                if (tra.CompararID(tramid))
                {
                    string estado = dataView[3].ToString();
                    if (estado == "Vigente")
                    {
                        estado = "No vigente";
                        Tramite tram = new Tramite();
                        if (tram.ModificarEstado(tramid, estado))
                        {
                            this.btnBuscarTra_Click(sender, e);
                            MessageBox.Show("Estado del trámite modificado con Éxito.");

                        }
                        else
                        {
                            MessageBox.Show("Error al modificar el estado del trámite.", "Advertencia");
                        }
                    }
                    else if (estado == "No vigente")
                    {
                        estado = "Vigente";
                        Tramite tram = new Tramite();
                        if (tram.ModificarEstado(tramid, estado))
                        {
                            this.btnBuscarTra_Click(sender, e);
                            MessageBox.Show("Estado del trámite modificado con Éxito.");

                        }
                        else
                        {
                            MessageBox.Show("Error al modificar el estado del trámite.", "Advertencia");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar el estado del trámite.", "Advertencia");
                    }
                }
                else
                {
                    MessageBox.Show("Este trámite esta relacionado con una solicitud. No se puede editar.", "Advertencia");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtValorTra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void btnModificarTramite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tramite tra = new Tramite();
                DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
                if (tra.CompararID(Int32.Parse(dataView[0].ToString())))
                {
                    WPF_EditarTra editarTra = new WPF_EditarTra();
                    editarTra.ObtenerTra = Int32.Parse(dataView[0].ToString());
                    editarTra.ShowDialog();
                    this.btnBuscarTra_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Este trámite esta relacionado con una solicitud. No se puede editar.","Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
