using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure.Application.Swagger;

/// <summary>
/// 
/// </summary>
public static class SwaggerDocument
{
    /// <summary>
    /// Add Swagger documentation / Authorization
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddSwaggerDocumentService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = configuration["swaggerConfig:ProjectVersion"],
                Title = configuration["swaggerConfig:ProjectName"],
                Description = configuration["swaggerConfig:ProjectDescription"],
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"authorization: Bearer {token}\"",
            }); ;
        });

        return services;
    }
}
