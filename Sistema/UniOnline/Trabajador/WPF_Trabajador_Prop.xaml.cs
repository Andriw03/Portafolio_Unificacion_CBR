﻿using System;
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
using System.Text.RegularExpressions;
using System.Data;
using MessageBox = System.Windows.MessageBox;
using MySql.Data.MySqlClient;

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

            }
            else
            {
                MessageBox.Show("Error Conexión");
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
            cmbComuna.Items.Clear();
            cmbComuna.Items.Add("-------");
            cmbProvincia.Items.Clear();
            cmbProvincia.Items.Add("-------");
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
            try
            {
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
            catch(Exception ex)
            {
                MessageBox.Show("Error", "nose que pasa");
            }
            
        }

        //Botón para volver a la vista principal de Trabajador
        private void btnVolverProp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRutDueño.Text != string.Empty && txtAnno.Text != string.Empty && txtFoja.Text != string.Empty && txtNumero.Text != string.Empty && txtDescripcion.Text != string.Empty && txtDireccion.Text != string.Empty && cmbTipoProp.SelectedItem != null && cmbRegion.SelectedItem != null && cmbRegion.SelectedItem != null && cmbComuna.SelectedItem != null)
            {
                try
                {
                    Propiedad prop = new Propiedad();
                    if (!prop.ExistePropiedad(Int32.Parse(txtFoja.Text)))
                    {
                        prop.Descripcion = txtDescripcion.Text;
                        DateTime fecha = DateTime.ParseExact((txtAnno.Text).ToString(), "yyyy", null);                                                                        
                        Duenno duenno = new Duenno();
                        duenno = duenno.BuscarDuenno(txtDueño.Text);
                        if (duenno != null)
                        {
                            prop.Direccion = con.InsertDireccion(txtDireccion.Text, Int32.Parse(txtNumero.Text), cmbComuna.SelectedItem.ToString());
                            prop.TipoPropiedad = Int32.Parse(cmbTipoProp.SelectedIndex.ToString());
                            prop.ClasPropiedad = con.InsertClasProp(Int32.Parse(txtFoja.Text), Int32.Parse(txtNumero.Text), fecha.ToString("yyyy/MM/dd HH:mm:ss"), txtRazonSocial.Text, txtRutEmpresa.Text);
                            prop.Duenno = duenno.duennoId;
                            MessageBox.Show(prop.Insertar(prop));
                        }
                        else
                        {
                            MessageBox.Show("Dueño no existe", "Error");
                        }
                                                                                               
                    }
                    else
                    {
                        MessageBox.Show("Ya existe una propiedad con esta Foja", "Advertencia");
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos","Advertencia");
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
        private void txtFojaListar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txtNumListar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void txtAnnoListar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtFojaListar.Text = "";
            txtNumListar.Text = "";
            txtAnnoListar.Text = "";
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtFojaListar.Text != string.Empty && txtNumListar.Text != string.Empty && txtAnnoListar.Text != string.Empty)
            {
                Propiedad prop = new Propiedad();
                if (prop.ExistePropiedad(Int32.Parse(txtFojaListar.Text)))
                {
                    DataTable tabla = prop.MostrarPropiedad(Int32.Parse(txtFojaListar.Text));
                    if (tabla != null)
                    {
                        try
                        {
                            
                            dtgPropiedad.ItemsSource = tabla.DefaultView;
                            //dtgPropiedad.ItemsSource = tabla.DefaultView;
                            //dtgPropiedad.Items.Refresh();
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
                MessageBox.Show("Debe llenar todos los campos para buscar","Advertencia");
            }
        }

        private void btnLimpiarProp_Click(object sender, RoutedEventArgs e)
        {
            txtDueño.Text = string.Empty;
            txtRutDueño.Text = string.Empty;
            txtNombreDueño.Text = string.Empty;
            cmbTipoProp.SelectedIndex = 0;
            txtFoja.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtAnno.Text = string.Empty; 
            txtRazonSocial.Text = string.Empty;
            txtRutEmpresa.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
            try
            {
                WPF_EditarProp editarProp = new WPF_EditarProp();
                editarProp.ObtenerFoja = Int32.Parse(dataView[0].ToString());
                //editarProp.v_foja = Int32.Parse(dataView[1].ToString());
                editarProp.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
