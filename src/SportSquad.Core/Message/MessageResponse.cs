using FluentValidation.Results;

namespace SportSquad.Core.Message;

public interface IMessageResponse
{
    public ValidationResult ValidationResult { get; set; }
}

public class MessageResponse<TResponse>: Message, IMessageResponse
{
    public ValidationResult ValidationResult { get; set; }

    public TResponse Response { get; set; } = default!;
}
