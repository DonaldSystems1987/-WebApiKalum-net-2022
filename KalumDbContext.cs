using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum
{
    public class KalumDbContext : DbContext //Control punto para colocar la libreria  using Microsoft.EntityFrameworkCore
    {
        public DbSet<CarreraTecnica> CarreraTecnica { get; set; }
        public DbSet<Aspirante> Aspirante { get; set; }

        public KalumDbContext(DbContextOptions options)  : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("CarreraTecnica").HasKey(ct => new {ct.CarreraId});
            modelBuilder.Entity<Aspirante>().ToTable("Aspirante").HasKey(a => new {a.NoExpediente});//estableciendo la relacion entityframework de tabla Aspirante y tabla CarreraTecnica
            modelBuilder.Entity<Aspirante>()
                .HasOne<CarreraTecnica>( c => c.CarreraTecnica)
                .WithMany(ct => ct.Aspirantes)
                .HasForeignKey(c => c.CarreraId);
        }
    }
}