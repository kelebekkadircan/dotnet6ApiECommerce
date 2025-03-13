using ECommerceApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);
        bool Update(T model);
        bool Remove(T model);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> models);

        Task<int> SaveAsync();

    }
}
