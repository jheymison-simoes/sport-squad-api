using FluentValidation.Results;
using SportSquad.Core.Interfaces;

namespace SportSquad.Core.Command;

public class CommandResponse<TResponse> : ICommandResponse
{
    public ValidationResult ValidationResult { get; set; }

    public TResponse Response { get; set; } = default!;
}