using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaPersistencia.MySQL
{
    public class GestorMySQL
    {
        private MySqlConnection conexion;
        private MySqlTransaction transaccion;

        public void abrirConexion()
        {
            try
            {
                conexion = new MySqlConnection();
                conexion.ConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=2901;database=proyecto_calidad;";
                conexion.Open();
            }
            catch (Exception err)
            {
                throw new Exception("Error en la conexión con la Base de Datos.", err);
            }
        }

        public void cerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception err)
            {
                throw new Exception("Error al cerrar la conexión con la Base de Datos.", err);
            }
        }

        public void iniciarTransaccion()
        {
            try
            {
                abrirConexion();
                transaccion = conexion.BeginTransaction();
            }
            catch (Exception err)
            {
                throw new Exception("Error al iniciar la transacción con la Base de Datos.", err);
            }
        }

        public void terminarTransaccion()
        {
            try
            {
                transaccion.Commit();
                conexion.Close();
            }
            catch (Exception err)
            {
                throw new Exception("Error al terminar la transacción con la Base de Datos.", err);
            }
        }

        public void cancelarTransaccion()
        {
            try
            {
                transaccion.Rollback();
                conexion.Close();
            }
            catch (Exception err)
            {
                throw new Exception("Error al cancelar la transacción con la Base de Datos.", err);
            }
        }

        public MySqlDataReader ejecutarConsulta(String sentenciaMySQL)
        {
            try
            {
                MySqlCommand comandoMySQL = conexion.CreateCommand();
                if (transaccion != null)
                    comandoMySQL.Transaction = transaccion;
                comandoMySQL.CommandText = sentenciaMySQL;
                comandoMySQL.CommandType = CommandType.Text;
                return comandoMySQL.ExecuteReader();
            }
            catch (Exception err)
            {
                throw new Exception("Error al ejecutar consulta en la Base de Datos.", err);
            }
        }

        public MySqlCommand obtenerComandoMySQL(String sentenciaMySQL)
        {
            try
            {
                MySqlCommand comandoMySQL = conexion.CreateCommand();
                if (transaccion != null)
                    comandoMySQL.Transaction = transaccion;
                comandoMySQL.CommandText = sentenciaMySQL;
                comandoMySQL.CommandType = CommandType.Text;
                return comandoMySQL;
            }
            catch (Exception err)
            {
                throw new Exception("Error al ejecutar comando en la Base de Datos.", err);
            }
        }
        public MySqlCommand obtenerComandoDeProcedimiento(MySqlCommand procedimientoAlmacenado)
        {
            try
            {
                MySqlCommand comandoMySQL = conexion.CreateCommand();
                if (transaccion != null)
                    comandoMySQL.Transaction = transaccion;
                comandoMySQL.CommandText = procedimientoAlmacenado.CommandText;
                comandoMySQL.CommandType = CommandType.StoredProcedure;
                return comandoMySQL;
            }
            catch (Exception err)
            {
                throw new Exception("Error al ejecutar comando en la Base de Datos.", err);
            }
        }

        public MySqlDataReader ejecutarComandoDeProcedimientoSinParametros(MySqlCommand procedimientoAlmacenado)
        {
            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                MySqlCommand comandoMySQL = conexion.CreateCommand();
                if (transaccion != null)
                    comandoMySQL.Transaction = transaccion;
                comandoMySQL.CommandText = procedimientoAlmacenado.CommandText;
                comandoMySQL.Parameters.AddRange(parameters.ToArray());
                comandoMySQL.CommandType = CommandType.StoredProcedure;
                var resultado = comandoMySQL.ExecuteReader();
                return resultado;
            }
            catch (Exception err)
            {
                throw new Exception("Error al ejecutar comando en la Base de Datos.", err);
            }
        }

        public MySqlDataReader ejecutarComandoDeProcedimientoConParametros(MySqlCommand procedimientoAlmacenado, List<MySqlParameter> parametros)
        {
            try
            {
                MySqlCommand comandoMySQL = conexion.CreateCommand();
                if (transaccion != null)
                    comandoMySQL.Transaction = transaccion;
                comandoMySQL.CommandText = procedimientoAlmacenado.CommandText;
                comandoMySQL.Parameters.AddRange(parametros.ToArray());
                comandoMySQL.CommandType = CommandType.StoredProcedure;
                var resultado = comandoMySQL.ExecuteReader();
                return resultado;
            }
            catch (Exception err)
            {
                throw new Exception("Error al ejecutar comando en la Base de Datos.", err);
            }
        }
    }
}
