using System.ComponentModel.DataAnnotations;

namespace WebApiKalum.Entities
{
    public class Jornada
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string JornadaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(2,MinimumLength = 2, ErrorMessage = "La cantidad minima de caracteres es {2} y maxima es {1} para el campo {0}")]
        public string Jorn { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Descripcion { get; set; }

        public virtual List<Aspirante> Aspirantes { get; set;} // Coleccion, Relacion de una Jornada a muchos Aspirantes

        public virtual List<Inscripcion> Inscripciones { get; set; } //Coleccion, Relacion de una Jornada a muchos Incripciones
    }
}