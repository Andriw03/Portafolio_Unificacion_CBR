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

namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_SolicitudesTrabajadores.xaml
    /// </summary>
    public partial class WPF_SolicitudesTrabajadores : Window
    {
        public WPF_SolicitudesTrabajadores()
        {
            InitializeComponent();
        }

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario us = new Usuario();
                DataTable tabla = us.MostrarFormulario();
                dg_listarFormulario.ItemsSource = tabla.DefaultView;
                dg_listarFormulario.Items.Refresh();
            }

            catch
            {

            }
        }

        private void btnDetalles_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
            Descripción des = new Descripción();
            des.ObtenerFormulario = dataView[0].ToString();
            des.ShowDialog();

           
        }

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {

        }

        private void lblB_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPerfil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dg_listartramite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
