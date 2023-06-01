using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;
using SportSquad.Business.Models;

namespace SportSquad.Api.Configuration;

public static class KissLogConfiguration
{
    public static void AddKissLogConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IKLogger>((_) => Logger.Factory.Get());
        
        services.AddLogging(logging =>
        {
            logging.AddKissLog(options =>
            {
                options.Formatter = args =>
                {
                    if (args.Exception == null)
                        return args.DefaultValue;
 
                    var exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                    return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
                };
            });
        });
    }
    
    public static void UseKissLogConfiguration(this IApplicationBuilder app, IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection("AppSettings");
        var appSettings = appSettingsSection.Get<AppSettings>();
        app.UseKissLogMiddleware((_) => ConfigureKissLog(appSettings));
    }
    
    private static void ConfigureKissLog(AppSettings appSettings)
    {
        var kissLogAplication = new Application(appSettings.KissLogOrganizationId, appSettings.KissLogApplicationId);
        
        var requestLogs = new RequestLogsApiListener(kissLogAplication)
        {
            ApiUrl = appSettings.KissLogApiUrl
        };
        
        KissLog.KissLogConfiguration.Listeners.Add(requestLogs);
    }
}