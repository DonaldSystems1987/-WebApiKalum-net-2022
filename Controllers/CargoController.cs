using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum.Controllers
{
    [ApiController]
    [Route("v1/KalumManagement/Cargos")]

    public class CargoController : ControllerBase
    {
        private readonly KalumDbContext DbContext;
        private readonly ILogger<CargoController> Logger;

        public CargoController(KalumDbContext _Dbcontext, ILogger<CargoController> _Logger)
        {
          this.DbContext = _Dbcontext;
          this.Logger = _Logger;
        }

        /*[HttpGet]
        public ActionResult<List<Cargo>> Get()
        { 
            List<Cargo> cargos = null;
            cargos = DbContext.Cargo.Include(c => c.CuentasPorCobrar).ToList();

            if(cargos == null || cargos.Count == 0)
            { 
                return new NoContentResult();
            }
            return Ok(cargos);
        }*/



       [HttpGet]
        public async Task<ActionResult<IEnumerable<Cargo>>> Get()
        {
            List<Cargo> cargos = null;

            Logger.LogDebug("Iniciando proceso de consulta de carreras tecnicas en la base de datos");

            cargos = await DbContext.Cargo.Include(c => c.CuentasPorCobrar).ToListAsync();
        
            if(cargos == null || cargos.Count == 0)
            {
                Logger.LogWarning("No existe cargos");
                return new NoContentResult();
            }
            Logger.LogInformation("Se ejecuto la peticion de forma exitosa");
            return Ok(cargos);
        }

        [HttpGet("{id}", Name = "GetCargo")]
        public async Task<ActionResult<Cargo>> GetCargo(string id)
        {
            Logger.LogDebug("Iniciando el proceso de busqueda con el id" + id);
            var cargo = await DbContext.Cargo.FirstOrDefaultAsync(c  => c.CargoId == id);
            if(cargo == null)
            {
                Logger.LogWarning("No existe la carrera tecnica con el id" + id);
                return new NoContentResult();
            }
            Logger.LogInformation("Finalizando el proceso de busqueda de forma exitosa");
            return Ok(cargo);
        }

        //Metodo para agregar nuevo registro
        [HttpPost]
        public async Task<ActionResult<Cargo>> Post([FromBody] Cargo value)
        {
            Logger.LogDebug("Iniciando el proceso de agregar un cargo nuevo");
            value.CargoId = Guid.NewGuid().ToString().ToUpper();
            await DbContext.Cargo.AddAsync(value);
            await DbContext.SaveChangesAsync();
            Logger.LogInformation("Finalizando el proceso de agregar un cargo");
            return new CreatedAtRouteResult("GetJornada",new {id = value.CargoId}, value);
        }

        //Metodo para eliminar registro
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cargo>> Delete(string id)
        {
            Cargo cargo = await DbContext.Cargo.FirstOrDefaultAsync(j => j.CargoId == id);
            if(cargo == null)
            {
                return NotFound();
            }
            else
            {
                DbContext.Cargo.Remove(cargo);
                await DbContext.SaveChangesAsync();
                Logger.LogInformation($"Se ha eliminado correctamente el cargo con el id {id}");
                return cargo;
            }
        }

        //Metodo para modificar registro 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Cargo value)
        {
           Cargo cargo = await DbContext.Cargo.FirstOrDefaultAsync(j => j.CargoId == id);
           if(cargo == null)
           {
               Logger.LogWarning($"No existe el cargo con el id {id}");
               return BadRequest();
           } 
           cargo.Descripcion = value.Descripcion;
           cargo.Prefijo = value.Prefijo;
           cargo.Monto = value.Monto;
           cargo.GeneraMora = value.GeneraMora;
           cargo.PorcentajeMora = value.PorcentajeMora;
           DbContext.Entry(cargo).State = EntityState.Modified;
           await DbContext.SaveChangesAsync();
           Logger.LogInformation("Los datos han sido actualizados correctamente");
           return NoContent();
        }

    }
}