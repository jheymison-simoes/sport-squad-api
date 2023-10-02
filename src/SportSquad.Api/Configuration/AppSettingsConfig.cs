using System.Reflection;
using SportSquad.Business.Models;

namespace SportSquad.Api.Configuration;

public static class AppSettingsConfig
{
    public static void AddAppSettingsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = new AppSettings();
        var properties = typeof(AppSettings).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            var configValue = configuration[property.Name];

            if (configValue == null) continue;
            var convertedValue = Convert.ChangeType(configValue, property.PropertyType);
            property.SetValue(appSettings, convertedValue);
        }

        services.AddSingleton(appSettings);
    }
}