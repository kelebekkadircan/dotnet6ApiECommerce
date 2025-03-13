using ECommerceApi.Domain.Entities;
using ECommerceApi.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Persistence.Contexts
{
    public class ECommerceAPIDbContext : DbContext
    {
        /// <summary>
        /// ECommerceAPIDbContext, Entity Framework Core kullanarak veritabanı işlemlerini yöneten DbContext sınıfıdır.
        /// Bu sınıf, dışarıdan gelen konfigürasyon ayarlarını (connection string vb.) kullanarak veritabanı bağlantısını yapılandırır.
        /// </summary>
        public ECommerceAPIDbContext(DbContextOptions<ECommerceAPIDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker => entityler üzerinden değişiklikleri takip etmek için kullanılır.
            ChangeTracker.Entries<BaseEntity>().ToList().ForEach(entry =>
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                        break;
                    
                }
            });


            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
