namespace WebApiKalum.Models
{
    public class ApiResponse//clase pare tener mejor control sobre lo que detectara el aplicativo y como quiero que lo imprima
    {
        public string TipoError { get; set; }
        public string HttpStatusCode { get; set; }
        public string Mensaje { get; set; }
    }
}