using Biker.Core;
using Biker.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Bike> GetBike(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Bikes.FindAsync(id);

            return await context.Bikes
               .Include(b => b.Features)
                   .ThenInclude(bf => bf.Feature)
               .Include(b => b.Model)
                   .ThenInclude(m => m.Make)
               .SingleOrDefaultAsync(b => b.Id == id);
        }

        //public async Task<Bike> GetBikeWithMake(int id)
        //{
        //}

        public void Add(Bike bike)
        {
            context.Bikes.Add(bike);
        }

        public void Remove(Bike bike)
        {
            context.Remove(bike);
        }

        public async Task<IEnumerable<Bike>> GetBikes(Filter filter)
        {
            var query = context.Bikes
              .Include(b => b.Model)
                .ThenInclude(m => m.Make)
              .Include(b => b.Features)
                .ThenInclude(bf => bf.Feature)
              .AsQueryable();

            if (filter.MakeId.HasValue)
                query = query.Where(b => b.Model.MakeId == filter.MakeId.Value);

            //if (filter.ModelId.HasValue)
            //    query = query.Where(b => b.ModelId == filter.ModelId.Value);

            return await query.ToListAsync();
        }
    }
}
