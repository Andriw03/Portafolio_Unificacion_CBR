using Controlador;
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
        public Usuario ObtenerUsuario
        {
            get
            {
                return this.us;
            }
            set
            {
                this.us = value;
                this.us = value;
                string[] motivacion = { "Si tú sabes lo que vales, ve y consigue lo que mereces. Rocky Balboa.", "Por muy alta que sea una montaña, siempre hay un camino hacia la cima.", "El triunfo verdadero del hombre surge de las cenizas del error. Pablo Neruda.", "Lo único imposible es aquello que no intentas. Anónimo.", "El 80% del éxito se basa simplemente en insistir. Woody Allen.", "Cuanto más hacemos, más podemos hacer. William Hazlitt." };
                Random rand = new Random();
                int randN = rand.Next(0, 5);
                LabelSaludo.Content = us.primer_nombre + " " + us.primer_apellido + " Recuerda:";
                LabelMotivacion.Content = motivacion[randN];
            }
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
    }
}
