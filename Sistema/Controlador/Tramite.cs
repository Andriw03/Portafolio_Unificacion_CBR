﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Controlador
{
    public class Tramite : Conexion
    {
        public string NombreTra { get; set; }
        public string ValorTra { get; set; }
        public int TipoTramite { get; set; }

        public Tramite()
        {
            this.Init();
        }

        private void Init()
        {
            NombreTra = string.Empty;
            ValorTra = string.Empty;
            TipoTramite = 0;
        }

        public string InsertarTramite(Tramite Tra)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`TRAMITE` (`nombre_tramite`, `valor_tramite`, `T_TRAMITE_id_tipoT`) VALUES ('" + Tra.NombreTra + "', " + Tra.ValorTra + ", " + Tra.TipoTramite + ");", conex);
                cmd.ExecuteNonQuery();
                salida = "Trámite agregado correctamente";
            }
            catch (Exception ex)
            {
                salida = "Error al agregar el Trámite: " + ex.ToString();
            }
            return salida;
        }

        public bool ExisteTramite(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.TRAMITE WHERE nombre_tramite = " + id + ";", conex);
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

    }
}