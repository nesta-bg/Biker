using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Core;
using Biker.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Biker.Controllers
{
    [Route("/api/bikes/{bikeId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IBikeRepository bikeRepository;
        private readonly IPhotoRepository photoRepository;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;
        private readonly PhotoSettings photoSettings;

        public PhotosController(
            IHostingEnvironment host,
            IBikeRepository bikeRepository,
            IPhotoRepository photoRepository,
            IMapper mapper,
            IOptionsSnapshot<PhotoSettings> options,
            IPhotoService photoService)
        {
            this.host = host;
            this.bikeRepository = bikeRepository;
            this.photoRepository = photoRepository;
            this.mapper = mapper;
            this.photoService = photoService;
            this.photoSettings = options.Value;
        }

        //IFormCollection - upload multiple files
        [HttpPost]
        public async Task<IActionResult> Upload(int bikeId, IFormFile file)
        {
            var bike = await bikeRepository.GetBike(bikeId, includeRelated: false);
            if (bike == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            var photo = await photoService.UploadPhoto(bike, file, uploadsFolderPath);

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int bikeId)
        {
            var photos = await photoRepository.GetPhotos(bikeId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
    }
}
