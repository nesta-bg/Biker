using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Core.Models;
using Biker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biker.Controllers
{
    public class MakesController : Controller
    {
        private readonly BikerDbContext context;
        private readonly IMapper mapper;

        public MakesController(BikerDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);

        }
    }
}

