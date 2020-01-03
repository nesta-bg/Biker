using Biker.Core;
using Biker.Core.Models;
using Biker.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<QueryResult<Bike>> GetBikes(BikeQuery queryObj)
        {
            var result = new QueryResult<Bike>();

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

            var columnsMap = new Dictionary<string, Expression<Func<Bike, object>>>()
            {
                ["make"] = b => b.Model.Make.Name,
                ["model"] = b => b.Model.Name,
                ["contactName"] = b => b.Contact.Name
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PieChart>> GetBikesGroupedByMake()
        {
            return await context.Bikes
                .GroupBy(b => b.Model.Make.Name, b => b.Id, (key, val) =>
                    new PieChart { MakeName = key, Items = val.Count() })
                .ToListAsync();
        }
    }
}
