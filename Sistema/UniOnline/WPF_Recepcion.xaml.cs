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

namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para WPF_Recepcion.xaml
    /// </summary>
    public partial class WPF_Recepcion : Window
    {
        public WPF_Recepcion()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async Task btn_consultar_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            //if (txt_buscar.Text != string.Empty)
            //{
                //modificar esto
                //Cliente cli = new Cliente();
                //if (cli.ExisteCliente(txt_buscar.Text))
                //{
                    //parametros string id,int id1, string tab, int cod
                    //ver bien como se llaman en la base de datos.
                    //aca hay que llamar al numero de seguimiento también
                    //esto referencia a otra base de datos. averiguar como se llamaria desde myqls
                    //DataTable tabla = cli.MostrarClientes(txt_buscar.Text, 0, "RutCliente", 1);
                    //if (tabla != null)
                    //{
                        //lista a la persona o numero de seguimiento en el grid
                        //dg_listartramite.ItemsSource = tabla.DefaultView;
                        //dg_listartramite.Items.Refresh();
                    //}
                    //else
                    //{
                        //await this.ShowMessageAsync("Error", "Sale null");
                    //}

                //}
                //else
                //{
                    //await this.ShowMessageAsync("Error", "El cliente no se encuentra registrado");
                //}
           // }
            //else
            //{
                //await this.ShowMessageAsync("Error", "Debe digitar un rut para usar esta funcion");
            //}
            //txt_buscar.Text = string.Empty;
        }

        private Task ShowMessageAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        private void btn_consultar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
