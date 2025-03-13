using ECommerceApi.Application.Repositories;
using ECommerceApi.Domain.Entities.Common;
using ECommerceApi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceApi.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {

        private readonly ECommerceAPIDbContext _context;

        public WriteRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table =>  _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {

            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;

        }

        public  bool RemoveRange(List<T> models)
        {
            
                Table.RemoveRange(models);
                 return true;

            
        }
        public async Task<bool> RemoveAsync(string id)
        {
          T model =  await Table.FirstOrDefaultAsync(d => d.Id == Guid.Parse(id));

            if (model == null)
                return false;
            
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
         =>await _context.SaveChangesAsync();


        
    }
}
