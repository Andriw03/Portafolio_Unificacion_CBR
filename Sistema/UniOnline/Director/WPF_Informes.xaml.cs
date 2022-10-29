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

                SaveFileDialog saveFileDialog1 = new SaveFileDialog { Title = "Descargar documento.." };
                saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

                saveFileDialog1.FileName = "COPIACARNET";
                string filename = saveFileDialog1.FileName;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    usu.crearPDF();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el documento. " + ex.Message, "Advertencia");
            }
        }

        private void btnInfoUsers_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = new Usuario();
            usu.CrearInformeUsuarios();
        }
    }
}
