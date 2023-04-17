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
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Controlador;

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador_Soporte.xaml
    /// </summary>
    public partial class WPF_Moderador_Soporte : Window
    {
        public WPF_Moderador_Soporte()
        {
            InitializeComponent();
        }

        Usuario us = new Usuario();

        private void btnInicioMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador wpfMod = new WPF_Moderador();
            this.Close();
            wpfMod.ShowDialog();
        }

        private void btnPerfilMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Perfil wpfPerfil = new WPF_Moderador_Perfil();
            this.Close();
            wpfPerfil.ShowDialog();
        }

        private void btnSoporteMod(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerTicket_Click(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Soporte_Tickets wpfTicket = new WPF_Moderador_Soporte_Tickets();
            wpfTicket.ShowDialog();
            try
            {
                MySqlConnection con = new MySqlConnection("server=unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com;user id=root;password=nohomo123;persistsecurityinfo=True;database=UNIONLINE");
                MySqlCommand cmd = new MySqlCommand("SELECT id_formulario, nombre_form, telefono, correo_form, asunto_form, detalle_form FROM UNIONLINE.FORMULARIO;", con);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmd.Dispose();
                adapter.Dispose();
                con.Close();
                wpfTicket.dataForm.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encuentran datos: " + ex.Message);
            }
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            this.Hide();
            ventana.ShowDialog();
            this.Close();
        }

        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            WPF_ChatWhatsApp wsp = new WPF_ChatWhatsApp();
            wsp.ShowDialog();
            
        }

        private void btnPerfilUsuario_Click(object sender, RoutedEventArgs e)
        {
            WPF_PerfilUsuario perfilUsuario = new WPF_PerfilUsuario();
            perfilUsuario.ObtenerUsuario = us;
            perfilUsuario.ShowDialog();
        }
    }
}
