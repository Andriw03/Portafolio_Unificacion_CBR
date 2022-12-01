using Controlador;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using MessageBox = System.Windows.MessageBox;

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_EditarProp.xaml
    /// </summary>
    public partial class WPF_EditarProp : Window
    {
        public WPF_EditarProp()
        {
            InitializeComponent();
            Conectar();
            LlenarCmbPropiedad();

        }
        int v_foja = 0;
        public int ObtenerFoja
        {
            get
            {
                return this.v_foja;
            }
            set
            {
                this.v_foja = value;
                this.v_foja = value;
                LlenadoPropiedad(v_foja);
            }
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
        //Llenado de campos
        private void LlenadoPropiedad(int id)
        {
            Propiedad prop = new Propiedad();
            DataTable tabla = prop.MostrarPropiedad(v_foja);
            Duenno duenno = new Duenno();
            if (tabla != null)
            {
                try
                {
                    txtDueño.Text = tabla.Rows[0]["rut_duenno"].ToString();
                    duenno = duenno.BuscarDuenno(txtDueño.Text);
                    txtRutDueño.Text = duenno.RutDuenno;
                    txtNombreDueño.Text = duenno.PrimerNombre + ' ' + duenno.PrimerApellido;
                    txtFoja.Text = tabla.Rows[0]["foja"].ToString();
                    //txtNumero.Text = tabla.Rows[0]["rut_duenno"].ToString();
                    //txtAnno.Text = ;
                    cmbTipoProp.SelectedItem = tabla.Rows[0]["nombre_tipoP"].ToString();
                    txtRazonSocial.Text = tabla.Rows[0]["razon_social"].ToString();
                    txtRutEmpresa.Text = tabla.Rows[0]["rut_empresa"].ToString();
                    txtDescripcion.Text = tabla.Rows[0]["descripcion"].ToString();
                    txtDireccion.Text = tabla.Rows[0]["direccion_prop"].ToString();
                    lblID.Content = tabla.Rows[0]["id_propiedad"].ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Tabla sin registro", "Error");
            }
        }
        //Método para llenar el combobox tipo de propiedad
        private void LlenarCmbPropiedad()
        {
            cmbTipoProp.Items.Clear();
            cmbTipoProp.Items.Add("-------");
            List<string> registro = con.Llenado("TIPO_PROPIEDAD", "nombre_tipoP");
            if (registro != null)
            {
                for (int i = 0; i < registro.Count(); i++)
                {
                    cmbTipoProp.Items.Add(registro[i]);
                }
                cmbTipoProp.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Error", "Error Conexion TIPO_PROPIEDAD");
            }
        }
        //Botón para volver a la vista principal de Trabajador
        private void btnVolverProp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Boton que busca el dueño en base su rut
        private void btnBuscarDueño_Click(object sender, RoutedEventArgs e)
        {
            if (txtDueño.Text != string.Empty)
            {
                Duenno duenno = new Duenno();
                duenno = duenno.BuscarDuenno(txtDueño.Text);
                if (duenno != null)
                {
                    txtRutDueño.Text = duenno.RutDuenno;
                    txtNombreDueño.Text = duenno.PrimerNombre + ' ' + duenno.PrimerApellido;
                }
                else
                {
                    MessageBox.Show("Dueño vacío", "Error");
                }
            }
            else
            {
                MessageBox.Show("Debe digitar un Rut para usar esta función", "Error");
            }
        }

        private void btnAgregarDuenno_Click(object sender, RoutedEventArgs e)
        {
            WPF_Trabajador_Dueño tdueño = new WPF_Trabajador_Dueño();
            tdueño.ShowDialog();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtRutDueño.Text != string.Empty && txtFoja.Text != string.Empty && txtDescripcion.Text != string.Empty && txtDireccion.Text != string.Empty && cmbTipoProp.SelectedItem != null)
            {
                Propiedad prop = new Propiedad();
                MessageBoxResult result = MessageBox.Show("Seguro que desea modificar esta propiedad", "Modificar Propiedad", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        if (prop.ModificarProp(txtDescripcion.Text, cmbTipoProp.SelectedIndex, txtRutDueño.Text, Int32.Parse(txtFoja.Text),txtRutEmpresa.Text,txtRazonSocial.Text))
                        {
                            Update(sender, e);
                            MessageBox.Show("Propiedad Modificada con Éxito");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error en modificar", "Error");
                        }
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Propiedad no modificada");
                        break;
                }
                
            }
            else
            {
                MessageBox.Show("Rellene todos los campos","Advertencia");
            }
        }
        private void cmbTipoProp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbTipoProp.SelectedItem.ToString() == "Comercial")
            {
                txtRazonSocial.IsEnabled = true;
                txtRutEmpresa.IsEnabled = true;
            }
            else
            {
                txtRazonSocial.IsEnabled = false;
                txtRutEmpresa.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            Propiedad prop = new Propiedad();
            MessageBoxResult result = MessageBox.Show("Seguro que desea modificar esta propiedad", "Modificar Propiedad", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (prop.EliminarProp(Int32.Parse(txtFoja.Text)))
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
                    break;
            }
            
        }

        private void btnModificarDuenno_Click(object sender, RoutedEventArgs e)
        {
            Duenno due = new Duenno();
            WPF_EditarDueño edue = new WPF_EditarDueño();
            edue.ObtenerDuenno = due.BuscarDuenno(txtDueño.Text);
            edue.ShowDialog();
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
        private void btnVerDocu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conexion con = new Conexion();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog { Title = "Descargar documento.." };
                saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

                string sql = "SELECT escritura FROM UNIONLINE.PROPIEDAD as prop inner join UNIONLINE.CLAS_PROP as cprop on prop.CLAS_PROP_id_clas = cprop.id_clas where cprop.foja = " + v_foja + ";";

                con.Conectar();
                MySqlDataAdapter adp = new MySqlDataAdapter(sql, con.conex);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                byte[] b = (byte[])dt.Rows[0]["escritura"];
                saveFileDialog1.FileName = "ESCRITURA" + "_" + v_foja;
                string filename = saveFileDialog1.FileName;
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var saveFileDialogStream = saveFileDialog1.OpenFile();
                    saveFileDialogStream.Write(b, 0, b.Length);
                    saveFileDialogStream.Close();
                    MessageBox.Show("Documento descargado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el documento. " + ex.Message, "Advertencia");
            }
        }

        private void btnDocu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog opendlg = new OpenFileDialog();
                opendlg.Filter = "PDF Files |*.pdf||*.pdf";
                opendlg.ShowDialog();
                lblURL.Content = opendlg.FileName;
                if (lblURL.Content.ToString() != string.Empty)
                {
                    MessageBox.Show("Documento subido con éxito");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al subir el documento " + ex.Message, "Error");
            }
        }

        private bool Update(object sender, RoutedEventArgs e)
        {
            try
            {
                //Checking if the label is empty    
                if (lblURL.Content.ToString() == string.Empty) //For WPF and labelURL.Text for Windows Form    
                {

                }
                else
                {
                    //Streaming browse file and convert it into bytes    
                    string Query = "UPDATE `UNIONLINE`.`PROPIEDAD` SET `escritura` = @escritura WHERE `id_propiedad` = " + lblID.Content + " ;";
                    string URLFileName = lblURL.Content.ToString();
                    byte[] GetPDFFileSize;
                    FileStream stream = new FileStream(URLFileName, FileMode.Open, FileAccess.ReadWrite);
                    BinaryReader breader = new BinaryReader(stream);
                    GetPDFFileSize = new byte[stream.Length];
                    GetPDFFileSize = breader.ReadBytes((int)stream.Length);
                    stream.Close();
                    //Mysql Update Codes    
                    Conexion con = new Conexion();
                    con.Conectar();
                    MySqlCommand cmd = new MySqlCommand(Query, con.conex);
                    var param2 = new MySqlParameter(@"escritura", MySqlDbType.LongBlob, GetPDFFileSize.Length);
                    param2.Value = GetPDFFileSize;
                    cmd.Parameters.Add(param2);
                    int InsertFiles = cmd.ExecuteNonQuery();
                    if (InsertFiles > 0)
                    {
                        lblURL.Content = string.Empty;
                    }
                    con.conex.Close();

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al subir archivo. " + ex.Message);
                return false;
            }
        }

        private void txtRutEmpresa_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            txtRutEmpresa.Text = FormatearRut(txtRutEmpresa.Text);
            txtRutEmpresa.SelectionStart = txtRutEmpresa.Text.Length;
            txtRutEmpresa.SelectionLength = 0;
        }
    }
}
