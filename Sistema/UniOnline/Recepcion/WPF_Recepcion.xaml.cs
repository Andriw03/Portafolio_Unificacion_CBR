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

namespace UniOnline.Recepcion
{
    /// <summary>
    /// Lógica de interacción para WPF_Recepcion.xaml
    /// </summary>
    public partial class WPF_Recepcion : Window
    {
        public WPF_Recepcion()
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

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
