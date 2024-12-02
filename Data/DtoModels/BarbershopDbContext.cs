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
    }
}
