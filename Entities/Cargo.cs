namespace WebApiKalum.Entities
{
    public class Cargo
    {
        public string CargoId { get; set; }
        public string Descripcion { get; set; }
        public string Prefijo { get; set; }
        public decimal Monto { get; set; } //double utilizado para declarar decimal
        public bool GeneraMora { get; set; }//bool utilizado para declarar bit
        public int PorcentajeMora { get; set; } //int utilizado para declarar enteros(int)

        public virtual List<CuentaPorCobrar> CuentasPorCobrar { get; set; } //Relecion de uno a muchos, un cargo puede tener varias cuentas por cubrar
    }
}