using System.Collections.Generic;
using System.Threading.Tasks;
using Biker.Core.Models;

namespace Biker.Core
{
    public interface IBikeRepository
    {
        Task<Bike> GetBike(int id, bool includeRelated = true);

        void Add(Bike bike);

        void Remove(Bike bike);

        Task<IEnumerable<Bike>> GetBikes(BikeQuery queryObj);
    }
}