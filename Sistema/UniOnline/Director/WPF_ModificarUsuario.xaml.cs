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
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Security.Cryptography;


namespace UniOnline.Director
{
    /// <summary>
    /// Lógica de interacción para WPF_ModificarUsuario.xaml
    /// </summary>
    public partial class WPF_ModificarUsuario : Window
    {
        public WPF_ModificarUsuario()
        {
            InitializeComponent();
            Conectar();
            LlenadoTipoU();
        }

        private void LlenadoTipoU()
        {
            cmbTipoU.Items.Clear();
            cmbTipoU.Items.Add("-------");
            List<string> registro = con.Llenado("T_USUARIO", "nombre_tipoU");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbTipoU.Items.Add(registro[i]);
                }
                cmbTipoU.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error Conexion T_TRAMITE.", "Advertencia");
            }
        }

        Conexion con = new Conexion();
        private void Conectar()
        {
            if (con.Conectar())
            {

            }
            else
            {
                MessageBox.Show("Error de Conexión.");
            }
        }

        private void rdBtn_Recepcionista_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void txtBoxRUT_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxSNombre_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnBuscarRUT_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxRUT.Text != string.Empty)
            {
                Hashing hash = new Hashing();
                Usuario usu = new Usuario();
                usu = usu.BuscarUsuario(txtBoxRUT.Text);
                if (usu != null)
                {
                    txtBoxNombre.Text = usu.primer_nombre;
                    txtBoxSNombre.Text = usu.segundo_nombre;
                    txtBoxApellido.Text = usu.primer_apellido;
                    txtBoxSApellido.Text = usu.segundo_apellido;
                    txtBoxTelefono.Text = usu.telefono;
                    txtBoxCorreo.Text = usu.correo_electronico;
                    txtBoxContra.Text =usu.contrasenna;
                    if (usu.id_tipoU == 4)
                    {
                        cmbTipoU.SelectedItem = "Trabajador";
                    }
                    else if (usu.id_tipoU == 3)
                    {
                        cmbTipoU.SelectedItem = "Recepcionista";
                    }
                    else if (usu.id_tipoU == 6)
                    {
                        cmbTipoU.SelectedItem = "Moderador";
                    }
                }
                else
                {
                    MessageBox.Show("Usuario no existe", "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe digitar un Rut para usar esta función", "Error");
            }


        }

        private void txtBoxApellido_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxSApellido_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxCorreo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtBoxContra_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            {
                if (txtBoxRUT.Text != string.Empty && txtBoxContra.Text != string.Empty && txtBoxNombre.Text != string.Empty && txtBoxSNombre.Text != string.Empty && txtBoxApellido.Text != string.Empty && txtBoxSApellido.Text != string.Empty && txtBoxCorreo.Text != string.Empty && txtBoxTelefono.Text != string.Empty)
                {
                    Usuario usu = new Usuario();
                    int idTUser = 0;
                    //if ((bool)rdBtn_Recepcionista.IsChecked)
                    //{
                    //    idTUser = 3;
                    //}
                    //else if ((bool)rdBtn_Moderador.IsChecked)
                    //{
                    //    idTUser = 6;
                    //}
                    //else if ((bool)rdBtn_Trabajador.IsChecked)
                    //{
                    //    idTUser = 4;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Seleccione un rol");
                    //}
                    if (cmbTipoU.SelectedItem.ToString() == "Recepcionista")
                    {
                        idTUser = 3;
                    }
                    else if (cmbTipoU.SelectedItem.ToString() == "Moderador")
                    {
                        idTUser = 6;
                    }
                    else if (cmbTipoU.SelectedItem.ToString() == "Trabajador")
                    {
                        idTUser = 4;
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un rol");
                    }
                    Hashing hash = new Hashing();
                    if (usu.ModificarUsuario(txtBoxRUT.Text, txtBoxNombre.Text, txtBoxSNombre.Text, txtBoxApellido.Text, txtBoxSApellido.Text, txtBoxTelefono.Text, txtBoxCorreo.Text, hash.ToSHA256(txtBoxContra.Text), idTUser))

                    {

                        MessageBox.Show("Usuario Modificado con Éxito");

                    }
                    else
                    {
                        MessageBox.Show("Error en el Update", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Rellene todos los campos", "Advertencia");
                }
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxRUT.Text != string.Empty && txtBoxContra.Text != string.Empty && txtBoxNombre.Text != string.Empty && txtBoxApellido.Text != string.Empty && txtBoxSApellido.Text != string.Empty && txtBoxCorreo.Text != string.Empty && txtBoxTelefono.Text != string.Empty)
            {
                try
                {
                    Hashing hash = new Hashing();
                    Usuario usu = new Usuario();
                    if (!usu.ExisteUsuario(txtBoxRUT.Text))
                    {
                        usu.rut_usuario = txtBoxRUT.Text;
                        usu.contrasenna = hash.ToSHA256(txtBoxContra.Text);
                        usu.primer_nombre = txtBoxNombre.Text;
                        usu.segundo_nombre = txtBoxSNombre.Text;
                        usu.primer_apellido = txtBoxApellido.Text;
                        usu.segundo_apellido = txtBoxSApellido.Text;
                        usu.telefono = txtBoxTelefono.Text;
                        usu.id_cbr = 1;


                        string email = txtBoxCorreo.Text;
                        if (!usu.CorreoValido(email) == true)
                        {
                            AdvertenciaCor.Text = "El formato del correo es invalido.";
                        }
                        else
                        {
                            AdvertenciaCor.Text = string.Empty;
                            usu.correo_electronico = txtBoxCorreo.Text;
                            string texto = txtBoxTelefono.Text;
                            if (texto.Length <= 8)
                            {
                                AdvertenciaTel.Text = "Teléfono debe tener al menos 9 dígitos.";
                            }
                            else
                            {

                                AdvertenciaTel.Text = string.Empty;
                                usu.telefono = txtBoxTelefono.Text;

                                if (cmbTipoU.SelectedItem.ToString() == "Recepcionista")
                                {
                                    usu.id_tipoU = 3;
                                    MessageBox.Show(usu.Insertar(usu), "Mensaje");
                                }
                                else if (cmbTipoU.SelectedItem.ToString() == "Trabajador")
                                {
                                    usu.id_tipoU = 4;
                                    MessageBox.Show(usu.Insertar(usu), "Mensaje");
                                }
                                else if (cmbTipoU.SelectedItem.ToString() == "Moderador")
                                {
                                    usu.id_tipoU = 6;
                                    MessageBox.Show(usu.Insertar(usu), "Mensaje");
                                }
                                else
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                        }
                        
                        
                    }
                    else
                    {
                        MessageBox.Show("Error El Usuario ya se encuentra registrado");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al ingresar el Usuario:" + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Error, debe ingresar los campos porfavor");
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

        

        private void txtBoxRUT_KeyUp(object sender, KeyEventArgs e)
        {
            txtBoxRUT.Text = FormatearRut(txtBoxRUT.Text);
            txtBoxRUT.SelectionStart = txtBoxRUT.Text.Length;
            txtBoxRUT.SelectionLength = 0;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usu = new Usuario();
            MessageBoxResult result = MessageBox.Show("Seguro que desea modificar este Usuario", "Modificar Usuario", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (usu.eliminarUsuario(txtBoxRUT.Text))
                    {
                        MessageBox.Show("Propiedad Eliminada con Éxito");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error en el Eliminar", "Error");
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Propiedad no modificada");
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void cmbTipoU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}