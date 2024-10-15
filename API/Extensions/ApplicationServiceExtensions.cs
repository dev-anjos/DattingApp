using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
        {
            // informando que vamos usar o sqlite | getconnectionstring = pegando a string de conexão
            //DefaultConnection = nome da string de conexaão
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });


        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
