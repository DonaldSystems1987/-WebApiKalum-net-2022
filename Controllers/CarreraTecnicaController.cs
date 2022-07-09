using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Dtos;
using WebApiKalum.Entities;
using WebApiKalum.Utilities;

namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/CarrerasTecnicas")]//podemos modificar si quisieramos en etiqueta controller colocar CarrerasTecnicas

    public class CarreraTecnicaController : ControllerBase
    {
        private readonly KalumDbContext DbContext;

        private readonly ILogger<CarreraTecnicaController> Logger;

        private readonly IMapper Mapper;

        public CarreraTecnicaController(KalumDbContext _Dbcontext, ILogger<CarreraTecnicaController> _Logger, IMapper _Mapper)
        {
            this.DbContext = _Dbcontext;
            this.Logger = _Logger;
            this.Mapper = _Mapper;
        }
        
       /* [HttpGet]
        public async Task<ActionResult<IEnumerable<CarreraTecnicaController>>> Get()
        {
           List<CarreraTecnica> carrerasTecnicas = null;

           Logger.LogDebug("Iniciando proceso de consulta de carreras tecnicas en la base de datos");

           //Tarea 1 
           carrerasTecnicas = await DbContext.CarreraTecnica.Include (c => c.Aspirantes).Include(c => c.Inscripciones).ToListAsync();

            //Tarea 2 
           if(carrerasTecnicas == null || carrerasTecnicas.Count == 0)
           {
               Logger.LogWarning("No existe carreras técnicas");
               return new NoContentResult();
           }    
            List<CarreraTecnicaListDTO> carreras = Mapper.Map<List<CarreraTecnicaListDTO>>(carrerasTecnicas);
            Logger.LogInformation("La consulta se ejecuta con existo");
            return Ok(carreras);
        }*/
           [HttpGet]
         public async Task<ActionResult<IEnumerable<CarreraTecnicaListDTO>>> Get()
        {
            Logger.LogDebug("Iniciando proceso de consulta de Carreras tecnicas");
            List<CarreraTecnica> lista = await DbContext.CarreraTecnica.Include(ct => ct.Aspirantes).Include(ct => ct.Inscripciones).ToListAsync();
            if(lista == null || lista.Count == 0)
            {
                Logger.LogWarning("No existen registros en la base de datos");
                return new NoContentResult();
            }
            List<CarreraTecnicaListDTO> carrerasTecnicas = Mapper.Map<List<CarreraTecnicaListDTO>>(lista);
            Logger.LogInformation("La consulta se ejecuta con existo");
            return Ok(carrerasTecnicas);
        }

        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<CarreraTecnicaListDTO>>> GetPaginacion(int page)
        {
            var queryable = this.DbContext.CarreraTecnica.Include(ct => ct.Aspirantes).Include(ct => ct.Inscripciones).AsQueryable();
            var paginacion = new HttpResponsePaginacion<CarreraTecnica>(queryable,page);
            if(paginacion.Content == null && paginacion.Content.Count == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(paginacion);
            }
        }

        [HttpGet("{id}", Name = "GetCarreraTecnica")]
        public async Task<ActionResult<CarreraTecnica>> GetCarreraTecnica(string id)
        {
            Logger.LogDebug("Iniciando el proceso de busqueda con el id" + id);
            var carrera = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carrera == null)
            { 
                Logger.LogWarning("No existe la carrera técnica con el id " + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exitosa");
            return Ok(carrera);
        }

       ///Metodo para agregar un nuevo registro
        [HttpPost]
        public async Task<ActionResult<CarreraTecnica>> Post([FromBody] CarreraTecnicaCreateDTO value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar una carrera tecnica nueva");

            //CarreraTecnica nuevo = new CarreraTecnica(){Nombre = value.Nombre};  //Transformado p haciendo el mapeo si en caso solo es una propiedad 
            CarreraTecnica nuevo = Mapper.Map<CarreraTecnica>(value); // Tranformando de forma automaper objetos de CarreraTecnicaCreateDTO a objetos tipo CarreraTecnica

            nuevo.CarreraId = Guid.NewGuid().ToString().ToUpper();//Generando nuevo id  ToUpper nos coloca el id en mayuscula
            await DbContext.CarreraTecnica.AddAsync(nuevo);//se hace la definicion para almacenar el registro
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finalizando el proceso de agregar una carrera tecnica");
            return new CreatedAtRouteResult("GetCarreraTecnica",new {id = nuevo.CarreraId}, nuevo);

        }

        /*//metodo agregar metodo anterior
        [HttpPost]
        public async Task<ActionResult<CarreraTecnica>> Post([FromBody] CarreraTecnica value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar una Carrera tecnica nueva");
            value.CarreraId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.CarreraTecnica.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finaalizando el proceso de agregar una jornada");
            return new CreatedAtRouteResult("GetJornada",new {id = value.CarreraId}, value);
        }*/


        //Metodo para eliminar 
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarreraTecnica>> Delete(string id)
        {
            CarreraTecnica carreraTecnica = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carreraTecnica == null)
            {
                return NotFound();
            }
            else
            {
                DbContext.CarreraTecnica.Remove(carreraTecnica);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se ha eliminado correctamente la carrera tecnica con el id {id}");
                return carreraTecnica;
            }
        }

        //Metodo para modificar
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] CarreraTecnica value)
        {
            Logger.LogDebug($"Iniciando el proceso de actualizacion de la carreara tecnica con el Id {id}");
            CarreraTecnica carreraTecnica = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if(carreraTecnica == null)
            {
                Logger.LogWarning($"No existe la carrera tecnica con el Id {id}");
                return  BadRequest();
            }
            carreraTecnica.Nombre = value.Nombre;
            DbContext.Entry(carreraTecnica).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Los datos han sido actualizados correctamente");
            return NoContent();
        }

     

    }
}