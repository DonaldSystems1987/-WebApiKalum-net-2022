using AutoMapper;
using WebApiKalum.Dtos;
using WebApiKalum.Entities;

namespace WebApiKalum.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CarreraTecnicaCreateDTO,CarreraTecnica>();//Transformando de objetos CarrearaaTecnicaCreateDTO hacia objetos tipo CarreraTecnica, Tenda todos los mapeos que se nos ocurran

            CreateMap<CarreraTecnica, CarreraTecnicaCreateDTO>(); //Transforma objetos que vienen de la base de datos y convierte a tipo DTO
            CreateMap<Jornada, JornadaCreateDTO>();
            CreateMap<ExamenAdmision, ExamenAdmisionCreateDTO>();
            CreateMap<Aspirante, AspiranteListDTO>().ConstructUsing(e => new AspiranteListDTO{NombreCompleto = $"{e.Apellidos} {e.Nombres}"});

            
            CreateMap<Aspirante, CarreraTecnicaAspiranteListDTO>().ConstructUsing(a => new CarreraTecnicaAspiranteListDTO{NombreCompleto = $"{a.Apellidos} {a.Nombres}"});
            CreateMap<Inscripcion, InscripcionListDTO>();
            CreateMap<CarreraTecnica, CarreraTecnicaListDTO>();
            
        }
        
    }
}