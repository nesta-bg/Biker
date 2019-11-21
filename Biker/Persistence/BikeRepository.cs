using Biker.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Biker.Persistence
{
    public class BikeRepository : IBikeRepository
    {
        private readonly BikerDbContext context;

        public BikeRepository(BikerDbContext context)
        {
            this.context = context;
        }

        public async Task<Bike> GetBike(int id)
        {
            return await context.Bikes
               .Include(b => b.Features)
                   .ThenInclude(bf => bf.Feature)
               .Include(b => b.Model)
                   .ThenInclude(m => m.Make)
               .SingleOrDefaultAsync(b => b.Id == id);
        }
    }
}
