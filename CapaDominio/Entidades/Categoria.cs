namespace CapaDominio.Entidades
{
    public class Categoria
    {
        private int idCategoria;

        public int IdCategoria
        {
            get { return idCategoria; }
            set { idCategoria = value; }
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

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set { imagen = value; }
        }
    }
}
