using CP4.MotoSecurityX.Domain.Repositories;
using CP4.MotoSecurityX.Infrastructure.Data;        // sua pasta atual
using CP4.MotoSecurityX.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP4.MotoSecurityX.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var cs = config.GetConnectionString("Default") ?? "Data Source=motosecurityx.db";

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlite(cs);
        });

        services.AddScoped<IMotoRepository, EfMotoRepository>();
        services.AddScoped<IPatioRepository, EfPatioRepository>();

        return services;
    }
}