using ECommerceApi.Application.DTOS.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApi.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator() 
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Name is required")
                .MaximumLength(100)
                   .WithMessage("Name must not exceed 100 characters")
                .MinimumLength(5)
                   .WithMessage("Name must be at least 5 characters");
            
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Stock is required")
                .Must(s => s >= 0)
                   .WithMessage("Stock must be greater than 0");
                
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                   .WithMessage("Price is required")
                .Must(p => p >= 0)
                   .WithMessage("Price must be greater than 0");


        
        }
    }
}
