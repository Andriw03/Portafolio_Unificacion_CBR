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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controlador;
using UniOnline.Director;
using UniOnline.Moderador;
using UniOnline.Recepcion;
using UniOnline.Trabajador;
using System.Security.Cryptography;

namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Conexion con = new Conexion();
        public MainWindow()
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



        private void btnOlvidarCon_Click(object sender, RoutedEventArgs e)
        {
            RecuperarContraseña mod = new RecuperarContraseña();
            this.Hide();
            mod.ShowDialog();
            this.Show();
        }

        private void btnInicioSesion_Click(object sender, RoutedEventArgs e)
        {
            if (txtRut.Text != string.Empty && txtPass.Password != string.Empty)
            {
                Hashing ha = new Hashing();
                Usuario us = new Usuario();
                us = us.LoginUsuario(txtRut.Text);
                this.txtPass.Password = ha.Descrypt(us.contrasenna);
               

                if (us.rut_usuario != string.Empty && ha.Descrypt(us.contrasenna) == txtPass.Password)
                {
                    if (us.id_tipoU == 2)
                    {
                        WPF_MainDirector dir = new WPF_MainDirector();
                        this.Close();
                        dir.ShowDialog();


                    }
                    else if (us.id_tipoU == 3)
                    {

                        WPF_MainRecepcion rep = new WPF_MainRecepcion();
                        rep.ObtenerUsuario = us;
                        this.Close();
                        rep.ShowDialog();

                    }
                    else if (us.id_tipoU == 4)
                    {

                        WPF_Trabajador tra = new WPF_Trabajador();
                        tra.ObtenerUsuario = us;
                        this.Close();
                        tra.ShowDialog();

                    }
                    else if (us.id_tipoU == 6)
                    {

                        WPF_Moderador mod = new WPF_Moderador();
                        this.Close();
                        mod.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("El Usuario no pertenece a este Conservador.", "Error");
                    }


                }
                else
                {
                    MessageBox.Show("RUT o Contraseña Incorrectos.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos.", "Advertencia");
            }
        }
        public static string FormatearRut(string rut)
        {
            string rutFormateado = string.Empty;

            if (rut.Length == 0)
            {
                rutFormateado = "";
            }
            else
            {
                string rutTemporal;
                string dv;
                Int64 rutNumerico;

                rut = rut.Replace("-", "").Replace(".", "");

                if (rut.Length == 1)
                {
                    rutFormateado = rut;
                }
                else
                {
                    rutTemporal = rut.Substring(0, rut.Length - 1);
                    dv = rut.Substring(rut.Length - 1, 1);

                    //aqui convierto a un numero el RUT si ocurre un error lo deja en CERO
                    if (!Int64.TryParse(rutTemporal, out rutNumerico))
                    {
                        rutNumerico = 0;
                    }

                    //este comando es el que formatea con los separadores de miles
                    rutFormateado = rutNumerico.ToString("N0");

                    if (rutFormateado.Equals("0"))
                    {
                        rutFormateado = string.Empty;
                    }
                    else
                    {
                        //si no hubo problemas con el formateo agrego el DV a la salida
                        rutFormateado += "-" + dv;

                        //y hago este replace por si el servidor tuviese configuracion anglosajona y reemplazo las comas por puntos
                        rutFormateado = rutFormateado.Replace(",", ".");
                    }
                }
            }

            return rutFormateado;
        }

        private void txtRut_KeyUp(object sender, KeyEventArgs e)
        {
            txtRut.Text = FormatearRut(txtRut.Text);
            txtRut.SelectionStart = txtRut.Text.Length;
            txtRut.SelectionLength = 0;
        }
    }
}
