using CapaDominio.Entidades;
using CapaPersistencia.MySQL;

namespace CapaAplicacion.Servicio
{
    public class ClienteServicio
    {
        private GestorMySQL gestorMySQL;
        private ClienteDAO clienteDAO;
        private Cliente clienteContrato;

        public ClienteServicio()
        {
            gestorMySQL = new GestorMySQL();
            clienteDAO = new ClienteDAO(gestorMySQL);
            clienteContrato = new Cliente();
        }
        public Cliente verificarAcceso(string cliente, string clave, out string mensaje)
        {
            gestorMySQL.abrirConexion();
            Cliente verificaCliente = clienteDAO.verificarAcceso(cliente, clave);
            gestorMySQL.cerrarConexion();
            verificaCliente = clienteContrato.VerificarAcceso(verificaCliente, out mensaje);
            return verificaCliente;
        }
        public bool insertarCliente(Cliente cliente)
        {
            gestorMySQL.iniciarTransaccion();
            bool verifica = cliente.DatosDeClienteValido();
            bool insertar = false;
            if (verifica)
            {
                insertar = clienteDAO.insertarCliente(cliente);
                if (insertar)
                    gestorMySQL.terminarTransaccion();
                else
                    gestorMySQL.cancelarTransaccion();
            }
            else
            {
                gestorMySQL.terminarTransaccion();
            }
            return insertar;
        }
        public bool editarCliente(Cliente cliente)
        {
            gestorMySQL.iniciarTransaccion();
            bool editar = clienteDAO.editarCliente(cliente);
            if (editar)
                gestorMySQL.terminarTransaccion();
            else
                gestorMySQL.cancelarTransaccion();
            return editar;
        }

    }
}
