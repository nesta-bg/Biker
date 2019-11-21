using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;
using Biker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biker.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly BikerDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(BikerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();

            return mapper.Map<List<Feature>, IEnumerable<KeyValuePairResource>>(features);
        }
    }
}
