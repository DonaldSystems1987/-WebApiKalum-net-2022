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
            _services.AddControllers();
            //_services.AddDbContext<>//setear el valor de mi cadena de coleccion, colocamos un contexto es el puente a nuestra base de datos
        }
    }

}