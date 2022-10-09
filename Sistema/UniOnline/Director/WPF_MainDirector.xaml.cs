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
    /// Lógica de interacción para WPF_MainDirector.xaml
    /// </summary>
    public partial class WPF_MainDirector : Window
    {
        public WPF_MainDirector()
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
                LblSaludo.Content = us.primer_nombre + " " + us.primer_apellido + " Recuerda:";
                LblMotivacion.Content = motivacion[randN];
            }
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

        private void btnIngresarUsuario_Click(object sender, RoutedEventArgs e)
        {
            WPF_Director tprop = new WPF_Director();
            tprop.ShowDialog();
        }

        private void btnGestionUsuario_Click(object sender, RoutedEventArgs e)
        {
            WPF_ModificarUsuario tprop = new WPF_ModificarUsuario();
            tprop.ShowDialog();
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnGestionInforme_Click(object sender, RoutedEventArgs e)
        {
            WPF_Informes tprop = new WPF_Informes();
            tprop.ShowDialog();
        }


        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            this.Hide();
            ventana.ShowDialog();
            this.Close();
        }
    }
}