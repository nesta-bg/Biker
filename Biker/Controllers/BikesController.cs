using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;
using Biker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> CreateBike([FromBody] SaveBikeResource bikeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bike = mapper.Map<SaveBikeResource, Bike>(bikeResource);
            bike.LastUpdate = DateTime.Now;

            context.Bikes.Add(bike);
            await context.SaveChangesAsync();

            bike = await context.Bikes
                .Include(b => b.Features)
                    .ThenInclude(bf => bf.Feature)
                .Include(b => b.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(b => b.Id == bike.Id);

            var result = mapper.Map<Bike, BikeResource>(bike);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBike(int id, [FromBody] SaveBikeResource bikeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bike = await context.Bikes
                .Include(b => b.Features)
                    .ThenInclude(bf => bf.Feature)
                .Include(b => b.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (bike == null)
                return NotFound();

            mapper.Map<SaveBikeResource, Bike>(bikeResource, bike);
            bike.LastUpdate = DateTime.Now;

            await context.SaveChangesAsync();

            var result = mapper.Map<Bike, BikeResource>(bike);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            var bike = await context.Bikes.FindAsync(id);

            if (bike == null)
                return NotFound();

            context.Remove(bike);
            await context.SaveChangesAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBike(int id)
        {
            var bike = await context.Bikes
                .Include(b => b.Features)
                    .ThenInclude(bf => bf.Feature)
                .Include(b => b.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(b => b.Id == id);

            if (bike == null)
                return NotFound();

            var bikeResource = mapper.Map<Bike, BikeResource>(bike);

            return Ok(bikeResource);
        }
    }
}
