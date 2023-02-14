namespace PruebaJorgeEliezer.Models.Solicitudes
{
    public class ProductoSolicitud
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public bool Existencia { get; set; }

        public bool Estado { get; set; }
    }
}
