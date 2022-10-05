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
                MessageBox.Show("Vas bien");
            }
            else
            {
                MessageBox.Show("Error Conexión");
            }
        }

        private void btnInicioMod(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPerfilMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Perfil wpfPerfil = new WPF_Moderador_Perfil();
            this.Close();
            wpfPerfil.ShowDialog();
        }

        private void btnSoporteMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Soporte wpfSoporte = new WPF_Moderador_Soporte();
            this.Close();
            wpfSoporte.ShowDialog(); 
        }
    }
}
