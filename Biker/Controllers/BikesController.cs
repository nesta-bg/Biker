using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;
using Biker.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Biker.Controllers
{
    [Route("/api/bikes")]
    public class BikesController : Controller
    {
        private readonly BikerDbContext context;
        private readonly IMapper mapper;

        public BikesController(BikerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBike([FromBody] BikeResource bikeResource)
        {
            var bike = mapper.Map<BikeResource, Bike>(bikeResource);
            bike.LastUpdate = DateTime.Now;

            context.Bikes.Add(bike);
            await context.SaveChangesAsync();

            var result = mapper.Map<Bike, BikeResource>(bike);

            return Ok(result);
        }
    }
}
