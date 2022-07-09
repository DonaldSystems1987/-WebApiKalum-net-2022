using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/Alumnos")]

    public class AlumnoController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<AlumnoController> Logger;

        public AlumnoController(KalumDbContext _Dbcontext, ILogger<AlumnoController> _Logger)
        {
            this.DbContext = _Dbcontext;
            this.Logger = _Logger;
        }

        /*[HttpGet]
        public ActionResult<List<Alumno>> Get()
        { 
            List<Alumno> alumnos = null;
            alumnos = DbContext.Alumno.Include(a => a.Inscripciones).ToList();
            
            if(alumnos == null || alumnos.Count == 0)
            { 
                return new NoContentResult();
            }
            return Ok(alumnos);
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> Get()
        {
            List<Alumno> alumnos = null;

            Logger.LogDebug("Iniciando proceso de consulta de alumnos en la base de datos");

            alumnos = await DbContext.Alumno.Include (a => a.Inscripciones).Include(a => a.CuentasPorCobrar).ToListAsync();

            if(alumnos == null || alumnos.Count == 0)
            {
                Logger.LogWarning("No existe alumnos en la base de datos");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecuto la peticion de forma exitosa");
            return Ok(alumnos);
        }

        [HttpGet("{id}", Name = "GetAlumno")]
        public async Task<ActionResult<Alumno>> GetAlumno(string id)
        { 
            Logger.LogDebug("Iniciando el proceso de busqueda con el id" + id);

            var alumnos = await DbContext.Alumno.FirstOrDefaultAsync(a => a.Carne == id);

            if(alumnos == null)
            {
                Logger.LogWarning("No existe el alumno con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exiosa");
            return Ok(alumnos);
        }

        
    }
}