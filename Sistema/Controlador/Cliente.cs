using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Controlador
{
    public class Cliente : Conexion
    {
        public string rut_usuario { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string correo_electronico { get; set; }

        public string contrasenna { get; set; }
        public int telefono { get; set; }

        public Cliente()
        {
            this.Init();
        }

        private void Init()
        {
            rut_usuario = string.Empty;
            primer_nombre = string.Empty;
            segundo_nombre = string.Empty;
            primer_apellido = string.Empty;
            segundo_apellido = string.Empty;
            correo_electronico = string.Empty;
            contrasenna = string.Empty;
            telefono = 0;
        }

        public bool ExisteCliente(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + id + "' ", conex);
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

        public Cliente BuscarCliente(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + id + "' and T_USUARIO_id_tipoU = 5;", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    rut_usuario = rd["rut_usuario"].ToString();
                    primer_nombre = rd["primer_nombre"].ToString();
                    segundo_nombre = rd["segundo_nombre"].ToString();
                    primer_apellido = rd["primer_apellido"].ToString();
                    segundo_apellido = rd["segundo_apellido"].ToString();
                    correo_electronico = rd["correo_electronico"].ToString();
                    telefono = int.Parse(rd["telefono"].ToString());
                }
                rd.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return this;
        }
        public bool Formulario(string nombre, string telefono, string correo, string asunto, string detalle, string rutUsuario)
        {
            try
            {
                Conectar();
                asunto = "Usuario CBR: " + asunto;
                int idUsuario = 0;
                cmd = new MySqlCommand("SELECT id_usuario FROM UNIONLINE.USUARIO where rut_usuario = '" + rutUsuario + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idUsuario = Int32.Parse(rd["id_usuario"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`FORMULARIO` (`nombre_form`, `telefono`, `correo_form`, `asunto_form`, `detalle_form`, `estado`,  `USUARIO_id_usuario`) VALUES ('" + nombre + "', '" + telefono + "', '" + correo + "', '" + asunto + "', '" + detalle + "', 'En Proceso', " + idUsuario + "); ", conex);
                cmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public List<string> Solicitud(string NumeroS)
        {
            List<string> soli = new List<string>();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT id_soli, numero_seguimiento, SOLICITUD.estado, fecha_solicitud ,rut_usuario, Concat(UNIONLINE.USUARIO.primer_nombre, ' ' , UNIONLINE.USUARIO.primer_apellido) as nombre_solicitante, nombre_tramite, foja, TRAMITE.descripcion, concat(nombre_calle,' ', numero_casa ,', ', nombre_comuna, ', ', nombre_region) as direccion_prop, Concat(UNIONLINE.DUENNO_PROP.primer_nombre, ' ' , UNIONLINE.DUENNO_PROP.primer_apellido) as nombre_duenno, Comentario FROM UNIONLINE.SOLICITUD inner join UNIONLINE.USUARIO on UNIONLINE.SOLICITUD.USUARIO_id_usuario = UNIONLINE.USUARIO.id_usuario inner join UNIONLINE.TRAMITE on UNIONLINE.SOLICITUD.TRAMITE_id_tramite = UNIONLINE.TRAMITE.id_tramite inner join UNIONLINE.PROPIEDAD on UNIONLINE.SOLICITUD.PROPIEDAD_id_propiedad = UNIONLINE.PROPIEDAD.id_propiedad inner join UNIONLINE.DUENNO_PROP on UNIONLINE.PROPIEDAD.DUENNO_PROP_id_duenno = UNIONLINE.DUENNO_PROP.id_duenno inner join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas inner join UNIONLINE.DIRECCION on UNIONLINE.PROPIEDAD.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion inner join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna  inner join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia= UNIONLINE.PROVINCIA.id_provincia  inner join UNIONLINE.REGION on UNIONLINE.PROVINCIA.REGION_id_region = UNIONLINE.REGION.id_region where numero_seguimiento = '" + NumeroS + "'; ", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    soli.Add(rd["numero_seguimiento"].ToString());
                    soli.Add(rd["estado"].ToString());
                    soli.Add(rd["fecha_solicitud"].ToString());
                    soli.Add(rd["rut_usuario"].ToString());
                    soli.Add(rd["nombre_solicitante"].ToString());
                    soli.Add(rd["nombre_tramite"].ToString());
                    soli.Add(rd["foja"].ToString());
                    soli.Add(rd["descripcion"].ToString());
                    soli.Add(rd["direccion_prop"].ToString());
                    soli.Add(rd["nombre_duenno"].ToString());
                    soli.Add(rd["id_soli"].ToString());
                    soli.Add(rd["Comentario"].ToString());
                }
                rd.Close();
                return soli;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool AgregarSolicitud(int IdUsu, int IdProp, int IdTra)
        {
            bool salida = false;
            try
            {
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`SOLICITUD` (`fecha_solicitud`, `fecha_cierre`, `estado`, `numero_seguimiento`, `Comentario`, `USUARIO_id_usuario`, `PROPIEDAD_id_propiedad`, `TRAMITE_id_tramite`) VALUES (SYSDATE(), '0000-00-00 00:00:00', 'En Proceso', 'SO-000', '', "+ IdUsu + ", " + IdProp + ", " + IdTra + ");", conex);
                cmd.ExecuteNonQuery();
                string idSoli = "";
                cmd = new MySqlCommand("SELECT id_soli FROM UNIONLINE.SOLICITUD where numero_seguimiento = 'SO-000';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idSoli = rd["id_soli"].ToString();
                }
                rd.Close();
                string Nseg = "SO-000" + idSoli;
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`SOLICITUD` SET `numero_seguimiento` = '"+ Nseg +"' WHERE `id_soli` = "+ idSoli +" ;", conex);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`CAR_COMPRA` (`estado`,`SOLICITUD_id_soli`) VALUES( 1, " + idSoli + ");", conex);
                cmd.ExecuteNonQuery();
                salida = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                salida = false;
            }
            return salida;
        }

        public List<string> Documento(int id)
        {
            List<string> doc = new List<string>();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT nombre_doc, doc FROM UNIONLINE.DOCUMENTO where SOLICITUD_id_soli = " + id + " and TIPO_DOCUMENTO_id_tipodoc = 1;", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    doc.Add(rd["nombre_doc"].ToString());
                    doc.Add(rd["doc"].ToString());

                }
                rd.Close();
                return doc;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Buscar documento: " + ex.Message);
                return null;
            }
        }

        public Cliente RecuperarContraseña(string userRequesting)
        {
            Cliente cli = new Cliente();
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE correo_electronico = '" + userRequesting + "' ", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    cli.rut_usuario = rd["rut_usuario"].ToString();
                    cli.primer_apellido = rd["primer_apellido"].ToString();
                    cli.primer_nombre = rd["primer_nombre"].ToString();
                    cli.contrasenna = rd["contrasenna"].ToString();
                    cli.correo_electronico = rd["correo_electronico"].ToString();
                }
                rd.Close();
                return cli;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //Metodo que permite al trabajador modificar una solicitud
        public bool ModificarSolicitud(string numeroSeguimiento, string estado, string comentario)
        {
            try
            {
                Conectar();
                int idSoli = 0;
                cmd = new MySqlCommand("SELECT id_soli FROM UNIONLINE.SOLICITUD where numero_seguimiento = '" + numeroSeguimiento + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read() == true)
                {
                    idSoli = Int32.Parse(rd["id_soli"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`SOLICITUD` SET `fecha_cierre` = SYSDATE(), `estado` = '" + estado + "', `Comentario` ='" + comentario + "' WHERE `id_soli` = " + idSoli + " ;", conex);
                rd = cmd.ExecuteReader();
                rd.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
            
        public void ModificarCliente(string rutCliente, string primerNomCli, string segundoNomCli, string primerApeCli, string segundoApeCli, string correoCli, int telCli)
        {
            try
            {
                Conectar();
                int idUsuario = 0;
                cmd = new MySqlCommand("SELECT id_usuario FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + rutCliente + "';", conex);
                rd = cmd.ExecuteReader();
                while(rd.Read())
                {
                    idUsuario = Int32.Parse(rd["id_usuario"].ToString());
                }

                rd.Close();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`USUARIO` SET `primer_nombre` = '" + primerNomCli + "', `segundo_nombre` = '" + segundoNomCli + "', `primer_apellido` = '" + primerApeCli + "', `segundo_apellido` = '" + segundoApeCli + "', `correo_electronico` = '" + correoCli + "', `telefono` = '" + telCli + "' WHERE `id_usuario` = '" + idUsuario + "'; ", conex);
                cmd.ExecuteNonQuery();
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool ModificarContraseña(string contrasenna, string rut)
        {
            try
            {
                Conectar();
                int idUsuario =  0;
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + rut + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idUsuario = int.Parse(rd["id_usuario"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`USUARIO` SET `contrasenna` = '"+ contrasenna +"' WHERE `id_usuario` = "+idUsuario+";", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
        }

        public string CorreoUsu(string rut)
        {
            string salida;
            try
            {
                Conectar();
                string correo = "";
                cmd = new MySqlCommand("SELECT correo_electronico FROM UNIONLINE.USUARIO where rut_usuario = '" + rut + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    correo = rd["correo_electronico"].ToString();
                }
                rd.Close();
                salida = correo;
            }
            catch (Exception ex)
            {
                salida = "Error al agregar el Dueño: " + ex.Message;
            }
            return salida;
        }

    }


}
