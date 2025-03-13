using ECommerceApi.Domain.Entities.Common;

namespace ECommerceApi.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string  ? Description { get; set; }

        public string Address { get; set; } =default!;

        public ICollection<Product> ? Products { get; set; }


        public Guid CustomerId { get; set; } // Foreign Key
        public Customer  ? Customer { get; set; }
    }
}
