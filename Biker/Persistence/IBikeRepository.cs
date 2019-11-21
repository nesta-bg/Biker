using System.Threading.Tasks;
using Biker.Models;

namespace Biker.Persistence
{
    public interface IBikeRepository
    {
        Task<Bike> GetBike(int id);
    }
}