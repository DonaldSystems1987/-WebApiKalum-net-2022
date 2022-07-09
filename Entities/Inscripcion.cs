namespace WebApiKalum.Entities
{
    public class Inscripcion
    {
        public string InscripcionId { get; set; }
        public string Carne { get; set; }
        public string CarreraId { get; set; }
        public string JornadaId { get; set; }
        public string Ciclo { get; set; }
        public DateTime FechaInscripcion { get; set; }


        public virtual CarreraTecnica CarreraTecnica { get; set;} // relacion uno a uno una CarreraTecnica  tiene Inscripcion
        public virtual Jornada Jornada { get; set; } // relacion uno a uno una Jornada  tiene una Inscripcion
        public virtual Alumno Alumno { get; set; } // relacion uno a uno, Incripcion solo puede tener un alumno

        
    }
}