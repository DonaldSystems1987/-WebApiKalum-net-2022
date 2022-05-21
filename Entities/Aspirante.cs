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
        public virtual CarreraTecnica CarreraTecnica { get; set; } //creando relacion de de muchos a uno
    }
}