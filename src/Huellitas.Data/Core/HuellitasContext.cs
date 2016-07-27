using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Huellitas.Data.Entities;
using Huellitas.Data.Entities.Mapping;

namespace Huellitas.Data.Core
{
    public partial class HuellitasContext : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //    optionsBuilder.UseSqlServer(@"Server=ASUS_CASA\SQL_ASUS;Database=HuellitasV2;Trusted_Connection=True;");
        //}

        public HuellitasContext(DbContextOptions<HuellitasContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdoptionForm>().Map();
            modelBuilder.Entity<AdoptionFormAnswer>().Map();
            modelBuilder.Entity<AdoptionFormAttribute>().Map();
            modelBuilder.Entity<Category>().Map();
            modelBuilder.Entity<Content>().Map();
            modelBuilder.Entity<ContentAttribute>().Map();
            modelBuilder.Entity<ContentCategory>().Map();
            modelBuilder.Entity<ContentFile>().Map();
            modelBuilder.Entity<CustomTable>().Map();
            modelBuilder.Entity<CustomTableRow>().Map();
            modelBuilder.Entity<EmailNotification>().Map();
            modelBuilder.Entity<File>().Map();
            modelBuilder.Entity<Location>().Map();
            modelBuilder.Entity<Permission>().Map();
            modelBuilder.Entity<RelatedContent>().Map();
            modelBuilder.Entity<Role>().Map();
            modelBuilder.Entity<RolePemission>().Map();
            modelBuilder.Entity<SystemSetting>().Map();
            modelBuilder.Entity<User>().Map();
        }

        public virtual DbSet<AdoptionForm> AdoptionForm { get; set; }
        public virtual DbSet<AdoptionFormAnswer> AdoptionFormAnswer { get; set; }
        public virtual DbSet<AdoptionFormAttribute> AdoptionFormAttribute { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<ContentAttribute> ContentAttribute { get; set; }
        public virtual DbSet<ContentCategory> ContentCategory { get; set; }
        public virtual DbSet<ContentFile> ContentFile { get; set; }
        public virtual DbSet<CustomTable> CustomTable { get; set; }
        public virtual DbSet<CustomTableRow> CustomTableRow { get; set; }
        public virtual DbSet<EmailNotification> EmailNotification { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<RelatedContent> RelatedContent { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RolePemission> RolePemission { get; set; }
        public virtual DbSet<SystemSetting> SystemSetting { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}