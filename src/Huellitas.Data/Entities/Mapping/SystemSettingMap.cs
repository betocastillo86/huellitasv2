﻿//-----------------------------------------------------------------------
// <copyright file="SystemSettingMap.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// System Setting Mapping
    /// </summary>
    public static class SystemSettingMap
    {
        /// <summary>
        /// Maps the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static void Map(this EntityTypeBuilder<SystemSetting> entity)
        {
            entity.ToTable("SystemSettings");

            entity.HasKey(c => c.Id)
                .HasName("PK_SystemSetting");

            entity.HasIndex(e => e.Name)
                   .HasDatabaseName("IX_SystemSetting")
                   .IsUnique();

            entity.Property(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");

            entity.Property(e => e.Value).IsRequired();
        }
    }
}