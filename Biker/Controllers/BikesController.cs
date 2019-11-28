﻿using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Core;
using Biker.Core.Models;
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
        public async Task<IEnumerable<BikeResource>> GetBikes(FilterResource filterResource)
        {
            var filter = mapper.Map<FilterResource, Filter>(filterResource);
            var bikes = await repository.GetBikes(filter);

            return mapper.Map<IEnumerable<Bike>, IEnumerable<BikeResource>>(bikes);
        }
    }
}
