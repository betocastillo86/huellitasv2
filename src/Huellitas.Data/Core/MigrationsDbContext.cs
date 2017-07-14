using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Huellitas.Data.Core
{
    public class MigrationsDbContext : IDbContextFactory<HuellitasContext>
    {
        public HuellitasContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<HuellitasContext>();
            builder.UseSqlServer("Server=localhost;Database=HuellitasV2;User Id=sa;Password=Temporal1;MultipleActiveResultSets=false");
            return new HuellitasContext(builder.Options);
        }
    }
}