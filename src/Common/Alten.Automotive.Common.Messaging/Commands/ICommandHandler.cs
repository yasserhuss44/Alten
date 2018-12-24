using System.Threading.Tasks;

namespace Common.Messaging.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
         Task HandleAsync(T command);
    }
}