using System.Threading.Tasks;

namespace Common.Messaging.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
         Task HandleAsync(T @event);
    }
}