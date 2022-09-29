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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Controlador;

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador.xaml
    /// </summary>
    public partial class WPF_Moderador : Window
    {
        public WPF_Moderador()
        {
            InitializeComponent();
            Conectar();
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

        private void Button_Soporte(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Perfil(object sender, RoutedEventArgs e)
        {
            WPF_Moderador wpfMod = new WPF_Moderador();
            wpfMod.ShowDialog();
        }
    }
}
