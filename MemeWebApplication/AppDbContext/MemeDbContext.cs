using MemeWebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeWebApplication.AppDbContext
{
    public class MemeDbContext : DbContext
    {

        public MemeDbContext(DbContextOptions<MemeDbContext> options) : base (options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Actor> Actors{ get; set; }
        public DbSet<UserReaction> UserReactions { get; set; }
        public DbSet<ActorTemplates> ActorTemplates { get; set; }
      


        public DbSet<Phrase> Phrases { get; set; }




        public MemeDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SQL5031.site4now.net;database=DB_A550E5_Afachat;User Id=DB_A550E5_Afachat_admin;Password=afachat12345;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
