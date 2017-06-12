namespace Huellitas.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    public static class SeedingFiles
    {
        public static void Seed(HuellitasContext context)
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
    }
}