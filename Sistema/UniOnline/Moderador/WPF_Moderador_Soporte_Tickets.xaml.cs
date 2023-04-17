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

namespace UniOnline.Moderador
{
    /// <summary>
    /// Lógica de interacción para WPF_Moderador_Soporte_Tickets.xaml
    /// </summary>
    public partial class WPF_Moderador_Soporte_Tickets : Window
    {
        public WPF_Moderador_Soporte_Tickets()
        {
            InitializeComponent();
        }

        private void btnCargaForm_Click(object sender, RoutedEventArgs e)
        {
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
                dataForm.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se encuentran datos: " + ex.Message);
            }
        }
    }
}
