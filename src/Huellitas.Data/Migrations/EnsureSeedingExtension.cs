using Huellitas.Data.Core;
using Huellitas.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Migrations
{
    public static class EnsureSeedingExtension
    {
        public static void EnsureSeeding(this HuellitasContext context)
        {
            if (context.AllMigrationsApplied())
            {
                SeedingContents(context);
            }
        }

        #region Contents
        private static void SeedingContents(HuellitasContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedCustomTables(context);
            SeedCustomTablesRows(context);
            SeedContents(context);
        }



        #region Roles
        private static void SeedRoles(HuellitasContext context)
        {
            var list = new List<Role>();

            list.Add(new Entities.Role() { Name = "SuperAdmin", Description = "Usuario con todos los privilegios" });

            foreach (var item in list)
            {
                if (!context.Roles.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Roles.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion
        #region Users
        private static void SeedUsers(HuellitasContext context)
        {
            var list = new List<User>();

            list.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "202cb962ac59075b964b07152d234b70"/*123*/, RoleId = 1, CreatedDate = DateTime.Now });

            foreach (var item in list)
            {
                if (!context.Users.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Users.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion
        #region CustomTables
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
        #endregion
        #region CustomTablesRows
        private static void SeedCustomTablesRows(HuellitasContext context)
        {
            var list = new List<CustomTableRow>();

            list.Add(new CustomTableRow() { CustomTableId = 1, Value = "Perro" }); //1
            list.Add(new CustomTableRow() { CustomTableId = 1, Value = "Gato" });//2
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Miniatura" });//3
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Pequeño" });//4
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Mediano-Pequeño" });//5
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Mediano-Grande" });//6
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Grande" });//7
            list.Add(new CustomTableRow() { CustomTableId = 3, Value = "Macho" });//8
            list.Add(new CustomTableRow() { CustomTableId = 3, Value = "Hembra" });//9

            foreach (var item in list)
            {
                if (!context.CustomTableRows.Any(c => c.Value.Equals(item.Value) && c.CustomTableId.Equals(item.CustomTableId)))
                {
                    context.CustomTableRows.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion


        #region Contents
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
                ContentAttributes = new List<ContentAttribute>() {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "5" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "3" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "1" }
                    }
            });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Pet Dos", Body = "Cuerpo de contenido de prueba Pet 2", Type = Entities.ContentType.Pet, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1,
                ContentAttributes = new List<ContentAttribute>() {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "1" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "5" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "2" }
                    }
            });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Pet Tres", Body = "Cuerpo de contenido de prueba Pet 3", Type = Entities.ContentType.Pet, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1,
                ContentAttributes = new List<ContentAttribute>() {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "1" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "3" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "2" }
                    }
            });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Uno", Body = "Cuerpo de contenido de prueba Shelter 1", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Dos", Body = "Cuerpo de contenido de prueba Shelter 2", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });


            foreach (var item in list)
            {
                if (!context.Contents.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Contents.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion




        #endregion
    }
}
