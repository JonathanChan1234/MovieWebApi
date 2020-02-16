using System;
using Microsoft.EntityFrameworkCore;
using NetApi.Models;

namespace NetApi.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region TableSplitting
            modelBuilder.Entity<Film>(dob =>
            {
                dob.ToTable("films");
            });

            modelBuilder.Entity<FilmAbstract>(ob =>
            {
                ob.ToTable("films");
                ob.HasOne(f => f.film)
                    .WithOne()
                    .HasForeignKey<Film>(f => f.filmId);
            });
            // modelBuilder.Entity<Broadcast>(ob =>
            // {
            //     ob.Property("filmId").HasColumnName("filmId");
            //     ob.HasOne(b => b.film)
            //        .WithOne()
            //        .HasForeignKey<FilmAbstract>(f => f.filmId);
            // });
            #endregion
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmAbstract> FilmsAbstract { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}