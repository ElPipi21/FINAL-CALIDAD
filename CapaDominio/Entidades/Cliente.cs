using System;
using System.ComponentModel.DataAnnotations;

namespace CapaDominio.Entidades
{
    public class Cliente
    {
        private int idCliente;

        public int IdCliente
        {
            get { return idCliente; }
            set { idCliente = value; }
        }
        private string nombre;
        [Required(ErrorMessage = "*El campo {0} es obligatorio")]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }
        private string direccion;

        private string dni;

        public string Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private string ciudad;

        public string Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }
        private int telefono;

        public int Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private DateTime fechaNacimiento;
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }
        private char sexo;

        public char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        private int password;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public int Password
        {
            get { return password; }
            set { password = value; }
        }
        private Boolean estado;

        public Boolean Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #region Reglas de negocio
        public Cliente VerificarAcceso(Cliente cliente, out string mensaje)
        {
            try
            {
                mensaje = "";
                if (cliente != null)
                {
                    if (!cliente.Estado)
                        mensaje = "¡El usuario está dado de baja!";

                }
                else
                {
                    mensaje = "¡Usuario o Password no válido!";
                }
                return cliente;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DatosDeClienteValido()
        {
            return fechaDeNacimientoCorrecta() && validarDni();
        }
        public Boolean validarDni()
        {
            if (dni.Length > 7 && dni.Length < 9)
                return true;
            else
                return false;
        }
        protected bool fechaDeNacimientoCorrecta()
        {
            return fechaNacimiento != null && (DateTime.Now.Year - fechaNacimiento.Year >= 18);
        }

        #endregion
    }
}
