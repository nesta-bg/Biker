using Biker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biker.Core
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int bikeId);
    }
}
