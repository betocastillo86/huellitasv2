using Huellitas.Data.Core;
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
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Entities.Role() { Name = "SuperAdmin", Description = "Usuario con todos los privilegios" });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "202cb962ac59075b964b07152d234b70"/*123*/, RoleId = 1, CreatedDate = DateTime.Now });
                context.SaveChanges();
            }

            if (!context.Contents.Any())
            {
                context.Contents.Add(new Entities.Content() { Name = "Contenido de prueba Pet 1", Body = "Cuerpo de contenido de prueba Pet 1", Type = Entities.ContentType.Pet, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });
                context.Contents.Add(new Entities.Content() { Name = "Contenido de prueba Pet 2", Body = "Cuerpo de contenido de prueba Pet 2", Type = Entities.ContentType.Pet, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });
                context.Contents.Add(new Entities.Content() { Name = "Contenido de prueba Pet 3", Body = "Cuerpo de contenido de prueba Pet 3", Type = Entities.ContentType.Pet, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });
                context.Contents.Add(new Entities.Content() { Name = "Contenido de prueba Shelter 1", Body = "Cuerpo de contenido de prueba Shelter 1", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });
                context.Contents.Add(new Entities.Content() { Name = "Contenido de prueba Shelter 2", Body = "Cuerpo de contenido de prueba Shelter 2", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1 });
                context.SaveChanges();
            }
        }
        #endregion
    }
}
