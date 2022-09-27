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

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_Trabajador_Prop.xaml
    /// </summary>
    public partial class WPF_Trabajador_Prop : Window
    {
        public WPF_Trabajador_Prop()
        {
            InitializeComponent();
            Conectar();
            LlenarCmbPropiedad();
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

        private void cmbRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbProvincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbComuna_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
