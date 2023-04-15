using System.Resources;

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
}