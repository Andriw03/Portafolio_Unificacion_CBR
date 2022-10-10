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
    /// Lógica de interacción para WPF_ModificarPerfil.xaml
    /// </summary>
    public partial class WPF_ModificarPerfil : Window
    {
        public WPF_ModificarPerfil()
        {
            InitializeComponent();
        }

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


        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            //string rut = txtRutCliente.Text;
            //MySqlDataReader reader = null;

            //MySqlConnection con = new MySqlConnection("server=unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com;user id=root;password=nohomo123;persistsecurityinfo=True;database=UNIONLINE");
            //MySqlCommand cmd = new MySqlCommand("SELECT rut_usuario, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, correo_electronico, telefono FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + rut + "'");
     
            //try
            //{
                
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            txtRutCliente.Text = reader.GetString(0);
            //            txtPrimerNom.Text = reader.GetString(1);
            //            txtSegundoNom.Text = reader.GetString(2);
            //            txtPrimerApe.Text = reader.GetString(3);
            //            txtSegundoApe.Text = reader.GetString(4);
            //            txtCorreo.Text = reader.GetString(5);
            //            txtTel.Text = reader.GetString(6);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se encontro el cliente solicitado.");
            //    }
            //}
            //catch(MySqlException ex)
            //{
            //    MessageBox.Show("Error al buscar " + ex.Message);
            //}

            //con.Close();
        }
    }
}
