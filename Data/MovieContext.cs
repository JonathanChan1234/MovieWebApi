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

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Broadcast> Broadcasts { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}