using System.ComponentModel.DataAnnotations;
using WebApiKalum.Dtos;

namespace WebApiKalum.Dtos
{
    public class CarreraTecnicaListDTO
    {
       
        public string CarreraId { get; set; }
        public string Nombre { get; set; }
        public List<CarreraTecnicaAspiranteListDTO> Aspirantes { get; set; }
        public List<InscripcionListDTO> inscripciones { get; set; }
    }
}