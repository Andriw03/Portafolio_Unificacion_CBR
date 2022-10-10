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
    /// Lógica de interacción para RecuperarContraseña.xaml
    /// </summary>
    public partial class RecuperarContraseña : Window
    {
        public RecuperarContraseña()
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

            }
            else
            {
                MessageBox.Show("Error Conexión");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Cliente clie = new Cliente();
            var resultado = main.passRecov = clie.RecuperarContraseña(txtCorreo.Text);
            lblResult.Content = resultado;
        }
    }
}
