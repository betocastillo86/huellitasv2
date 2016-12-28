//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

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
                EnsureSeedingExtension.SeedingContents(context);
            }
        }

        /// <summary>
        /// Seeds the contents.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedingContents(HuellitasContext context)
        {
            EnsureSeedingExtension.SeedRoles(context);
            EnsureSeedingExtension.SeedUsers(context);
            EnsureSeedingExtension.SeedCustomTables(context);
            EnsureSeedingExtension.SeedCustomTablesRows(context);
            EnsureSeedingExtension.SeedContents(context);
            EnsureSeedingExtension.SeedFiles(context);
            EnsureSeedingExtension.SeedLocations(context);
            EnsureSeedingExtension.SeedSettings(context);
        }

        #region Roles

        /// <summary>
        /// Seeds the roles.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedRoles(HuellitasContext context)
        {
            var list = new List<Role>();

            list.Add(new Entities.Role() { Name = "SuperAdmin", Description = "Usuario con todos los privilegios" });
            list.Add(new Entities.Role() { Name = "Public", Description = "Usuario publico" });

            foreach (var item in list)
            {
                if (!context.Roles.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Roles.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Roles

        #region Users

        /// <summary>
        /// Seeds the users.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedUsers(HuellitasContext context)
        {
            var list = new List<User>();

            list.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "210c1680b957c6ed6df5d9afd17f205cd9a01dbb"/*123.admin@admin.com*/, RoleId = 1, CreatedDate = DateTime.Now });
            list.Add(new Entities.User() { Name = "Publico", Email = "public@public.com", Password = "fbaee72e992b7892dcb3e3a3918befac5c1cb553"/*123.public@public.com*/, RoleId = 2, CreatedDate = DateTime.Now });

            foreach (var item in list)
            {
                if (!context.Users.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Users.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Users

        #region CustomTables

        /// <summary>
        /// Seeds the custom tables.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedCustomTables(HuellitasContext context)
        {
            var list = new List<CustomTable>();

            list.Add(new CustomTable() { Id = 1, Name = "Subtipo Animales", Description = "Subtipo de contenido para animales" });
            list.Add(new CustomTable() { Id = 2, Name = "Tamaño", Description = "Tamaño de los animales existentes" });
            list.Add(new CustomTable() { Id = 3, Name = "Genero de los animales", Description = "Tamaño de los animales existentes" });

            foreach (var item in list)
            {
                if (!context.CustomTables.Any(c => c.Name.Equals(item.Name)))
                {
                    context.CustomTables.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion CustomTables

        #region CustomTablesRows

        /// <summary>
        /// Seeds the custom tables rows.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedCustomTablesRows(HuellitasContext context)
        {
            var list = new List<CustomTableRow>();

            list.Add(new CustomTableRow() { CustomTableId = 1, Value = "Perro" });
            list.Add(new CustomTableRow() { CustomTableId = 1, Value = "Gato" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Miniatura" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Pequeño" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Mediano-Pequeño" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Mediano-Grande" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Grande" });
            list.Add(new CustomTableRow() { CustomTableId = 3, Value = "Macho" });
            list.Add(new CustomTableRow() { CustomTableId = 3, Value = "Hembra" });

            foreach (var item in list)
            {
                if (!context.CustomTableRows.Any(c => c.Value.Equals(item.Value) && c.CustomTableId.Equals(item.CustomTableId)))
                {
                    context.CustomTableRows.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion CustomTablesRows

        #region Contents

        /// <summary>
        /// Seeds the contents.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedContents(HuellitasContext context)
        {
            var list = new List<Content>();

            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Uno",
                Body = "Cuerpo de contenido de prueba Pet 1",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = 1,
                FileId = 1,
                LocationId = 1,
                FriendlyName = "pet-uno",
                ContentAttributes = new List<ContentAttribute>()
                {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "5" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "3" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "1" }
                    }
            });
            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Dos",
                Body = "Cuerpo de contenido de prueba Pet 2",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = 1,
                FileId = 2,
                LocationId = 1,
                FriendlyName = "pet-dos",
                ContentAttributes = new List<ContentAttribute>()
                {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "1" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "5" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "2" }
                    }
            });
            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Tres",
                Body = "Cuerpo de contenido de prueba Pet 3",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = 1,
                FileId = 1,
                LocationId = 1,
                FriendlyName = "pet-tres",
                ContentAttributes = new List<ContentAttribute>()
                {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "1" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "3" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "2" }
                    }
            });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Uno", Body = "Cuerpo de contenido de prueba Shelter 1", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1, FileId = 2, LocationId = 1, FriendlyName = "shelter-uno" });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Dos", Body = "Cuerpo de contenido de prueba Shelter 2", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1, FileId = 1, LocationId = 1, FriendlyName = "shelter-dos" });

            foreach (var item in list)
            {
                if (!context.Contents.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Contents.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Contents

        #region Files

        /// <summary>
        /// Seeds the files.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedFiles(HuellitasContext context)
        {
            var list = new List<File>();

            list.Add(new File()
            {
                Name = "Imagen Uno",
                MimeType = "image/jpg",
                FileName = "1_imagen1.jpg"
            });

            list.Add(new File()
            {
                Name = "Imagen Dos",
                MimeType = "image/jpg",
                FileName = "2_imagen2.jpg"
            });

            foreach (var item in list)
            {
                if (!context.Files.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Files.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Files

        #region Locations

        /// <summary>
        /// Seeds the locations.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedLocations(HuellitasContext context)
        {
            var list = new List<Location>();

            var parent = new Location() { Name = "Colombia" };
            list.Add(parent);
            list.Add(new Location() { Name = "Bogotá", ParentLocation = parent });

            foreach (var item in list)
            {
                if (!context.Locations.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Locations.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Locations

        #region Settings        
        /// <summary>
        /// Seeds the settings.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedSettings(HuellitasContext context)
        {
            var list = new List<SystemSetting>();

            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationAudience", Value = "AudienceAuthentication" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationIssuer", Value = "AuthenticationIssuer" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationSecretKey", Value = "TheSecretKey132456789" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.ExpirationTokenMinutes", Value = "60" });

            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthDetail", Value = "800" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightDetail", Value = "800" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthList", Value = "500" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightList", Value = "500" });

            foreach (var item in list)
            {
                if (!context.SystemSettings.Any(c => c.Name.Equals(item.Name)))
                {
                    context.SystemSettings.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion
    }
}