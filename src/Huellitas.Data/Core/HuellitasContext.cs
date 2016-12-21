//-----------------------------------------------------------------------
// <copyright file="HuellitasContext.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Core
{
    using Huellitas.Data.Entities;
    using Huellitas.Data.Entities.Mapping;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Context of <![CDATA[Huellitas]]>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class HuellitasContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HuellitasContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public HuellitasContext(DbContextOptions<HuellitasContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the adoption form answers.
        /// </summary>
        /// <value>
        /// The adoption form answers.
        /// </value>
        public virtual DbSet<AdoptionFormAnswer> AdoptionFormAnswers { get; set; }

        /// <summary>
        /// Gets or sets the adoption form attributes.
        /// </summary>
        /// <value>
        /// The adoption form attributes.
        /// </value>
        public virtual DbSet<AdoptionFormAttribute> AdoptionFormAttributes { get; set; }

        /// <summary>
        /// Gets or sets the adoption forms.
        /// </summary>
        /// <value>
        /// The adoption forms.
        /// </value>
        public virtual DbSet<AdoptionForm> AdoptionForms { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the content attributes.
        /// </summary>
        /// <value>
        /// The content attributes.
        /// </value>
        public virtual DbSet<ContentAttribute> ContentAttributes { get; set; }

        /// <summary>
        /// Gets or sets the content categories.
        /// </summary>
        /// <value>
        /// The content categories.
        /// </value>
        public virtual DbSet<ContentCategory> ContentCategories { get; set; }

        /// <summary>
        /// Gets or sets the content files.
        /// </summary>
        /// <value>
        /// The content files.
        /// </value>
        public virtual DbSet<ContentFile> ContentFiles { get; set; }

        /// <summary>
        /// Gets or sets the contents.
        /// </summary>
        /// <value>
        /// The contents.
        /// </value>
        public virtual DbSet<Content> Contents { get; set; }

        /// <summary>
        /// Gets or sets the custom table rows.
        /// </summary>
        /// <value>
        /// The custom table rows.
        /// </value>
        public virtual DbSet<CustomTableRow> CustomTableRows { get; set; }

        /// <summary>
        /// Gets or sets the custom tables.
        /// </summary>
        /// <value>
        /// The custom tables.
        /// </value>
        public virtual DbSet<CustomTable> CustomTables { get; set; }

        /// <summary>
        /// Gets or sets the email notifications.
        /// </summary>
        /// <value>
        /// The email notifications.
        /// </value>
        public virtual DbSet<EmailNotification> EmailNotifications { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        public virtual DbSet<File> Files { get; set; }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>
        /// The locations.
        /// </value>
        public virtual DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public virtual DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public virtual DbSet<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the related contents.
        /// </summary>
        /// <value>
        /// The related contents.
        /// </value>
        public virtual DbSet<RelatedContent> RelatedContents { get; set; }

        /// <summary>
        /// Gets or sets the role <c>pemissions</c>.
        /// </summary>
        /// <value>
        /// The role <c>pemissions</c>.
        /// </value>
        public virtual DbSet<RolePemission> RolePemissions { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public virtual DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the system settings.
        /// </summary>
        /// <value>
        /// The system settings.
        /// </value>
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
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
            modelBuilder.Entity<Log>().Map();
            modelBuilder.Entity<ContentUser>().Map();
        }
    }
}