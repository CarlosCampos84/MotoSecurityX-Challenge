using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using CP4.MotoSecurityX.Application.UseCases.Motos;
using CP4.MotoSecurityX.Application.UseCases.Patios;
using CP4.MotoSecurityX.Application.UseCases.Usuarios;
using CP4.MotoSecurityX.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new() { Title = "MotoSecurityX.Api", Version = "v1" });
    o.EnableAnnotations();   // pacote Annotations
    o.ExampleFilters();      // pacote Filters
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>(); // registra exemplos

// Infra (EF + repos)
builder.Services.AddInfrastructure(builder.Configuration);

// Handlers Application
builder.Services.AddScoped<CreateMotoHandler>();
builder.Services.AddScoped<GetMotoByIdHandler>();
builder.Services.AddScoped<ListMotosHandler>();
builder.Services.AddScoped<MoveMotoToPatioHandler>();
builder.Services.AddScoped<UpdateMotoHandler>();
builder.Services.AddScoped<DeleteMotoHandler>();

builder.Services.AddScoped<CreatePatioHandler>();
builder.Services.AddScoped<GetPatioByIdHandler>();
builder.Services.AddScoped<ListPatiosHandler>();
builder.Services.AddScoped<UpdatePatioHandler>();
builder.Services.AddScoped<DeletePatioHandler>();

builder.Services.AddScoped<CreateUsuarioHandler>();
builder.Services.AddScoped<GetUsuarioByIdHandler>();
builder.Services.AddScoped<ListUsuariosHandler>();
builder.Services.AddScoped<UpdateUsuarioHandler>();
builder.Services.AddScoped<DeleteUsuarioHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();