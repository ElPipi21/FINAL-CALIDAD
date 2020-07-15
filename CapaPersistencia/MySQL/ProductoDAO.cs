using CapaDominio.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CapaPersistencia.MySQL
{
    public class ProductoDAO
    {
        private GestorMySQL gestorMySQL;
        public ProductoDAO(GestorMySQL gestorMySQL)
        {
            this.gestorMySQL = gestorMySQL;
        }

        public List<Producto> listarProductos()
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spListarProductos");
            List<Producto> listaProductos = new List<Producto>();
            try
            {
                Producto producto = null;
                Categoria categoria = null;
                MySqlDataReader resultadoMySQL = gestorMySQL.ejecutarComandoDeProcedimientoSinParametros(comandoMySQL);
                while (resultadoMySQL.Read())
                {
                    categoria = new Categoria()
                    {
                        Nombre = resultadoMySQL["categoria"].ToString()
                    };
                    producto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(resultadoMySQL["id_producto"]),
                        Nombre = resultadoMySQL["nombre"].ToString(),
                        Descripcion = resultadoMySQL["descripcion"].ToString(),
                        Precio = Convert.ToDouble(resultadoMySQL["precio"]),
                        Imagen = resultadoMySQL["imagen"].ToString(),
                        Estado = Convert.ToBoolean(resultadoMySQL["estado"]),
                        Stock = Convert.ToInt32(resultadoMySQL["stock"])
                    };
                    producto.Categoria = categoria;
                    listaProductos.Add(producto);
                }
                resultadoMySQL.Close();
                return listaProductos;
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al listar los productos.", err);
            }
        }
    }
}
