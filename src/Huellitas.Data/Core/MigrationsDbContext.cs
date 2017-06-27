using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Huellitas.Data.Core
{
    public class MigrationsDbContext : IDbContextFactory<HuellitasContext>
    {
        public HuellitasContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<HuellitasContext>();
            builder.UseSqlServer("Server=13.64.198.189;Database=HuellitasProd;User Id=sa;Password=Temporal1;MultipleActiveResultSets=false");
            return new HuellitasContext(builder.Options);
        }
    }
}