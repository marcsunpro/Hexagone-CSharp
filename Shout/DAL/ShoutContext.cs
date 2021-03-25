using Microsoft.EntityFrameworkCore;
using Shout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shout.DAL
{
    public class ShoutContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Shoutt> Shoutts { get; set; }
        public DbSet<Follow> Follows { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySQL("server=localhost;database=shout;user=root;password=marc1997");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasMany(e => e.Shoutts).WithOne(e => e.User);
            });

            modelBuilder.Entity<Shoutt>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.DatePublication).IsRequired();
                entity.HasOne(e => e.User).WithMany(e => e.Shoutts);
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasNoKey();
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.UserFollow).IsRequired();
            });
        }
    }
}
