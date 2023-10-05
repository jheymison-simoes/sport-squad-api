using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.Extensions.Options;
using SportSquad.Business.Interfaces.Services;
using SportSquad.Business.Models;

namespace SportSquad.Business.Services;

public class EncryptService : BaseService, IEncryptService
{
    public EncryptService(
        IMapper mapper, 
        AppSettings appSettings, 
        ResourceManager resourceManager, 
        CultureInfo cultureInfo) : base(mapper, appSettings, resourceManager, cultureInfo)
    {
    }
    
    public string EncryptPassword(string password)
    {
        var md5 = System.Security.Cryptography.MD5.Create();
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
        var hash = md5.ComputeHash(inputBytes);
        var sb = new System.Text.StringBuilder();
        foreach (var t in hash)
            sb.Append(t.ToString("X2"));
        
        return sb.ToString(); 
    }
}