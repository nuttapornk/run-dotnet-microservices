using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Services;

namespace Ordering.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Set database Connection string
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Ordering")));

        // Interface IApplicationDbContext DbContext for Datebase connection
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.Configure<EmailSetting>(c => configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
