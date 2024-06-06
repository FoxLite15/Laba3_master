using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Film> Filmss { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmGenre>()
                .HasKey(bg => new { bg.FilmId, bg.GenreId });

            modelBuilder.Entity<FilmGenre>()
                .HasOne(bg => bg.Film)
                .WithMany(b => b.FilmGenres)
                .HasForeignKey(bg => bg.FilmId);

            modelBuilder.Entity<FilmGenre>()
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.FilmGenres)
                .HasForeignKey(bg => bg.GenreId);
        }

    }
}
