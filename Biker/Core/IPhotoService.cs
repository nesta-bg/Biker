using Biker.Core.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Biker.Core
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhoto(Bike bike, IFormFile file, string uploadsFolderPath);
    }
}
