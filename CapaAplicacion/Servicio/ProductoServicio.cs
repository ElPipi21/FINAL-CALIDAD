using CapaDominio.Entidades;
using CapaPersistencia.MySQL;
using System.Collections.Generic;

namespace CapaAplicacion.Servicio
{
    public class ProductoServicio
    {
        private GestorMySQL gestorMySQL;
        private ProductoDAO productoDAO;

        public ProductoServicio()
        {
            gestorMySQL = new GestorMySQL();
            productoDAO = new ProductoDAO(gestorMySQL);
        }

        public List<Producto> listarProductos()
        {
            gestorMySQL.abrirConexion();
            List<Producto> listaProductos = productoDAO.listarProductos();
            gestorMySQL.cerrarConexion();
            return listaProductos;
        }
    }
}
