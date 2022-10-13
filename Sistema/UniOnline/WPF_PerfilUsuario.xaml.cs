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
namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para WPF_PerfilUsuario.xaml
    /// </summary>
    public partial class WPF_PerfilUsuario : Window
    {
        public WPF_PerfilUsuario()
        {
            InitializeComponent();
        }
        //Variable y metodo que recibe el usuario conectado
        Usuario usuario = new Usuario();
        public Usuario ObtenerUsuario
        {
            get
            {
                return this.usuario;
            }
            set
            {
                this.usuario = value;
                this.usuario = value;
                labelNombre.Content = "Nombre: " + usuario.primer_nombre +" "+ usuario.primer_apellido;
                labelRut.Content = "Rut: " + usuario.rut_usuario;
                labelCorreo.Content = "Correo Electronico: " + usuario.correo_electronico;
                labelTelefono.Content = "Telefono: " + usuario.telefono;
                Llenado();
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void Llenado()
        {
            cmbEstado.Items.Clear();
            Usuario us = new Usuario();
            List<string> registro = us.LlenadoEstado(usuario.rut_usuario);
            if (registro[0] != string.Empty)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbEstado.Items.Add(registro[i]);
                }
                cmbEstado.Items.Refresh();
                labelasasd.Visibility = Visibility.Visible;
                cmbEstado.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Error al cargar Estado Solicitud", "Error");
            }
        }

        private void btnSolcitud_Click(object sender, RoutedEventArgs e)
        {
            Form_SolicitudModificar modificar = new Form_SolicitudModificar();
            
            labelasasd.Visibility = Visibility.Visible;
            cmbEstado.Visibility = Visibility.Visible;
            modificar.ShowDialog();
        }
        
    }

}
