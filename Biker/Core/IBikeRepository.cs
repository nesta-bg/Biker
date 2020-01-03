using Biker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biker.Core
{
    public interface IBikeRepository
    {
        Task<Bike> GetBike(int id, bool includeRelated = true);

        void Add(Bike bike);

        void Remove(Bike bike);

        Task<QueryResult<Bike>> GetBikes(BikeQuery queryObj);

        Task<IEnumerable<PieChart>> GetBikesGroupedByMake();
    }
}