﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.RequestParameters
{
    public record Pagination
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
