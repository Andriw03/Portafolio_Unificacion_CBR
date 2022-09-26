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
using System.Windows.Navigation;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using Controlador;


namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para WPF_Trabajador.xaml
    /// </summary>
    public partial class WPF_Trabajador : Window
    {
        public WPF_Trabajador()
        {
            InitializeComponent();
            startclock();
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

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Gestion_Prop(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Gestion_Tra(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Consulta_Tra(object sender, RoutedEventArgs e)
        {

        }
        Conexion con = new Conexion();
        private void Button_Conect(object sender, RoutedEventArgs e)
        {
            if (con.Conectar())
            {
                MessageBox.Show("Conexion lista");
            }
            else
            {
                MessageBox.Show("Error");
            }

        }
    }

}
