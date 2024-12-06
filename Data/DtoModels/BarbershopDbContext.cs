using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DtoModels
{
    public class BarbershopDbContext : DbContext
    {
        public BarbershopDbContext(DbContextOptions<BarbershopDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<QueueEntry> QueueEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QueueEntry>()
                .HasOne(q => q.User) // A QueueEntry has one User
                .WithMany(u => u.QueueEntries) // A User has many QueueEntries
                .HasForeignKey(q => q.CustomerId) // Foreign key in QueueEntry
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete
        }
    }
}
