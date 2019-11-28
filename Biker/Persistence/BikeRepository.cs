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

        public async Task<IEnumerable<Bike>> GetBikes(BikeQuery queryObj)
        {
            var query = context.Bikes
              .Include(b => b.Model)
                .ThenInclude(m => m.Make)
              .Include(b => b.Features)
                .ThenInclude(bf => bf.Feature)
              .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(b => b.Model.MakeId == queryObj.MakeId.Value);

            //if (queryObj.ModelId.HasValue)
            //    query = query.Where(b => b.ModelId == queryObj.ModelId.Value);

            if (queryObj.SortBy == "make")
                query = (queryObj.IsSortAscending) ? query.OrderBy(b => b.Model.Make.Name) : query.OrderByDescending(b => b.Model.Make.Name);

            if (queryObj.SortBy == "model")
                query = (queryObj.IsSortAscending) ? query.OrderBy(b => b.Model.Name) : query.OrderByDescending(b => b.Model.Name);

            if (queryObj.SortBy == "contactName")
                query = (queryObj.IsSortAscending) ? query.OrderBy(b => b.Contact.Name) : query.OrderByDescending(b => b.Contact.Name);

            if (queryObj.SortBy == "id")
                query = (queryObj.IsSortAscending) ? query.OrderBy(b => b.Id) : query.OrderByDescending(b => b.Id);

            return await query.ToListAsync();
        }
    }
}
