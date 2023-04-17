using Controlador;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Forms;
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
                    lbNombreDoc.Text = "Hay " + (cant / 2) + " Documentos.";
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

            try
            {
                Conexion con = new Conexion();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog { Title = "Descargar documento.." };
                saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

                string sql = "SELECT nombre_doc, doc FROM UNIONLINE.DOCUMENTO inner join UNIONLINE.SOLICITUD on UNIONLINE.DOCUMENTO.SOLICITUD_id_soli = UNIONLINE.SOLICITUD.id_soli where numero_seguimiento = '" + nSeguimiento + "' and TIPO_DOCUMENTO_id_tipodoc = 1;";
                
                con.Conectar();
                MySqlDataAdapter adp = new MySqlDataAdapter(sql, con.conex);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count != 0 && dt.Rows.Count < 2)
                {
                    byte[] b = (byte[])dt.Rows[0]["doc"];
                    saveFileDialog1.FileName = dt.Rows[0]["nombre_doc"].ToString();
                    string filename = saveFileDialog1.FileName;
                    saveFileDialog1.ShowDialog();
                    var saveFileDialogStream = saveFileDialog1.OpenFile();
                    saveFileDialogStream.Write(b, 0, b.Length);
                    saveFileDialogStream.Close();
                    MessageBox.Show("Documento descargado.");
                }
                else if (dt.Rows.Count > 1)
                {
                    WPF_DescargaArchivos da = new WPF_DescargaArchivos();
                    da.ObtenerSeguimiento = nSeguimiento;
                    da.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error.", "Advertencia");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el documento. " + ex.Message, "Advertencia");
            }

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(cmbEstado.SelectedItem.ToString() == "Aprobado")
            {
                if (txtFileName.Text != string.Empty)
                {
                    Cliente cli = new Cliente();
                    if (cli.ModificarSolicitud(lbNumeroSO.Text, cmbEstado.SelectedItem.ToString(), txtComentario.Text))
                    {

                        if (Update(sender, e))
                        {
                            if (cmbEstado.SelectedItem.ToString() == "Aprobado")
                            {
                                btnEnviarCorreo_Click(sender, e);
                            }
                            else
                            {
                                labelURL.Content = "URL";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error al guardar la solicitud", "Advertencia.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al subir documento", "Advertencia.");
                    }
                }
                else
                {
                    lbErrorNombre.Content = "Debe elegir un archivo.";
                    lbErrorNombre.Visibility = Visibility.Visible;
                }
            }
            else
            {
                Cliente cli = new Cliente();
                if (cli.ModificarSolicitud(lbNumeroSO.Text, cmbEstado.SelectedItem.ToString(), txtComentario.Text))
                {
                    MessageBox.Show("Solicitud Modificada Correctamente.");
                }
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
                txtFileName.Text = opendlg.SafeFileName;
                MessageBox.Show("Documento subido con éxito");
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

        private void btnEnviarCorreo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente();
                lblCorreoDuenno.Content = cli.CorreoUsu(lbRutSolicitante.Text);

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("soportecbr@outlook.com");
                mail.To.Add(lblCorreoDuenno.Content.ToString());
                mail.Subject = "Copia de Trámite solicitado";
                mail.Body = "Estimado cliente: "+ lbNombreSolicitante.Text + ". \nJunto con saludar se adjunta el trámite solicitado: "+ lbNombreTramite.Text + "\nSaludos. \n\n\n\n________________________________________________________\nConservador De Bienes Raíces Chile\nGobierno de Chile\nMinisterio de Vivienda y Urbanismo | Gobierno de Chile";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(labelURL.Content.ToString());

                mail.Attachments.Add(attachment);

                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    Credentials = new NetworkCredential("soportecbr@outlook.com", "nohomo123"),
                    EnableSsl = true
                };

                try
                {
                    client.Send(mail);
                    MessageBox.Show("Se ha enviado una copia del trámite al cliente "+ lbNombreSolicitante.Text + ". \nSi no lo encuentra en la bandeja principal que revise la bandeja de SPAM.", "Correo enviado");
                    labelURL.Content = "URL";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
