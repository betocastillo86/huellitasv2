namespace Huellitas.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    public static class SeedingCustomTables
    {
        public static void Seed(HuellitasContext context)
        {
            var list = new List<CustomTable>();

            list.Add(new CustomTable() { Id = 1, Name = "Subtipo Animales", Description = "Subtipo de contenido para animales" });
            list.Add(new CustomTable() { Id = 2, Name = "Tamaño", Description = "Tamaño de los animales existentes" });
            list.Add(new CustomTable() { Id = 3, Name = "Genero de los animales", Description = "Tamaño de los animales existentes" });
            list.Add(new CustomTable() { Id = 4, Name = "Preguntas Formularios", Description = "Preguntas hechas en los formularios de adopción" });
            list.Add(new CustomTable() { Id = Convert.ToInt32(CustomTableType.Jobs), Name = "Trabajos para formularios", Description = "Trabajos para formularios" });
            list.Add(new CustomTable() { Id = Convert.ToInt32(CustomTableType.Breed), Name = "Razas", Description = "Razas de animales" });

            foreach (var item in list)
            {
                if (!context.CustomTables.Any(c => c.Id.Equals(item.Id)))
                {
                    context.CustomTables.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}