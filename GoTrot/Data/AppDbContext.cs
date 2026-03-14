using Microsoft.EntityFrameworkCore;
using GoTrot.Models;

namespace GoTrot.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Zona> Zone { get; set; }
        public DbSet<Servis> Servisi { get; set; }
        public DbSet<RatingVoznje> Rati { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(AppConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Balance)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Scooter>()
                .Property(s => s.PricePerMinute)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ride>()
                .Property(r => r.TotalCost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Payment>()
                .Property(p => p.Iznos)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rides)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Scooter)
                .WithMany(s => s.Rides)
                .HasForeignKey(r => r.ScooterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Scooter>()
                .HasOne(s => s.Zona)
                .WithMany(z => z.Scooters)
                .HasForeignKey(s => s.ZonaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Servis>()
                .HasOne(s => s.Scooter)
                .WithMany(sc => sc.Servisi)
                .HasForeignKey(s => s.ScooterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatingVoznje>()
                .HasOne(r => r.Ride)
                .WithOne(ride => ride.Rating)
                .HasForeignKey<RatingVoznje>(r => r.RideId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rezervacija>()
                .HasOne(r => r.Scooter)
                .WithMany()
                .HasForeignKey(r => r.ScooterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
