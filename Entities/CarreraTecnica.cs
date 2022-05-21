namespace WebApiKalum.Entities
{
    public class CarreraTecnica 
    {
        public string CarreraId { get; set; }

        public string Nombre { get; set; }
        public virtual List<Aspirante> Aspirantes { get; set;} //creando relacion de uno a muchos

    }
}