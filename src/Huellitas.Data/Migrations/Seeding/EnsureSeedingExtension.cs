//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using System;
    using Huellitas.Data.Core;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Ensure the seeding of data base
    /// </summary>
    public static class EnsureSeedingExtension
    {
        /// <summary>
        /// Ensures the seeding.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="config">The configuration.</param>
        public static void EnsureSeeding(this HuellitasContext context, IConfigurationRoot config)
        {
            var execMigrations = config["ExecSeed"] != null && Convert.ToBoolean(config["ExecSeed"]);
            if (execMigrations && context.AllMigrationsApplied())
            {
                EnsureSeedingExtension.Seed(context);
            }
        }

        /// <summary>
        /// Seeds the contents.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HuellitasContext context)
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

            //// guarda registro de que se corrió la semilla
            context.Logs.Add(new Entities.Log()
            {
                CreationDate = DateTime.Now,
                FullMessage = "Ejecución de seed",
                LogLevel = Entities.LogLevel.Information,
                ShortMessage = "Ejecución de seed"
            });
        }
    }
}