using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiKalum.Utilities;

namespace WebApiKalum
{
    public class Startup
    {
        public IConfiguration Configuration { get;}
    //crecion de propiedaes y constructor
        public Startup(IConfiguration _Configuration)
        {
            this.Configuration = _Configuration;
        }

        public void ConfigureServices(IServiceCollection _services)
        {
            _services.AddTransient<ActionFilter>();
            _services.AddControllers(options => options.Filters.Add(typeof(ErrorFilterException)));
            _services.AddAutoMapper(typeof(Startup));//Creando Configuracion para iniciar los Automate o mapeos
            _services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);//Configuracion ayuda a que redundancia se ignore 
            _services.AddDbContext<KalumDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));//setear el valor de mi cadena de coleccion, colocamos un contexto es el puente a nuestra base de datos
            _services.AddEndpointsApiExplorer();
            _services.AddSwaggerGen();//Ahorra trabajar con postman 
        }

        //Metodo para levantar configuracion
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>{endpoints.MapControllers();});
        }
    }
}