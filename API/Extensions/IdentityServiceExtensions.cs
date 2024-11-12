using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
    /// <summary>
    /// Adiciona os servicos de autenticacao e autorizacao do asp.net core
    /// </summary>
    /// <param name="services">Colecao de servicos</param>
    /// <param name="config">Instancia de configuracao do asp.net core</param>
    /// <returns> Colecao de servicos atualizada</returns>
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        
    {

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var tokenKey = config["TokenKey"] ??
            throw new InvalidOperationException("Nao foi possivel acessar a chave de token em appsettings.json");
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;

    }
}

