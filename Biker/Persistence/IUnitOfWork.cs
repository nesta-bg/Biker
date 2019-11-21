using System.Threading.Tasks;

namespace Biker.Persistence
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
