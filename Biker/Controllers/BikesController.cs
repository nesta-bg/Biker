using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biker.Controllers
{
    [Route("/api/bikes")]
    public class BikesController : Controller
    {
        private readonly IMapper mapper;

        public BikesController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateBike([FromBody] BikeResource bikeResource)
        {
            var bike = mapper.Map<BikeResource, Bike>(bikeResource);
            return Ok(bike);
        }
    }
}
