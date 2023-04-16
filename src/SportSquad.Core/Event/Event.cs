using MediatR;

namespace SportSquad.Core.Event;

public abstract class Event : Message.Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event() => Timestamp = DateTime.Now;
}