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
using UniOnline.Director;
using UniOnline.Moderador;
using UniOnline.Recepcion;
using UniOnline.Trabajador;

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
            if(txtRut.Text != string.Empty && txtPass.Password!= string.Empty)
            {
                Usuario us = new Usuario();
                us = us.LoginUsuario(txtRut.Text);
                if (us.rut_usuario != string.Empty)
                {
                    if (us.id_tipoU == 2)
                    {
                        WPF_Director dir = new WPF_Director();
                        dir.ShowDialog();
                        this.Close();

                    }
                    else if (us.id_tipoU == 3)
                    {
                        WPF_Recepcion rep = new WPF_Recepcion();
                        rep.ShowDialog();
                        this.Close();
                    }
                    else if(us.id_tipoU == 4)
                    {
                        WPF_Trabajador tra = new WPF_Trabajador();
                        tra.ObtenerUsuario = us;
                        tra.ShowDialog();
                        this.Close();
                    }else if(us.id_tipoU == 6)
                    {
                        WPF_Moderador mod = new WPF_Moderador();
                        mod.ShowDialog();
                        this.Close();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Usuario no existe","Error");
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos","Advertencia");
            }
        }
    }
}
