using Biker.Core;
using Biker.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly BikerDbContext context;
        public PhotoRepository(BikerDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int bikeId)
        {
            return await context.Photos
              .Where(p => p.BikeId == bikeId)
              .ToListAsync();
        }
    }
}
