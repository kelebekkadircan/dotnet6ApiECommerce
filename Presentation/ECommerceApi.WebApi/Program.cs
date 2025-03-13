using ECommerceApi.Application.Validators.Products;
using ECommerceApi.Infrastructure;
using ECommerceApi.Infrastructure.Filters;
using ECommerceApi.Persistence;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddCors(
    opt =>
    {
        opt.AddDefaultPolicy(
            policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                // .WithOrigins("http://localhost:3000"); kullanýlmasý daha mantýklý
            }
        );
    }
    );

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CreateProductValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
