﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
               var errors =  context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .ToDictionary(k => k.Key, v => v.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();
                
                context.Result = new BadRequestObjectResult(errors);
                return;
            
            }
            await next();
        }
    }
}
