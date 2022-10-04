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

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador_Soporte.xaml
    /// </summary>
    public partial class WPF_Moderador_Soporte : Window
    {
        public WPF_Moderador_Soporte()
        {
            InitializeComponent();
        }

        private void btnInicioMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador wpfMod = new WPF_Moderador();
            wpfMod.ShowDialog();
        }

        private void btnPerfilMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Perfil wpfPerfil = new WPF_Moderador_Perfil();
            wpfPerfil.ShowDialog();
        }

        private void btnSoporteMod(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerTicket_Click(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Soporte_Tickets wpfTicket = new WPF_Moderador_Soporte_Tickets();
            wpfTicket.ShowDialog();
        }
    }
}
