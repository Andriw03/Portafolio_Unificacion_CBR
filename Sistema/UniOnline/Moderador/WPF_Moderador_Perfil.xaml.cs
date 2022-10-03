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

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

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

        }
    }
}
