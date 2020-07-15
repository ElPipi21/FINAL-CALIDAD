using System;

namespace CapaDominio.Entidades
{
    public class Ubicacion
    {
        private int idUbicacion;

        public int IdUbicacion
        {
            get { return idUbicacion; }
            set { idUbicacion = value; }
        }
        private int codigo_postal;

        public int Codigo_postal
        {
            get { return codigo_postal; }
            set { codigo_postal = value; }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        private double latitud;

        public double Latitud
        {
            get { return latitud; }
            set { latitud = value; }
        }
        private double longitud;

        public double Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }


        //REGLAS DE NEGOCIO
        // no debe exceder cierta cantidad (x) sino se le agrega un % adicional

        public const double RadioTierra = 6371;
        public double ObtenerDistancia(double lat1, double lng1, double lat2, double lng2)
        {
            double Lat = (lat2 - lat1) * (Math.PI / 180);
            double Lon = (lng2 - lng1) * (Math.PI / 180);
            double a = Math.Sin(Lat / 2) * Math.Sin(Lat / 2) + Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180)) * Math.Sin(Lon / 2) * Math.Sin(Lon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = RadioTierra * c;
            return distance;
        }
    }
}
