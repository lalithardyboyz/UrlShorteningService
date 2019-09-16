using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace UrlShorteningWithLibrary.Data
{
    public class ShortnerDbContext: DbContext
    {
        public ShortnerDbContext(DbContextOptions<ShortnerDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Configuring the connection string from app settings.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer("Data Source=localhost\\sqlexpress; Initial Catalog=NintexDB; Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Gets or sets the data from DB.
        /// </summary>
        public DbSet<UrlShorteningDetails> UrlShorteningDetails { get; set; }

        /// <summary>
        /// Binding the DB Table to entity.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<UrlShorteningDetails>(builder => {
                builder.ToTable("UrlShorteningDetails");
                builder.Property(p => p.Id).HasColumnName("Id").HasColumnType("bigint");
                builder.Property(p => p.LongUrl).HasColumnName("LongUrl").HasColumnType("varchar(max)");
                builder.Property(p => p.DateCreated).HasColumnName("DateCreated").HasColumnType("smalldatetime");
                builder.Property(p => p.DateExpiry).HasColumnName("DateExpiry").HasColumnType("smalldatetime");
                builder.Property(p => p.Active).HasColumnName("Active").HasColumnType("char(1)");
                builder.HasIndex(p => new { p.Id }).IsUnique();
                builder.HasKey(p => p.Id);
                });
        }
    }
}

