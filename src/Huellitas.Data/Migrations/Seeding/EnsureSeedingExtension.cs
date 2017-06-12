//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using Huellitas.Data.Core;

    /// <summary>
    /// Ensure the seeding of data base
    /// </summary>
    public static class EnsureSeedingExtension
    {
        /// <summary>
        /// Ensures the seeding.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void EnsureSeeding(this HuellitasContext context)
        {
            if (context.AllMigrationsApplied())
            {
                EnsureSeedingExtension.Seed(context);
            }
        }

        /// <summary>
        /// Seeds the contents.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void Seed(HuellitasContext context)
        {
            SeedingRoles.Seed(context);
            SeedingUsers.Seed(context);
            SeedingCustomTables.Seed(context);
            SeedingCustomTableRows.Seed(context);
            SeedingFiles.Seed(context);
            SeedingLocations.Seed(context);
            SeedingContents.Seed(context);
            SeedingSettings.Seed(context);
            SeedingAdoptionForms.Seed(context);
            SeedingNotifications.Seed(context);
            SeedingResources.Seed(context);
        }
    }
}