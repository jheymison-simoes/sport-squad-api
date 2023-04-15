using System.Globalization;
using System.Resources;
using FluentValidation;

namespace SportSquad.Domain.Validate;

public abstract class BaseDomainAbstractValidator<TValidator> : AbstractValidator<TValidator> where TValidator : class
{
    protected readonly ResourceSet ResourceSet;
    
    protected BaseDomainAbstractValidator(ResourceManager resourceManager, CultureInfo cultureInfo)
    {
        ResourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);
    }

    public async Task<(bool error, string messageError)> ValidateModel(TValidator validateModel)
    {
        var validator = await ValidateAsync(validateModel);
        return validator.IsValid ? (false, null) : (true, validator.ToString(" "));
    }

    protected string GetMessageResource(string name, params object[] parameters)
    {
        var resourceMessage = ResourceSet.GetString(name);
        return parameters.Any() ? ResourceFormat(resourceMessage, parameters) : resourceMessage;
    }

    #region Private Methods
    private static string ResourceFormat(string message, params object[] args)
    {
        return string.Format(message, args);
    }
    #endregion
    
}