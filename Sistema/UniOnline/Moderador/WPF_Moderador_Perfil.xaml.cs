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
using System.Data;


namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador_Perfil.xaml
    /// </summary>
    public partial class WPF_Moderador_Perfil : Window
    {
        public WPF_Moderador_Perfil()
        {
            InitializeComponent();
            Conectar();         
            
        }

    
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
        private void btnInicioMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador wpfMod= new WPF_Moderador();
            wpfMod.ShowDialog();
        }

        private void btnPerfilMod(object sender, RoutedEventArgs e)
        {

        }

        private void btnSoporteMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Soporte wpfSoporte = new WPF_Moderador_Soporte();
            wpfSoporte.ShowDialog();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Duenno cli = new Duenno();
            if (txtRutCliente.Text != string.Empty)
            {
                try
                {
                    cli = cli.BuscarDuenno(txtRutCliente.Text);
                    if (cli.ExisteDuenno(txtRutCliente.Text))
                    {
                        MessageBox.Show("EL CLIENTE EXISTE PERO AUN NO SABI RELLENAR LA TABLA XD");
                    }
                    else
                    {
                        MessageBox.Show("Error, Cliente no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }

        }

        private void datagridPerfil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
