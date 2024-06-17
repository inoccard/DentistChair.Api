using Microsoft.EntityFrameworkCore;
using Sds.DentistChair.Data;

namespace Sds.DentistChair.Api.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(
            builder => builder.UseMySql(configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 30))));
    }

    public static void ApplyMigrations(this WebApplication app)
    {
        var services = app.Services.CreateScope().ServiceProvider;
        var dataContext = services.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}
