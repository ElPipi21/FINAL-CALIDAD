using CapaPersistencia.MySQL;
using System;

namespace CapaAplicacion.Servicio
{
    public class RealizarPagoServicio
    {
        private readonly GestorMySQL gestorMySQL;
        private readonly PagoDAO pagoDAO;

        public RealizarPagoServicio()
        {
            this.gestorMySQL = new GestorMySQL();
            pagoDAO = new PagoDAO(gestorMySQL);
        }

        public bool pagoPedido(int idPedido)
        {
            try
            {
                gestorMySQL.iniciarTransaccion();
                bool pago = pagoDAO.pagarPedido(idPedido);
                if (pago)
                    gestorMySQL.terminarTransaccion();
                else
                    gestorMySQL.cancelarTransaccion();
                return pago;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
