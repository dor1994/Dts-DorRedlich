using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Data.DtoModels
{
    public class BarbershopDbContextFactory : IDesignTimeDbContextFactory<BarbershopDbContext>
    {
        public BarbershopDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory()) // Set base directory
             .AddJsonFile("appsettings.json")              // Add appsettings.json
             .Build();

            // Get connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<BarbershopDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BarbershopDbContext(optionsBuilder.Options);
        }
    }
}
