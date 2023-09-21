using Infrastructure.Provider.Checkout;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Application.DependencyInjection;

/// <summary>
/// ApiModules
/// </summary>
public static class ApiModules
{
    /// <summary>
    /// Setup Inversion of Control
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection SetupIoC(this IServiceCollection services)
    {
        services.AddTransient<ICheckoutProvider, CheckoutProvider>();

        return services;
    }
}