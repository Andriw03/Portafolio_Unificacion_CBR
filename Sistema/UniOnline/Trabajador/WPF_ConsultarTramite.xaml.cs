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

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_ConsultarTramite.xaml
    /// </summary>
    public partial class WPF_ConsultarTramite : Window
    {
        public WPF_ConsultarTramite()
        {
            InitializeComponent();
        }

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {
            if (txtBusqueda.Text != string.Empty )
            {
                Recepcionista recep = new Recepcionista();
                if (recep.ExisteRut(txtBusqueda.Text))
                {
                    DataTable tabla = recep.MostrarSolicitud(txtBusqueda.Text, txtBusqueda.Text);
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
                MessageBox.Show("Debe un valor para buscar", "Advertencia");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
            try
            {
                WPF_EditarProp editarProp = new WPF_EditarProp();
                editarProp.ObtenerFoja = Int32.Parse(dataView[0].ToString());
                //editarProp.v_foja = Int32.Parse(dataView[1].ToString());
                editarProp.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
