using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(){ }

        /*public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }*/

        public DbSet<BookingSet> BookingSet { get; set; }
        public DbSet<FlightSet> FlightSet { get; set; }
        public DbSet<PassengerSet> PassengerSet { get; set; }
        public DbSet<PilotSet> PilotSet { get; set; }

        public static string ConnectionString { get; set; } = @"Server=(localdb)\MSSQLLocalDB;Database=VSJetDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
            //optionsBuilder.UseSqlServer(ConnectionString);

            // convinient, flexible BUT DANGEROUS FOR PERFORMANCE
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingSet>().HasKey(x => new { x.FlightNo, x.PassengerID });

            // mapping many to many relationship
            modelBuilder.Entity<BookingSet>()
                .HasOne(x => x.Flight)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.FlightNo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BookingSet>()
                .HasOne(x => x.Passenger)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.PassengerID)
                .OnDelete(DeleteBehavior.Restrict);



        }

    }
}
