using System.Threading.Tasks;

namespace Biker.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
