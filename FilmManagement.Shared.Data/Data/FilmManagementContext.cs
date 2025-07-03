using FilmManagement.Shared.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmManagement.Shared.Data.Data
{
    public class FilmManagementContext : DbContext
    {
        public DbSet<Filmmaker> Filmmakers { get; set; }
        public DbSet<Films> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }

        private string _connectionStringRemote = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FilmManagementV0;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public FilmManagementContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder
                .UseSqlServer(_connectionStringRemote)
                .UseLazyLoadingProxies();
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Films>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Films);
        }
    }
}
