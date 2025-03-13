using ECommerceApi.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ECommerceApi.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        // sorgu oluşturmak için querayble kullanılır
        IQueryable<T> GetAll(bool tracking = true);
               

        IQueryable<T> GetWhere(Expression<Func<T, bool>> method , bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> method , bool tracking = true);

        Task<T> GetByIdAsync(string id , bool tracking = true);

        Task<T> GetByIdAsync(Guid id , bool tracking = true);

    }
}
