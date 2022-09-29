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
        //Conexión BD
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

        private async void btn_consultar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_buscar.Text != string.Empty)
            {
                Consultas_seg con = new Consultas_seg();
                if (con.ExisteRut(txt_buscar.Text))
                {
                    //parametros string id,int id1, string tab, int cod
                    DataTable tabla = con.Mostrarsoli(txt_buscar.Text, 0, "rut_usuario", 1);
                    if (tabla != null)
                    {
                        dg_listartramite.ItemsSource = tabla.DefaultView;
                        dg_listartramite.Items.Refresh();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Error", "Sale null");
                    }

                }
                else
                {
                    await this.ShowMessageAsync("Error", "El cliente no se encuentra registrado");
                }
            }
            else
            {
                await this.ShowMessageAsync("Error", "Debe digitar un rut para usar esta funcion");
            }
            txt_buscar.Text = string.Empty;
        }

        private Task ShowMessageAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
    }
}
