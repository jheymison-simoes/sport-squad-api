using MediatR;
using SportSquad.Core.Command;
using SportSquad.Core.Interfaces;

namespace SportSquad.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResponse<TResponse>> SendCommand<T, TResponse>(T command) where T : Command<TResponse>
    {
        var commandResponse = await _mediator.Send(command);
        return commandResponse;
    }

    public async Task PublishEvent<T>(T messageEvent) where T : Event.Event
    {
        await _mediator.Publish(messageEvent);
    }
}