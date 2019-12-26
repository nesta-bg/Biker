using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Core;
using Biker.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biker.Controllers
{
    [Route("/api/bikes")]
    public class BikesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBikeRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public BikesController(
            IMapper mapper, 
            IBikeRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBike([FromBody] SaveBikeResource bikeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bike = mapper.Map<SaveBikeResource, Bike>(bikeResource);
            bike.LastUpdate = DateTime.Now;

            repository.Add(bike);
            await unitOfWork.CompleteAsync();

            bike = await repository.GetBike(bike.Id);

            var result = mapper.Map<Bike, BikeResource>(bike);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBike(int id, [FromBody] SaveBikeResource bikeResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bike = await repository.GetBike(id);

            if (bike == null)
                return NotFound();

            mapper.Map<SaveBikeResource, Bike>(bikeResource, bike);
            bike.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            bike = await repository.GetBike(bike.Id);
            var result = mapper.Map<Bike, BikeResource>(bike);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBike(int id)
        {
            var bike = await repository.GetBike(id, includeRelated: false);

            if (bike == null)
                return NotFound();

            repository.Remove(bike);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBike(int id)
        {
            var bike = await repository.GetBike(id);

            if (bike == null)
                return NotFound();

            var bikeResource = mapper.Map<Bike, BikeResource>(bike);

            return Ok(bikeResource);
        }

        [HttpGet]
        public async Task<QueryResultResource<BikeResource>> GetBikes(BikeQueryResource bikeQueryResource)
        {
            var bikeQuery = mapper.Map<BikeQueryResource, BikeQuery>(bikeQueryResource);
            var queryResult = await repository.GetBikes(bikeQuery);

            return mapper.Map<QueryResult<Bike>, QueryResultResource<BikeResource>>(queryResult);
        }
    }
}
