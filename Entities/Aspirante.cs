namespace WebApiKalum.Entities
{
    public class Aspirante
    {
        public string NoExpediente { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set;}
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estatus { get; set; }
        public string CarreraId { get; set; }//Campo en comun de tabla Aspirante y CarreraTecnica
        public string JornadaId { get; set; }//Campo en comun tabla Aspirante y Jornada
        public string ExamenId { get; set; }//Campo en comun tabla Aspirante y ExamenAdmision

        public virtual CarreraTecnica CarreraTecnica { get; set; } //creando relacion de de muchos a uno
        public virtual Jornada Jornada { get; set; } //Coleccion, Creando relacion  de muchos a uno de aspirante a Jornada
        public virtual ExamenAdmision ExamenAdmision { get; set; } 
        public virtual List<InscripcionPago> Inscripcionespago { get; set; }
        public virtual List<ResultadoExamenAdmision> ResultadosExamenesAdmision { get; set; }
    }
}