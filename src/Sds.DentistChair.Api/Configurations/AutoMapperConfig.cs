using System.Reflection;

namespace Sds.DentistChair.Api.Configurations;

public static class AutoMapperConfig
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
