
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace UniOnline.BD
{
    public class ClsDB
    {
        ////
        //#region Variables Privadas

        //private SqlConnection _objSqlConnection;
        //private SqlDataAdapter _objSqlDataAdapter;
        //private SqlCommand sqlCommand;
        //private DataSet _dsResultado;
        //private DataTable _dtParametros;
        //private string _nombreTabla, _nombreSP, _mensajeErrorDB, _valorScalar, _nombreDB;
        //private bool _scalar;

        //#endregion

        //#region Variables publicas

        //public SqlConnection ObjSqlConnection { get => _objSqlConnection; set => _objSqlConnection = value; }
        //public SqlDataAdapter ObjSqlDataAdapter { get => _objSqlDataAdapter; set => _objSqlDataAdapter = value; }
        //public SqlCommand SqlCommand { get => sqlCommand; set => sqlCommand = value; }
        //public DataSet DsResultado { get => _dsResultado; set => _dsResultado = value; }
        //public DataTable DtParametros { get => _dtParametros; set => _dtParametros = value; }
        //public string NombreTabla { get => _nombreTabla; set => _nombreTabla = value; }
        //public string NombreSP { get => _nombreSP; set => _nombreSP = value; }
        //public string MensajeErrorDB { get => _mensajeErrorDB; set => _mensajeErrorDB = value; }
        //public string ValorScalar { get => _valorScalar; set => _valorScalar = value; }
        //public string NombreDB { get => _nombreDB; set => _nombreDB = value; }
        //public bool Scalar { get => _scalar; set => _scalar = value; }
        //#endregion

        //#region Constructores
        //public ClsDB()
        //{
        //    DtParametros = new DataTable("SpParametros");
        //    DtParametros.Columns.Add("Nombre");
        //    DtParametros.Columns.Add("TipoDato");
        //    DtParametros.Columns.Add("Valor");

        //    NombreDB = "UNIONLINE";
        //}
        //#endregion

        //#region Metodos privados
        //private void CrearConexionBaseDatos(ref ClsDB ObjDataBase)
        //{
        //    switch (ObjDataBase.NombreDB)
        //    {
        //        case "UNIONLINE":
        //            ObjDataBase.ObjSqlConnection = new SqlConnection();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //private void ValidarConexionBaseDatos(ref ClsDB ObjDataBase)
        //{
        //}

        //private void AgregarParametros(ref ClsDB ObjDataBase)
        //{
        //}

        //private void PrepararConexionBaseDatos(ref ClsDB ObjDataBase)
        //{
        //}
        //private void EjecutarDataAdapter(ref ClsDB ObjDataBase)
        //{
        //}

        //private void EjecutarComando(ref ClsDB ObjDataBase)
        //{
        //}

        //#endregion

        //#region Metodos Publicos
        //#endregion
        //
    }
}

