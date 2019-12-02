using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Core;
using Biker.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Biker.Controllers
{
    [Route("/api/bikes/{bikeId}/photos")]
    public class PhotosController : Controller
    {
        private readonly int MAX_BYTES = 1 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

        private readonly IHostingEnvironment host;
        private readonly IBikeRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PhotosController(
            IHostingEnvironment host,
            IBikeRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.host = host;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //IFormCollection - upload multiple files
        [HttpPost]
        public async Task<IActionResult> Upload(int bikeId, IFormFile file)
        {
            var bike = await repository.GetBike(bikeId, includeRelated: false);
            if (bike == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > MAX_BYTES) return BadRequest("Max file size exceeded");
            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName))) return BadRequest("Invalid file type.");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

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

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}
