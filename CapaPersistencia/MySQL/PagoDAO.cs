using MySql.Data.MySqlClient;
using System;

namespace CapaPersistencia.MySQL
{
    public class PagoDAO
    {
        private readonly GestorMySQL gestorMySQL;

        public PagoDAO(GestorMySQL gestorMySQL)
        {
            this.gestorMySQL = gestorMySQL;
        }

        public bool pagarPedido(int idPedido)
        {
            MySqlCommand comando = new MySqlCommand("spPagarPedido");
            bool pagado = false;

            try
            {
                comando = gestorMySQL.obtenerComandoDeProcedimiento(comando);
                comando.Parameters.AddWithValue("idPedido", idPedido);
                int indiceInsertado = comando.ExecuteNonQuery();
                if (indiceInsertado > 0)
                {
                    pagado = true;
                }
                return pagado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
