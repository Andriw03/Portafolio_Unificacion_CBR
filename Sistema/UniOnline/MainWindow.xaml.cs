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
using System.Security.Cryptography;

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
      
      
        Cliente cli = new Cliente();
        MasterMailServer mailService = new MasterMailServer();
        public Cliente passRecov
        {
            get
            {
                return this.cli;
            }
            set
            {
                this.cli = value;
                this.cli = value;


                MasterMailServer mailService = new MasterMailServer();

                mailService.sendMail(
                subject: "SYSTEM: Restauración de Contraseña ",
                body: "Hola " + cli.primer_nombre + " " + cli.primer_apellido + " has pedido recuperar tu contraseña, la cual es: "
                + cli.contrasenna + " ,pero recuerda que recomendamos que cambies tu contrseña lo antes posible. Atte. Soporte CBR.",
                recipientMail: cli.correo_electronico

                );

            }
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
            RecuperarContraseña mod = new RecuperarContraseña();
            this.Close();
            mod.ShowDialog();

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
                        WPF_MainDirector dir = new WPF_MainDirector();
                        this.Close();
                        dir.ShowDialog();
                        

                    }
                    else if (us.id_tipoU == 3)
                    {
                        
                        WPF_MainRecepcion rep = new WPF_MainRecepcion();
                        rep.ObtenerUsuario = us;
                        this.Close();
                        rep.ShowDialog();
                        
                    }
                    else if(us.id_tipoU == 4)
                    {
                        
                        WPF_Trabajador tra = new WPF_Trabajador();
                        tra.ObtenerUsuario = us;
                        this.Close();
                        tra.ShowDialog();
                        
                    }else if(us.id_tipoU == 6)
                    {
                        
                        WPF_Moderador mod = new WPF_Moderador();
                        this.Close();
                        mod.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("El Usuario no pertenece a este Conservador","Error");
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
