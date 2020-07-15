using System;

namespace CapaDominio.Entidades
{
    public class LineaDePedido
    {
        private int cantidadVendida;
        public int CantidadVendida
        {
            get { return cantidadVendida; }
            set { cantidadVendida = value; }
        }
        private double precioVenta;
        public double PrecioDeVenta
        {
            get { return precioVenta; }
            set { precioVenta = value; }
        }

        private Producto producto;
        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        private Pago pago;
        public Pago Pago
        {
            get { return pago; }
            set { pago = value; }
        }

        #region Reglas de Negocio
        public double calcularSubTotal()
        {
            double subTotal = cantidadVendida * precioVenta;
            return subTotal;
        }
        public Boolean esCantidadValida()
        {
            if (cantidadVendida > 0 && cantidadVendida <= 20)
                return true;
            return false;
        }
        #endregion
    }
}
