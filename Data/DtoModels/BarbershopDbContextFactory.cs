using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DtoModels
{
    public class BarbershopDbContextFactory : IDesignTimeDbContextFactory<BarbershopDbContext>
    {
        public BarbershopDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BarbershopDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-V0P97I1\\SQLEXPRESS;Database=BarberDB;Trusted_Connection=True;TrustServerCertificate=True;");
            return new BarbershopDbContext(optionsBuilder.Options);
        }
    }
}
