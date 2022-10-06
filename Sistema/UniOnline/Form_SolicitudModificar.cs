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
                if (cli.Formulario(txtNombre.Text,txtTelefono.Text, txtCorreo.Text, txtAsunto.Text, txtDescripcion.Text))
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


    }
}
