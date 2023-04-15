using System.Globalization;
using System.Resources;
using SportSquad.Business.Utils;

namespace SportSquad.Business.Tests.Utils;

public static class ResourceUtils
{
    public static string GetMessageResource(this ResourceManager resourceManager, string name, string culture = "pt-BR", params object[] parameters)
    {
        return parameters.Length > 0
            ? resourceManager.GetString(name, CultureInfo.GetCultureInfo(culture))!.ResourceFormat(parameters)
            : resourceManager.GetString(name);
    }
}