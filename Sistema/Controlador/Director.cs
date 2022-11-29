using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using MySql.Data;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using VerticalAlignment = iText.Layout.Properties.VerticalAlignment;
using System.Text.RegularExpressions;

namespace Controlador
{
    public class Usuario : Conexion
    {
        public int id_usuario { get; set; }
        public string rut_usuario { get; set; }
        public string contrasenna { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public int id_cbr { get; set; }
        public int id_tipoU { get; set; }
        public string detalle_form { get; set; }

        public Usuario()
        {
            this.Init();
        }

        private void Init()
        {
            id_usuario = 0;
            rut_usuario = string.Empty;
            contrasenna = string.Empty;
            primer_nombre = string.Empty;
            segundo_nombre = string.Empty;
            primer_apellido = string.Empty;
            segundo_apellido = string.Empty;
            correo_electronico = string.Empty;
            telefono = string.Empty;
            id_cbr = 1;
            id_tipoU = 0;
        }

        public string Insertar(Usuario usu)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("insert into USUARIO (`rut_usuario`,`contrasenna`,`primer_nombre`,`segundo_nombre`,`primer_apellido`,`segundo_apellido`,`correo_electronico`,`telefono`,`CBR_id_cbr`,`T_USUARIO_id_tipoU`) VALUES ('" + usu.rut_usuario + "','" + usu.contrasenna + "','" + usu.primer_nombre + "','" + usu.segundo_nombre + "','" + usu.primer_apellido + "','" + usu.segundo_apellido + "','" + usu.correo_electronico + "','" + usu.telefono + "'," + usu.id_cbr + "," + usu.id_tipoU + ")", conex);
                cmd.ExecuteNonQuery();
                salida = "Usuario agregado correctamente.";
            }
            catch (Exception ex)
            {
                salida = "Error al agregar el Cliente: " + ex.ToString();
            }
            return salida;
        }
        public bool ExisteUsuario(string id)
        {

            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM USUARIO where rut_usuario = '" + id + "' ", conex);
                rd = cmd.ExecuteReader();
                bool e = rd.Read();
                rd.Close();
                return e;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;

            }
        }
        public Usuario LoginUsuario(string rut)
        {
            Usuario usuario = new Usuario();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT id_usuario, rut_usuario, contrasenna, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, correo_electronico, telefono, CBR_id_cbr, T_USUARIO_id_tipoU FROM UNIONLINE.USUARIO where rut_usuario = '" + rut + "' and T_USUARIO_id_tipoU not in (1,5);", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    usuario.id_usuario = Int32.Parse(rd["id_usuario"].ToString());
                    usuario.rut_usuario = rd["rut_usuario"].ToString();
                    usuario.contrasenna = rd["contrasenna"].ToString();
                    usuario.primer_nombre = rd["primer_nombre"].ToString();
                    usuario.segundo_nombre = rd["segundo_nombre"].ToString();
                    usuario.primer_apellido = rd["primer_apellido"].ToString();
                    usuario.segundo_apellido = rd["segundo_apellido"].ToString();
                    usuario.correo_electronico = rd["correo_electronico"].ToString();
                    usuario.telefono = rd["telefono"].ToString();
                    usuario.id_cbr = Int32.Parse(rd["CBR_id_cbr"].ToString());
                    usuario.id_tipoU = Int32.Parse(rd["T_USUARIO_id_tipoU"].ToString());
                }
                rd.Close();
                return usuario;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public Usuario BuscarUsuario(string rut)
        {
            try
            {

                Conectar();
                cmd = new MySqlCommand("SELECT * FROM `UNIONLINE`.`USUARIO` where rut_usuario =  '" + rut + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    id_usuario = Int32.Parse(rd["id_usuario"].ToString());
                    rut_usuario = rd["rut_usuario"].ToString();
                    contrasenna = rd["contrasenna"].ToString();
                    primer_nombre = rd["primer_nombre"].ToString();
                    segundo_nombre = rd["segundo_nombre"].ToString();
                    primer_apellido = rd["primer_apellido"].ToString();
                    segundo_apellido = rd["segundo_apellido"].ToString();
                    correo_electronico = rd["correo_electronico"].ToString();
                    telefono = rd["telefono"].ToString();
                    id_tipoU = Int32.Parse(rd["T_USUARIO_id_tipoU"].ToString());

                }
                rd.Close();
                return this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }

        public bool ModificarUsuario(string rutUsuario, string Nombre, string SNombre, string Apellido, string SApellido, string Telefono, string Correo, string Contrasenna, int idTUser)
        {

            try
            {
                Conectar();
                int idUsuario = 0;
                int idTu = 0;
                cmd = new MySqlCommand("SELECT id_usuario, T_USUARIO_id_tipoU from T_USUARIO INNER JOIN USUARIO ON T_USUARIO.id_tipoU = USUARIO.T_USUARIO_id_tipoU WHERE rut_usuario = '" + rutUsuario + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idUsuario = Int32.Parse(rd["id_usuario"].ToString());
                    idTu = Int32.Parse(rd["T_USUARIO_id_tipoU"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("UPDATE USUARIO SET contrasenna = '" + Contrasenna + "', primer_nombre = '" + Nombre + "', segundo_nombre = '" + SNombre + "', primer_apellido = '" + Apellido + "', segundo_apellido = '" + SApellido + "', correo_electronico = '" + Correo + "', telefono = '" + Telefono + "' , T_USUARIO_id_tipoU = " + idTUser + " WHERE id_usuario = " + idUsuario + " ;", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public void crearPDF()
        {
            try
            {
                
                PdfWriter pdfWriter = new PdfWriter("Reporte.pdf");
                PdfDocument pdf = new PdfDocument(pdfWriter);
                Document documento = new Document(pdf, PageSize.LETTER);

                documento.SetMargins(60, 10, 55, 10);

                PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                string[] columnas = { "ID Boleta", "Fecha Emisión", "Monto de Boleta", "Metodo de Pago", "Conservador Perteneciente" };

                float[] tamanios = { 2, 4, 4, 3, 5 };
                Table tabla = new Table(UnitValue.CreatePercentArray(tamanios));
                tabla.SetWidth(UnitValue.CreatePercentValue(100));


                foreach (string columna in columnas)
                {
                    tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
                }
                try
                {
                    Conectar();
                    cmd = new MySqlCommand("SELECT id_boleta, fecha_emision, monto_pago, T_PAGO.nombre_tipoP, CBR.nombre_cbr FROM UNIONLINE.B_PAGO INNER JOIN T_PAGO ON T_PAGO.id_tipoP = B_PAGO.tipo_pago INNER JOIN CBR ON CBR.id_cbr = B_PAGO.nombre_conservador;", conex);
                    cmd.ExecuteNonQuery();
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["id_boleta"].ToString()).SetFont(fontContenido)));
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["fecha_emision"].ToString()).SetFont(fontContenido)));
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["monto_pago"].ToString()).SetFont(fontContenido)));
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["nombre_tipoP"].ToString()).SetFont(fontContenido)));
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["nombre_cbr"].ToString()).SetFont(fontContenido)));
                    }
                    rd.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
                

                documento.Add(tabla);
                documento.Close();

                var logo = new Image(ImageDataFactory.Create(@"D:\Documentos\Github\Portafolio_Unificacion_CBR\Sistema\UniOnline\LogoCBR.png")).SetWidth(50);
                var plogo = new Paragraph("").Add(logo);

                var titulo = new Paragraph("Reporte de Ventas");
                titulo.SetTextAlignment(TextAlignment.CENTER);
                titulo.SetFontSize(12);

                var dfecha = DateTime.Now.ToString("dd-MM-yyyy");
                var dhora = DateTime.Now.ToString("hh:mm:ss");
                var fecha = new Paragraph("Fecha: " + dfecha + "\nHora: " + dhora);
                fecha.SetFontSize(12);

                PdfDocument pdfDoc = new PdfDocument(new PdfReader("Reporte.pdf"), new PdfWriter(@"C:\Reportes\Reporte de Ventas "+dfecha+".pdf"));
                Document doc = new Document(pdfDoc);

                int numeros = pdfDoc.GetNumberOfPages();

                for (int i = 1; i <= numeros; i++)
                {
                    PdfPage pagina = pdfDoc.GetPage(i);

                    float y = pdfDoc.GetPage(i).GetPageSize().GetTop() - 15;
                    doc.ShowTextAligned(plogo, 40, y, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    doc.ShowTextAligned(titulo, 150, y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    doc.ShowTextAligned(fecha, 520, y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);

                    doc.ShowTextAligned(new Paragraph(string.Format("Página {0} de {1}", i, numeros)), pdfDoc.GetPage(i).GetPageSize().GetWidth() / 2, pdfDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                }
                doc.Close();
            }
            catch
            {
                MessageBox.Show("Informe generado con éxito.");
            }
            
        }


        public void crearPDFTramites()
        {
            try
            {

                PdfWriter pdfWriter = new PdfWriter("Reporte.pdf");
                PdfDocument pdf = new PdfDocument(pdfWriter);
                Document documento = new Document(pdf, PageSize.LETTER);

                documento.SetMargins(60, 10, 55, 10);

                PdfFont fontColumnas = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                PdfFont fontContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                string[] columnas = { "ID Solicitud", "Fecha Emisión", "Nombre Trámite" };

                float[] tamanios = { 2, 4, 4 };
                Table tabla = new Table(UnitValue.CreatePercentArray(tamanios));
                tabla.SetWidth(UnitValue.CreatePercentValue(100));


                foreach (string columna in columnas)
                {
                    tabla.AddHeaderCell(new Cell().Add(new Paragraph(columna).SetFont(fontColumnas)));
                }
                try
                {
                    Conectar();
                    cmd = new MySqlCommand("SELECT distinct id_soli, fecha_solicitud , nombre_tramite FROM UNIONLINE.SOLICITUD join TRAMITE ON TRAMITE_id_tramite = SOLICITUD.TRAMITE_id_tramite group by id_soli;", conex);
                    cmd.ExecuteNonQuery();
                    rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["id_soli"].ToString()).SetFont(fontContenido)));
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["fecha_solicitud"].ToString()).SetFont(fontContenido)));
                        tabla.AddCell(new Cell().Add(new Paragraph(rd["nombre_tramite"].ToString()).SetFont(fontContenido)));
                       
                    }
                    rd.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }


                documento.Add(tabla);
                documento.Close();

                var logo = new Image(ImageDataFactory.Create(@"D:\Documentos\Github\Portafolio_Unificacion_CBR\Sistema\UniOnline\LogoCBR.png")).SetWidth(50);
                var plogo = new Paragraph("").Add(logo);

                var titulo = new Paragraph("Reporte de Trámites");
                titulo.SetTextAlignment(TextAlignment.CENTER);
                titulo.SetFontSize(12);

                var dfecha = DateTime.Now.ToString("dd-MM-yyyy");
                var dhora = DateTime.Now.ToString("hh:mm:ss");
                var fecha = new Paragraph("Fecha: " + dfecha + "\nHora: " + dhora);
                fecha.SetFontSize(12);

                PdfDocument pdfDoc = new PdfDocument(new PdfReader("Reporte.pdf"), new PdfWriter(@"C:\Reportes\Reporte de Trámites " + dfecha + ".pdf"));
                Document doc = new Document(pdfDoc);

                int numeros = pdfDoc.GetNumberOfPages();

                for (int i = 1; i <= numeros; i++)
                {
                    PdfPage pagina = pdfDoc.GetPage(i);

                    float y = pdfDoc.GetPage(i).GetPageSize().GetTop() - 15;
                    doc.ShowTextAligned(plogo, 40, y, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    doc.ShowTextAligned(titulo, 150, y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                    doc.ShowTextAligned(fecha, 520, y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);

                    doc.ShowTextAligned(new Paragraph(string.Format("Página {0} de {1}", i, numeros)), pdfDoc.GetPage(i).GetPageSize().GetWidth() / 2, pdfDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                }
                doc.Close();
            }
            catch
            {
                MessageBox.Show("Informe generado con éxito.");
            }

        }



        public DataTable MostrarFormulario()
        {
            Conectar();
            DataTable tabla = new DataTable();


            try
            {
                cmd = new MySqlCommand("SELECT id_formulario, FORMULARIO.nombre_form, T_USUARIO.nombre_tipoU, FORMULARIO.correo_form, FORMULARIO.estado,FORMULARIO.asunto_form, FORMULARIO.detalle_form FROM UNIONLINE.FORMULARIO inner join UNIONLINE.USUARIO on UNIONLINE.FORMULARIO.USUARIO_id_usuario = UNIONLINE.USUARIO.id_usuario inner join UNIONLINE.T_USUARIO on UNIONLINE.USUARIO.T_USUARIO_id_tipoU = UNIONLINE.T_USUARIO.id_tipoU where FORMULARIO.estado = 'En Proceso';", conex);
                MySqlDataAdapter ap = new MySqlDataAdapter(cmd);
                ap.Fill(tabla);
                cmd.Dispose();
                ap.Dispose();
                return tabla;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }

        public List<string> Descripcion(string idFormulario)
        {
            List<string> formulario = new List<string>();
            try
            {
                Conectar();
                int idFor = Int32.Parse(idFormulario);
                cmd = new MySqlCommand("SELECT  rut_usuario, concat(primer_nombre, ' ', primer_apellido) nombre_usuario ,asunto_form, detalle_form FROM UNIONLINE.FORMULARIO inner join UNIONLINE.USUARIO on UNIONLINE.FORMULARIO.USUARIO_id_usuario = UNIONLINE.USUARIO.id_usuario where id_formulario = " + idFor + ";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    formulario.Add(rd["rut_usuario"].ToString());
                    formulario.Add(rd["nombre_usuario"].ToString());
                    formulario.Add(rd["asunto_form"].ToString());
                    formulario.Add(rd["detalle_form"].ToString());
                }
                rd.Close();
                return formulario;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

        }
        public bool AprobarSolicitud(string idFormulario)
        {
            try
            {
                Conectar();
                int idForm = Int32.Parse(idFormulario);
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`FORMULARIO` SET `estado` = 'Aprobado' WHERE `id_formulario` = " + idForm + ";", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool RechazarSolicitud(string idFormulario)
        {
            try
            {
                Conectar();
                int idForm = Int32.Parse(idFormulario);
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`FORMULARIO` SET `estado` = 'Rechazada' WHERE `id_formulario` = " + idForm + ";", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<string> LlenadoEstado(string rutUsuario)
        {
            List<string> registro = new List<string>();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT concat(asunto_form, ', Estado: ', estado) form FROM UNIONLINE.FORMULARIO inner join UNIONLINE.USUARIO on UNIONLINE.FORMULARIO.USUARIO_id_usuario = UNIONLINE.USUARIO.id_usuario where rut_usuario = '"+rutUsuario+"';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd["form"].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool CorreoValido(string correo)
        {
            Regex correoregex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);

            return correoregex.IsMatch(correo);
        }

        public bool eliminarUsuario(string RUTUsuario)
        {
            Conectar();
            try
            {
                int idUser = 0;
                cmd = new MySqlCommand("SELECT id_usuario FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + RUTUsuario + "' ;", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idUser = Int32.Parse(rd["id_usuario"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("DELETE FROM `UNIONLINE`.`USUARIO`WHERE id_usuario = " + idUser + ";", conex);
                cmd.ExecuteReader();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Usuario BuscarUsuarioCliente(string rut)
        {
            try
            {

                Conectar();
                cmd = new MySqlCommand("SELECT * FROM `UNIONLINE`.`USUARIO` where rut_usuario =  '" + rut + "' and T_USUARIO_id_tipoU = 5;", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    id_usuario = Int32.Parse(rd["id_usuario"].ToString());
                    rut_usuario = rd["rut_usuario"].ToString();
                    contrasenna = rd["contrasenna"].ToString();
                    primer_nombre = rd["primer_nombre"].ToString();
                    segundo_nombre = rd["segundo_nombre"].ToString();
                    primer_apellido = rd["primer_apellido"].ToString();
                    segundo_apellido = rd["segundo_apellido"].ToString();
                    correo_electronico = rd["correo_electronico"].ToString();
                    telefono = rd["telefono"].ToString();
                    id_tipoU = Int32.Parse(rd["T_USUARIO_id_tipoU"].ToString());

                }
                rd.Close();
                return this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;

            }
        }



    }



}