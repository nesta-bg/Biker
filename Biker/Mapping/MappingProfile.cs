using AutoMapper;
using Biker.Controllers.Resources;
using Biker.Models;
using System.Linq;

namespace Biker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();

            // From API Resource to Domain
            CreateMap<BikeResource, Bike>()
              .ForPath(b => b.Contact.Name, opt => opt.MapFrom(br => br.Contact.Name))
              .ForPath(b => b.Contact.Email, opt => opt.MapFrom(br => br.Contact.Email))
              .ForPath(b => b.Contact.Phone, opt => opt.MapFrom(br => br.Contact.Phone))
              .ForMember(b => b.Features, opt => opt.MapFrom(br => br.Features.Select(id => new BikeFeature { FeatureId = id })));
        }
    }
}
