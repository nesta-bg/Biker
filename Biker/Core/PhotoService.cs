using Biker.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Biker.Core
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork unitOfWork;
        public PhotoService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Photo> UploadPhoto(Bike bike, IFormFile file, string uploadsFolderPath)
        {
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //TO DO: create thumbnails

            var photo = new Photo { FileName = fileName };
            bike.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return photo;
        }
    }
}
