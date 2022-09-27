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

namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para WPF_Trabajador_Prop.xaml
    /// </summary>
    public partial class WPF_Trabajador_Prop : Window
    {
        public WPF_Trabajador_Prop()
        {
            InitializeComponent();
        }

        private void btnVolverProp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
