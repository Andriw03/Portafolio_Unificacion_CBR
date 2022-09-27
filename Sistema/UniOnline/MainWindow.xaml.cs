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
using Controlador;

namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Conexion con = new Conexion();
        public MainWindow()
        {
            InitializeComponent();
            Conectar();
        }
        //Conexión BD
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


        private void btnOlvidarCon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInicioSesion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
