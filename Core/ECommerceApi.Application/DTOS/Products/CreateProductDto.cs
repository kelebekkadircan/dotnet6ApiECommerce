using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.DTOS.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; } = default!;

        public int Stock { get; set; } = default!;

        public float Price { get; set; } = default!;
    }
}
