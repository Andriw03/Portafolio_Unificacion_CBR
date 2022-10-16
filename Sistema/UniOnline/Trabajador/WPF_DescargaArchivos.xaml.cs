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
using Controlador;
using MySql.Data.MySqlClient;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;

namespace UniOnline.Trabajador
{
    public partial class WPF_DescargaArchivos : Window
    {
        public WPF_DescargaArchivos()
        {
            InitializeComponent();
        }
        string nSeguimiento = string.Empty;
        Conexion conex = new Conexion();
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
                MostrarDocumentos(nSeguimiento);
            }
        }

        private void MostrarDocumentos(string nSegui)
        {
            conex.Conectar();
            DataTable tabla = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT id_documento,nombre_doc, doc FROM UNIONLINE.DOCUMENTO inner join UNIONLINE.SOLICITUD on UNIONLINE.DOCUMENTO.SOLICITUD_id_soli = UNIONLINE.SOLICITUD.id_soli where numero_seguimiento = '" + nSegui + "' and TIPO_DOCUMENTO_id_tipodoc = 1;", conex.conex);
                MySqlDataAdapter ap = new MySqlDataAdapter(cmd);
                ap.Fill(tabla);
                cmd.Dispose();
                ap.Dispose();
                if (tabla != null)
                {
                    dtgDescargar.ItemsSource = tabla.DefaultView;
                    dtgDescargar.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Tabla sin registro", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDescargar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView dataView = (DataRowView)((Button)e.Source).DataContext;
                Conexion con = new Conexion();
                int idDoc = Int32.Parse(dataView[0].ToString());

                SaveFileDialog saveFileDialog1 = new SaveFileDialog { Title = "Descargar documento.." };
                saveFileDialog1.Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

                string sql = "SELECT nombre_doc, doc FROM UNIONLINE.DOCUMENTO where id_documento = " + idDoc + " and TIPO_DOCUMENTO_id_tipodoc = 1;";

                con.Conectar();
                MySqlDataAdapter adp = new MySqlDataAdapter(sql, con.conex);
                DataTable dt = new DataTable();

                adp.Fill(dt);

                if (dt.Rows.Count != 0)
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
    }
}
