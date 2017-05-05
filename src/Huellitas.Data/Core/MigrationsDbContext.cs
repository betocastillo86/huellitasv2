using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huellitas.Data.Core
{
    public class MigrationsDbContext : IDbContextFactory<HuellitasContext>
    {
        public HuellitasContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<HuellitasContext>();
            builder.UseSqlServer("Server=ASUS_CASA\\SQL_ASUS;Database=HuellitasV2;User Id=sa;Password=Temporal1;MultipleActiveResultSets=false");
            return new HuellitasContext(builder.Options);
        }
    }
}
