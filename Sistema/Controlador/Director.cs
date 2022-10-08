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
using MySqlConnector;
using MySql.Data;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Element;
using iText.Layout.Properties;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;
using MySqlDataReader = MySql.Data.MySqlClient.MySqlDataReader;

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
                return true;

            }
        }
        public Usuario LoginUsuario(string rut)
        {
            Usuario usuario = new Usuario();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT id_usuario, rut_usuario, contrasenna, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, correo_electronico, telefono, CBR_id_cbr, T_USUARIO_id_tipoU FROM UNIONLINE.USUARIO where rut_usuario = '" + rut + "';", conex);
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
            catch
            {
                MessageBox.Show("");
            }
            Exception ex;

            documento.Add(tabla);
            documento.Close();
        }

    }



}