using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;


namespace Controlador
{
    public class Tramite : Conexion
    {
        public string NombreTra { get; set; }
        public string ValorTra { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public int TipoTramite { get; set; }
        public string Doc { get; set; }

        public Tramite()
        {
            this.Init();
        }

        private void Init()
        {
            NombreTra = string.Empty;
            ValorTra = string.Empty;
            Estado = string.Empty;
            TipoTramite = 0;
            Doc = string.Empty;
        }

        public string InsertarTramite(Tramite Tra)
        {
            string salida;
            try
            {
                Conectar();
                cmd = new MySqlCommand("INSERT INTO `UNIONLINE`.`TRAMITE` (`nombre_tramite`, `valor_tramite`, `estado`, `descripcion`, `T_TRAMITE_id_tipoT`, `t_documento`) VALUES ('" + Tra.NombreTra + "', " + Tra.ValorTra + ",'" + Tra.Estado + "','" + Tra.Descripcion + "'," + Tra.TipoTramite + ", '" + Tra.Doc + "');", conex);
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
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.TRAMITE WHERE nombre_tramite = '" + id + "';", conex);
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

        public bool ExisteTipo(string id)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT id_tipoT FROM UNIONLINE.T_TRAMITE WHERE UNIONLINE.T_TRAMITE.id_tipoT = " + id + ";", conex);
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

        public DataTable MostrarTipoTra(int tipo)
        {
            Conectar();
            DataTable tabla = new DataTable();

            try
            {
                cmd = new MySqlCommand("SELECT tra.id_tramite, tra.nombre_tramite, tra.valor_tramite, tra.estado, tra.descripcion, titra.nombre_tipoT, tra.t_documento FROM UNIONLINE.TRAMITE AS tra INNER JOIN UNIONLINE.T_TRAMITE AS titra ON tra.T_TRAMITE_id_tipoT = titra.id_tipoT WHERE tra.T_TRAMITE_id_tipoT = " + tipo + ";", conex);
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

        public DataTable MostrarEstado(int tipot, string estadot)
        {
            Conectar();
            DataTable testado = new DataTable();

            try
            {
                cmd = new MySqlCommand("SELECT tra.id_tramite, tra.nombre_tramite, tra.valor_tramite, tra.estado, tra.descripcion, titra.nombre_tipoT, tra.t_documento FROM UNIONLINE.TRAMITE AS tra INNER JOIN UNIONLINE.T_TRAMITE AS titra ON tra.T_TRAMITE_id_tipoT = titra.id_tipoT WHERE tra.T_TRAMITE_id_tipoT = " + tipot +" AND estado = '" + estadot +"';", conex);
                MySqlDataAdapter ap = new MySqlDataAdapter(cmd);
                ap.Fill(testado);
                cmd.Dispose();
                ap.Dispose();
                return testado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
        }

        public bool ModificarEstado(int idTramite, string nestado)
        {
            try
            {
                Conectar();
                int idTra = 0;
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.TRAMITE where id_tramite = " + idTramite + ";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    idTra = Int32.Parse(rd["id_tramite"].ToString());
                }
                rd.Close();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`TRAMITE` SET `estado` = '" + nestado + "' WHERE `id_tramite` = " + idTra + ";", conex);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        public bool CompararID(int idTramite)
        {
            bool comparar = false;
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.TRAMITE where id_tramite = " + idTramite + " and id_tramite not in (SELECT TRAMITE_id_tramite FROM UNIONLINE.SOLICITUD);", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    comparar = true;
                }
                rd.Close();
                return comparar;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return comparar;
            }
        }

        public Tramite BuscarTramite(int idTra)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("SELECT * FROM UNIONLINE.TRAMITE where id_tramite = " + idTra + ";", conex);
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    NombreTra = rd["nombre_tramite"].ToString();
                    ValorTra = rd["valor_tramite"].ToString();
                    Estado = rd["estado"].ToString();
                    Descripcion = rd["descripcion"].ToString();
                    TipoTramite = Int32.Parse(rd["T_TRAMITE_id_tipoT"].ToString());
                    Doc = rd["t_documento"].ToString();
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

        public bool EliminarTra(int idTram)
        {
            Conectar();
            try
            {
                cmd = new MySqlCommand("DELETE FROM `UNIONLINE`.`TRAMITE` WHERE id_tramite = " + idTram + ";", conex);
                cmd.ExecuteReader();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ModificarTra(int idTra, string NombreTra, string ValorTra, string Estado, string Descripcion, int TipoTra, string Doc)
        {
            try
            {
                Conectar();
                cmd = new MySqlCommand("UPDATE `UNIONLINE`.`TRAMITE` SET `nombre_tramite` = '" + NombreTra + "', `valor_tramite` = '" + ValorTra + "', `estado` = '" + Estado + "', `descripcion` = '" + Descripcion + "', `T_TRAMITE_id_tipoT` = " + TipoTra + ", `t_documento` = '" + Doc + "' WHERE `id_tramite` = " + idTra + "; ", conex);
                cmd.ExecuteNonQuery();
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
