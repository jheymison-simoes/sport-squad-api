using SportSquad.Core.Command;

namespace SportSquad.Core.Interfaces;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T messageEvent) where T : Event.Event;

    Task<CommandResponse<TResponse>> SendCommand<T, TResponse>(T command) where T : Command<TResponse>;
}