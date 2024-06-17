using Sds.DentistChair.Data;
using Sds.DentistChair.Domain.Models.ChairAggregate.Services;
using Sds.DentistChair.Domain.Notifier;
using Sds.DentistChair.Domain.Repository;

namespace Sds.DentistChair.Api.Configurations;

public static class RegisterAppServicesConfig
{
    public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Data

        services.AddScoped<IRepository, DataContext>();

        #endregion Data

        #region Services

        services.AddScoped<IChairService, ChairService>();

        #endregion Services

        #region Domain.Notifier

        services.AddScoped<INotifierMessage, NotifierMessage>();

        #endregion Domain.Notifier
    }
}
