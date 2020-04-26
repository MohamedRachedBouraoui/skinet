using System.Linq;
using System;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Logging;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        private static DebugLoggerProvider debugLoggerProvider = new DebugLoggerProvider();

        LoggerFactory _myLoggerFactory = new LoggerFactory(new[] { debugLoggerProvider });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_myLoggerFactory);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ConvertDecimalToDoubleForSqlite(modelBuilder);
        }


        /// <summary>
        /// We are using this method because we have a problem when ordering by a decimal property in an Sqlite Db
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void ConvertDecimalToDoubleForSqlite(ModelBuilder modelBuilder)
        {
            if (Database.ProviderName.ToLower().EndsWith(".sqlite") == false)
            {
                return;
            }

            var entities = modelBuilder.Model.GetEntityTypes();

            foreach (var entityType in entities)
            {
                ConfigureSqliteForDecimals(modelBuilder, entityType);
                ConfigureSqliteForDateTimeOffset(modelBuilder, entityType);
            }
        }

        private void ConfigureSqliteForDateTimeOffset(ModelBuilder modelBuilder, IMutableEntityType entityType)
        {
            var dateTimeOffsetProperties = entityType.ClrType.GetProperties().Where(pr => pr.PropertyType == typeof(DateTimeOffset));
            foreach (var prop in dateTimeOffsetProperties)
            {
                modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
            }
        }

        private static void ConfigureSqliteForDecimals(ModelBuilder modelBuilder, IMutableEntityType entityType)
        {
            var decimalProperties = entityType.ClrType.GetProperties().Where(pr => pr.PropertyType == typeof(decimal));
            foreach (var prop in decimalProperties)
            {
                modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion<double>();
            }
        }
    }
}