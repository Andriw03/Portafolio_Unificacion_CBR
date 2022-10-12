using Controlador;
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

namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_Descripción.xaml
    /// </summary>
    public partial class Descripción : Window
    {
        public Descripción()
        {
            InitializeComponent();
        }

        Usuario usu = new Usuario();
        public Usuario ObtenerFormulario
        {
            get
            {
                return this.usu;
            }
            set
            {
                this.usu = value;
                this.usu = value;


                lblDescForm.Content = "Descripción de Formulario : " + usu.detalle_form;



            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }
    }
}
