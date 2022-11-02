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
using Microsoft.Graph;
using System.Reflection;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Net.Mail;
using System.Net;

namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para RecuperarContraseña.xaml
    /// </summary>
    public partial class RecuperarContraseña : Window
    {
        public RecuperarContraseña()
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

            }
            else
            {
                MessageBox.Show("Error Conexión");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Cliente clie = new Cliente();
            try
            {
                clie = clie.RecuperarContraseña(txtCorreo.Text);
                if (clie.correo_electronico != string.Empty)
                {
                    string mensaje = "Estimad@: " + clie.primer_nombre + " " + clie.primer_apellido + " Su codigo se a generado con éxito, ingreselo en la aplicación por favor: ";
                    CreateTimeoutTestMessage(clie.correo_electronico, mensaje);
                    lblingresa.Visibility = Visibility.Hidden;
                    txtCorreo.Visibility = Visibility.Hidden;
                    btnEnviar.Visibility = Visibility.Hidden;
                    lbCodigo.Visibility = Visibility.Visible;
                    txtCodigo.Visibility = Visibility.Visible;
                    btnCodigo.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Correo no registrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            

            
        }
        string codigo = string.Empty;
        public void CreateTimeoutTestMessage(string para, string mensaje)
        {
            codigo = GenerarCodigo();
            string to = para;
            string from = "soportecbr@outlook.com";
            string subject = "Codigo Recuperación Contraseña";
            string body = @mensaje + codigo;

            MailMessage message = new MailMessage(from, to, subject, body);

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                Credentials = new NetworkCredential("soportecbr@outlook.com", "nohomo123"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
                MessageBox.Show("Se ha enviado un código a su correo, si no lo encuentra revise la bandeja de SPAM.");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string GenerarCodigo()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            var resultString = new String(Charsarr);
            return resultString;
        }

        private void btnCodigo_Click(object sender, RoutedEventArgs e)
        {
            if (codigo == txtCodigo.Text)
            {
                lbCodigo.Visibility = Visibility.Hidden;
                txtCodigo.Visibility = Visibility.Hidden;
                btnCodigo.Visibility = Visibility.Hidden;
                lbContrasenna.Visibility = Visibility.Visible;
                pssContrasenna.Visibility = Visibility.Visible;
                btnContrasenna.Visibility = Visibility.Visible;
            }else
            {
                MessageBox.Show("Código Incorrecto");
            }
        }

        private void btnContrasenna_Click(object sender, RoutedEventArgs e)
        {
            Cliente clie = new Cliente();
            try
            {
                clie = clie.RecuperarContraseña(txtCorreo.Text);
                if (clie.correo_electronico != string.Empty)
                {
                    if (clie.ModificarContraseña(pssContrasenna.Password, clie.rut_usuario))
                    {
                        MessageBox.Show("Contraseña Modificada Con Éxito ");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar contraseña");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Correo no registrado");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
