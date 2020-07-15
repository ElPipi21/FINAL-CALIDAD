using CapaDominio.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CapaPersistencia.MySQL
{
    public class ClienteDAO
    {
        private GestorMySQL gestorMySQL;
        public ClienteDAO(GestorMySQL gestorMySQL)
        {
            this.gestorMySQL = gestorMySQL;
        }
        public Cliente verificarAcceso(string nombreUsuario, string clave)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spVerificarAcceso");
            Cliente clienteEntidad = null;
            List<MySqlParameter> parametros = new List<MySqlParameter>()
            {
                new MySqlParameter("prmstrCliente", nombreUsuario),
                new MySqlParameter("prmstrPassword", clave)
            };
            try
            {
                MySqlDataReader resultado = gestorMySQL.ejecutarComandoDeProcedimientoConParametros(comandoMySQL, parametros);
                if (resultado.Read())
                {
                    clienteEntidad = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(resultado["Id_cliente"]),
                        Nombre = resultado["nombre"].ToString(),
                        Apellido = resultado["apellido"].ToString(),
                        Direccion = resultado["direccion"].ToString(),
                        Ciudad = resultado["ciudad"].ToString(),
                        Telefono = Convert.ToInt32(resultado["telefono"]),
                        Email = resultado["email"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(resultado["fecha_Nacimiento"]),
                        Sexo = Convert.ToChar(resultado["sexo"]),
                        Password = Convert.ToInt32(resultado["password"]),
                        Estado = Convert.ToBoolean(resultado["estado"])
                    };
                }
                resultado.Close();
                return clienteEntidad;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public bool insertarCliente(Cliente cliente)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spInsertarCliente");
            bool insertar = false;
            try
            {
                comandoMySQL = gestorMySQL.obtenerComandoDeProcedimiento(comandoMySQL);
                comandoMySQL.Parameters.AddWithValue("prmstrNombre", cliente.Nombre);
                comandoMySQL.Parameters.AddWithValue("prmstrApellido", cliente.Apellido);
                comandoMySQL.Parameters.AddWithValue("prmstrDireccion", cliente.Direccion);
                comandoMySQL.Parameters.AddWithValue("prmstrCiudad", cliente.Ciudad);
                comandoMySQL.Parameters.AddWithValue("prmintTelefono", cliente.Telefono);
                comandoMySQL.Parameters.AddWithValue("prmstrEmail", cliente.Email);
                comandoMySQL.Parameters.AddWithValue("prmdateFecha_nacimiento", cliente.FechaNacimiento);
                comandoMySQL.Parameters.AddWithValue("prmstrSexo", cliente.Sexo);
                comandoMySQL.Parameters.AddWithValue("prmstrPassword", cliente.Password);
                int indiceInsertado = comandoMySQL.ExecuteNonQuery();
                if (indiceInsertado > 0)
                {
                    insertar = true;
                }
                return insertar;
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al insertar cliente.", err);
            }
        }

        public bool editarCliente(Cliente cliente)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spEditarCliente");
            bool editar = false;
            try
            {
                comandoMySQL = gestorMySQL.obtenerComandoDeProcedimiento(comandoMySQL);
                comandoMySQL.Parameters.AddWithValue("prmintIdCliente", cliente.IdCliente);
                comandoMySQL.Parameters.AddWithValue("prmstrNombre", cliente.Nombre);
                comandoMySQL.Parameters.AddWithValue("prmstrApellido", cliente.Apellido);
                comandoMySQL.Parameters.AddWithValue("prmstrDireccion", cliente.Direccion);
                comandoMySQL.Parameters.AddWithValue("prmstrCiudad", cliente.Ciudad);
                comandoMySQL.Parameters.AddWithValue("prmintTelefono", cliente.Telefono);
                comandoMySQL.Parameters.AddWithValue("prmstrEmail", cliente.Email);
                comandoMySQL.Parameters.AddWithValue("prmdateFecha_nacimiento", cliente.FechaNacimiento);
                comandoMySQL.Parameters.AddWithValue("prmstrSexo", cliente.Sexo);
                comandoMySQL.Parameters.AddWithValue("prmstrPassword", cliente.Password);
                int indiceInsertado = comandoMySQL.ExecuteNonQuery();
                if (indiceInsertado > 0)
                {
                    editar = true;
                }
                return editar;
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al editar cliente.", err);
            }
        }
    }
}
