using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;

namespace SportSquad.Business.Utils;

public static class StringUtils
{
    public static string GetResourceFormat(this ResourceSet resourceSet, string getString, params object[] args)
    {
        return args.Length == 0 
            ? resourceSet.GetString(getString)! 
            : string.Format(resourceSet.GetString(getString)!, args);
    }
    
    public static string ResourceFormat(this string message, params object[] args)
    {
        return string.Format(message, args);
    }

    public static string GetFirstName(this string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return name;
        
        var nameFormatted = Regex.Replace(name, @"\s+", " "); // Remove espaços extras
        nameFormatted = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nameFormatted.ToLower()); // Capitaliza a primeira letra de cada palavra
        
        var firstName = nameFormatted.Split(' ').FirstOrDefault() ?? "";
        return firstName;
    }
}