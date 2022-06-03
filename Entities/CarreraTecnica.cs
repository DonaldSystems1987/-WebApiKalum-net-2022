namespace WebApiKalum.Entities
{
    public class CarreraTecnica 
    {
        public string CarreraId { get; set; }

        public string Nombre { get; set; }

         public virtual List<Aspirante> Aspirantes { get; set;} //creando relacion de uno a muchos, una carrera tecnica tiene varios aspiratntes

         public virtual List<Inscripcion> Inscripciones { get; set; } //creando relacion de uno a muchos, una carrera tecnica tiene varias inscripciones
         public virtual List<InversionCarreraTecnica> InversionesCarrerasTecnicas { get; set; } 

    }
}