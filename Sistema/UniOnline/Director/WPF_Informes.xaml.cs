using Controlador;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_Informes.xaml
    /// </summary>
    public partial class WPF_Informes : Window
    {
        Conexion con = new Conexion();
        public WPF_Informes()
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


        private void btnInfoVentas_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Usuario usu = new Usuario();
                usu.crearPDF();
                MessageBox.Show("Informe generado con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el documento. " + ex.Message, "Advertencia");
            }
        }

        private void btnInfoUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario usu = new Usuario();
                usu.crearPDFTramites();
                MessageBox.Show("Informe generado con éxito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el documento. " + ex.Message, "Advertencia");
            }
            
        }
    }
}
