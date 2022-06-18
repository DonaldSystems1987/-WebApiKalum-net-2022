using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/ExamenesAdmisiones")]

    public class ExamenAdmisionController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<ExamenAdmisionController> Logger;

        public ExamenAdmisionController (KalumDbContext _Dbcontext, ILogger<ExamenAdmisionController> _Logger)
        {
          this.DbContext = _Dbcontext;
         this.Logger = _Logger;
        }
        
       /* [HttpGet]
        public ActionResult<List<ExamenAdmision>> Get()
        {
            List<ExamenAdmision> examenesAdmisiones = null;
            examenesAdmisiones = DbContext.ExamenAdmision.Include( e => e.Aspirantes).ToList();
            
            if(examenesAdmisiones == null || examenesAdmisiones.Count == 0)
            { 
                return new NoContentResult();
            }
            return Ok(examenesAdmisiones);
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenAdmision>>> Get()
        {
            List<ExamenAdmision> examenesAdmisiones = null;

            Logger.LogDebug("Iniciando proceso de consulta de examenes de admision en la base de datos");

            examenesAdmisiones = await DbContext.ExamenAdmision.Include( e => e.Aspirantes).ToListAsync();

            if(examenesAdmisiones == null || examenesAdmisiones.Count == 0)
            {
                Logger.LogWarning("No existe examenes de admisiones");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecuto la peticion de forma exitosa");
            return Ok(examenesAdmisiones);
        }
    

        [HttpGet("{id}", Name = "GetExamenAdmision")]
        public async Task<ActionResult<ExamenAdmision>> GetExamenAdmision(string id)
        {
            Logger.LogDebug("Iniciando el proceso de busqueda con el id" + id);

            var examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(e => e.ExamenId == id);

            if(examen == null)
            { 
                Logger.LogWarning("No existe examen de admision con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exitosa");
            return Ok(examen);
        }

        //Metodo para agregar nuevo registro
        [HttpPost]
        public async Task<ActionResult<ExamenAdmision>> Post([FromBody] ExamenAdmision value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar un examen de admision nuevo");
            value.ExamenId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.ExamenAdmision.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finalizando el proceso de agregar una jornada");
            return new CreatedAtRouteResult("GetJornada",new {id = value.ExamenId}, value);
        }

        //Metodo para eliminar registro
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExamenAdmision>> Delete(string id)
        {
            ExamenAdmision examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(j => j.ExamenId == id);
            if(examen == null)
            {
                return NotFound();
            }
            else
            {
                DbContext.ExamenAdmision.Remove(examen);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se ha eliminado correctamente el examen de admision con el id {id}");
                return examen;
            }
        }

        //Metodo para modificar registro 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ExamenAdmision value)
        {
           ExamenAdmision examen = await DbContext.ExamenAdmision.FirstOrDefaultAsync(j => j.ExamenId == id);
           if(examen == null)
           {
               Logger.LogWarning($"No existe la el examen de admision con el id {id}");
               return BadRequest();
           } 
           examen.FechaExamen = value.FechaExamen;
           DbContext.Entry(examen).State = EntityState.Modified;
           await DbContext.SaveChangesAsync();
           Logger.LogInformation("Los datos han sido actualizados correctamente");
           return NoContent();
        }

    }
}