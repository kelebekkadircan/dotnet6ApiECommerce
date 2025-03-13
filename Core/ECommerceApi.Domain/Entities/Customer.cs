using ECommerceApi.Domain.Entities.Common;

namespace ECommerceApi.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = default!;



        ICollection<Order>? Orders { get; set; }
    }
}
