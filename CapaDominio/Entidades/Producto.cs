using System;

namespace CapaDominio.Entidades
{
    public class Producto
    {
        private int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { idProducto = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private double precio;

        public double Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
        private Boolean estado;

        public Boolean Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        private Categoria categoria;

        public Categoria Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        private int stock;

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public String EstadoVerificaStock()
        {
            return Stock <= 25 ? "warning" :
                Stock == 0 ? "danger" :
                "success";
        }

        public bool DesabilitaOpcionAgregarACarrito()
        {
            return Stock == 0 ? true : false;
        }
    }
}
