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

        public virtual DbSet<AdoptionForm> AdoptionForms { get; set; }
        public virtual DbSet<AdoptionFormAnswer> AdoptionFormAnswers { get; set; }
        public virtual DbSet<AdoptionFormAttribute> AdoptionFormAttributes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentAttribute> ContentAttributes { get; set; }
        public virtual DbSet<ContentCategory> ContentCategories { get; set; }
        public virtual DbSet<ContentFile> ContentFiles { get; set; }
        public virtual DbSet<CustomTable> CustomTables { get; set; }
        public virtual DbSet<CustomTableRow> CustomTableRows { get; set; }
        public virtual DbSet<EmailNotification> EmailNotifications { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RelatedContent> RelatedContents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePemission> RolePemissions { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
    
}