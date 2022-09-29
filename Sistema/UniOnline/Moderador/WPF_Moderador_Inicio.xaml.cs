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

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador_Inicio.xaml
    /// </summary>
    public partial class WPF_Moderador_Inicio : Page
    {
        public WPF_Moderador_Inicio()
        {
            InitializeComponent();
        }


        private void Button_Perfil(object sender, RoutedEventArgs e)
        {
            WPF_Moderador wpfMod = new WPF_Moderador();
            wpfMod.ShowDialog();
        }
    }
}
