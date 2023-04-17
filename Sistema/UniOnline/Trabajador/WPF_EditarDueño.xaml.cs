using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using MySql.Data.MySqlClient;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;

namespace UniOnline.Trabajador
{
    public partial class WPF_EditarDueño : Window
    {
        public WPF_EditarDueño()
        {
            InitializeComponent();
        }

        Duenno duenno = new Duenno();
        public Duenno ObtenerDuenno
        {
            get
            {
                return this.duenno;
            }
            set
            {
                this.duenno = value;
                this.duenno = value;
                lblID.Content = duenno.duennoId;
                txtRutDuenno.Text = duenno.RutDuenno;
                txtPrimerNombre.Text = duenno.PrimerNombre;
                txtPrimerApellido.Text = duenno.PrimerApellido;
                txtSegundoNombre.Text = duenno.SegundoNombre;
                txtSegundoApellido.Text = duenno.SegundoApellido;
                txtCorreo.Text = duenno.CorreoElectronico;
                txtTelefono.Text = duenno.Telefono;

            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (txtPrimerNombre.Text != string.Empty && txtPrimerApellido.Text != string.Empty && txtSegundoApellido.Text != string.Empty && txtCorreo.Text != string.Empty && txtTelefono.Text != string.Empty)
            {
                try
                {
                    Duenno duen = new Duenno();
                    string email = txtCorreo.Text;
                    if (duen.CorreoValido(email) == true)
                    {
                        AdvertenciaCor.Text = string.Empty;
                        duen.CorreoElectronico = txtCorreo.Text;
                        string texto = txtTelefono.Text;
                        if (texto.Length > 8)
                        {
                            AdvertenciaTel.Text = string.Empty;
                            if (duen.ModificarDuenno(txtRutDuenno.Text, txtPrimerNombre.Text, txtSegundoNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, txtCorreo.Text, txtTelefono.Text))
                            {
                                Update(sender, e);
                                MessageBox.Show("Dueño Modificado con Éxito");
                            }
                            else
                            {
                                MessageBox.Show("Error al modificar el dueño.", "Error");
                            }
                        }
                        else
                        {
                            AdvertenciaTel.Text = "Teléfono debe tener al menos 9 dígitos.";
                        }
                    }
                    else
                    {
                        AdvertenciaCor.Text = "El formato del correo es invalido.";
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error al modificar el Dueño:" + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Rellene todos los campos", "Advertencia");
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

        private void txtRutDuenno_KeyUp(object sender, KeyEventArgs e)
        {
            txtRutDuenno.Text = FormatearRut(txtRutDuenno.Text);
            txtRutDuenno.SelectionStart = txtRutDuenno.Text.Length;
            txtRutDuenno.SelectionLength = 0;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Duenno duen = new Duenno();
            MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar este dueño", "Eliminar Dueño", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    if (duen.EliminarDuenno(txtRutDuenno.Text))
                    {
                        MessageBox.Show("Dueño Eliminado con Éxito");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error al Eliminar", "Error");
                    }
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("Dueño no eliminado");
                    break;
            }
        }

        private void btnVerCarnet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Conexion con = new Conexion();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog { Title = "Descargar documento.." };
                saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

                string sql = "SELECT copia_carnet FROM UNIONLINE.DUENNO_PROP WHERE rut_duenno = '" + txtRutDuenno.Text + "';";

                con.Conectar();
                MySqlDataAdapter adp = new MySqlDataAdapter(sql, con.conex);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                byte[] b = (byte[])dt.Rows[0]["copia_carnet"];
                saveFileDialog1.FileName = "COPIACARNET"+"_"+txtPrimerNombre.Text+"_"+txtPrimerApellido.Text+"_"+txtRutDuenno.Text;
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

        private void btnCarnet_Click(object sender, RoutedEventArgs e)
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
                    string Query = "UPDATE `UNIONLINE`.`DUENNO_PROP` SET `copia_carnet` = @copia_carnet  WHERE `id_duenno` = " + lblID.Content + ";";
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
                    var param2 = new MySqlParameter(@"copia_carnet", MySqlDbType.LongBlob, GetPDFFileSize.Length);
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
    }
}
