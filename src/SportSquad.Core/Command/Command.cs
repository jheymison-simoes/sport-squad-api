using MediatR;

namespace SportSquad.Core.Command;

public abstract class Command<TResponse> : Message.Message, IRequest<CommandResponse<TResponse>>
{
    public DateTime Timestamp { get; private set; }
    public CommandResponse<TResponse> CommandResponse { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }
}