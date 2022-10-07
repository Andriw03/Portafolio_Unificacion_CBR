using System;
using System.Collections.Generic;
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
using Controlador;
using MessageBox = System.Windows.MessageBox;


namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_VistaDoc.xaml
    /// </summary>
    public partial class WPF_VistaDoc : Window
    {
        public WPF_VistaDoc()
        {
            InitializeComponent();
            algo();
        }
        public void algo()
        {
            Frame frame = new Frame();
            System.Windows.Controls.WebBrowser browser = new System.Windows.Controls.WebBrowser();
            //owser.Navigate(new Uri(fileName));
            frame.Content = browser;
        }
    }
   

}
