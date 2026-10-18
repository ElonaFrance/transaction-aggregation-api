using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TransactionAggregationApi.Application.Repositories;
using TransactionAggregationApi.Application.Repositories.Contracts;
using TransactionAggregationApi.Application.Services;
using TransactionAggregationApi.Application.Services.Contracts;

namespace TransactionAggregationApi.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<DapperService>();
        services.AddSingleton<KafkaProducerService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IAggregationRepository, AggregationRepository>();
        services.AddScoped<ITransactionQueryService, TransactionQueryService>();
        services.AddScoped<IAggregationQueryService, AggregationQueryService>();
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtKey = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key not configured");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        return services;
    }

    public static IServiceCollection AddFluentValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
        // Runs the validators against action parameters; [ApiController] then returns 400 on failure.
        services.AddFluentValidationAutoValidation();
        return services;
    }
}
