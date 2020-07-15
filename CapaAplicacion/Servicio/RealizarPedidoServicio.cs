using CapaDominio.Entidades;
using CapaPersistencia.MySQL;
using System;
using System.Collections.Generic;


namespace CapaAplicacion.Servicio
{
    public class RealizarPedidoServicio
    {
        private readonly GestorMySQL gestorMySQL;
        private readonly PedidoDAO pedidoDAO;

        public RealizarPedidoServicio()
        {
            this.gestorMySQL = new GestorMySQL();
            this.pedidoDAO = new PedidoDAO(gestorMySQL);
        }

        public int insertarPedido(Pedido pedido)
        {
            try
            {
                gestorMySQL.iniciarTransaccion();
                int inserta = pedidoDAO.insertarPedido(pedido);
                if (inserta > 0)
                {
                    gestorMySQL.terminarTransaccion();
                    foreach (LineaDePedido lineaDePedido in pedido.ListaLineasDePedido)
                    {
                        gestorMySQL.iniciarTransaccion();
                        bool insertaDetalle = insertarDetallePedido(lineaDePedido, inserta);
                        if (insertaDetalle)
                        {
                            gestorMySQL.terminarTransaccion();
                        }
                        else
                        {
                            gestorMySQL.cancelarTransaccion();
                        }
                    }
                }
                else
                {
                    gestorMySQL.cancelarTransaccion();
                }
                return inserta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool insertarDetallePedido(LineaDePedido lineaDePedido, int idPedido)
        {
            try
            {
                bool inserta = pedidoDAO.insertarLineaDePedido(lineaDePedido, idPedido);
                if (!inserta)
                    gestorMySQL.cancelarTransaccion();
                return inserta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Pedido> listaPedidosPorCliente(int idCliente)
        {
            try
            {
                gestorMySQL.abrirConexion();
                List<Pedido> listaPedidos = pedidoDAO.listarPedidosPorCliente(idCliente);
                gestorMySQL.cerrarConexion();
                return listaPedidos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Pedido listarDetallePedidoPorCliente(int idPedido)
        {
            try
            {
                gestorMySQL.abrirConexion();
                Pedido pedido = pedidoDAO.listarDetallePedidoPorCliente(idPedido);
                gestorMySQL.cerrarConexion();
                return pedido;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
