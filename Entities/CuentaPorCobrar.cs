namespace WebApiKalum.Entities
{
    public class CuentaPorCobrar
    {
        public string Correlativo { get; set; }
        public string Anio { get; set; }
        public string Carne { get; set; }
        public string CargoId { get; set; }
        public string Descripcion { get; set; }
        public  DateTime FechaCargo { get; set; }
        public DateTime FechaAplica { get; set; }
        public  double Monto { get; set; }
        public double Mora { get; set; }
        public double Descuente { get; set; }

        public virtual Cargo Cargo { get; set; } //relacion de muchos a uno, muchas cuentas para un cargo

        public virtual Alumno Alumno { get; set; } //relacion de muchos a uno, muchas cuentas para un alumno
        
    }
}