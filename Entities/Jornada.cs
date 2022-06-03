namespace WebApiKalum.Entities
{
    public class Jornada
    {
        public string JornadaId { get; set; }
        public string Jorn { get; set; }
        public string Descripcion { get; set; }

        public virtual List<Aspirante> Aspirantes { get; set;} // Coleccion, Relacion de una Jornada a muchos Aspirantes

        public virtual List<Inscripcion> Inscripciones { get; set; } //Coleccion, Relacion de una Jornada a muchos Incripciones
    }
}