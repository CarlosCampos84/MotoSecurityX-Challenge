using CP4.MotoSecurityX.Application.UseCases.Motos;
using CP4.MotoSecurityX.Application.UseCases.Patios;
using CP4.MotoSecurityX.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infra (EF + repos)
builder.Services.AddInfrastructure(builder.Configuration);

// Handlers Application
builder.Services.AddScoped<CreateMotoHandler>();
builder.Services.AddScoped<GetMotoByIdHandler>();
builder.Services.AddScoped<ListMotosHandler>();
builder.Services.AddScoped<MoveMotoToPatioHandler>();

builder.Services.AddScoped<CreatePatioHandler>();
builder.Services.AddScoped<GetPatioByIdHandler>();
builder.Services.AddScoped<ListPatiosHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();