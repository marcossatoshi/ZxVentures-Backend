using Microsoft.EntityFrameworkCore;
using ZxVentures.Backend.Model.Entities;

namespace ZxVentures.Backend.DAL.Context
{
    public class ProjectContext : DbContext
    {
        public DbSet<PontoDeVenda> PontoDeVenda { get; set; }

        public ProjectContext() { }

        public ProjectContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<PontoDeVenda>(new PontoDeVendaConfiguration());
            modelBuilder.Entity<PontoDeVenda>().ToTable("PontoDeVenda");
            base.OnModelCreating(modelBuilder);
        }
    }
}
