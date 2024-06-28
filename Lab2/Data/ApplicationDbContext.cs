using Lab2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<StationModel> Stations { get; set; }
        public DbSet<TicketModel> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StationModel>().HasKey(s => s.Id);
            modelBuilder.Entity<StationModel>().Property(s => s.Id).ValueGeneratedOnAdd(); // Автогенерация ID

            modelBuilder.Entity<TicketModel>().HasKey(t => t.Id);
            modelBuilder.Entity<TicketModel>().Property(t => t.Id).ValueGeneratedOnAdd(); // Автогенерация ID

            modelBuilder.Entity<TicketModel>()
                .HasOne(t => t.Station)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StationId)
                .IsRequired(false); // Nullable foreign key
        }
    }
}