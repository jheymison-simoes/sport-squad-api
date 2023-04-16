using FluentValidation.Results;

namespace SportSquad.Core.Interfaces;

public interface ICommandResponse
{
    public ValidationResult ValidationResult { get; set; }
}