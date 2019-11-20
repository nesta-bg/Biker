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
        public async Task<IActionResult> CreateBike([FromBody] BikeResource bikeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var model = await context.Models.FindAsync(bikeResource.ModelId);
            //if (model == null)
            //{
            //    ModelState.AddModelError("ModelId", "Invalid modelId.");
            //    return BadRequest(ModelState);
            //}

            var bike = mapper.Map<BikeResource, Bike>(bikeResource);
            bike.LastUpdate = DateTime.Now;

            context.Bikes.Add(bike);
            await context.SaveChangesAsync();

            var result = mapper.Map<Bike, BikeResource>(bike);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBike(int id, [FromBody] BikeResource bikeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bike = await context.Bikes.Include(b => b.Features).SingleOrDefaultAsync(b => b.Id == id);

            if (bike == null)
                return NotFound();

            mapper.Map<BikeResource, Bike>(bikeResource, bike);
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
    }
}
