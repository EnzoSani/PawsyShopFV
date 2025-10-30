using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Pawsy.Application.Common.Interfaces;
using Pawsy.Application.Mapping;
using Pawsy.Infrastructure.Data;
using Pawsy.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PawsyShop API v1");
        //c.RoutePrefix = string.Empty; // <-- hace que Swagger UI esté en '/'
        c.RoutePrefix = "swagger"; // <-- si preferís en /swagger
    });
}
else
{
    // Si querés habilitarlo también en producción, mové UseSwagger + UseSwaggerUI fuera del if
}


app.Run();
