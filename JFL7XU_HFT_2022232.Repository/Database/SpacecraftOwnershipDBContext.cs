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
    public class SpacecraftOwnershipDBContext : DbContext
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
                new Owner(4,"Béla",32),
                new Owner(5,"Feri",20),
                new Owner(6,"Anakin Skywalker",18),
                new Owner(7,"Grogu",55),
                new Owner(8,"Owen Lars",78)
            }) ;

            //Transport = 1,
            //Fregatte = 2,
            //Cruiser = 3,
            //Fighter = 4

            modelBuilder.Entity<Starship>().HasData(new Starship[]
            {
                new Starship(1,"Borotvaél",100,20271,3,2),
                new Starship(2,"Nabui Csillagvadász",60,20150,4,2),
                new Starship(3,"Scorpion",65,2970,4,3),
                new Starship(4,"890 Jump",270,2998,4,3),
                new Starship(5,"Idris",405,2870,2,3),
                new Starship(6,"LAAT",140,20198,3,1),
                new Starship(7,"SnowSpeeder",20,2020,1,4),
                new Starship(8,"SnowSpeeder II.",18,2023,1,5),
                new Starship(9,"T3-C Shuttle",20,2020,1,5),
                new Starship(10,"Pod Racer",20,20120,1,6),
                new Starship(11,"Star Skiff",20,20102,4,6),
                new Starship(12,"Tranporter SM-11",35,20120,1,7),
                new Starship(13,"IB-37",40,20229,4,7),
                new Starship(14,"Desert Skiff",20,20101,1,8),
            });

            modelBuilder.Entity<Hangar>().HasData(new Hangar[]
            {
                new Hangar(1,"Trade league platform","Nabu",1),
                new Hangar(2,"Imperial Landing Pod","Nevarró",2),
                new Hangar(3,"Self-Land","Orison",3),
                new Hangar(4,"Kenedy Launch Station","Houston",4),
                new Hangar(5,"Falcon Launch Station","Cape Canaveral",5),
                new Hangar(6,"Desert Launch Pod","Tatuin",6),
                new Hangar(7,"Rocky's Station","Nevarró",7),
                new Hangar(8,"Dock 07","Tatuin",8)
            });
        }
    }
}   
