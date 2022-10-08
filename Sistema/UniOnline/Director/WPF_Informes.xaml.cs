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


namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_Informes.xaml
    /// </summary>
    public partial class WPF_Informes : Window
    {
        Conexion con = new Conexion();
        public WPF_Informes()
        {
            InitializeComponent();
            Conectar();

        }
        //Conexión BD
        private void Conectar()
        {
            if (con.Conectar())
            {

            }
            else
            {
                MessageBox.Show("Error Conexión");
            }
        }



        private void btnInfoVentas_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = new Usuario();
            usu.crearPDF();
        }
    }
}
