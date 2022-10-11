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
using MySql.Data.MySqlClient;
using MessageBox = System.Windows.MessageBox;

namespace UniOnline.Trabajador
{
    /// <summary>
    /// Lógica de interacción para WPF_VisorSolicitud.xaml
    /// </summary>
    public partial class WPF_VisorSolicitud : Window
    {

        public WPF_VisorSolicitud()
        {
            InitializeComponent();
        }
        string nSeguimiento = string.Empty;
        public string ObtenerSeguimiento
        {
            get
            {
                return this.nSeguimiento;
            }
            set
            {
                this.nSeguimiento = value;
                this.nSeguimiento = value;
                LlenadoEstado();
                LlenadoSolicitud(nSeguimiento);
            }
        }
        //Metodo que llena el combobox estado
        public void LlenadoEstado()
        {
            cmbEstado.Items.Add("Aprobado");
            cmbEstado.Items.Add("Rechazado");
            cmbEstado.Items.Add("En Proceso");
        }
        //Metodo que llena los datos de la solicitud
        public void LlenadoSolicitud(string numeroS)
        {
            Cliente cli = new Cliente();
            List<string> soli = new List<string>();
            soli = cli.Solicitud(numeroS);
            if (soli != null)
            {
                lbNumeroSO.Text = soli[0];
                lbFecha.Text = soli[2];
                cmbEstado.SelectedItem = soli[1];
                lbRutSolicitante.Text = soli[3];
                lbNombreSolicitante.Text = soli[4];
                lbFoja.Text = soli[6];
                lbDireccion.Text = soli[8];
                lbDescripcion.Text = soli[7];
                lbNombreTramite.Text = soli[5];
                lbNombreDuenno.Text = soli[9];
                txtComentario.Text = soli[11];
                List<string> doc = cli.Documento(Int32.Parse(soli[10]));
                if (doc.Count != 0)
                {
                    int cant = doc.Count;
                    btnVerDoc.Visibility = Visibility.Visible;
                    lbDocumento.Visibility = Visibility.Visible;
                    lbNombreDoc.Visibility = Visibility.Visible;
                    lbNombreDoc.Text = "Hay " + (cant/2) +" Documentos";
                }
                else
                {
                    btnVerDoc.Visibility = Visibility.Hidden;
                    lbDocumento.Visibility = Visibility.Hidden;
                    lbNombreDoc.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                MessageBox.Show("Solicitud Vacía");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVerDoc_Click(object sender, RoutedEventArgs e)
        {
            WPF_VistaDoc vDoc = new WPF_VistaDoc();
            vDoc.ShowDialog();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(txtFileName.Text != string.Empty)
            {
                Cliente cli = new Cliente();
                if (Update(sender, e))
                {
                    if (cli.ModificarSolicitud(lbNumeroSO.Text, cmbEstado.SelectedItem.ToString(), txtComentario.Text))
                    {

                        MessageBox.Show("Solicitud Guardada con éxito");
                    }
                    else
                    {
                        MessageBox.Show("Error al guardar la solicitud", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Error al subir documento", "Error");
                }
            }
            else
            {
                lbErrorNombre.Content = "Debe ingresar un nombre";
                lbErrorNombre.Visibility = Visibility.Visible;
            }
            
            
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog opendlg = new OpenFileDialog();
                opendlg.Filter = "PDF Files |*.pdf||*.pdf";
                opendlg.ShowDialog();
                labelURL.Content = opendlg.FileName;
                MessageBox.Show("Documento subido con éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al subir el documento "+ ex.Message,"Error");
            }
            


        }
        private bool Update(object sender, RoutedEventArgs e)
        {
            try
            {
                //Checking if the label is empty    
                if (labelURL.Content.ToString() == "URL") //For WPF and labelURL.Text for Windows Form    
                {
                    MessageBox.Show("Seleccione un archivo PDF para subir...", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Cliente cli = new Cliente();
                    List<string> soli = new List<string>();
                    soli = cli.Solicitud(lbNumeroSO.Text);
                    int idSoli = Int32.Parse(soli[10]);
                    //Streaming browse file and convert it into bytes    
                    string Query = "INSERT INTO `UNIONLINE`.`DOCUMENTO` (`nombre_doc`, `doc`, `SOLICITUD_id_soli`, `TIPO_DOCUMENTO_id_tipodoc`) VALUES ('" + txtFileName.Text + "', @doc, " + idSoli + ", 2);";
                    string URLFileName = labelURL.Content.ToString();
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
                    var param2 = new MySqlParameter(@"doc", MySqlDbType.LongBlob, GetPDFFileSize.Length);
                    param2.Value = GetPDFFileSize;
                    cmd.Parameters.Add(param2);
                    int InsertFiles = cmd.ExecuteNonQuery();
                    if (InsertFiles > 0)
                    {
                        //Proceed ..     
                        labelURL.Content = "URL";
                    }
                    con.conex.Close();
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

    }    
}
