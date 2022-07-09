namespace WebApiKalum.Entities
{
    public class InversionCarreraTecnica
    {
        public string InversionId { get; set; }
        public string CarreraId { get; set; }
        public decimal MontoInscripcion { get; set; }
        public int NumeroPagos { get; set; }
        public decimal MontoPagos { get; set; }

        public virtual CarreraTecnica CarreraTecnica { get; set; } //Relacion de muchos a uno, muchas carreras tecnicas a una carrera tecnica
    }
}