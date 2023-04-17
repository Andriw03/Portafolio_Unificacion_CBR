using Controlador;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace UniOnline.Moderador
{
    public partial class WPF_Moderador : Window
    {
        public WPF_Moderador()
        {
            InitializeComponent();
            startclock();
            Conectar();
        }


        Usuario us = new Usuario();


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


        private void startclock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }


        private void tickevent(object sender, EventArgs e)
        {
            MyTime.Text = DateTime.Now.ToString("T");
            datelbl.Text = DateTime.Now.ToString("d");
        }


        private void btnInicioMod(object sender, RoutedEventArgs e)
        {

        }

        private void btnPerfilMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Perfil wpfPerfil = new WPF_Moderador_Perfil();
            this.Close();
            wpfPerfil.ShowDialog();
            WPF_Moderador_Perfil wpfModPer = new WPF_Moderador_Perfil();
            try
            {
                MySqlConnection con = new MySqlConnection("server=unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com;user id=root;password=nohomo123;persistsecurityinfo=True;database=UNIONLINE");
                MySqlCommand cmd = new MySqlCommand("SELECT rut_usuario, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, correo_electronico, telefono FROM UNIONLINE.USUARIO WHERE T_USUARIO_id_tipoU = 5;", con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmd.Dispose();
                adapter.Dispose();
                con.Close();
                wpfModPer.datagridPerfil.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encuentran datos: " + ex.Message);
            }
        }

        private void btnSoporteMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Soporte wpfSoporte = new WPF_Moderador_Soporte();
            this.Close();
            wpfSoporte.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            this.Hide();
            ventana.ShowDialog();
            this.Close();
        }

        private void btnPerfilUsuario_Click(object sender, RoutedEventArgs e)
        {
            WPF_PerfilUsuario perfilUsuario = new WPF_PerfilUsuario();
            perfilUsuario.ObtenerUsuario = us;
            perfilUsuario.ShowDialog();
        }

        private void LabelMotivacion_Initialized(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            LabelSaludo.Content = us.primer_nombre + " " + us.primer_apellido + " Recuerda:";
            LabelMotivacion.Content = "Si tú sabes lo que vales, ve y consigue lo que mereces. Rocky Balboa.";
        }  
       
    }
}
