using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/Jornadas")]

    public class JornadaController : ControllerBase
    {
        private readonly KalumDbContext DbContext; 
        private readonly ILogger<JornadaController> Logger;
        
        public JornadaController(KalumDbContext _Dbcontext, ILogger<JornadaController> _Logger) //Constructor
        {
            this.DbContext = _Dbcontext;
            this.Logger = _Logger;
        }

        //crear metodo que traer todas las jornadas que estan en la base de datos 
        //Mostrara en postman el request de consultas de todas las jornadas
        /*[HttpGet]
        public ActionResult<List<Jornada>> Get()
        {
            List<Jornada> jornadas = null;
            jornadas = DbContext.Jornada.Include( j => j.Aspirantes).ToList(); //convirtiendo la informacion de jornadas en una lista

            if(jornadas == null || jornadas.Count == 0)
            {
                return new NoContentResult();
            }   
            return Ok(jornadas);
        }*/

         
        //Mostrara en postman el request de consultas de todas las jornadas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jornada>>> Get()
        {
            List<Jornada> jornadas = null;

            Logger.LogDebug("Iniciando proceso de consulta de jornadas en la base de datos");

            jornadas = await DbContext.Jornada.Include (j => j.Aspirantes).Include(j => j.Inscripciones).ToListAsync();

            if(jornadas == null || jornadas.Count == 0)
            {
                Logger.LogWarning("No existe jornadas");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecuto la peticion de forma exitosa");
            return Ok(jornadas);
        }
        
        //Mostrara en postman el request de busqueda de JornadaId 
        [HttpGet("{id}", Name = "GetJornada")]
        
        public async Task<ActionResult<Jornada>> GetJornada(string id)
        {
            Logger.LogDebug("Iniciando el proceso de busqueda con el id" + id);

            var jornada = await DbContext.Jornada.FirstOrDefaultAsync(j => j.JornadaId == id);

            if(jornada == null)
            {
                Logger.LogWarning("No existe la jornada con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exitosa");
            return Ok(jornada);
        }

        //Metodo para agregar nuevo registro
        [HttpPost]
        public async Task<ActionResult<Jornada>> Post([FromBody] Jornada value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar una jornada nueva");
            value.JornadaId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.Jornada.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finaalizando el proceso de agregar una jornada");
            return new CreatedAtRouteResult("GetJornada",new {id = value.JornadaId}, value);
        }

        //Metodo para eliminar registro
        [HttpDelete("{id}")]
        public async Task<ActionResult<Jornada>> Delete(string id)
        {
            Jornada jornada = await DbContext.Jornada.FirstOrDefaultAsync(j => j.JornadaId == id);
            if(jornada == null)
            {
                return NotFound();
            }
            else
            {
                DbContext.Jornada.Remove(jornada);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se ha eliminado correctamente la jornada con el id {id}");
                return jornada;
            }
        }

        //Metodo para modificar registro 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Jornada value)
        {
           Jornada jornada = await DbContext.Jornada.FirstOrDefaultAsync(j => j.JornadaId == id);
           if(jornada == null)
           {
               Logger.LogWarning($"No existe la jornada con el id {id}");
               return BadRequest();
           } 
           jornada.Jorn = value.Jorn;
           jornada.Descripcion = value.Descripcion;
           DbContext.Entry(jornada).State = EntityState.Modified;
           await DbContext.SaveChangesAsync();
           Logger.LogInformation("Los datos han sido actualizados correctamente");
           return NoContent();
        }

    }
}