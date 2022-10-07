﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                return true;
            }
        }

        public Cliente BuscarCliente(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.USUARIO WHERE rut_usuario = '" + id + "' and T_USUARIO_id_tipoU = 5;");
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
                return null;
            }
            return this;
        }
        public bool Formulario(string nombre, string telefono, string correo, string asunto, string detalle)
        {
            try
            {
                Conectar();
                asunto = "Usuario CBR: " + asunto;
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`FORMULARIO` (`nombre_form`, `telefono`, `correo_form`, `asunto_form`, `detalle_form`) VALUES ('"+ nombre + "', '" + telefono + "', '" + correo + "', '" + asunto + "', '" + detalle + "'); ", conex);
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
                cmd = new MySqlCommand("SELECT numero_seguimiento, estado, fecha_solicitud ,rut_usuario, Concat(UNIONLINE.USUARIO.primer_nombre, ' ' , UNIONLINE.USUARIO.primer_apellido) as nombre_solicitante, nombre_tramite, foja, descripcion, concat(nombre_calle,' ', numero_casa ,', ', nombre_comuna, ', ', nombre_region) as direccion_prop,nombre_doc, doc, id_documento, Concat(UNIONLINE.DUENNO_PROP.primer_nombre, ' ' , UNIONLINE.DUENNO_PROP.primer_apellido) as nombre_duenno FROM UNIONLINE.SOLICITUD inner join UNIONLINE.DOCUMENTO on UNIONLINE.SOLICITUD.DOCUMENTO_id_documento = UNIONLINE.DOCUMENTO.id_documento inner join UNIONLINE.USUARIO on UNIONLINE.SOLICITUD.USUARIO_id_usuario = UNIONLINE.USUARIO.id_usuario inner join UNIONLINE.TRAMITE on UNIONLINE.SOLICITUD.TRAMITE_id_tramite = UNIONLINE.TRAMITE.id_tramite inner join UNIONLINE.PROPIEDAD on UNIONLINE.SOLICITUD.PROPIEDAD_id_propiedad = UNIONLINE.PROPIEDAD.id_propiedad inner join UNIONLINE.DUENNO_PROP on UNIONLINE.PROPIEDAD.DUENNO_PROP_id_duenno = UNIONLINE.DUENNO_PROP.id_duenno inner join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas inner join UNIONLINE.DIRECCION on UNIONLINE.PROPIEDAD.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion inner join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna  inner join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia= UNIONLINE.PROVINCIA.id_provincia  inner join UNIONLINE.REGION on UNIONLINE.PROVINCIA.REGION_id_region = UNIONLINE.REGION.id_region where numero_seguimiento = '" + NumeroS+"'; ", conex);
                rd = cmd.ExecuteReader();
                while(rd.Read()){
                    soli.Add(rd["numero_seguimiento"].ToString());
                    soli.Add(rd["estado"].ToString());
                    soli.Add(rd["fecha_solicitud"].ToString());
                    soli.Add(rd["rut_usuario"].ToString());
                    soli.Add(rd["nombre_solicitante"].ToString());
                    soli.Add(rd["nombre_tramite"].ToString());
                    soli.Add(rd["foja"].ToString());
                    soli.Add(rd["descripcion"].ToString());
                    soli.Add(rd["direccion_prop"].ToString());
                    soli.Add(rd["nombre_doc"].ToString());
                    if (rd["doc"].ToString() != string.Empty)
                    {
                        soli.Add("id_documento");
                    }
                    else {
                        soli.Add(null);
                    }
                    soli.Add(rd["nombre_duenno"].ToString());
                    
                }
                return soli;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
       
        
    }


}
