using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum
{
    public class KalumDbContext : DbContext //Control punto para colocar la libreria  using Microsoft.EntityFrameworkCore
    {
        public DbSet<CarreraTecnica> CarreraTecnica { get; set; }
        public DbSet<Aspirante> Aspirante { get; set; }
        public DbSet<Jornada> Jornada { get; set; }
        public DbSet<ExamenAdmision>  ExamenAdmision { get; set; }
        public DbSet<Inscripcion> Inscripcion { get; set; }
        public DbSet<Alumno> Alumno { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<CuentaPorCobrar> CuentaPorCobrar { get; set; }
        public DbSet<InversionCarreraTecnica> InversionCarreraTecnica { get; set; }
        public DbSet<InscripcionPago> InscripcionPago { get; set; }
        public DbSet<ResultadoExamenAdmision> ResultadoExamenAdmision { get; set;}



        public KalumDbContext(DbContextOptions options)  : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("CarreraTecnica").HasKey(ct => new {ct.CarreraId});
            modelBuilder.Entity<Jornada>().ToTable("Jornada").HasKey(j => new {j.JornadaId});
            modelBuilder.Entity<ExamenAdmision>().ToTable("ExamenAdmision").HasKey(ex => new {ex.ExamenId});
            modelBuilder.Entity<Aspirante>().ToTable("Aspirante").HasKey(a => new {a.NoExpediente});//estableciendo la relacion entityframework de tabla Aspirante y tabla CarreraTecnica
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion").HasKey(i => new {i.InscripcionId});
            modelBuilder.Entity<Alumno>().ToTable("Alumno").HasKey(alu => new {alu.Carne}); //Definiendo la llave primaria Carne
            modelBuilder.Entity<Cargo>().ToTable("Cargo").HasKey( c => new {c.CargoId}); //Definiendo la llave primaria CargoId
            modelBuilder.Entity<CuentaPorCobrar>().ToTable("CuentaPorCobrar").HasKey(cpc => new {cpc.Correlativo,cpc.Anio,cpc.Carne});
            modelBuilder.Entity<InversionCarreraTecnica>().ToTable("InversionCarreraTecnica").HasKey(i => new {i.InversionId,i.CarreraId});
            modelBuilder.Entity<InscripcionPago>().ToTable("InscripcionPago").HasKey(ins => new {ins.BoletaPago,ins.NoExpediente,ins.Anio});
            modelBuilder.Entity<ResultadoExamenAdmision>().ToTable("ResultadoExamenAdmision").HasKey(r => new {r.NoExpediente,r.Anio});

            modelBuilder.Entity<Aspirante>()
                .HasOne<CarreraTecnica>( a => a.CarreraTecnica)//hacemos referencia a una entidad llamada CarreraTecnica
                .WithMany(ct => ct.Aspirantes)
                .HasForeignKey(a => a.CarreraId);

            modelBuilder.Entity<Aspirante>()//Creando relacion entre aspirante y jornada
                .HasOne<Jornada>(a => a.Jornada)//hacia que entidad vamos a relacionar
                .WithMany(j => j.Aspirantes)
                .HasForeignKey(a => a.JornadaId);   

            modelBuilder.Entity<Aspirante>()
                .HasOne<ExamenAdmision>(a => a.ExamenAdmision)
                .WithMany(ex => ex.Aspirantes)
                .HasForeignKey(a => a.ExamenId);

            modelBuilder.Entity<Inscripcion>()
                .HasOne<CarreraTecnica>(i => i.CarreraTecnica)
                .WithMany(ct => ct.Inscripciones)
                .HasForeignKey(i => i.CarreraId);

            modelBuilder.Entity<Inscripcion>()
                .HasOne<Jornada>(i => i.Jornada)
                .WithMany(j => j.Inscripciones)
                .HasForeignKey(i => i.JornadaId);

            modelBuilder.Entity<Inscripcion>()
                .HasOne<Alumno>(i => i.Alumno)
                .WithMany(alu => alu.Inscripciones)
                .HasForeignKey( i => i.Carne);

            modelBuilder.Entity<CuentaPorCobrar>().ToTable("CuentaPorCobrar")
                .HasOne<Cargo>(cpc => cpc.Cargo)
                .WithMany(c => c.CuentasPorCobrar)
                .HasForeignKey(cpc => cpc.CargoId);

            modelBuilder.Entity<CuentaPorCobrar>().ToTable("CuentaPorCobrar")
                .HasOne<Alumno>(cpc => cpc.Alumno)
                .WithMany(a => a.CuentasPorCobrar)
                .HasForeignKey(cpc => cpc.Carne);

            modelBuilder.Entity<InversionCarreraTecnica>().ToTable("InversionCarreraTecnica")
                .HasOne<CarreraTecnica>(i => i.CarreraTecnica)
                .WithMany(ct => ct.InversionesCarrerasTecnicas)
                .HasForeignKey(i => i.CarreraId);

            modelBuilder.Entity<InscripcionPago>().ToTable("InscripcionPago")
                .HasOne<Aspirante>(ins => ins.Aspirante)
                .WithMany(a => a.Inscripcionespago)
                .HasForeignKey(ins => ins.NoExpediente);

            modelBuilder.Entity<ResultadoExamenAdmision>().ToTable("ResultadoExamenAdmision")
                .HasOne<Aspirante>(r => r.Aspirante)
                .WithMany(a => a.ResultadosExamenesAdmision)
                .HasForeignKey(r => r.NoExpediente);
        }
    }
}