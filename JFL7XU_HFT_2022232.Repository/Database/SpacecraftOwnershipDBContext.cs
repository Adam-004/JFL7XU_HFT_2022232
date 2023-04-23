using JFL7XU_HFT_2022232.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFL7XU_HFT_2022232.Repository.Database
{
    class SpacecraftOwnershipDBContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Starship> Starships { get; set; }
        public DbSet<Hangar> Hangars { get; set; }

        public SpacecraftOwnershipDBContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseLazyLoadingProxies()
                       .UseInMemoryDatabase("Spacecrafts&Owners");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Hangar
            modelBuilder.Entity<Hangar>(hangar => hangar
                .HasOne(hangar => hangar.Owner)
                .WithOne(owner => owner.Hangar)
                .OnDelete(DeleteBehavior.Cascade));

            //Starship
            modelBuilder.Entity<Starship>(sp => sp
                .HasOne(sp => sp.Owner)
                .WithMany(owner => owner.Ships)
                .HasForeignKey(sp => sp.OwnerID)
                .OnDelete(DeleteBehavior.Cascade));

            //Owner
            modelBuilder.Entity<Owner>(owner => owner
                .HasOne(owner => owner.Hangar)
                .WithOne(hangar => hangar.Owner)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Owner>(owner => owner
                .HasMany(owner => owner.Ships)
                .WithOne(ships => ships.Owner)
                .HasForeignKey(ships => ships.OwnerID)
                .OnDelete(DeleteBehavior.Cascade));

            //Starting data
            modelBuilder.Entity<Owner>().HasData(new Owner[]
            {
                new Owner(1,"Yoda mester",507),
                new Owner(2,"Din Djarin",48),
                new Owner(3,"Bahets",21),
                new Owner(4,"Din Djarin",32),
                new Owner(5,"Din Djarin",20),
                new Owner(6,"Din Djarin",18),
                new Owner(7,"Din Djarin",55),
                new Owner(8,"Din Djarin",78),
            }) ;
        }
    }
}   
