using FluentValidation.Results;
using SportSquad.Core.Interfaces;

namespace SportSquad.Core.Command;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected void AddValidationResult(ValidationResult validationResult)
    {
        ValidationResult = validationResult;
    }

    protected bool ValidOperation()
    {
        return !ValidationResult.Errors.Any();
    }

    protected CommandResponse<TResponse> ReturnReply<TResponse>(TResponse response = default!)
    {
        return ValidOperation()
            ? new CommandResponse<TResponse>() { Response = response }
            : new CommandResponse<TResponse>() { ValidationResult = ValidationResult };
    }
    
    protected async Task SaveData(IUnitOfWork uow)
    {
        if (!await uow.Commit()) AddError("Houve um erro ao salvar os dados");
    }
}