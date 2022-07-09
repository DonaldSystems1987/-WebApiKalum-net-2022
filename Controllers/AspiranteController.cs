using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Dtos;
using WebApiKalum.Entities;
using WebApiKalum.Utilities;

namespace WebApiKalum.Controllers
{   
    
    [ApiController]
    [Route("v1/KalumManagement/Aspirantes")]

    public class AspiranteController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<AspiranteController> Logger;    

        private readonly IMapper Mapper;

        public AspiranteController(KalumDbContext _Dbcontext, ILogger<AspiranteController> _Logger, IMapper _Mapper)
        {
            this.DbContext = _Dbcontext;
            this.Logger = _Logger;
            this.Mapper = _Mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Aspirante>> Post([FromBody] Aspirante value)
        {
            Logger.LogDebug("Iniciando proceso para almacenar un registro de aspirante ");
            CarreraTecnica carreraTecnica = await DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == value.CarreraId);
            if(carreraTecnica == null)
            {
                Logger.LogInformation($"No existe la carrera técnica con el id {value.CarreraId}");
                return BadRequest();
            }
            Jornada jornada = await DbContext.Jornada.FirstOrDefaultAsync(j => j.JornadaId == value.JornadaId);
            if(jornada == null)
            {
                Logger.LogInformation($"No existe la jornada con el id {value.JornadaId}");
                return BadRequest();
            }
            ExamenAdmision examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(e => e.ExamenId == value.ExamenId);
            if(examen == null)
            {
                Logger.LogInformation($"No existe el examen de admision con el id {value.ExamenId}");
                return BadRequest();
            }

            await DbContext.Aspirante.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation($"Se ha creado el aspirante con exito");
            return Ok(value);

        }

        //Lista de aspirantes
        [HttpGet]
        [ServiceFilter(typeof(ActionFilter))] //filtro que permite ejecutar alguna tarea antes de que se ejecute una accion
        public async Task<ActionResult<IEnumerable<AspiranteListDTO>>> Get()
        {
            Logger.LogDebug("Iniciando proceso de consulta de aspirante");
            List<Aspirante> lista = await DbContext.Aspirante.Include(a => a.Jornada).Include(a => a.CarreraTecnica).Include(a => a.ExamenAdmision).ToListAsync();
            if(lista == null || lista.Count == 0)
            {
                Logger.LogWarning("No existen registros en la base de datos");
                return new NoContentResult();
            }
            List<AspiranteListDTO> aspirantes = Mapper.Map<List<AspiranteListDTO>>(lista);
            Logger.LogInformation("La consulta se ejecuta con existo");
            return Ok(aspirantes);
        }

        [HttpGet("{id}", Name = "GetAspirante")]
        public async Task<ActionResult<Aspirante>> GetAspirante(string id)
        { 
            Logger.LogDebug("Iniciando el proceso de busqueda con el id" + id);

            var aspirante = await DbContext.Aspirante.FirstOrDefaultAsync(a => a.NoExpediente == id);

            if(aspirante == null)
            {
                Logger.LogWarning("No existe el aspirante con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exiosa");
            return Ok(aspirante);
        }

         [HttpDelete("{id}")]
        public async Task<ActionResult<Aspirante>> Delete(string id)
        {
            Aspirante aspirante = await DbContext.Aspirante.FirstOrDefaultAsync(a => a.NoExpediente == id);
            if(aspirante == null)
            {
                return NotFound();
            }
            else
            {
                DbContext.Aspirante.Remove(aspirante);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se ha eliminado correctamente el Aspirante con el id {id}");
                return aspirante;
            }
        }

         //Metodo para modificar registro 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Aspirante value)
        {
           Aspirante aspirante = await DbContext.Aspirante.FirstOrDefaultAsync(a => a.NoExpediente == id);
           if(aspirante == null)
           {
               Logger.LogWarning($"No existe el cargo con el id {id}");
               return BadRequest();
           } 
           aspirante.Apellidos = value.Apellidos;
           aspirante.Nombres = value.Nombres;
           aspirante.Direccion = value.Direccion;
           aspirante.Telefono = value.Telefono;
           aspirante.Email = value.Email;
           DbContext.Entry(aspirante).State = EntityState.Modified;
           await DbContext.SaveChangesAsync();
           Logger.LogInformation("Los datos han sido actualizados correctamente");
           return NoContent();
        }
    }
}