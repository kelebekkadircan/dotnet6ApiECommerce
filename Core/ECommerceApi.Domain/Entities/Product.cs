using ECommerceApi.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; } = default!;

        public int Stock { get; set; } = default!;

        public float Price { get; set; } = default!;

        public ICollection<Order>? Orders { get; set; }

    }
}
