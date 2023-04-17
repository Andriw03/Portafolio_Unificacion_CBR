using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controlador;

namespace UniOnline
{
    public partial class Form_SolicitudModificar : Form
    {
        public Form_SolicitudModificar()
        {
            InitializeComponent();
            Conectar();
        }

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != string.Empty && txtRut.Text != string.Empty && txtTelefono.Text != string.Empty && txtDescripcion.Text != string.Empty && txtCorreo.Text != string.Empty && txtAsunto.Text != string.Empty)
            {
                Cliente cli = new Cliente();
                if (cli.Formulario(txtNombre.Text,txtTelefono.Text, txtCorreo.Text, txtAsunto.Text, txtDescripcion.Text, txtRut.Text))
                {
                    MessageBox.Show("Formulario ingresado con éxito");
                }
                else
                {
                    MessageBox.Show("Error al ingresar la solicitud", "Error");
                }
            }else
            {
                MessageBox.Show("Debe llenar todos los campos","Advertencia");
            }
        }

        private void txtRut_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtRut_KeyUp(object sender, EventArgs e)
        {
            txtRut.Text = FormatearRut(txtRut.Text);
            txtRut.SelectionStart = txtRut.Text.Length;
            txtRut.SelectionLength = 0;
        }
        // solo numeros, puntos, guion y letra K
        private void txtRut_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar.ToString().ToUpper().Equals("K"))
            {
                e.Handled = false;
            }
            else if (e.KeyChar.ToString().ToUpper().Equals("-"))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
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


    }
}
