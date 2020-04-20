//-----------------------------------------------------------------------
// <copyright file="SeedingUsers.cs" company="Gabriel Castillo">
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
    /// seeding users
    /// </summary>
    public static class SeedingUsers
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HuellitasContext context)
        {
            var list = new List<User>();

            list.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "619651c6cb5dc394373a5bb75fea9d4314f3564b"/*123.123456*/, RoleId = 1, CreatedDate = DateTime.Now, Salt = "123" });
            list.Add(new Entities.User() { Name = "Publico", Email = "public@public.com", Password = "619651c6cb5dc394373a5bb75fea9d4314f3564b"/*123456.public@public.com*/, RoleId = 2, CreatedDate = DateTime.Now, Salt = "123" });
            list.Add(new Entities.User() { Name = "Fundación", Email = "fundacion@fundacion.com", Password = "619651c6cb5dc394373a5bb75fea9d4314f3564b"/*123456.fundacion@fundacion.com*/, RoleId = 2, CreatedDate = DateTime.Now, Salt = "123" });

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