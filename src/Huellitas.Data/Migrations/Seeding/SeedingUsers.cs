namespace Huellitas.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    public static class SeedingUsers
    {
        public static void Seed(HuellitasContext context)
        {
            var list = new List<User>();

            list.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "12a80b33b30a82e27e46634b44679a7946e0c6e9"/*123.123456*/, RoleId = 1, CreatedDate = DateTime.Now, Salt = "123" });
            list.Add(new Entities.User() { Name = "Publico", Email = "public@public.com", Password = "55d81fa21753c11353fcb3a4721a5d8ab59e5813"/*123456.public@public.com*/, RoleId = 2, CreatedDate = DateTime.Now, Salt = "123" });
            list.Add(new Entities.User() { Name = "Fundación", Email = "fundacion@fundacion.com", Password = "614c39951f7372fcad450958b43e6fe9edd34923"/*123456.fundacion@fundacion.com*/, RoleId = 2, CreatedDate = DateTime.Now, Salt = "123" });

            foreach (var item in list)
            {
                if (!context.Users.Any(c => c.Email.Equals(item.Email)))
                {
                    context.Users.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}