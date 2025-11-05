using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pawsy.Application.Common.Interfaces;
using Pawsy.Application.Mapping;
using Pawsy.Application.Services.Implementation;
using Pawsy.Application.Services.Interface;
using Pawsy.Domain.Entities;
using Pawsy.Infrastructure;
using Pawsy.Infrastructure.Data;
using Pawsy.Infrastructure.Identity;
using Pawsy.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructure(builder.Configuration);

// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderHeaderService, OrderHeaderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PawsyShop API",
        Version = "v1",
        Description = "API de ejemplo para PawsyShop"
    });

    
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Run Identity Seeding
await IdentitySeeder.SeedAsync(app.Services);

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawsyShop API v1");
        
        c.RoutePrefix = "swagger"; 
    });
}
else
{
    // Si querés habilitarlo también en producción, mové UseSwagger + UseSwaggerUI fuera del if
}


app.Run();
