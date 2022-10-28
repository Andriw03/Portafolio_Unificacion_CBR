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
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador_Perfil.xaml
    /// </summary>
    public partial class WPF_Moderador_Perfil : Window
    {
        public WPF_Moderador_Perfil()
        {
            InitializeComponent();
            Conectar();         
            
        }

        Usuario us = new Usuario();
        

    
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

        private void btnFillGrid_Click(object sender, RoutedEventArgs e)
        {
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
                datagridPerfil.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encuentran datos: "+ ex.Message);
            }
        }







        private void btnInicioMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador wpfMod= new WPF_Moderador();
            this.Close();
            wpfMod.ShowDialog();
        }

        private void btnPerfilMod(object sender, RoutedEventArgs e)
        {

        }

        private void btnSoporteMod(object sender, RoutedEventArgs e)
        {
            WPF_Moderador_Soporte wpfSoporte = new WPF_Moderador_Soporte();
            this.Close();
            wpfSoporte.ShowDialog();
        }

        //private void btnBuscar_Click(object sender, RoutedEventArgs e)
        //{
        //    Cliente cli = new Cliente();

        //    if(txtRutCliente.Text != string.Empty)
        //    {
        //        try
        //        {
        //            cli = cli.BuscarCliente(txtRutCliente.Text);
        //            if (cli.ExisteCliente(txtRutCliente.Text))
        //            {
        //                datagridPerfil.FindName(txtRutCliente.Text);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Cliente no encontrado");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //        }
        //    }

        //}

        private void datagridPerfil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            WPF_ModificarPerfil wpfModificarPerfil = new WPF_ModificarPerfil();
            wpfModificarPerfil.ShowDialog();
           


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
    }
}
