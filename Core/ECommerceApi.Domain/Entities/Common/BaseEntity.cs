using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedDate { get; set; } 
        public DateTimeOffset? UpdatedDate { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}
