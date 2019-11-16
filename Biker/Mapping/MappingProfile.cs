using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;

namespace Biker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
        }
    }
}
