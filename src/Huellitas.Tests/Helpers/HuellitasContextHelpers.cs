//-----------------------------------------------------------------------
// <copyright file="HuellitasContextHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Helpers
{
    using Huellitas.Data.Core;

    /// <summary>
    /// Create Context Helper
    /// </summary>
    public static class HuellitasContextHelpers
    {
        /// <summary>
        /// Gets the <c>huellitas</c> context.
        /// </summary>
        /// <returns>the context</returns>
        public static HuellitasContext GetHuellitasContext()
        {
            return new HuellitasContext(new Microsoft.EntityFrameworkCore.DbContextOptions<HuellitasContext>());
        }
    }
}