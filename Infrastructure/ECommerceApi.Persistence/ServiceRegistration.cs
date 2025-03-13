
using ECommerceApi.Persistence.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ECommerceApi.Application.Repositories;
using ECommerceApi.Persistence.Repositories;

namespace ECommerceApi.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            // Add services here
            //services.AddSingleton<IProductService,ProductService>();
            services.AddDbContext<ECommerceAPIDbContext>(options =>
            {
                options.UseNpgsql(Configuration.ConnectionString);
            });
            services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository,OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository,OrderWriteRepository>();
            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();
            
        }
    }
}
