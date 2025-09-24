using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using CP4.MotoSecurityX.Application.UseCases.Motos;
using CP4.MotoSecurityX.Application.UseCases.Patios;
using CP4.MotoSecurityX.Application.UseCases.Usuarios;
using CP4.MotoSecurityX.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// MVC Controllers + Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger/OpenAPI
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MotoSecurityX.Api",
        Version = "v1",
        Description = "API para controle de motos, pátios e usuários (Clean Architecture + DDD)"
    });

    // Swashbuckle extras
    o.EnableAnnotations();  // [SwaggerOperation], etc.
    o.ExampleFilters();     // IExamplesProvider<T>
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

// Infrastructure (EF Core + Repositórios)
builder.Services.AddInfrastructure(builder.Configuration);

// Application Handlers
// Motos
builder.Services.AddScoped<CreateMotoHandler>();
builder.Services.AddScoped<GetMotoByIdHandler>();
builder.Services.AddScoped<ListMotosHandler>();
builder.Services.AddScoped<MoveMotoToPatioHandler>();
builder.Services.AddScoped<UpdateMotoHandler>();
builder.Services.AddScoped<DeleteMotoHandler>();

// Pátios
builder.Services.AddScoped<CreatePatioHandler>();
builder.Services.AddScoped<GetPatioByIdHandler>();
builder.Services.AddScoped<ListPatiosHandler>();
builder.Services.AddScoped<UpdatePatioHandler>();
builder.Services.AddScoped<DeletePatioHandler>();

// Usuários
builder.Services.AddScoped<CreateUsuarioHandler>();
builder.Services.AddScoped<GetUsuarioByIdHandler>();
builder.Services.AddScoped<ListUsuariosHandler>();
builder.Services.AddScoped<UpdateUsuarioHandler>();
builder.Services.AddScoped<DeleteUsuarioHandler>();

var app = builder.Build();

// Swagger somente em Development (pode habilitar em Prod se quiser)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware para mapear violação de UNIQUE -> 409 Conflict (opcional, mas recomendado)
app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("UNIQUE") == true)
    {
        ctx.Response.StatusCode = StatusCodes.Status409Conflict;
        await ctx.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "Conflito de dados",
            Detail = "Já existe um registro com esse valor único.",
            Status = StatusCodes.Status409Conflict
        });
    }
});

app.MapControllers();

// Redirect raiz -> Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
