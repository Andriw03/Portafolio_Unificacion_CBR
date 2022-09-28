﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Controlador
{
    public class Conexion
    {
        public MySqlConnection conex { get; set; }
        public MySqlCommand cmd { get; set; }
        public MySqlDataReader rd { get; set; }
        public MySqlDataAdapter adapter { get; set; }

        public Conexion()
        {
            this.Init();
        }

        private void Init()
        {
            conex = null;
            cmd = null;
            rd = null;
            adapter = null;
        }

        public bool Conectar()
        {
            try
            {
                conex = new MySqlConnection("server=unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com;user id=root;password=nohomo123;persistsecurityinfo=True;database=UNIONLINE");
                conex.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Método para Buscar en la base de datos una columna de una tabla
        public List<string> Llenado(string tab, string colum)
        {
            List<string> registro = new List<string>();
            try
            {
                cmd = new MySqlCommand("SELECT "+colum+" FROM UNIONLINE."+tab+";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd[colum].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Método para Buscar en la base de datos una columna de una tabla con un where = INT
        public List<string> LlenadoWhereInt(string tab, string colum, string colum2, int filtro)
        {
            List<string> registro = new List<string>();
            try
            {
                cmd = new MySqlCommand("SELECT " + colum + " FROM UNIONLINE." + tab + " where "+colum2+" = "+filtro+";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd[colum].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                registro.Add("Error BD");
                registro.Add(ex.Message);
                return registro;
            }
        }
        //Método para Buscar en la base de datos una columna de una tabla con un where = String
        public List<string> LlenadoWhereString(string tab, string colum, string colum2, string filtro)
        {
            List<string> registro = new List<string>();
            try
            {
                cmd = new MySqlCommand("SELECT " + colum + " FROM UNIONLINE." + tab + " where " + colum2 + " = '" + filtro + "';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    registro.Add(rd[colum].ToString());
                }
                rd.Close();
                return registro;
            }
            catch (Exception ex)
            {
                registro.Add("Error BD");
                registro.Add(ex.Message);
                return registro;
            }
        }
        public bool InsertDireccion(string nombreCalle, int numeroCasa, String comuna)
        {
            try
            {
                string idComuna = string.Empty;
                cmd = new MySqlCommand("SELECT id_comuna FROM UNIONLINE.COMUNA where nombre_comuna = '"+comuna+"';", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idComuna = rd["id_comuna"].ToString();
                }
                rd.Close();
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`DIRECCION` (`nombre_calle`, `numero_casa`, `COMUNA_id_comuna`) VALUES ( '"+ nombreCalle + "',"+ numeroCasa + ", "+Int32.Parse(idComuna)+");", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ClasProp(int foja, int numero, DateTime anno, string razonSocial, string rutEmpresa)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
