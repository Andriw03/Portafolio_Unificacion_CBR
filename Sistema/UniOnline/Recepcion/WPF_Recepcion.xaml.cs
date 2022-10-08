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
using Controlador;

namespace UniOnline.Recepcion
{
    /// <summary>
    /// Lógica de interacción para WPF_Recepcion.xaml
    /// </summary>
    public partial class WPF_Recepcion : Window
    {
        
        public WPF_Recepcion()
        {
            InitializeComponent();
            Conectar();
        }
        Recepcionista rec = new Recepcionista();
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

                lblBienvenidoRec.Content = us.primer_nombre + " " + us.primer_apellido + " ";

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

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_buscar.Text != string.Empty)
            {
                Recepcionista recep = new Recepcionista();
                if (recep.ExisteRut(txt_buscar.Text))
                {
                    DataTable tabla = recep.MostrarSolicitud( txt_buscar.Text,txt_buscar.Text);
                    if (tabla != null)
                    {
                        dg_listartramite.ItemsSource = tabla.DefaultView;
                        dg_listartramite.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Tabla sin registro", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Solicitud no encontrada", "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos para buscar", "Advertencia");
            }
            
        }

        private void txtPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPlaceHolder.Visibility = System.Windows.Visibility.Collapsed;
            txt_buscar.Visibility = System.Windows.Visibility.Visible;
            txt_buscar.Focus();

        }

        private void txt_buscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_buscar.Text))
            {
                txt_buscar.Visibility = System.Windows.Visibility.Collapsed;
                txtPlaceHolder.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void txtPlaceHolder_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_buscar.Text))
            {
                txtPlaceHolder.Visibility = System.Windows.Visibility.Collapsed;
                txt_buscar.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtPlaceHolder_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {
            WPF_MainRecepcion mrec = new WPF_MainRecepcion();
            mrec.ObtenerUsuario = us;
            mrec.ShowDialog();
            
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow ventana = new MainWindow();
            this.Hide();
            ventana.ShowDialog();
            this.Close();

        }

        private void btnDetalles_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
            try
            {
                Detalles_Solicitud dSoli = new Detalles_Solicitud();
                dSoli.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPerfil_Click(object sender, RoutedEventArgs e)
        {
            WPF_PerfilUsuario perfilUsuario = new WPF_PerfilUsuario();
            perfilUsuario.ObtenerUsuario = us;
            perfilUsuario.ShowDialog();

        }
    }
    }


