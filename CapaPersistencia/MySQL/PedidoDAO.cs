using CapaDominio.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CapaPersistencia.MySQL
{
    public class PedidoDAO
    {
        private readonly GestorMySQL gestorMySQL;
        public PedidoDAO(GestorMySQL gestorMySQL)
        {
            this.gestorMySQL = gestorMySQL;
        }
        public int insertarPedido(Pedido pedido)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spInsertarPedido");
            try
            {
                comandoMySQL = gestorMySQL.obtenerComandoDeProcedimiento(comandoMySQL);
                comandoMySQL.Parameters.AddWithValue("prmstrNumero", pedido.Numero);
                comandoMySQL.Parameters.AddWithValue("prmstrIdCliente", pedido.Cliente.IdCliente);
                int lastId = Convert.ToInt32(comandoMySQL.ExecuteScalar());
                return lastId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool insertarLineaDePedido(LineaDePedido lineaDePedido, int idPedido)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spInsertarLineaDeVenta");
            bool inserta = false;
            try
            {
                comandoMySQL = gestorMySQL.obtenerComandoDeProcedimiento(comandoMySQL);
                comandoMySQL.Parameters.AddWithValue("prmIntIdProducto", lineaDePedido.Producto.IdProducto);
                comandoMySQL.Parameters.AddWithValue("prmDoublePrecio", lineaDePedido.PrecioDeVenta);
                comandoMySQL.Parameters.AddWithValue("prmIntCantidad", lineaDePedido.CantidadVendida);
                comandoMySQL.Parameters.AddWithValue("prmIntIdPedido", idPedido);
                int indiceInsertado = comandoMySQL.ExecuteNonQuery();

                if (indiceInsertado > 0)
                {
                    inserta = true;
                }
                return inserta;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Pedido> listarPedidosPorCliente(int idCliente)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spListarPedidoPorCliente");
            List<Pedido> listaPedidos = new List<Pedido>();
            List<LineaDePedido> listaLineaDePedidos = new List<LineaDePedido>();

            List<MySqlParameter> parametros = new List<MySqlParameter>()
            {
                new MySqlParameter("id_cliente", idCliente),
            };
            try
            {
                MySqlDataReader resultadoMySQL = gestorMySQL.ejecutarComandoDeProcedimientoConParametros(comandoMySQL, parametros);
                Producto producto = null;
                Pedido pedido = null;
                LineaDePedido lineaDePedido = null;
                while (resultadoMySQL.Read())
                {
                    producto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(resultadoMySQL["id_producto"]),
                        Nombre = resultadoMySQL["nombre"].ToString(),
                        Imagen = resultadoMySQL["imagen"].ToString(),
                    };
                    lineaDePedido = new LineaDePedido()
                    {
                        CantidadVendida = Convert.ToInt32(resultadoMySQL["cantidad"]),
                        PrecioDeVenta = Convert.ToDouble(resultadoMySQL["precio"])
                    };
                    pedido = new Pedido()
                    {
                        IdPedido = Convert.ToInt32(resultadoMySQL["id_pedido"]),
                        Fecha = Convert.ToDateTime(resultadoMySQL["fecha"]),
                        Numero = Convert.ToInt32(resultadoMySQL["numero"]),
                        Estado = resultadoMySQL["estado"].ToString()
                    };
                    lineaDePedido.Producto = producto;
                    listaLineaDePedidos.Add(lineaDePedido);
                    pedido.ListaLineasDePedido = listaLineaDePedidos;
                    listaPedidos.Add(pedido);
                }
                resultadoMySQL.Close();
                return listaPedidos;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Pedido listarDetallePedidoPorCliente(int idPedido)
        {
            MySqlCommand comandoMySQL = new MySqlCommand("spListarDetallePedidoPorCliente");
            List<Pedido> listaPedidos = new List<Pedido>();
            List<LineaDePedido> listaLineaDePedidos = new List<LineaDePedido>();

            List<MySqlParameter> parametros = new List<MySqlParameter>()
            {
                new MySqlParameter("idPedido", idPedido),
            };
            try
            {
                MySqlDataReader resultadoMySQL = gestorMySQL.ejecutarComandoDeProcedimientoConParametros(comandoMySQL, parametros);
                Producto producto = null;
                Pedido pedido = null;
                LineaDePedido lineaDePedido = null;
                while (resultadoMySQL.Read())
                {
                    producto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(resultadoMySQL["id_producto"]),
                        Nombre = resultadoMySQL["nombre"].ToString(),
                        Imagen = resultadoMySQL["imagen"].ToString(),
                    };
                    lineaDePedido = new LineaDePedido()
                    {
                        CantidadVendida = Convert.ToInt32(resultadoMySQL["cantidad"]),
                        PrecioDeVenta = Convert.ToDouble(resultadoMySQL["precio"])
                    };
                    pedido = new Pedido()
                    {
                        IdPedido = Convert.ToInt32(resultadoMySQL["id_pedido"]),
                        Fecha = Convert.ToDateTime(resultadoMySQL["fecha"]),
                        Numero = Convert.ToInt32(resultadoMySQL["numero"]),
                        Estado = resultadoMySQL["estado"].ToString()
                    };
                    lineaDePedido.Producto = producto;
                    listaLineaDePedidos.Add(lineaDePedido);
                    pedido.ListaLineasDePedido = listaLineaDePedidos;
                    //listaPedidos.Add(pedido);
                }
                resultadoMySQL.Close();
                return pedido;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
